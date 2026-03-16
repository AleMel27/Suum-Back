using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuumBackend.Models
{
    [Table("productos")] // Nombre exacto en MySQL
    public class Producto
    {
        [Key] // Define la llave primaria
        public int id_producto { get; set; }

        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public int? id_categoria { get; set; }
        public int? id_talla { get; set; }
        public string? imagen { get; set; }
        public int? estado { get; set; }
    }
}