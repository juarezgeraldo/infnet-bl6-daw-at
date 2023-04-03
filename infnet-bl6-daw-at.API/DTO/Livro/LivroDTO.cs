using infnet_bl6_daw_at.API.DTO.Autor;

namespace infnet_bl6_daw_at.API.DTO.Livro
{
    public class LivroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public ICollection<LivroAutorDTO> Autores { get; set; }

    }
}
