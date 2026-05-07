using Microsoft.EntityFrameworkCore;
using SuumBackend.Models;

namespace SuumBackend.Data
{
    public class SuumContext : DbContext
    {
        public SuumContext(DbContextOptions<SuumContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Talla> Tallas { get; set; }

        public DbSet<ProductoTalla> ProductoTallas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .ToTable("productos")
                .HasKey(p => p.id_producto);

            modelBuilder.Entity<Categoria>()
                .ToTable("categoria")
                .HasKey(c => c.id_categoria);

            modelBuilder.Entity<Talla>()
                .ToTable("tallas")
                .HasKey(t => t.id_talla);

            modelBuilder.Entity<Usuario>()
                .ToTable("usuarios")
                .HasKey(u => u.id_usuario);

            modelBuilder.Entity<ProductoTalla>()
                .ToTable("producto_tallas");

            modelBuilder.Entity<ProductoTalla>()
                .HasKey(pt => new { pt.id_producto, pt.id_talla });

            modelBuilder.Entity<ProductoTalla>()
                .HasOne(pt => pt.producto)
                .WithMany(p => p.producto_tallas)
                .HasForeignKey(pt => pt.id_producto);

            modelBuilder.Entity<ProductoTalla>()
                .HasOne(pt => pt.talla)
                .WithMany(t => t.producto_tallas)
                .HasForeignKey(pt => pt.id_talla);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.categoria)
                .WithMany(c => c.productos)
                .HasForeignKey(p => p.id_categoria);
        }
    }
}