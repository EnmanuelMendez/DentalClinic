using DentalClinic.Data;
using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Controllers
{
    public class ServicioController : Controller
    {
        private readonly DentalClinicContext _context;

        public ServicioController(DentalClinicContext context)
        {
            _context = context;
        }

        // GET: Servicio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servicio.ToListAsync());
        }

        // GET: Servicio/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            return servicio == null ? NotFound() : View(servicio);
        }

        // GET: Servicio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        [HttpPost]
        public async Task<IActionResult> Create(Servicio servicio)
        {
            if (!ModelState.IsValid)
                return View(servicio);

            _context.Servicio.Add(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Servicio/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            return servicio == null ? NotFound() : View(servicio);
        }

        // POST: Servicio/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Servicio servicio)
        {
            if (!ModelState.IsValid)
                return View(servicio);

            _context.Servicio.Update(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Servicio/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            return servicio == null ? NotFound() : View(servicio);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);

            if (servicio != null)
            {
                _context.Servicio.Remove(servicio);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

