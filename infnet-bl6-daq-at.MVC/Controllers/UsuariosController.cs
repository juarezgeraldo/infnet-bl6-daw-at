using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using infnet_bl6_daw_at.MVC.Models.Conta;
using Flurl.Http;
using infnet_bl6_daw_at.API.DTO;

namespace infnet_bl6_daw_at.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: UsuariosController
        public IActionResult Login(string ReturnUrl = "/Livro/Index")
        {
            LoginModel objLoginModel = new LoginModel();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }

        // POST: UsuariosController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("NomeUsuario,SenhaUsuario,ReturnUrl")] LoginModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await "https://localhost:7202/api/Autorizacao/Login"
                        .PostJsonAsync(login)
                        .ReceiveJson<TokenModel>();

                    HttpContext.Response.Cookies.Append("token", response.Token,
                        new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(10) });

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.NomeUsuario)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return LocalRedirect(login.ReturnUrl);
                }
                catch (FlurlHttpException ex)
                {
                    var error = await ex.GetResponseJsonAsync<IdentityErrorModel>();

                    if (error == null)
                    {
                        return View();
                    }

                    ViewBag.ErrorMessage = error.Mensagem;

                    return View(login);
                }
            }

            return View(login);
        }


        public async Task<IActionResult>  Adicionar(AdicionarUsuarioViewModel adicionarUsuarioViewModel)
        {
            return View(adicionarUsuarioViewModel);
        }

        // POST: UsuariosController/Adicionar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(UsuarioDto usuario)
        {
            var adicionarUsuarioViewModel = new AdicionarUsuarioViewModel();
            adicionarUsuarioViewModel.Usuario = usuario;

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await "https://localhost:7202/api/Autorizacao/Registrar"
                        .PostJsonAsync(usuario);

                    return RedirectToAction(nameof(Login));
                }
                catch(FlurlHttpException ex)
                {
                    var error = await ex.GetResponseJsonAsync<IdentityErrorModel>();

                    if (error == null)
                    {
                        return View();
                    }

                    foreach (var erro in error.Erros)
                    {
                        if (erro.Code.ToLowerInvariant().Contains("Username"))
                        {
                            ModelState.AddModelError(nameof(usuario.NomeUsuario), erro.Description);
                        }
                        else if (erro.Code.ToLowerInvariant().Contains("password"))
                        {
                            ModelState.AddModelError(nameof(usuario.SenhaUsuario), erro.Description);
                        }
                        else if (erro.Code.ToLowerInvariant().Contains("Email"))
                        {
                            ModelState.AddModelError(nameof(usuario.EmailUsuario), erro.Description);
                        }
                        else if (erro.Code.Contains(nameof(usuario.TelefoneUsuario)))
                        {
                            ModelState.AddModelError(nameof(usuario.TelefoneUsuario), erro.Description);
                        }
                    }

                    ViewBag.ErrorMessage = error.Mensagem;
                    return View(adicionarUsuarioViewModel);
                }
            }

            return View(adicionarUsuarioViewModel);
        }

        public async Task<IActionResult> RecuperarSenha()
        {
            return View();
        }


        // GET: UsuariosController
        public IActionResult Logout()
        {
            return View();
        }

        // POST: UsuariosController/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(bool logout)
        {
            try
            {
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Login", "Usuarios");
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync<IdentityErrorModel>();

                if (error == null)
                {
                    return View();
                }

                ViewBag.ErrorMessage = error.Mensagem;

                return View();
            }
        }
    }
}
