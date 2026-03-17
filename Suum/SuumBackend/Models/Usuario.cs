using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuumBackend.Models
{
    [Table("usuarios")] // nombre exacto de la tabla en MySQL
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

        // opcional si quieres manejar fecha de registro
        public DateTime? fecha_registro { get; set; }
    }
}