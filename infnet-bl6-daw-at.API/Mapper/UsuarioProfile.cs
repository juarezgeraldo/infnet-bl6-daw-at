using AutoMapper;
using infnet_bl6_daw_at.API.DTO;
using infnet_bl6_daw_at.Domain.Entities;

namespace infnet_bl6_daw_at.API.Mapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<InsereUsuarioDto, Usuario>();
            CreateMap<UsuarioLoginDto, UsuarioLoginDto>();
            CreateMap<UsuarioLoginDto, UsuarioLogin>();
            CreateMap<Token, TokenDto>()
                .ForMember(m => m.Token, d => d.MapFrom(o => o.BearerToken));
        }
    }
}
