
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantillaMicroServicio.Aplication.Feature.Salud.Consulta.VerificarEstado;
using PlantillaMicroServicio.Infrastructure.Logging;

namespace Connexy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaludController : ControllerBase
    {
        private readonly IMediator _mediador;
        private readonly ILoggerService _loggerService;

        public SaludController(IMediator mediator, ILoggerService loggerService)
        {
            _mediador = mediator;
            _loggerService = loggerService;
        }
        [HttpGet]
        public async Task<IActionResult> VerificarEstado()
        {
            _loggerService.LogInformation("Verificando estado del servicio", new { Timestamp = DateTime.UtcNow });

            try
            {
                var resultado = await _mediador.Send(new VerificarEstadoConsulta());

                _loggerService.LogBusinessOperation("VerificarEstado", new { Estado = resultado.Estado }, resultado.Estado == "Saludable");

                return resultado.Estado == "Saludable" ? Ok(resultado) : StatusCode(500, resultado);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error al verificar estado del servicio", ex, new { Timestamp = DateTime.UtcNow });
                throw;
            }
        }
    }
}
