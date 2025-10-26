using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoTesteICI.Data;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using ProjetoTesteICI.Models;

namespace ProjetoTesteICI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
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



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
