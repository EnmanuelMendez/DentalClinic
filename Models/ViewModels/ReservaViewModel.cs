using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentalClinic.Models.ViewModels
{
    public class ReservaViewModel
    {
        public Cliente Cliente { get; set; } = new Cliente();
        public int ServicioId { get; set; }
        public DateTime FechaHora { get; set; }

        public IEnumerable<SelectListItem> Servicios { get; set; } = new List<SelectListItem>();
    }
}
