using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SuumBackend.Models
{
    public class Talla
    {
        [Key]
        public int id_talla { get; set; }
        public string talla { get; set; }

        [JsonIgnore] // 🔥 rompe el ciclo aquí también
        public ICollection<ProductoTalla>? producto_tallas { get; set; }
    }
}