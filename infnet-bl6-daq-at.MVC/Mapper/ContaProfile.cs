using AutoMapper;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.MVC.Models.Conta;

namespace infnet_bl6_daw_at.MVC.Mapper
{
    public class ContaProfile : Profile
    {
        public ContaProfile()
        {
            CreateMap<UsuarioLoginViewModel, UsuarioLogin>();
            CreateMap<InsereUsuarioViewModel, Usuario>();
        }
    }
}
