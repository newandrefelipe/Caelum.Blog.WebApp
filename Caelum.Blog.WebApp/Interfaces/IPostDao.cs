using System.Collections.Generic;
using Caelum.Blog.WebApp.Models;

namespace Caelum.Blog.WebApp.Interfaces
{
    public interface IPostDao
    {
        IEnumerable<Post> Listar();
        Post BuscarPorId(int id);
        void Incluir(Post post);
        void Alterar(Post post);
        void Remover(Post post);
    }
}
