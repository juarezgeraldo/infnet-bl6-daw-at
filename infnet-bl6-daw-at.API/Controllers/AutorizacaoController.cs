using infnet_bl6_daw_at.API.DTO;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.API.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace infnet_bl6_daw_at.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly JwtBearerTokenSettings _jwtTokenSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly infnet_bl6_daw_atDbContext _context;


        public AutorizacaoController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<ApplicationUser> userManager, infnet_bl6_daw_atDbContext context)
        {
            this._jwtTokenSettings = jwtTokenOptions.Value;
            this._userManager = userManager;
            _context = context;

        }

        [HttpPost]
        [Route("AlterarSenha")]
        public async Task<ActionResult> AlterarSenha([FromBody] CredencialLogin credencialLogin)
        {
            var identidadeUsuario = await _userManager.FindByNameAsync(credencialLogin.NomeUsuario);

            if (identidadeUsuario != null)
            {

                identidadeUsuario.PasswordHash = _userManager.PasswordHasher.HashPassword(identidadeUsuario, credencialLogin.SenhaUsuario);
                var resultado = await _userManager.UpdateAsync(identidadeUsuario);
                return NoContent();
            }
            return BadRequest(new BadRequestObjectResult(new { Mensagem = "Usuário inválido." }));
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioDto usuario)
        {
            if (!ModelState.IsValid || usuario == null)
            {
                return new BadRequestObjectResult(new { Mensagem = "Registro do Usuário não efetuado." });
            }

            var identidadeUsuario = new ApplicationUser()
            {
                UserName = usuario.NomeUsuario,
                Email = usuario.EmailUsuario,
                PhoneNumber = usuario.TelefoneUsuario
            };

            var resultado = await _userManager.CreateAsync(identidadeUsuario, usuario.SenhaUsuario);

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
            ApplicationUser identidadeUsuario;

            if (!ModelState.IsValid ||
                credencialLogin == null ||
                (identidadeUsuario = await ValidarUsuario(credencialLogin)) == null)
            {
                return new BadRequestObjectResult(new { Mensagem = "Não foi possível realizar o login do Usuário." });
            }

            var token = GerarToken(identidadeUsuario);

            return Ok(new { Token = token, nomeUsuario = credencialLogin.NomeUsuario, Mensagem = "Login do Usuário realizado com sucesso." });
        }
        [HttpGet]
        [Route("GetUsuario")]
        public async Task<ActionResult<Usuario>> GetUsuario(string nomeUsuario)
        {
            var identidadeUsuario = await _userManager.FindByNameAsync(nomeUsuario);
            if (identidadeUsuario != null)
            {
                var usuario = new Usuario();
                usuario.NomeUsuario = identidadeUsuario.UserName;
                usuario.EmailUsuario = identidadeUsuario.Email;
                usuario.TelefoneUsuario = identidadeUsuario.PhoneNumber;

                return usuario;
            }
            return null;
        }

        private object GerarToken(ApplicationUser applicationUser)
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

        private async Task<ApplicationUser> ValidarUsuario(CredencialLogin credencialLogin)
        {
            var identidadeUsuario = await _userManager.FindByNameAsync(credencialLogin.NomeUsuario);
            if (identidadeUsuario != null)
            {
                var resultado = _userManager.PasswordHasher
                            .VerifyHashedPassword(identidadeUsuario,
                                    identidadeUsuario.PasswordHash,
                                    credencialLogin.SenhaUsuario);
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

            Task<ApplicationUser> applicationUser = BuscarUsuario(emailUsuario);

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

        private async Task<ApplicationUser> BuscarUsuario(string emailUsuario)
        {
            Task<ApplicationUser> applicationUser = _userManager.FindByEmailAsync(emailUsuario);

            return await applicationUser;



        }
    }
}