using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantillaMicroServicio.Aplication.Feature.Salud.Consulta.VerificarEstado;
using PlantillaMicroServicio.Aplication.Feature.Salud.Dto;
using PlantillaMicroServicio.Infrastructure.Logging;

namespace PlantillaMicroServicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaludController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _loggerService;

        public SaludController(IMediator mediator, ILoggerService loggerService)
        {
            _mediator = mediator;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<IActionResult> VerificarEstado()
        {
            try
            {
                var consulta = new VerificarEstadoConsulta();
                var resultado = await _mediator.Send(consulta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error al verificar estado del servicio", ex);
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }
    }
}
