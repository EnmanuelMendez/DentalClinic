using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class HorarioAtencion
    {
        [Key]
        public int Id { get; set; }

        [Required, Range(1, 7)]
        public int DiaSemana { get; set; }  // 1 = Lunes

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        [Required, StringLength(100)]
        public string Medico { get; set; } = string.Empty;
    }
}
