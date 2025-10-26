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

            // 🔹 Relacionamento Noticia -> Usuario (1:N)
            modelBuilder.Entity<Noticia>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Noticias)
                .HasForeignKey(n => n.UsuarioId);

            // 🔹 Relacionamento NoticiaTag -> Noticia (N:1)
            modelBuilder.Entity<NoticiaTag>()
                .HasOne(nt => nt.Noticia)
                .WithMany(n => n.NoticiaTags)
                .HasForeignKey(nt => nt.NoticiaId);

            // 🔹 Relacionamento NoticiaTag -> Tag (N:1)
            // ⚠️ Aqui removemos a navegação t.NoticiaTags
            modelBuilder.Entity<NoticiaTag>()
                .HasOne(nt => nt.Tag)
                .WithMany() // sem propriedade de navegação em Tag
                .HasForeignKey(nt => nt.TagId);

            // 🔹 Garante que NoticiaId + TagId não se repitam
            modelBuilder.Entity<NoticiaTag>()
                .HasIndex(nt => new { nt.NoticiaId, nt.TagId })
                .IsUnique();
        }
    }
}
