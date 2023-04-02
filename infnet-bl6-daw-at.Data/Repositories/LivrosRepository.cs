using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Service;
using Microsoft.EntityFrameworkCore;

namespace AT.Data.Repositories
{
    public class LivrosRepository : ILivrosRepository
    {
        private readonly infnet_bl6_daw_atDbContext _infnet_Bl6_Daw_AtDbContext;

        public LivrosRepository(infnet_bl6_daw_atDbContext infnet_Bl6_Daw_AtDbContext)
        {
            _infnet_Bl6_Daw_AtDbContext = infnet_Bl6_Daw_AtDbContext;
        }

        public async Task<IEnumerable<Livro>> GetAsync()
        {
            var Livros = await _infnet_Bl6_Daw_AtDbContext
                .Livros
                .Include(b => b.Autores)
                .ToListAsync();

            return Livros;
        }

        public async Task<Livro> GetAsync(int id)
        {
            var Livro = _infnet_Bl6_Daw_AtDbContext
                .Livros
                .Where(a => a.Id == id)
                .Include(b => b.Autores)
                .FirstOrDefault();

            return Livro;
        }

        public async Task<Livro> CreateAsync(Livro Livro)
        {
            _infnet_Bl6_Daw_AtDbContext.Livros.Add(Livro);
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return Livro;
        }

        public async Task<Livro> UpdateAsync(Livro Livro)
        {
            _infnet_Bl6_Daw_AtDbContext.Entry(Livro).State = EntityState.Modified;
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return Livro;
        }

        public async Task<Livro> DeleteAsync(Livro Livro)
        {
            _infnet_Bl6_Daw_AtDbContext.Entry(Livro).State = EntityState.Modified;
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return Livro;
        }

        public async Task<Livro> AddAutor(Livro Livro)
        {
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return Livro;
        }

        public async Task<Livro> RemoveAutor(Livro Livro)
        {
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return Livro;
        }
    }
}
