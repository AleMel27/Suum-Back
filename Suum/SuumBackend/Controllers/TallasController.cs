using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuumBackend.Data;

namespace SuumBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallasController : ControllerBase
    {
        private readonly SuumContext _context;

        public TallasController(SuumContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTallas()
        {
            var tallas = await _context.Tallas.ToListAsync();

            return Ok(tallas);
        }
    }
}