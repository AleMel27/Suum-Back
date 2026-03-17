using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuumBackend.Data;
using SuumBackend.Models;

namespace SuumBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly SuumContext _context;

        public CategoriasController(SuumContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }
    }
}