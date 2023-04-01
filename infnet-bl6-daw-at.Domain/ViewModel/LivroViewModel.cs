using infnet_bl6_daw_at.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infnet_bl6_daw_at.Domain.ViewModel
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public virtual ICollection<Autor> Autores { get; set; }

        public LivroViewModel(Livro livro)
        {
            this.Id = livro.Id;
            this.Titulo = livro.Titulo;
            this.Isbn = livro.Isbn;
            this.Ano = livro.Ano;
            this.Autores = livro.Autores;
        }

        public static List<LivroViewModel> GetAll(List<Livro> livros)
        {
            var list = new List<LivroViewModel>();
            foreach (var item in livros)
            {
                list.Add(new LivroViewModel(item));
            }
            return list;
        }
        public static LivroViewModel Get(Livro livro)
        {
            return new LivroViewModel(livro);
        }

        public static LivroViewModel PutLivro(int Id, Livro livro)
        {
            return new LivroViewModel(livro);
        }

    }
}
