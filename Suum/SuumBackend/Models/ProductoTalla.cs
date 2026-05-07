namespace SuumBackend.Models
{
    public class ProductoTalla
    {
        public int id_producto { get; set; }
        public int id_talla { get; set; }
        public int stock { get; set; }
        public Producto? producto { get; set; }
        public Talla? talla { get; set; }
    }
}