using DTO.Livro;

namespace DTO.Autor
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<AutorLivroDTO> LivroAutores { get; set; }
    }
}
