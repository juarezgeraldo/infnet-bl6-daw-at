using AT.Models;
using infnet_bl6_daw_at.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace infnet_bl6_daw_at.Domain.Interfaces
{
    public interface IUsuariosService
    {
        Task<IdentityResult> CreateUserAsync(Usuario usuario, string password);
        Task<bool> ValidateUserAsync(UsuarioLogin loginDto);
        Task<Token> CreateTokenAsync(UsuarioLogin usuario);
    }
}
