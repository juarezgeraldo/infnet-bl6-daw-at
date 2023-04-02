using infnet_bl6_daw_at.MVC.Models.Autores;

namespace infnet_bl6_daw_at.MVC.Models.Autores
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public ICollection<LivroAutorViewModel> Autores { get; set; }

    }
}
