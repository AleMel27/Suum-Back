using Microsoft.AspNetCore.Mvc;
using SuumBackend.Data;
using System.Linq;

namespace SuumBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SuumContext _context;

        public AuthController(SuumContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.correo == request.correo
                                  && u.password == request.password);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos" });
            }

            return Ok(new
            {
                message = "Login correcto",
                rol = usuario.id_tipo == 1 ? "admin" : "cliente"
            });
        }
    }

    public class LoginRequest
    {
        public string correo { get; set; } = "";
        public string password { get; set; } = "";
    }
}