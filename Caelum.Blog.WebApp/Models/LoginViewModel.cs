using System.ComponentModel.DataAnnotations;

namespace Caelum.Blog.WebApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuário", Prompt = "Digite seu nome de usuário")]
        public string Usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
