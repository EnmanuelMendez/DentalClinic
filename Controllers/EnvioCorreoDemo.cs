using DentalClinic.Models;
using DentalClinic.Services;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EnvioCorreoDemo : ControllerBase
    {
        private readonly IServicioEmail servicioEmail;

        public EnvioCorreoDemo(IServicioEmail servicioEmail)
        {
            this.servicioEmail = servicioEmail;
        }

        [HttpPost]
        public async Task<ActionResult> Enviar([FromBody] EmailRequest request)
        {
            await servicioEmail.EnviarEmail(request.Email, request.Tema, request.Cuerpo);
            return Ok("Correo enviado correctamente.");
        }
    }
}
