using System.Security.Claims;
using AutoMapper;
using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using infnet_bl6_daw_at.MVC.Models.Conta;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace infnet_bl6_daw_at.MVC.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUsuariosService _usersService;
        private readonly IMapper _mapper;

        // GET: ContaController/Create
        public ContaController(IUsuariosService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public ActionResult Login(string ReturnUrl = "/Livro/Index")
        {
            var objLoginModel = new UsuarioLoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(objLoginModel);
        }

        // POST: ContaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UsuarioLoginViewModel login)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var loginModel = _mapper.Map<UsuarioLogin>(login);
                var token = await _usersService.CreateTokenAsync(loginModel);

                if (token != null)
                {
                    HttpContext.Response.Cookies.Append("token", token.BearerToken,
                        new Microsoft.AspNetCore.Http.CookieOptions { Expires = token.ExpirationDate });

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, login.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = token.ExpirationDate,
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return LocalRedirect(login.ReturnUrl);
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ContaController/Edit/5
        public ActionResult Registrar()
        {
            return View(new InsereUsuarioViewModel { ReturnUrl = "/" });
        }

        // POST: ContaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(InsereUsuarioViewModel createUser)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var createModel = _mapper.Map<Usuario>(createUser);
                var result = await _usersService.CreateUserAsync(createModel, createUser.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                var messages = result.Errors.Select(e => e.Description);
                var errorMessages = string.Join(", ", messages);

                ViewBag.ErrorMessage = errorMessages;

                return View(createUser);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(createUser);
            }
        }

        // GET: ContaController/Delete/5
        public ActionResult Logout()
        {
            return View();
        }

        // POST: ContaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout(IFormCollection collection)
        {
            try
            {
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }
    }
}
