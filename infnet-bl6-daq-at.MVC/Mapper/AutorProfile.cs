using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.MVC.Models.Autores;
using AutoMapper;


namespace infnet_bl6_daw_at.MVC.Mapper
{
    public class AutorProfile : Profile
{
        public AutorProfile()
        {
            CreateMap<Autor, AutorViewModel>()
                .ForMember(m => m.NomeCompleto, d => d.MapFrom(mf => $"{mf.Nome} {mf.Sobrenome}"));
            CreateMap<LivroAutor, AutorLivroViewModel>();
            CreateMap<InsereAutorViewModel, Autor>();
            CreateMap<AutorLivroViewModel, LivroAutor>();
            CreateMap<Autor, InsereAutorViewModel>();
            CreateMap<AtualizaAutorViewModel, Autor>();
            CreateMap<Autor, AtualizaAutorViewModel>();
            CreateMap<InsereLivroAutorViewModel, Autor>();
            CreateMap<Autor, LivroAutorViewModel>();
        }
    }
}
