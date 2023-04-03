using AutoMapper;
using infnet_bl6_daw_at.API.DTO;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace infnet_bl6_daw_at.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly IUsuariosService _usersService;
        private readonly IMapper _mapper;

        public AutorizacaoController(IUsuariosService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] InsereUsuarioDto usuario)
        {
            var userResult = await _usersService.CreateUserAsync(_mapper.Map<Usuario>(usuario), usuario.Password);

            if (userResult.Succeeded)
                return StatusCode(201);

            return BadRequest(userResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UsuarioLoginDto usuario)
        {
            var token = await _usersService.CreateTokenAsync(_mapper.Map<UsuarioLogin>(usuario));

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
