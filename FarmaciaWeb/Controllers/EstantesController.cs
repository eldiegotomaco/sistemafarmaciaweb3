using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaWeb.Models;
using FarmaciaWeb.Data;
using Microsoft.AspNetCore.Authorization;

namespace FarmaciaWeb.Controllers
{
    public class EstantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estantes.ToListAsync());
        }

        [Authorize(Roles = "Administrador,Farmaceutico")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Farmaceutico")]
        public async Task<IActionResult> Create(Estante estante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estante);
        }

        [Authorize(Roles = "Administrador,Farmaceutico")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var estante = await _context.Estantes.FindAsync(id);
            if (estante == null) return NotFound();

            return View(estante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Farmaceutico")]
        public async Task<IActionResult> Edit(int id, Estante estante)
        {
            if (id != estante.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(estante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estante);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var estante = await _context.Estantes.FindAsync(id);
            if (estante == null) return NotFound();

            return View(estante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estante = await _context.Estantes.FindAsync(id);

            if (estante != null)
            {
                _context.Estantes.Remove(estante);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}