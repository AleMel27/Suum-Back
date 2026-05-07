namespace SuumBackend.Models
{
    public class ProductoCreateDTO
    {
        public string nombre { get; set; } = "";

        public decimal precio { get; set; }

        public int id_categoria { get; set; }

        public string tallas { get; set; } = "";
    }
}