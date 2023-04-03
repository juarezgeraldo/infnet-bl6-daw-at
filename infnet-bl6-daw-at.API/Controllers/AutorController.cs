using infnet_bl6_daw_at.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.API.DTO.Autor;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace infnet_bl6_daw_at.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService, IMapper mapper)
        {
            _mapper = mapper;
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<AutorDTO>>(await _autorService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            return Ok(_mapper.Map<AutorDTO>(await _autorService.Get(id)));
        }


        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor(InsereAutorDTO autorDTO)
        {
            var autor = _mapper.Map<Autor>(autorDTO);
            return Ok(_mapper.Map<AutorDTO>(await _autorService.Add(autor)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AutorDTO>> PutAutor(int id, AtualizaAutorDTO autorDTO)
        {
            var autor = _mapper.Map<Autor>(autorDTO);
            autor.Id = id;
            return Ok(_mapper.Map<AutorDTO>(await _autorService.Save(autor)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            return Ok(_mapper.Map<AutorDTO>(await _autorService.Remove(id)));
        }

    }
}
