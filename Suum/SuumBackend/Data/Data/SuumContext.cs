using Microsoft.EntityFrameworkCore;
using SuumBackend.Models;

namespace SuumBackend.Data
{
    public class SuumContext : DbContext
    {
        public SuumContext(DbContextOptions<SuumContext> options) : base(options)
        {
        }

        // 🔥 TABLAS
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Talla> Tallas { get; set; }
        public DbSet<ProductoTalla> ProductoTallas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔥 NOMBRES EXACTOS DE TABLAS (por si MySQL está sensible)
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Categoria>().ToTable("categoria");
            modelBuilder.Entity<Talla>().ToTable("tallas");
            modelBuilder.Entity<ProductoTalla>().ToTable("producto_tallas");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");

            // 🔥 CLAVE COMPUESTA (tabla intermedia)
            modelBuilder.Entity<ProductoTalla>()
                .HasKey(pt => new { pt.id_producto, pt.id_talla });

            // 🔥 RELACIÓN PRODUCTO → TALLAS
            modelBuilder.Entity<ProductoTalla>()
                .HasOne(pt => pt.producto)
                .WithMany(p => p.producto_tallas)
                .HasForeignKey(pt => pt.id_producto);

            // 🔥 RELACIÓN TALLA → PRODUCTOS
            modelBuilder.Entity<ProductoTalla>()
                .HasOne(pt => pt.talla)
                .WithMany(t => t.producto_tallas)
                .HasForeignKey(pt => pt.id_talla);

            // 🔥 RELACIÓN PRODUCTO → CATEGORIA (EL FIX DEL ERROR 💀)
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.categoria)
                .WithMany()
                .HasForeignKey(p => p.id_categoria);

            // ⚠️ OPCIONAL PERO RECOMENDADO (evita nombres raros)
            modelBuilder.Entity<Producto>()
                .Property(p => p.id_categoria)
                .HasColumnName("id_categoria");
        }
    }
}