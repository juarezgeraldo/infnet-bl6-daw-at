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
            var books = await _infnet_Bl6_Daw_AtDbContext
                .Livros
                .Include(b => b.Autores)
                .ToListAsync();

            return books;
        }

        public async Task<Livro> GetAsync(int id)
        {
            var book = _infnet_Bl6_Daw_AtDbContext
                .Livros
                .Where(a => a.Id == id)
                .Include(b => b.Autores)
                .FirstOrDefault();

            return book;
        }

        public async Task<Livro> CreateAsync(Livro book)
        {
            _infnet_Bl6_Daw_AtDbContext.Livros.Add(book);
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Livro> UpdateAsync(Livro book)
        {
            _infnet_Bl6_Daw_AtDbContext.Entry(book).State = EntityState.Modified;
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Livro> DeleteAsync(Livro book)
        {
            _infnet_Bl6_Daw_AtDbContext.Entry(book).State = EntityState.Modified;
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Livro> AddAutor(Livro book)
        {
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Livro> RemoveAutor(Livro book)
        {
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return book;
        }
    }
}
