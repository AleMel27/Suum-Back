using Microsoft.AspNetCore.Mvc;
using SuumBackend.Data;
using System.Linq;

namespace SuumBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly SuumContext _context;

        public TestController(SuumContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TestConexion()
        {
            var productos = _context.Productos.ToList();
            return Ok(productos);
        }
    }
}