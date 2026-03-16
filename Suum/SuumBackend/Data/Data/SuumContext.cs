using Microsoft.EntityFrameworkCore;
using SuumBackend.Models;

namespace SuumBackend.Data
{
    public class SuumContext : DbContext
    {
        public SuumContext(DbContextOptions<SuumContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}