using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infnet_bl6_daw_at.Domain.Interfaces
{
    public interface IAutorService
    {
        ICollection<AutorViewModel> GetAll();
        Autor Add(Autor livro);
        Autor Save(Autor livro);
        Autor Remove(Autor livro);
    }
}
