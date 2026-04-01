using FarmaciaWeb.Models;
using FarmaciaWeb.Data; // IMPORTANTE: Para que reconozca el ApplicationDbContext
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FarmaciaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // CORRECCIÓN 1: Cambiamos 'object' por el tipo real de tu base de datos
        private readonly ApplicationDbContext _context;

        // CORRECCIÓN 2: Pasamos el context por el constructor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; // Ahora _context ya no está vacío ni es un objeto genérico
        }

        public async Task<IActionResult> Index()
        {
            var hoy = DateTime.Now;
            var proximoMes = hoy.AddDays(30);

            // Ahora estos 'await' sí funcionarán porque _context ya sabe qué es Medicamentos
            ViewBag.TotalMedicamentos = await _context.Medicamentos.CountAsync();
            ViewBag.Vencidos = await _context.Medicamentos.CountAsync(m => m.FechaVencimiento < hoy);
            ViewBag.PorVencer30 = await _context.Medicamentos.CountAsync(m => m.FechaVencimiento >= hoy && m.FechaVencimiento <= proximoMes);
            ViewBag.BajoStock = await _context.Medicamentos.CountAsync(m => m.Stock < 5);
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync();
            ViewBag.TotalEstantes = await _context.Estantes.CountAsync();

            return View();
        }

        // ... el resto de tus métodos (Privacy, Error) se quedan igual
    }
}