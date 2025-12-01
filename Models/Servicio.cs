using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Required]
        public decimal CostoBase { get; set; }

        public ICollection<ItemServicio>? Items { get; set; }
        public ICollection<Reserva>? Reservas { get; set; }
    }
}
