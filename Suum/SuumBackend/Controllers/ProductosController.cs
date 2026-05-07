using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.categoria)
                .Include(p => p.producto_tallas)
                    .ThenInclude(pt => pt.talla)
                .Select(p => new
                {
                    p.id_producto,
                    p.nombre,
                    p.precio,
                    p.id_categoria,
                    p.imagen,
                    p.estado,

                    categoria = new
                    {
                        p.categoria.id_categoria,
                        p.categoria.nombre
                    },

                    producto_tallas = p.producto_tallas.Select(pt => new
                    {
                        pt.id_producto,
                        pt.id_talla,
                        pt.stock,

                        talla = new
                        {
                            pt.talla.id_talla,
                            pt.talla.talla
                        }
                    })
                })
                .ToListAsync();

            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.categoria)
                .Include(p => p.producto_tallas)
                    .ThenInclude(pt => pt.talla)
                .Where(p => p.id_producto == id)
                .Select(p => new
                {
                    p.id_producto,
                    p.nombre,
                    p.precio,
                    p.id_categoria,
                    p.imagen,
                    p.estado,

                    categoria = new
                    {
                        p.categoria.id_categoria,
                        p.categoria.nombre
                    },

                    producto_tallas = p.producto_tallas.Select(pt => new
                    {
                        pt.id_producto,
                        pt.id_talla,
                        pt.stock,

                        talla = new
                        {
                            pt.talla.id_talla,
                            pt.talla.talla
                        }
                    })
                })
                .FirstOrDefaultAsync();

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromForm] ProductoCreateDTO dto)
        {
            var producto = new Producto
            {
                nombre = dto.nombre,
                precio = dto.precio,
                id_categoria = dto.id_categoria,
                estado = 1
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            var tallas = JsonConvert.DeserializeObject<List<ProductoTalla>>(dto.tallas);

            foreach (var t in tallas)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarProducto(int id, [FromForm] ProductoUpdateDTO dto)
        {
            var producto = await _context.Productos
                .Include(p => p.producto_tallas)
                .FirstOrDefaultAsync(p => p.id_producto == id);

            if (producto == null)
                return NotFound();

            producto.nombre = dto.nombre;
            producto.precio = dto.precio;
            producto.id_categoria = dto.id_categoria;

            _context.ProductoTallas.RemoveRange(producto.producto_tallas);

            var tallas = JsonConvert.DeserializeObject<List<ProductoTalla>>(dto.tallas);

            foreach (var t in tallas)
            {
                var pt = new ProductoTalla
                {
                    id_producto = id,
                    id_talla = t.id_talla,
                    stock = t.stock
                };

                _context.ProductoTallas.Add(pt);
            }

            await _context.SaveChangesAsync();

            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}