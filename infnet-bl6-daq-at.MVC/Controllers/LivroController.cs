using AutoMapper;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.MVC.Models.Autores;
using infnet_bl6_daw_at.MVC.Models.Livros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace infnet_bl6_daw_at.MVC.Controllers
{
    public class LivroController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;

        public LivroController(IMapper mapper, ILivroService livroService, IAutorService autorService)
        {
            _mapper = mapper;
            _livroService = livroService;
            _autorService = autorService;
        }

        // GET: livroController
        public async Task<ActionResult> Index()
        {
            var livros = await _livroService.GetAll();
            return View(_mapper.Map<IEnumerable<LivroViewModel>>(livros));
        }

        // GET: livroController/Details/5
        public async Task<ActionResult> Detalhes(int id)
        {
            var livro = await _livroService.Get(id);
            return View(_mapper.Map<LivroViewModel>(livro));
        }

        // GET: livroController/Create
        public async Task<ActionResult> Incluir()
        {
            var autores = await _autorService.GetAll();

            var autorViewModel = _mapper.Map<IEnumerable<AutorViewModel>>(autores);

            ViewBag.Autores = new MultiSelectList(autorViewModel, "Id", "FullName");

            return View();
        }

        // POST: livroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Incluir(InsereLivroViewModel insereLivro, int[] autoresId)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var autores = autoresId.Select(id => new InsereLivroAutorViewModel() { Id = id }).ToList();

                insereLivro.Autores = autores;

                await _livroService.Add(_mapper.Map<Livro>(insereLivro));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: livroController/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            var livro = await _livroService.Get(id);
            var outroAutores = await _autorService.GetAll();
            var autores = livro.Autores;

            ViewBag.Autores = autores;
            ViewBag.outrosAutores = outroAutores.Except(autores);

            return View(_mapper.Map<AtualizaLivroViewModel>(livro));
        }

        // POST: livroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int id, AtualizaLivroViewModel atualizaLivro)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                await _livroService.Save(_mapper.Map<Livro>(atualizaLivro));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: livroController/Delete/5
        public async Task<ActionResult> Excluir(int id)
        {
            var livro = await _livroService.Get(id);
            return View(_mapper.Map<LivroViewModel>(livro));
        }

        // POST: livroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(LivroViewModel livro)
        {
            try
            {
                await _livroService.Remove(livro.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> ExcluiAutor(int livroId, int autorId)
        {
            var livro = await _livroService.Get(livroId);
            var autor = await _autorService.Get(autorId);

            var removeAuthor = new ExcluiAutorViewModel()
            {
                AutorId = autor.Id,
                LivroId = livro.Id
            };

            ViewBag.Author = _mapper.Map<AutorViewModel>(autor);
            ViewBag.Book = _mapper.Map<LivroViewModel>(livro);

            return View(removeAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveAuthor(ExcluiAutorViewModel excluiAutor)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                await _livroService.RemoveAutor(excluiAutor.LivroId, excluiAutor.AutorId);
                return RedirectToAction(nameof(Editar), new { id = excluiAutor.LivroId});
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AdicionaAutor(int livroId, int autorId)
        {
            var livro = await _livroService.Get(livroId);
            var autor = await _autorService.Get(autorId);

            var adicionaAutor = new IncluiAutorViewModel()
            {
                LivroId = livro.Id,
                AutorId = autor.Id
            };

            ViewBag.Autor = _mapper.Map<AutorViewModel>(autor);
            ViewBag.Livro = _mapper.Map<LivroViewModel>(livro);

            return View(adicionaAutor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdicionaAutor(IncluiAutorViewModel incluiAutor)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                await _livroService.AddAutor(incluiAutor.LivroId, incluiAutor.AutorId);
                return RedirectToAction(nameof(Editar), new { id = incluiAutor.LivroId});
            }
            catch
            {
                return View();
            }
        }

    }
}
