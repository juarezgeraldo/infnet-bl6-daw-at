using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.Domain.ViewModel;
using System.Data.Entity;

namespace infnet_bl6_daw_at.Service.Services
{
    public class AutorService : IAutorService
    {
        public readonly infnet_bl6_daw_atDbContext _dbContext;
        public AutorService(infnet_bl6_daw_atDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<AutorViewModel> GetAll()
        {
            var autores = _dbContext.Autores
                .Include(l => l.Livros)
                .ToList();

            return AutorViewModel.GetAll(autores);
        }

        public Autor Add(Autor autor)
        {
            _dbContext.Add(autor);
            _dbContext.SaveChanges();
            return autor;
        }

        public Autor Save(Autor autor)
        {
            _dbContext.Update(autor);
            _dbContext.SaveChanges();
            return autor;
        }
        public Autor Remove(Autor autor)
        {
            _dbContext.Remove(autor);
            _dbContext.SaveChanges();
            return autor;
        }
    }
}
