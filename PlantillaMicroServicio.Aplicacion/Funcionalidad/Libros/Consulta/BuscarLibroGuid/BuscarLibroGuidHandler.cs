using AutoMapper;
using Bogus;
using MediatR;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroServicio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroGuid
{
    internal class BuscarLibroGuidHandler : IRequestHandler<BuscarLibroGuidConsulta, LibroVm>
    {
        private readonly IMapper _mapper;
        public BuscarLibroGuidHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<LibroVm> Handle(BuscarLibroGuidConsulta request, CancellationToken cancellationToken)
        {
            var datosFalso = new Faker<Libro>()
           .RuleFor(e => e.Id, f => Guid.NewGuid())
           .RuleFor(e => e.Nombre, f => f.Name.FirstName())
           .RuleFor(e => e.Apellido, f => f.Name.LastName());

            var datosFalsos = datosFalso.Generate(50);
            datosFalsos[0].Id = Guid.Empty;

            var registro = datosFalsos.FirstOrDefault(e => e.Id == request.Guid);


            if (registro is null)
            {
                throw new KeyNotFoundException($"No se encontró un registro con el ID: {request.Guid}");
            }

            var ejemploDto = _mapper.Map<EjemploDTO>(registro);


            return Task.FromResult(ejemploDto);

        }
    }
}
