using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaWeb.Models;
using FarmaciaWeb.Data;

namespace FarmaciaWeb.Controllers
{
    public class EstantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Estantes.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Estantes.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Estante estante)
        {
            _context.Update(estante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _context.Estantes.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estante = await _context.Estantes.FindAsync(id);
            _context.Estantes.Remove(estante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}