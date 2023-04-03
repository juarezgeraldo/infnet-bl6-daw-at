using infnet_bl6_daw_at.Domain.Entities;
using AutoMapper;
using infnet_bl6_daw_at.API.DTO.Autor;

namespace infnet_bl6_daw_at.API.Mapper
{
    public class AutorProfile : Profile
    {
        public AutorProfile()
        {
            CreateMap<Autor, AutorDTO>();
            CreateMap<InsereAutorDTO, Autor>();
            CreateMap<AtualizaAutorDTO, Autor>();
            CreateMap<InsereLivroAutorDTO, Autor>();
            CreateMap<Autor, LivroAutorDTO>();
        }
    }
}
