using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuumBackend.Models
{
    [Table("usuarios")] // Nombre exacto en MySQL
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public int id_tipo { get; set; }

        // El campo fecha_registro no lo pusiste, 
        // pero está en tu SQL. Podrías agregarlo si lo necesitas:
        // public DateTime fecha_registro { get; set; }
    }
}