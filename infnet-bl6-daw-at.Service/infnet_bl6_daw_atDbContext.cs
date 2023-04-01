using infnet_bl6_daw_at.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace infnet_bl6_daw_at.Service
{
    public class infnet_bl6_daw_atDbContext : IdentityDbContext<IdentityUser>
    {
        public infnet_bl6_daw_atDbContext(DbContextOptions<infnet_bl6_daw_atDbContext> options) : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
                        modelBuilder
                            .Entity<Autor>()
                            .HasMany(a => a.Livros)
                            .WithMany(a => a.Autores)
                            .UsingEntity(j => j.ToTable("LivrosAutores"));

                        modelBuilder.Entity<Livro>()
                            .HasMany(a => a.Autores)
                            .WithMany(a => a.Livros)
                            .UsingEntity(j => j.HasData(new { LivrosId = 1, AutoresId = 1 }));


*/

            modelBuilder.Entity<Autor>()
                .ToTable(name: "Autor")
                .HasMany<Livro>(b => b.Livros)
                .WithMany(a => a.Autores);
            //.UsingEntity(j => j.ToTable("LivrosAutores"));

            modelBuilder.Entity<Livro>()
                .ToTable(name: "Livro")
                .HasMany<Autor>(b => b.Autores)
                .WithMany(a => a.Livros);
                //.UsingEntity(j => j.ToTable("LivrosAutores"));

        }

    }
}
