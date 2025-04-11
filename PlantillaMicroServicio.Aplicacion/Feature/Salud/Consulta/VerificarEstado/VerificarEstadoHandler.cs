using MediatR;
using Microsoft.EntityFrameworkCore;
using PlantillaMicroServicio.Aplicacion.Feature.Salud.Dto;
using PlantillaMicroServicio.Dal.Contexto;

namespace PlantillaMicroServicio.Aplicacion.Feature.Salud.Consulta.VerificarEstado
{
    public class VerificarEstadoHandler : IRequestHandler<VerificarEstadoConsulta, ControlSalud>
    {
        private readonly ContextPlantillaMicroServicio _context;
        public VerificarEstadoHandler(ContextPlantillaMicroServicio context)
        {
            _context = context;
        }
        public async Task<ControlSalud> Handle(VerificarEstadoConsulta request, CancellationToken cancellationToken)
        {
            var response = new ControlSalud();

            try
            {
                await _context.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);
                response.Estado = "Saludable";
                response.BaseDeDatos = "Conectada";
            }
            catch
            {
                response.Estado = "No saludable";
                response.BaseDeDatos = "Desconectada";
            }

            return response;
        }
    }
}
