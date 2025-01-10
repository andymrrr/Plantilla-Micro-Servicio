
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Ejemplos.DTO;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Ejemplos.Consulta.BuscarEjemploPorId;
namespace PlantillaMicroServicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemploController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EjemploController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet("BuscarEjemplo/{guid}", Name = "BuscarEjemplo")]
        [ProducesResponseType(typeof(EjemploDTO), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<EjemploDTO>> BuscarEjemplo(Guid? guid)
        {
            var consulta = new BuscarEjemploPorIdConsulta(guid);
            return await _mediator.Send(consulta);
        }

    }
}
