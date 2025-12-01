using DentalClinic.Data;
using DentalClinic.Models;
using DentalClinic.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Controllers
{
    public class ReservaController : Controller
    {
        private readonly DentalClinicContext _context;

        public ReservaController(DentalClinicContext context)
        {
            _context = context;
        }

        // Buscar reservas por email
        public IActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(string email)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Email == email);
            if (cliente == null)
            {
                ViewBag.Error = "No se encontró un cliente con ese email.";
                return View();
            }

            return RedirectToAction("MisReservas", new { clienteId = cliente.Id });
        }

        // Listado para el cliente
        public async Task<IActionResult> MisReservas(int clienteId)
        {
            var cliente = await _context.Cliente.FindAsync(clienteId);
            if (cliente == null) return NotFound();

            var reservas = await _context.Reserva
                .Include(r => r.Servicio)
                .Where(r => r.ClienteId == clienteId)
                .ToListAsync();

            ViewBag.ClienteNombre = cliente.Nombre;

            return View(reservas);
        }

        // Crear reserva
        public async Task<IActionResult> Create(int clienteId)
        {
            var cliente = await _context.Cliente.FindAsync(clienteId);
            if (cliente == null) return NotFound();

            var vm = new ReservaViewModel
            {
                Cliente = cliente,
                Servicios = _context.Servicio
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Nombre
                    }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Servicios = _context.Servicio
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Nombre
                    }).ToList();

                return View(vm);
            }

            var servicio = await _context.Servicio.FindAsync(vm.ServicioId);

            var reserva = new Reserva
            {
                ClienteId = vm.Cliente.Id,
                ServicioId = vm.ServicioId,
                FechaHora = vm.FechaHora,
                PrecioTotal = servicio!.CostoBase,
                Estado = "Pendiente"
            };

            _context.Reserva.Add(reserva);
            await _context.SaveChangesAsync();

            return RedirectToAction("MisReservas", new { clienteId = reserva.ClienteId });
        }

        // EDITAR
        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _context.Reserva.Include(r => r.Cliente).FirstOrDefaultAsync(r => r.Id == id);
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

            return RedirectToAction("MisReservas", new { clienteId = reserva.ClienteId });
        }

        // Cancelar
        public async Task<IActionResult> Cancelar(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null) return NotFound();

            reserva.Estado = "Cancelada";
            await _context.SaveChangesAsync();

            return RedirectToAction("MisReservas", new { clienteId = reserva.ClienteId });
        }
    }
}
