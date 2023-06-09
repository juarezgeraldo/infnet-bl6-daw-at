﻿using AutoMapper;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.MVC.Models.Autores;
using infnet_bl6_daw_at.MVC.Models.Livros;

namespace infnet_bl6_daw_at.MVC.Mapper
{
    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<Livro, LivroViewModel>();
            CreateMap<InsereLivroAutorViewModel, Livro>();
            CreateMap<InsereLivroViewModel, Livro>();
            CreateMap<Livro, InsereLivroViewModel>();
            CreateMap<AtualizaLivroViewModel, Livro>();
            CreateMap<Livro, AtualizaLivroViewModel>();
            CreateMap<Livro, AutorLivroViewModel>();
        }
    }
}
