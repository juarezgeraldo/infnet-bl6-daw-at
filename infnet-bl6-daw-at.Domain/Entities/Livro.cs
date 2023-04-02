﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace infnet_bl6_daw_at.Domain.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public ICollection<Autor> Autores { get; set; }
        public List<LivroAutor> LivroAutores { get; set; }


    }
}
