using AT.Models;
using infnet_bl6_daw_at.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Data.Repositories
{
    public interface IUsuariosRepository
    {
        Task<IdentityResult> CreateUserAsync(Usuario usuario, string password);
        Task<bool> ValidateUserAsync(UsuarioLogin usuarioLoginDto);
        Task<Token> CreateTokenAsync(UsuarioLogin usuario);
    }
}
