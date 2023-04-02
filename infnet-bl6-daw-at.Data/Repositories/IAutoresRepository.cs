using infnet_bl6_daw_at.Domain.Entities;

namespace AT.Data.Repositories
{
    public interface IAutoresRepository
    {
        Task<IEnumerable<Autor>> GetAsync();
        Task<Autor> GetAsync(int id);
        Task<Autor> CreateAsync(Autor autor);
        Task<Autor> UpdateAsync(Autor autor);
        Task<Autor> DeleteAsync(Autor autor);
    }
}
