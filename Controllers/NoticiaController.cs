using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTesteICI.Data;
using ProjetoTesteICI.Models;
using ProjetoTesteICI.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetoTesteICI.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var noticias = await _context.Noticias
               .Include(n => n.Usuario)
               .Include(n => n.NoticiaTags)
               .ThenInclude(nt => nt.Tag)
               .OrderByDescending(n => n.Id)
               .ToListAsync();

            return View(noticias);

        }

        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }


        [HttpGet("/Noticia/GetModal/{id?}")]
        public async Task<IActionResult> GetModal(int? id)
        {
            var formViewModel = new NoticiaFormViewModel();
            var modalViewModel = new NoticiaModalViewModel { Form = formViewModel };


            modalViewModel.Form.TodasTags = await _context.Tags
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Descricao
                })
                .ToListAsync();

            modalViewModel.Form.Usuarios = await _context.Usuarios
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Nome
                })
                .ToListAsync();


            if (id.HasValue && id.Value != 0)
            {

                var noticia = await _context.Noticias
                    .Include(n => n.NoticiaTags)
                    .FirstOrDefaultAsync(n => n.Id == id.Value);

                if (noticia == null)
                {
                    return NotFound();
                }


                modalViewModel.Form.Id = noticia.Id;
                modalViewModel.Form.Titulo = noticia.Titulo;
                modalViewModel.Form.Texto = noticia.Texto;
                modalViewModel.Form.UsuarioId = noticia.UsuarioId;
                modalViewModel.Form.SelectedTagIds = noticia.NoticiaTags.Select(nt => nt.TagId).ToList();
            }


            return PartialView("_NoticiaModal", modalViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoticiaFormViewModel form)
        {
            if (ModelState.IsValid)
            {

                var noticia = new Noticia
                {
                    Titulo = form.Titulo,
                    Texto = form.Texto,
                    UsuarioId = form.UsuarioId,
                };


                noticia.NoticiaTags = form.SelectedTagIds.Select(tagId => new NoticiaTag
                {
                    TagId = tagId
                }).ToList();

                _context.Add(noticia);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NoticiaFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var noticia = await _context.Noticias
                    .Include(n => n.NoticiaTags)
                    .FirstOrDefaultAsync(n => n.Id == form.Id);

                if (noticia == null) return NotFound();


                noticia.Titulo = form.Titulo;
                noticia.Texto = form.Texto;
                noticia.UsuarioId = form.UsuarioId;

                noticia.NoticiaTags.Clear();
                noticia.NoticiaTags = form.SelectedTagIds.Select(tagId => new NoticiaTag
                {
                    NoticiaId = noticia.Id,
                    TagId = tagId
                }).ToList();

                _context.Update(noticia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var noticia = await _context.Noticias.FindAsync(id);
                if (noticia != null)
                {
                    _context.Noticias.Remove(noticia);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
