using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuumBackend.Data;
using SuumBackend.Models;

namespace SuumBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly SuumContext _context;

        public ProductosController(SuumContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromForm] ProductoCrearDTO datos)
        {
            if (datos.imagen == null)
                return BadRequest("La imagen es obligatoria");

            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(datos.imagen.FileName);

            var ruta = Path.Combine(carpeta, nombreArchivo);

            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                await datos.imagen.CopyToAsync(stream);
            }

            var producto = new Producto
            {
                nombre = datos.nombre,
                precio = datos.precio,
                stock = datos.stock,
                id_categoria = datos.id_categoria,
                imagen = "/imagenes/" + nombreArchivo,
                estado = 1
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return Ok(producto);
        }
        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.id_producto)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}