using System.Collections.Generic;
using Caelum.Blog.WebApp.Models;

namespace Caelum.Blog.WebApp.Interfaces
{
    public interface IUsuarioDao
    {
        IEnumerable<Usuario> Usuarios { get; }
    }
}
