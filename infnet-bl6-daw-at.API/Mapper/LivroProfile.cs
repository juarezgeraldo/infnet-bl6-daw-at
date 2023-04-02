using infnet_bl6_daw_at.Domain.Entities;
using AutoMapper;
using DTO.Autor;
using DTO.Livro;

namespace infnet_bl6_daw_at.API.Mapper
{
    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<Livro, LivroDTO>();
            CreateMap<InsereLivroDTO, Livro>();
            CreateMap<AtualizaLivroDTO, Livro>();
            CreateMap<Livro, AutorLivroDTO>();
        }
    }
}
