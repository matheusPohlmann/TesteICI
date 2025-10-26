using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTesteICI.Models
{
    public class Noticia
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Titulo { get; set; }

        [Required]
        public string Texto { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        
        public ICollection<NoticiaTag> NoticiaTags { get; set; }
    }
}