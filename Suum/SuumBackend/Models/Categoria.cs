using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuumBackend.Models
{
    [Table("categoria")] // Indica el nombre exacto de la tabla en MySQL
    public class Categoria
    {
        [Key] // Define que esta es tu Llave Primaria
        public int id_categoria { get; set; }
        
        public string nombre { get; set; }
        
        public string? descripcion { get; set; }
    }
}