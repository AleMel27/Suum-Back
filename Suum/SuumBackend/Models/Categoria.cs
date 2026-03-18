using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuumBackend.Models
{
    [Table("categoria")]
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set; }

        public string nombre { get; set; } = "";

    }
}