
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantillaMicroServicio.Aplication.Feature.Salud.Consulta.VerificarEstado;

namespace Connexy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaludController : ControllerBase
    {
        private IMediator _mediador;
        public SaludController(IMediator mediator)
        {
            _mediador = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> VerificarEstado()
        {
            var resultado = await _mediador.Send(new VerificarEstadoConsulta());
            return resultado.Estado == "Saludable" ? Ok(resultado) : StatusCode(500, resultado);
        }
    }
}
