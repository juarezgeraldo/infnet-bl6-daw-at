using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Autor;

namespace DTO.Livro
{
    public class InsereLivroDTO
    {
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int Ano { get; set; }
        public ICollection<InsereLivroAutorDTO> Autores { get; set; }
    }
}
