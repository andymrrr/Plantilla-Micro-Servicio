using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroGuid;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroPaginacion;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroServicio.Dal.Nucleo.Paginacion.Modelos;
using System.Net;

namespace PlantillaMicroServicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly IMediator _mediador;
        public LibrosController(IMediator mediador)
        {
            _mediador = mediador;
        }
        [HttpGet("BuscarLibro/{guid}", Name = "BuscarLibro")]
        [ProducesResponseType(typeof(LibroVm), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<LibroVm>> BuscarLibro(Guid? guid)
        {
            var consulta = new BuscarLibroGuidConsulta(guid);
            return await _mediador.Send(consulta);
        }

        [HttpGet("Paginacion", Name = "Paginacion")]
        [ProducesResponseType(typeof(PaginacionVm<LibroVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<LibroVm>>> Paginacion([FromQuery] BuscarLibroPaginacionConsulta consulta)
        {

            var paginacion = await _mediador.Send(consulta);
            return Ok(paginacion);
        }


    }
  
}
