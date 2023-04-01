using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infnet_bl6_daw_at.Domain.Entities
{
    public class Autor
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }
        public virtual ICollection<Livro> Livros { get; set; }


    }
}
