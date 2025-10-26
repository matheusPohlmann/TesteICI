using System.ComponentModel.DataAnnotations;

namespace ProjetoTesteICI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Senha { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public ICollection<Noticia> Noticias { get; set; }
    }
}