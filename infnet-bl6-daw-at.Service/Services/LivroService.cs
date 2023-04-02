using AT.Data.Repositories;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace infnet_bl6_daw_at.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly IAutorService _autorService;
        private readonly ILivrosRepository _livrosRepository;

        public LivroService(IAutorService autorService, ILivrosRepository livrosRepository)
        {
            _autorService = autorService;
            _livrosRepository = livrosRepository;

        }

        public async Task<IEnumerable<Livro>> GetAll()
        {
            return await _livrosRepository.GetAsync();
        }

        public async Task<Livro> Get(int id)
        {
            return await _livrosRepository.GetAsync(id);
        }
        public async Task<Livro> Add(Livro livro)
        {
            var autores = await GetAutoresPeloId(livro);
            livro.Autores = autores;
            return await _livrosRepository.CreateAsync(livro);
        }

        private async Task<List<Autor>> GetAutoresPeloId(Livro livro)
        {
            var autores = new List<Autor>();

            foreach (var novoAutor in livro.Autores)
            {
                var autor = await _autorService.Get(novoAutor.Id);
                if (autor != null)
                {
                    autores.Add(autor);
                }
            }
            return autores;
        }

        public async Task<Livro> Save(Livro livro)
        {
            return await _livrosRepository.UpdateAsync(livro);
        }
        async Task<Livro> ILivroService.Remove(int id)
        {
            var livro = await Get(id);
            return await _livrosRepository.DeleteAsync(livro);
        }

        public async Task<Livro> AddAutor(int livroId, int autorId)
        {
            var livro = await Get(livroId);
            var autor = await _autorService.Get(autorId);

            livro.Autores.Add(autor);

            return await _livrosRepository.AddAutor(livro);
        }

        public async Task<Livro> RemoveAutor(int livroId, int autorId)
        {
            var livro = await Get(livroId);
            var autor = await _autorService.Get(autorId);

            livro.Autores.Remove(autor);

            return await _livrosRepository.RemoveAutor(livro);
        }
    }
}
