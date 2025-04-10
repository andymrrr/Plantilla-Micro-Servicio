using MediatR;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm;


namespace PlantillaMicroServicio.Aplicacion.Feature.Libros.Consulta.BuscarLibroGuid
{
    public class BuscarLibroGuidConsulta : IRequest<LibroVm>
    {
        public Guid? Guid { get; set; }
        public BuscarLibroGuidConsulta(Guid? libroGuid)
        {

            if (libroGuid is null)
            {
                throw new ArgumentException("El ID del libro no puede estar vacio", nameof(libroGuid));
            }

            Guid = libroGuid;
        }

    }
}
