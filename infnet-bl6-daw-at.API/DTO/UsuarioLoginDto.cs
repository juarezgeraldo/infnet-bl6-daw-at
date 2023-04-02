using System.ComponentModel.DataAnnotations;

namespace AT.API.DTOs.Users
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "Nome do usuário é necessário")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password do usuário é necessária")]
        public string? Password { get; init; }
    }
}
