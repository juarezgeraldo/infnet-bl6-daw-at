using System.ComponentModel.DataAnnotations;

namespace infnet_bl6_daw_at.API.DTO
{
    public class InsereUsuarioDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        [Required(ErrorMessage = "Usuário é necessário")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Password é necessária")]
        public string Password { get; init; }
    }
}
