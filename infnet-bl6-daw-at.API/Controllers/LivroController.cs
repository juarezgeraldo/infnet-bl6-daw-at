using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.ViewModel;
using infnet_bl6_daw_at.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace infnet_bl6_daw_at.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly infnet_bl6_daw_atDbContext _dbContext;
        public LivroController(infnet_bl6_daw_atDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<LivroViewModel>>> GetLivros()
        {
            return LivroViewModel.GetAll(_dbContext.Livros.ToList()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroViewModel>> GetLivro(int id)
        {
            var livro = await  _dbContext.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return LivroViewModel.Get(livro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(livro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
            _dbContext.Livros.Add(livro);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetLivro", new { id = livro.Id }, livro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _dbContext.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _dbContext.Livros.Remove(livro);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        private bool LivroExists(int id)
        {
            return _dbContext.Livros.Any(e => e.Id == id);
        }

    }
}
