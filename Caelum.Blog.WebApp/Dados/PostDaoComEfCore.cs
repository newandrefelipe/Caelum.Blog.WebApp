using System.Collections.Generic;
using System.Linq;
using Caelum.Blog.WebApp.Interfaces;
using Caelum.Blog.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Caelum.Blog.WebApp.Dados
{
    public class PostDaoComEfCore : IPostDao
    {
        BlogContext ctx;

        public PostDaoComEfCore(BlogContext context)
        {
            ctx = context;
        }

        public void Incluir(Post novoPost)
        {
            ctx.Posts.Add(novoPost);
            ctx.SaveChanges();
        }

        public IEnumerable<Post> Listar()
        {
            return ctx.Posts
                .Include(p => p.Autor)
                .AsNoTracking()
                .ToList(); // SELECT * FROM Posts
        }

        public Post BuscarPorId(int id)
        {
            return ctx.Posts
                .Include(p => p.Autor)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Alterar(Post post)
        {
            ctx.Posts.Update(post);
            ctx.SaveChanges();
        }

        public void Remover(Post post)
        {
            ctx.Posts.Remove(post);
            ctx.SaveChanges();
        }

        public IList<Post> BuscarPorCategoria(string categ)
        {
            return ctx.Posts.Where(p => p.Categoria.Contains(categ)).ToList();
        }
    }
}
