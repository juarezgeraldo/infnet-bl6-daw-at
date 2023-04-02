using System.ComponentModel.DataAnnotations;

namespace infnet_bl6_daw_at.MVC.Models.Autores
{
    public class UsuarioLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
