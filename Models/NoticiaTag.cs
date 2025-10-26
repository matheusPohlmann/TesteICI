using System.ComponentModel.DataAnnotations; // ESSA LINHA É CRUCIAL
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTesteICI.Models 
{
    public class NoticiaTag
    {
        public int Id { get; set; }

        [Required]
        public int NoticiaId { get; set; }

        [Required]
        public int TagId { get; set; }


        [ForeignKey("NoticiaId")]
        public Noticia Noticia { get; set; }
        
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
