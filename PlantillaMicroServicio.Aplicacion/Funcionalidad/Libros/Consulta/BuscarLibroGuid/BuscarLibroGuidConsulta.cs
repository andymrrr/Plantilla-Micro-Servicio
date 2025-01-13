using MediatR;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroGuid
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
