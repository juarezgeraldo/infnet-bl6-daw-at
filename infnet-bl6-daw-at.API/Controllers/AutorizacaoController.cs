using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Service;
using infnet_bl6_daw_at.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.API.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace infControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly JwtBearerTokenSettings _jwtTokenSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly infnet_bl6_daw_atDbContext _context;


        public AutorizacaoController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<IdentityUser> userManager, infnet_bl6_daw_atDbContext context)
        {
            this._jwtTokenSettings = jwtTokenOptions.Value;
            this._userManager = userManager;
            _context = context;

        }

        [HttpPost]
        [Route("AlterarSenha")]
        public async Task<ActionResult> AlterarSenha([FromBody] CredencialLogin credencialLogin)
        {
            var identidadeUsuario = await _userManager.FindByNameAsync(credencialLogin.Username);

            if (identidadeUsuario != null)
            {

                identidadeUsuario.PasswordHash = _userManager.PasswordHasher.HashPassword(identidadeUsuario, credencialLogin.Password);
                var resultado = await _userManager.UpdateAsync(identidadeUsuario);
                return NoContent();
            }
            return BadRequest(new BadRequestObjectResult(new { Mensagem = "Usuário inválido." }));
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if(!ModelState.IsValid || usuario == null)
            {
                return new BadRequestObjectResult(new { Mensagem = "Registro do Usuário não efetuado." });
            }

            var identidadeUsuario = new IdentityUser() { 
                UserName = usuario.UserName, 
                Email = usuario.Email,
            };

            var resultado = await _userManager.CreateAsync(identidadeUsuario, usuario.Password);

            if (!resultado.Succeeded)
            {
                return new BadRequestObjectResult(new { Mensagem = "Não foi possível registrar Usuario.", Erros = resultado.Errors });
            }

            return Ok(new { Mensagem = "Registro do Usuario concluído com sucesso." });
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] CredencialLogin credencialLogin)
        {
            IdentityUser identidadeUsuario;

            if (!ModelState.IsValid || 
                credencialLogin == null || 
                (identidadeUsuario = await ValidarUsuario(credencialLogin)) == null)
            {
                return new BadRequestObjectResult(new { Mensagem = "Não foi possível realizar o login do Usuário." });
            }

            var token = GerarToken(identidadeUsuario);

            return Ok(new { Token = token, nomeUsuario = credencialLogin.Username, Mensagem = "Login do Usuário realizado com sucesso." });
        }
        [HttpGet]
        [Route("GetUsuario")]
        public async Task<ActionResult<Usuario>> GetUsuario(string nomeUsuario)
        {
            var identidadeUsuario = await _userManager.FindByNameAsync(nomeUsuario);
            if (identidadeUsuario != null)
            {
                var usuario = new Usuario();
                usuario.UserName = identidadeUsuario.UserName;
                usuario.Email = identidadeUsuario.Email;

                return usuario;
            }
            return null;
        }

        private object GerarToken(IdentityUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtTokenSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, applicationUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, applicationUser.Email)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtTokenSettings.Audience,
                Issuer = _jwtTokenSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<IdentityUser> ValidarUsuario(CredencialLogin credencialLogin)
        {
            var identidadeUsuario = await _userManager.FindByNameAsync(credencialLogin.Username);
            if (identidadeUsuario != null)
            {
                var resultado = _userManager.PasswordHasher
                            .VerifyHashedPassword(identidadeUsuario, 
                                    identidadeUsuario.PasswordHash, 
                                    credencialLogin.Password);
                return resultado == PasswordVerificationResult.Failed ? null : identidadeUsuario;
            }
            return null;
        }


        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(new { Token = "", Message = "Logged Out" });
        }

        [HttpDelete]
        [Route("Excluir")]
        public async Task<IActionResult> Excluir(string emailUsuario)
        {
            if (!ModelState.IsValid || emailUsuario == null)
            {
                return new BadRequestObjectResult(new { Mensagem = "Exclusão do Usuário não efetuada." });
            }

            Task<IdentityUser> applicationUser = BuscarUsuario(emailUsuario);

            if (applicationUser == null)
            {
                return new BadRequestObjectResult(new { Mensagem = "Exclusão do Usuário não efetuada." });
            }

            var resultado = await _userManager.DeleteAsync(applicationUser.Result);

            if (!resultado.Succeeded)
            {
                var dicionario = new ModelStateDictionary();
                foreach (IdentityError erro in resultado.Errors)
                {
                    dicionario.AddModelError(erro.Code, erro.Description);
                }
                return new BadRequestObjectResult(new { Mensagem = "Não foi possível Excluir Usuario.", Erros = dicionario });
            }
            return Ok(new { Mensagem = "Usuário excluído com sucesso." });

        }

        private async Task<IdentityUser> BuscarUsuario(string emailUsuario)
        {
            Task<IdentityUser> applicationUser = _userManager.FindByEmailAsync(emailUsuario);

            return await applicationUser;

        }
    }
}
