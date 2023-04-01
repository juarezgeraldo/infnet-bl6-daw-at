

using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.Domain.ViewModel;
using System.Data.Entity;

namespace infnet_bl6_daw_at.Service.Services
{
    public class LivroService : ILivroService
    {
        public readonly infnet_bl6_daw_atDbContext _dbContext;
        public LivroService(infnet_bl6_daw_atDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<LivroViewModel> GetAll()
        {
            var livros = _dbContext.Livros
                .Include(a => a.Autores)
                .ToList();

            return LivroViewModel.GetAll(livros);
        }

        public Livro Add(Livro livro)
        {
            _dbContext.Add(livro);
            _dbContext.SaveChanges();
            return livro;
        }

        public Livro Save(Livro livro)
        {
            _dbContext.Update(livro);
            _dbContext.SaveChanges();
            return livro;
        }
        public Livro Remove(Livro livro)
        {
            _dbContext.Remove(livro);
            _dbContext.SaveChanges();
            return livro;
        }

    }
}
