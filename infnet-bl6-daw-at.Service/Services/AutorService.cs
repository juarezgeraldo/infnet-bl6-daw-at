using AT.Data.Repositories;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace infnet_bl6_daw_at.Service.Services
{
    public class AutorService : IAutorService
    {
        public readonly IAutoresRepository _autoresRepository;
        public AutorService(IAutoresRepository autoresRepository)
        {
            _autoresRepository = autoresRepository;
        }

        public async Task<IEnumerable<Autor>> GetAll()
        {
            return await _autoresRepository.GetAsync();
        }

        public async Task<Autor> Get(int id)
        {
            return await _autoresRepository.GetAsync(id);
        }

        public async Task<Autor> Add(Autor autor)
        {
            return await _autoresRepository.CreateAsync(autor);
        }

        async Task<Autor> IAutorService.Save(Autor autor)
        {
            return await _autoresRepository.UpdateAsync(autor);
        }
        async Task<Autor> IAutorService.Remove(int id)
        {
            var autor = await Get(id);

            return await _autoresRepository.DeleteAsync(autor);
        }

    }
}
