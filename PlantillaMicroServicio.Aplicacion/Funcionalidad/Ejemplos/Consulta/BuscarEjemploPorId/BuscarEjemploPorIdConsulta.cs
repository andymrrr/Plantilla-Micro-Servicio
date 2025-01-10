
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Ejemplos.DTO;
using MediatR;


namespace PlantillaMicroServicio.Aplicacion.Funcionalidad.Ejemplos.Consulta.BuscarEjemploPorId
{
    public class BuscarEjemploPorIdConsulta : IRequest<EjemploDTO>
    {
        public Guid? Guid { get; set; }
        public BuscarEjemploPorIdConsulta(Guid? ejemploGuid)
        {

            if (ejemploGuid is null)
            {
                throw new ArgumentException("El ID del ejemplo no puede estar vacio", nameof(ejemploGuid));
            }

            Guid = ejemploGuid;
        }
    }
}
