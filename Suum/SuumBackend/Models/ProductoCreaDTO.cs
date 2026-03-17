using Microsoft.AspNetCore.Http;

namespace SuumBackend.Models
{
    public class ProductoCrearDTO
    {
        public string nombre { get; set; } = "";

        public decimal precio { get; set; }

        public int stock { get; set; }

        public int id_categoria { get; set; }

        public IFormFile imagen { get; set; }
    }
}