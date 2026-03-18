using Microsoft.AspNetCore.Http;

namespace SuumBackend.Models
{
    public class ProductoCrearDTO
    {
        public string nombre { get; set; }
        public decimal? precio { get; set; }
        public int? id_categoria { get; set; }
        public IFormFile? imagen { get; set; }

        public List<TallaStockDTO> tallas { get; set; }
    }

    public class TallaStockDTO
    {
        public int id_talla { get; set; }
        public int stock { get; set; }
    }
}