using System.Collections.Generic;
using Caelum.Blog.WebApp.Interfaces;
using Caelum.Blog.WebApp.Models;

namespace Caelum.Blog.WebApp.Dados
{
    public class UsuarioDaoComEfCore : IUsuarioDao
    {
        BlogContext context;

        public UsuarioDaoComEfCore(BlogContext context)
        {
            this.context = context;
        }

        public IEnumerable<Usuario> Usuarios => context.Usuarios;
    }
}
