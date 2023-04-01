using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infnet_bl6_daw_at.Domain.Interfaces
{
    public interface ILivroService
    {
        ICollection<LivroViewModel> GetAll();
        Livro Add(Livro autor);
        Livro Save(Livro autor);
        Livro Remove(Livro autor);
    }
}
