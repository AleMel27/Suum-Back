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
            return await _context.Productos
                .Include(p => p.producto_tallas)
                .ThenInclude(pt => pt.talla)
                .ToListAsync();
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
            var producto = new Producto
            {
                nombre = datos.nombre,
                precio = datos.precio ?? 0,
                id_categoria = datos.id_categoria,
                estado = 1
            };

            // guardar imagen
            if (datos.imagen != null)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(datos.imagen.FileName);
                var ruta = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await datos.imagen.CopyToAsync(stream);
                }

                producto.imagen = "/imagenes/" + nombreArchivo;
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            foreach (var t in datos.tallas)
            {
                var pt = new ProductoTalla
                {
                    id_producto = producto.id_producto,
                    id_talla = t.id_talla,
                    stock = t.stock
                };

                _context.ProductoTallas.Add(pt);
            }

            await _context.SaveChangesAsync();

            return Ok(producto);
        }
        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromForm] ProductoCrearDTO datos)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            if (datos.nombre != null)
                producto.nombre = datos.nombre;

            if (datos.precio.HasValue)
                producto.precio = datos.precio.Value;

            if (datos.id_categoria.HasValue)
                producto.id_categoria = datos.id_categoria.Value;

            if (datos.imagen != null)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(datos.imagen.FileName);
                var ruta = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await datos.imagen.CopyToAsync(stream);
                }

                producto.imagen = "/imagenes/" + nombreArchivo;
            }

            await _context.SaveChangesAsync();

            return Ok(producto);
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