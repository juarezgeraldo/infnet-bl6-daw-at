using infnet_bl6_daw_at.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace infnet_bl6_daw_at.Data.Repositories
{
    public interface IUsuariosRepository
    {
        Task<IdentityResult> CreateUserAsync(Usuario usuario, string password);
        Task<bool> ValidateUserAsync(UsuarioLogin usuarioLoginDto);
        Task<Token> CreateTokenAsync(UsuarioLogin usuario);
    }
}
