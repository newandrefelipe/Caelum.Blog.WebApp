using Microsoft.AspNetCore.Mvc;
using Caelum.Blog.WebApp.Models;
using Caelum.Blog.WebApp.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;

namespace Caelum.Blog.WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        IUsuarioDao dao;

        public UsuariosController(IUsuarioDao dao)
        {
            this.dao = dao;
        }

        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // verificar usuário no banco
                Usuario usuario = dao.Usuarios
                    .FirstOrDefault(u => u.Login == model.Usuario && u.Senha == model.Senha);

                if (usuario == null)
                {
                    ModelState.AddModelError("Usuario", "Usuário ou senha inválidos");
                    return View(model);
                }

                // guardar usuário na sessão
                List<Claim> direitos = new List<Claim>
                {
                    new Claim("Id",  usuario.Id.ToString()),
                    new Claim("Nome",  usuario.Nome),
                    new Claim("Email", usuario.Email),
                    new Claim("Login", usuario.Login)
                };

                ClaimsIdentity identidade = new ClaimsIdentity(direitos, "Cookies");
                ClaimsPrincipal user = new ClaimsPrincipal(identidade);
                await HttpContext.SignInAsync(user);

                return RedirectToAction("Index", "Posts");
            }
            return View(model);
        }

        [HttpPost("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
