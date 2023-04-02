using infnet_bl6_daw_at.Domain.Entities;

namespace infnet_bl6_daw_at.Domain.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<Autor>> GetAll();
        Task<Autor> Get(int id);
        Task<Autor> Add(Autor autor);
        Task<Autor> Save(Autor autor);
        Task<Autor> Remove(int id);
    }
}
