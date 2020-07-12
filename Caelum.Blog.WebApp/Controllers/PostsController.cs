using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Caelum.Blog.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Caelum.Blog.WebApp.HttpClients;
using System.Threading.Tasks;

namespace Caelum.Blog.WebApp.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        readonly BlogApiClient api;

        public PostsController(BlogApiClient client)
        {
            api = client;
        }

        private IActionResult ViewForm(Post model)
        {
            return View("Form", model);
        }

        public async Task<IActionResult> Index()
        {
            var lista = await api.Listar();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return ViewForm(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Novo(Post novoPost)
        {
            // validar o modelo
            if (ModelState.IsValid)
            {
                IEnumerable<Claim> direitos = HttpContext.User.Claims;
                Claim idClaim = direitos.FirstOrDefault(c => c.Type == "Id");
                int idUsuario = Int32.Parse(idClaim.Value);
                novoPost.IdAutor = idUsuario;
                await api.Incluir(novoPost);
                return RedirectToAction("Index");
            }
            // mandar de volta as informações digitadas (ié, mandar o modelo)
            // colocar uma mensagem informando do problema
            return ViewForm(novoPost);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var post = await api.BuscarPorId(id);
            ViewData["Title"] = "Edição do post";
            return ViewForm(post);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Post post)
        {
            if (ModelState.IsValid)
            {
                await api.Alterar(post);
                return RedirectToAction("Index");
            }
            return ViewForm(post);
        }

        public async Task<IActionResult> Publicar(int id)
        {
            var post = await api.BuscarPorId(id);
            post.DataPublicacao = DateTime.Today;
            post.Publicado = true;
            await api.Alterar(post);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Excluir(int id)
        {
            await api.Remover(id);
            return RedirectToAction("Index");
        }

        [Route("[controller]/[action]/{categoria}")]
        public async Task<IActionResult> Categoria(string categoria)
        {
            var lista = await api.Listar();
            var postsDaCategoria = lista.Where(p => p.Categoria.Equals(categoria)).ToList();
            return View("Index", postsDaCategoria);
        }

        public async Task<IActionResult> CategoriaAutocomplete(string term)
        {
            var posts  = await api.Listar();
            IEnumerable<string> categorias = posts
                .Where(p => p.Categoria.Contains(term))
                .Select(p => p.Categoria)
                .Distinct();
            return Json(categorias);
        }
    }
}
