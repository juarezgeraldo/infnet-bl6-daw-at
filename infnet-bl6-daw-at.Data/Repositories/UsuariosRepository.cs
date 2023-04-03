using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using infnet_bl6_daw_at.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace infnet_bl6_daw_at.Data.Repositories
{

    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public UsuariosRepository(UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> CreateUserAsync(Usuario user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<bool> ValidateUserAsync(UsuarioLogin login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            var result = user != null && await _userManager.CheckPasswordAsync(user, login.Password);
            return result;
        }

        public async Task<Token> CreateTokenAsync(UsuarioLogin userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var jwtConfig = _configuration.GetSection("jwtConfig");
            var expiresIn = jwtConfig["expiresIn"];

            var token = new Token
            {
                BearerToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                ExpirationDate = DateTime.Now.AddMinutes(Convert.ToDouble(expiresIn))
            };

            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(Usuario user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

    }
}