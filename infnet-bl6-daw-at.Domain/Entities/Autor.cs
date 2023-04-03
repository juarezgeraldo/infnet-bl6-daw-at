namespace infnet_bl6_daw_at.Domain.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<Livro> Livros { get; set; }
        public List<LivroAutor> LivroAutores { get; set; }


    }
}
