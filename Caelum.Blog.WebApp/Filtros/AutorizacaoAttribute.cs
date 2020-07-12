using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caelum.Blog.WebApp.Filtros
{
    public class AutorizacaoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string usuario = context.HttpContext.Session.GetString("usuario");
            if (string.IsNullOrWhiteSpace(usuario))
            {
                context.Result = new RedirectToRouteResult(new 
                {
                    action = "Login",
                    controller = "Usuarios"
                });
            }
        }
    }
}
