using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Caelum.Blog.WebApp.Models;
using Caelum.Blog.WebApp.Interfaces;

namespace Caelum.Blog.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IPostDao dao;

        public HomeController(ILogger<HomeController> logger, IPostDao dao)
        {
            _logger = logger;
            this.dao = dao;
        }

        public IActionResult Index()
        {
            var publicados = dao.Listar().Where(p => p.Publicado);
            return View(publicados);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Busca(string termo)
        {
            var resultado = dao.Listar()
                .Where(p => p.Publicado && 
                    p.Titulo.ToUpper().Contains(termo.ToUpper())
                );
            return View("Index", resultado);
        }
    }
}
