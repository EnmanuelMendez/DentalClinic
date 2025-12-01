using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [Required, ForeignKey("Servicio")]
        public int ServicioId { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required, StringLength(20)]
        public string Estado { get; set; } = "Pendiente";

        [Required]
        public decimal PrecioTotal { get; set; }

        public Cliente? Cliente { get; set; }
        public Servicio? Servicio { get; set; }
    }
}
