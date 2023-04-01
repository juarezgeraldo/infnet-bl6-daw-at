using infnet_bl6_daw_at.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace infnet_bl6_daw_at.Domain.ViewModel;

public class AutorViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }

    [Display(Name = "Data de nascimento")]
    [DataType(DataType.Date)]
    public DateTime Nascimento { get; set; }

    public virtual ICollection<Livro> Livros { get; set; }

    public AutorViewModel(Autor autor)
    {
        this.Id = autor.Id;
        this.Nome = autor.Nome;
        this.Sobrenome = autor.Sobrenome;
        this.Email = autor.Email;
        this.Nascimento = autor.Nascimento;
        this.Livros = autor.Livros;
    }

    [Display(Name = "Nome completo")]
    public string NomeCompleto
    {
        get { return Nome + " " + Sobrenome; }
    }

    public static List<AutorViewModel> GetAll(List<Autor> autores)
    {
        var list = new List<AutorViewModel>();
        foreach (var item in autores)
        {
            list.Add(new AutorViewModel(item));
        }
        return list;
    }

    public static AutorViewModel Get(Autor autor)
    {
        return new AutorViewModel(autor);
    }

    public static AutorViewModel PutAutor(int Id, Autor autor)
    {
        return new AutorViewModel(autor);
    }

    /*    [Display(Name = "Próximo aniversário")]
        [DataType(DataType.Date)]
        public DateTime ProximoAniversario
        {
            get { return ProximoAniversarioFuncao(); }
        }
        [Display(Name = "Dias para aniversário")]
        public int DiasFaltantes
        {
            get { return CalculaDiasFaltantesFuncao(); }
        }

        public DateTime ProximoAniversarioFuncao()
        {
            DateTime dataProximoAniversario = new(DateTime.Now.Year, Nascimento.Month, Nascimento.Day, 0, 0, 0);
            if (DateTime.Compare(dataProximoAniversario, DateTime.Today) < 0)
            {
                dataProximoAniversario = dataProximoAniversario.AddYears(1);
            }
            return dataProximoAniversario;
        }
        public int CalculaDiasFaltantesFuncao()
        {
            DateTime dataAtual = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateTime dataAniversario = ProximoAniversarioFuncao();
            DateTime dataProximoAniversario = dataAniversario;

            if (dataAtual.Month == dataAniversario.Month &&
                dataAtual.Day == dataAniversario.Day)
            {
                return 0;
            }
            int difDatas = (int)dataAtual.Subtract(dataProximoAniversario).TotalDays;
            if (difDatas < 0) { difDatas *= -1; }

            return difDatas;
        }
        public bool NomeCompletoPossui(string nomePesquisa)
        {
            return NomeCompleto.ToLowerInvariant().Contains(nomePesquisa.Trim().ToLowerInvariant());
        }
        public static List<AutorViewModel> GetAll(List<Livro> amigos)
        {
            var listTipo = new List<AutorViewModel>();
            foreach (var item in amigos)
            {
                listTipo.Add(new AutorViewModel(item));
            }
            return listTipo;
        }
    */
}
