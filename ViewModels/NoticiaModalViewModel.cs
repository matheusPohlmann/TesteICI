using System.Collections.Generic;

namespace ProjetoTesteICI.ViewModels
{
    public class NoticiaModalViewModel
    {
        public NoticiaFormViewModel Form { get; set; } = new NoticiaFormViewModel();
        public List<TagDto> AvailableTags { get; set; }
    }
}