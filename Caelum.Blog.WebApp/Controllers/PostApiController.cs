using Caelum.Blog.WebApp.Interfaces;
using Caelum.Blog.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caelum.Blog.WebApp.Controllers
{
    [ApiController]
    [Route("/api/posts")]
    public class PostApiController : ControllerBase
    {
        IPostDao dao;

        public PostApiController(IPostDao dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public IActionResult EndpointGetPosts()
        {
            var posts = dao.Listar();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult EndpointGetPostPorId(int id)
        {
            var post = dao.BuscarPorId(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public IActionResult EndpointInsertPost([FromForm] Post post)
        {
            dao.Incluir(post);
            return Created($"/api/posts/{post.Id}", post);
        }

        [HttpPut]
        public IActionResult EndpointEditPost([FromForm] Post post)
        {
            dao.Alterar(post);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeletePost(int id)
        {
            var post = dao.BuscarPorId(id);
            dao.Remover(post);
            return NoContent();
        }

        [HttpPatch]
        public IActionResult EndpointPublishPost(AutorPublica model)
        {
            var post = dao.BuscarPorId(model.IdPost);
            if (post == null) return NotFound();
            post.Publicado = true;
            post.DataPublicacao = model.DataPublicacao;
            post.IdAutor = model.IdAutor;
            dao.Alterar(post);
            return Ok(post);
        }
    }
}
