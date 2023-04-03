using infnet_bl6_daw_at.MVC.Models.Autores;

namespace infnet_bl6_daw_at.MVC.Models.Livros
{
    public class InsereLivroViewModel
    {
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public ICollection<InsereLivroAutorViewModel> Autores { get; set; } = new List<InsereLivroAutorViewModel>();
    }
}
