using infnet_bl6_daw_at.Domain.Entities;

namespace infnet_bl6_daw_at.Domain.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<Livro>> GetAll();
        Task<Livro> Get(int id);
        Task<Livro> Add(Livro livro);
        Task<Livro> Save(Livro livro);
        Task<Livro> Remove(int id);
        Task<Livro> AddAutor(int livroId, int autorId);
        Task<Livro> RemoveAutor(int livroId, int autorId);
    }
}
