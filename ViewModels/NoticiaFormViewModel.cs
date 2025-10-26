using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetoTesteICI.ViewModels
{
    public class NoticiaFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(250)] 
        public string Titulo { get; set; } = string.Empty; 

        [Required(ErrorMessage = "O texto é obrigatório")]
        public string Texto { get; set; } = string.Empty; 

        [Required(ErrorMessage = "O Autor é obrigatório.")] 
        public int UsuarioId { get; set; }
        
        public List<SelectListItem>? Usuarios { get; set; }
        
        public List<int> SelectedTagIds { get; set; } = new List<int>();

        public List<SelectListItem>? TodasTags { get; set; }
    }

}
