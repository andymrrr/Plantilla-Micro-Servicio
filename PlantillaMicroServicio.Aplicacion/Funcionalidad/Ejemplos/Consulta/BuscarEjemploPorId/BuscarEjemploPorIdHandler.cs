using AutoMapper;
using Bogus;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Ejemplos.DTO;
using MediatR;
using PlantillaMicroServicio.DAL.Modelo;


namespace PlantillaMicroServicio.Aplicacion.Funcionalidad.Ejemplos.Consulta.BuscarEjemploPorId
{
    public class BuscarEjemploPorIdHandler : IRequestHandler<BuscarEjemploPorIdConsulta, EjemploDTO>
    {
        private readonly IMapper _mapper;
        public BuscarEjemploPorIdHandler(IMapper mapper)
        {
            _mapper = mapper;   
        }
        public Task<EjemploDTO> Handle(BuscarEjemploPorIdConsulta request, CancellationToken cancellationToken)
        {


            var datosFalso = new Faker<Ejemplo>()
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
