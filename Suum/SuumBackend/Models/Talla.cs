using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuumBackend.Models
{
    [Table("tallas")]
    public class Talla
    {
        [Key]
        public int id_talla { get; set; }
        public string talla { get; set; } = "";
        public ICollection<ProductoTalla>? producto_tallas { get; set; }
    }
}