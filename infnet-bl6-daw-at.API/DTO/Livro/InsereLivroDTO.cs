using infnet_bl6_daw_at.API.DTO.Autor;

namespace infnet_bl6_daw_at.API.DTO.Livro
{
    public class InsereLivroDTO
    {
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public ICollection<InsereLivroAutorDTO> Autores { get; set; }
    }
}
