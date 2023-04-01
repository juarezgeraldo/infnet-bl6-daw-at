using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.ViewModel;
using infnet_bl6_daw_at.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Data.Entity;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace infnet_bl6_daw_at.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly infnet_bl6_daw_atDbContext _dbContext;
        public AutorController(infnet_bl6_daw_atDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorViewModel>>> GetAutores()
        {
            return AutorViewModel.GetAll(_dbContext.Autores.ToList()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorViewModel>> GetAutor(int id)
        {
            var autor = await  _dbContext.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return AutorViewModel.Get(autor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor autor)
        {

            if (id != autor.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(autor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
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
        public async Task<ActionResult<Autor>> PostAutor(Autor autor)
        {
            _dbContext.Autores.Add(autor);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetAutor", new { id = autor.Id }, autor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            var autor = await _dbContext.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _dbContext.Autores.Remove(autor);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        private bool AutorExists(int id)
        {
            return _dbContext.Autores.Any(e => e.Id == id);
        }

    }
}
