using DentalClinic.Data;
using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Controllers
{
    public class HorarioAtencionController : Controller
    {
        private readonly DentalClinicContext _context;

        public HorarioAtencionController(DentalClinicContext context)
        {
            _context = context;
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var lista = await _context.HorarioAtencion.ToListAsync();
            return View(lista);
        }

        // DETALLES
        public async Task<IActionResult> Details(int id)
        {
            var horario = await _context.HorarioAtencion.FindAsync(id);
            return horario == null ? NotFound() : View(horario);
        }

        // CREAR
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HorarioAtencion horario)
        {
            if (!ModelState.IsValid)
                return View(horario);

            _context.HorarioAtencion.Add(horario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDITAR
        public async Task<IActionResult> Edit(int id)
        {
            var horario = await _context.HorarioAtencion.FindAsync(id);
            return horario == null ? NotFound() : View(horario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HorarioAtencion horario)
        {
            if (!ModelState.IsValid)
                return View(horario);

            _context.HorarioAtencion.Update(horario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR
        public async Task<IActionResult> Delete(int id)
        {
            var horario = await _context.HorarioAtencion.FindAsync(id);
            return horario == null ? NotFound() : View(horario);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horario = await _context.HorarioAtencion.FindAsync(id);

            if (horario != null)
            {
                _context.HorarioAtencion.Remove(horario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
