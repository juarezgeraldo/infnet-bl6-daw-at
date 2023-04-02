using System.ComponentModel.DataAnnotations;

namespace infnet_bl6_daw_at.MVC.Models.Autores
{
    public class InsereUsuarioViewModel
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        [Required(ErrorMessage = "Usuário é necessário")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "Email é necessário")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password é necessária")]
        public string Password { get; init; }
        public string ReturnUrl { get; init; }
    }
}
