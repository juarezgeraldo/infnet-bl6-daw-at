namespace infnet_bl6_daw_at.Domain.Entities
{
    public class LivroAutor
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }

    }
}
