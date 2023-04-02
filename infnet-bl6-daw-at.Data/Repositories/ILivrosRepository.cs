using infnet_bl6_daw_at.Domain.Entities;

namespace AT.Data.Repositories
{
    public interface ILivrosRepository
    {
        Task<IEnumerable<Livro>> GetAsync();
        Task<Livro> GetAsync(int id);
        Task<Livro> CreateAsync(Livro livro);
        Task<Livro> UpdateAsync(Livro livro);
        Task<Livro> DeleteAsync(Livro livro);
        Task<Livro> AddAutor(Livro livro);
        Task<Livro> RemoveAutor(Livro livro);
    }
}
