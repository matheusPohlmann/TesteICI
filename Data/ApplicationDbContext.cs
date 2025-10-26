using Microsoft.EntityFrameworkCore;
using ProjetoTesteICI.Models;

namespace ProjetoTesteICI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<NoticiaTag> NoticiaTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Noticia>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Noticias)
                .HasForeignKey(n => n.UsuarioId);

            modelBuilder.Entity<NoticiaTag>()
                .HasOne(nt => nt.Noticia)
                .WithMany(n => n.NoticiaTags)
                .HasForeignKey(nt => nt.NoticiaId);

            modelBuilder.Entity<NoticiaTag>()
                .HasOne(nt => nt.Tag)
                .WithMany() 
                .HasForeignKey(nt => nt.TagId);

            modelBuilder.Entity<NoticiaTag>()
                .HasIndex(nt => new { nt.NoticiaId, nt.TagId })
                .IsUnique();
        }
    }
}
