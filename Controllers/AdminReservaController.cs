using DentalClinic.Data;
using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Controllers
{
    public class AdminReservaController : Controller
    {
        private readonly DentalClinicContext _context;

        public AdminReservaController(DentalClinicContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string busqueda)
        {
            var query = _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .AsQueryable();

            if (!string.IsNullOrEmpty(busqueda))
            {
                query = query.Where(r =>
                    r.Cliente.Nombre.Contains(busqueda) ||
                    r.Cliente.Email.Contains(busqueda));
            }

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null) return NotFound();

            ViewBag.Servicios = new SelectList(_context.Servicio, "Id", "Nombre");

            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Reserva reserva)
        {
            if (!ModelState.IsValid) return View(reserva);

            _context.Reserva.Update(reserva);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Completar(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null) return NotFound();

            reserva.Estado = "Completada";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancelar(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null) return NotFound();

            reserva.Estado = "Cancelada";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
