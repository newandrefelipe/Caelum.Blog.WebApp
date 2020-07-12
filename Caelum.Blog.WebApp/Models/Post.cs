using System;
using System.ComponentModel.DataAnnotations;

namespace Caelum.Blog.WebApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Título", Prompt = "Digite o título do post")]
        public string Titulo { get; set; }
        
        [Required]
        [StringLength(250)]
        public string Resumo { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Categoria { get; set; }

        public DateTime? DataPublicacao { get; set; }

        public bool Publicado { get; set; }

        public Usuario Autor { get; set; }
        public int? IdAutor { get; set; }
    }
}
