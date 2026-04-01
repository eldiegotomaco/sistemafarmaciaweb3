using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaWeb.Models;
using FarmaciaWeb.Data;
using Microsoft.AspNetCore.Authorization;

namespace FarmaciaWeb.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        [Authorize(Roles = "Administrador,Farmaceutico")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Farmaceutico")]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [Authorize(Roles = "Administrador,Farmaceutico")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Farmaceutico")]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}