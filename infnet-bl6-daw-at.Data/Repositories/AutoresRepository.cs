using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Service;
using Microsoft.EntityFrameworkCore;

namespace infnet_bl6_daw_at.Data.Repositories
{
    public class AutoresRepository : IAutoresRepository
    {
        private readonly infnet_bl6_daw_atDbContext _infnet_Bl6_Daw_AtDbContext;

        public AutoresRepository(infnet_bl6_daw_atDbContext infnet_Bl6_Daw_AtDbContext)
        {
            _infnet_Bl6_Daw_AtDbContext = infnet_Bl6_Daw_AtDbContext;
        }

        public async Task<IEnumerable<Autor>> GetAsync()
        {
            var autores = await _infnet_Bl6_Daw_AtDbContext
                .Autores
                .Include(b => b.Livros)
                .ToListAsync();

            return autores;
        }

        public async Task<Autor> GetAsync(int id)
        {
            var autores = _infnet_Bl6_Daw_AtDbContext
                .Autores
                .Where(a => a.Id == id)
                .Include(b => b.Livros)
                .FirstOrDefault();

            return autores;
        }

        public async Task<Autor> CreateAsync(Autor autor)
        {
            _infnet_Bl6_Daw_AtDbContext.Autores.Add(autor);
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();

            return autor;
        }

        public async Task<Autor> UpdateAsync(Autor autor)
        {
            _infnet_Bl6_Daw_AtDbContext.Entry(autor).State = EntityState.Modified;
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();
            return autor;
        }

        public async Task<Autor> DeleteAsync(Autor autor)
        {
            _infnet_Bl6_Daw_AtDbContext.Autores.Remove(autor);
            await _infnet_Bl6_Daw_AtDbContext.SaveChangesAsync();

            return autor;
        }
    }
}
