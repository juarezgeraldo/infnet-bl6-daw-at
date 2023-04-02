using AutoMapper;
using DTO.Livro;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace infnet_bl6_daw_at.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILivroService _livroService;
        public LivroController(ILivroService livroService, IMapper mapper)
        {
            _mapper = mapper;
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroDTO>>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<LivroDTO>>(await _livroService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroDTO>> Get(int id)
        {
            return Ok(_mapper.Map<LivroDTO>(await _livroService.Get(id)));
        }

        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(InsereLivroDTO insereLivroDTO)
        {
            var livro = _mapper.Map<Livro>(insereLivroDTO);
            return Ok(_mapper.Map<LivroDTO>(await _livroService.Add(livro)));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutLivro(int id, AtualizaLivroDTO atualizaLivroDTO)
        {
            var livro = _mapper.Map<Livro>(atualizaLivroDTO);
            livro.Id = id;
            return Ok(await _livroService.Save(livro));
//            return Ok(_mapper.Map<LivroDTO>(await _livroService.Save(livro)));
        }

        [HttpPatch("{livroId}/autores/{autorId}")]
        public async Task<ActionResult<LivroDTO>> AddAutorNoLivro(int livroId, int autorId)
        {
            return Ok(_mapper.Map<LivroDTO>(await _livroService.AddAutor(livroId, autorId)));
        }

        [HttpDelete("{livroId}/autores/{autorId}")]
        public async Task<ActionResult<LivroDTO>> DeleteAutorDoLivro(int livroId, int autorId)
        {
            return Ok(_mapper.Map<LivroDTO>(await _livroService.RemoveAutor(livroId, autorId)));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            return Ok(_mapper.Map<LivroDTO>(await _livroService.Remove(id)));
        }

    }
}
