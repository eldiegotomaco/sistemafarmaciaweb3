using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmaciaWeb.Data;
using FarmaciaWeb.Models;

namespace FarmaciaWeb.Controllers
{
    public class MedicamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string buscar, int? filtroCategoria, int? filtroEstante, string tipoReporte)
        {
            var hoy = DateTime.Now;
            var query = _context.Medicamentos
                .Include(m => m.Categoria)
                .Include(m => m.Estante)
                .AsQueryable();

            // Lógica de Reportes Rápidos
            if (tipoReporte == "vencidos")
                query = query.Where(m => m.FechaVencimiento < hoy);
            else if (tipoReporte == "porVencer")
                query = query.Where(m => m.FechaVencimiento >= hoy && m.FechaVencimiento <= hoy.AddDays(30));
            else if (tipoReporte == "bajoStock")
                query = query.Where(m => m.Stock < 5);

            // Filtros de búsqueda
            if (!string.IsNullOrEmpty(buscar))
                query = query.Where(m => m.Nombre.Contains(buscar));

            if (filtroCategoria.HasValue)
                query = query.Where(m => m.CategoriaId == filtroCategoria);

            if (filtroEstante.HasValue)
                query = query.Where(m => m.EstanteId == filtroEstante);

            ViewData["FiltroActual"] = buscar;
            ViewBag.TipoReporte = tipoReporte;
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", filtroCategoria);
            ViewBag.Estantes = new SelectList(_context.Estantes, "Id", "Nombre", filtroEstante);

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var medicamento = await _context.Medicamentos
                .Include(m => m.Categoria)
                .Include(m => m.Estante)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicamento == null) return NotFound();

            return View(medicamento);
        }

        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Stock,FechaVencimiento,CategoriaId,EstanteId,Descripcion,Activo")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.CategoriaId);
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "Id", "Nombre", medicamento.EstanteId);
            return View(medicamento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null) return NotFound();

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.CategoriaId);
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "Id", "Nombre", medicamento.EstanteId);
            return View(medicamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,Stock,FechaVencimiento,CategoriaId,EstanteId,Descripcion,Activo")] Medicamento medicamento)
        {
            if (id != medicamento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentoExists(medicamento.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.CategoriaId);
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "Id", "Nombre", medicamento.EstanteId);
            return View(medicamento);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var medicamento = await _context.Medicamentos
                .Include(m => m.Categoria)
                .Include(m => m.Estante)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicamento == null) return NotFound();

            return View(medicamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento != null)
            {
                _context.Medicamentos.Remove(medicamento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.Id == id);
        }
    }
}