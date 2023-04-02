using infnet_bl6_daw_at.MVC.Models.Autores;

namespace infnet_bl6_daw_at.MVC.Models.Autores
{
    public class InsereLivroViewModel
    {
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public ICollection<InsereAutorViewModel> Autores { get; set; }
    }
}
