using AutoMapper;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.MVC.Models.Autores;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infnet_bl6_daw_at.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AutorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAutorService _autorService;

        public AutorController(IMapper mapper, IAutorService autorService)
        {
            _mapper = mapper;
            _autorService = autorService;
        }
        // GET: AutorController
        public async Task<ActionResult> Index()
        {
            var autores = await _autorService.GetAll();
            return View(_mapper.Map<IEnumerable<AutorViewModel>>(autores));
        }

        // GET: AutorController/Details/5
        public async Task<ActionResult> Detalhes(int id)
        {
            var autor = await _autorService.Get(id);
            return View(_mapper.Map<AutorViewModel>(autor));
        }

        // GET: AutorController/Create
        public async Task<ActionResult> Incluir()
        {
            return View();
        }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Incluir(InsereAutorViewModel insereAutor)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                await _autorService.Add(_mapper.Map<Autor>(insereAutor));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutorController/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            var autor = await _autorService.Get(id);
            return View(_mapper.Map<AtualizaAutorViewModel>(autor));
        }

        // POST: AutorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int id, AtualizaAutorViewModel atualizaAutor)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                await _autorService.Save(_mapper.Map<Autor>(atualizaAutor));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutorController/Delete/5
        public async Task<ActionResult> Excluir(int id)
        {
            var autor = await _autorService.Get(id);
            return View(_mapper.Map<AutorViewModel>(autor));
        }

        // POST: AutorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(int id, AutorViewModel autor)
        {
            try
            {
                await _autorService.Remove(autor.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
