using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTesteICI.Data;
using ProjetoTesteICI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTesteICI.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tag
        public async Task<IActionResult> Index()
        {
            var tags = await _context.Tags.ToListAsync();
            return View(tags);
        }

        // GET: Tag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] Tag tag)
        {
            Console.WriteLine($"🟢 TAG RECEBIDA: {tag.Descricao}");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    var key = error.Key;
                    foreach (var e in error.Value.Errors)
                    {
                        Console.WriteLine($"❌ Campo: {key} | Erro: {e.ErrorMessage}");
                    }
                }
                return View(tag);
            }

            _context.Add(tag);
            await _context.SaveChangesAsync();
            Console.WriteLine("✅ Tag salva com sucesso");
            return RedirectToAction(nameof(Index));
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Tag tag)
        {
            if (id != tag.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(tag);

            try
            {
                _context.Update(tag);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tag atualizada com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tags.Any(e => e.Id == tag.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Tag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tag = await _context.Tags.FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tag excluída com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
