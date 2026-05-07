using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuumBackend.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public string nombre { get; set; } = "";
        public string apellido { get; set; } = "";
        public string telefono { get; set; } = "";
        public string correo { get; set; } = "";
        public string password { get; set; } = "";
        public string direccion { get; set; } = "";
        public int id_tipo { get; set; }
        public int estado { get; set; }
    }
}