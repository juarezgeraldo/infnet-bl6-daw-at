namespace infnet_bl6_daw_at.MVC.Models.Autores
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<AutorLivroViewModel> LivroAutores { get; set; }
    }
}
