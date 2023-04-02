using AT.API.DTOs.Users;
using AT.Models;
using AutoMapper;
using infnet_bl6_daw_at.Domain.Entities;

namespace AT.API.Mapper
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
