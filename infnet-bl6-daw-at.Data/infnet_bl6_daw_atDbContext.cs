using infnet_bl6_daw_at.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace infnet_bl6_daw_at.Service
{
    public class infnet_bl6_daw_atDbContext : IdentityDbContext<ApplicationUser>
    {
        public infnet_bl6_daw_atDbContext(DbContextOptions<infnet_bl6_daw_atDbContext> options) : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Livro>()
           .HasMany(p => p.Autores)
           .WithMany(p => p.Livros)
           .UsingEntity<LivroAutor>(
               j => j
                   .HasOne(pt => pt.Autor)
                   .WithMany(t => t.LivroAutores)
                   .HasForeignKey(pt => pt.AutorId),
               j => j
                   .HasOne(pt => pt.Livro)
                   .WithMany(p => p.LivroAutores)
                   .HasForeignKey(pt => pt.LivroId),
               j =>
               {
                   j.HasKey(t => new { t.LivroId, t.AutorId });
               });

        }

    }
}
