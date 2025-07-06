using Bogus;
using MediatR;
using PlantillaMicroServicio.Dal.Core.Interfaces;
using PlantillaMicroServicio.Modelos;
namespace PlantillaMicroServicio.Aplication.Feature.Libros.Comando.InicializarLibros
{
    public class InicializarLibrosHandler : IRequestHandler<InicializarLibrosComando, bool>
    {
        private readonly IPlantillaMicroServicioUoW _context;
        public InicializarLibrosHandler(IPlantillaMicroServicioUoW context)
        {
            _context = context;
        }
        public async Task<bool> Handle(InicializarLibrosComando request, CancellationToken cancellationToken)
        {
            var faker = new Faker<Libro>()
                      .RuleFor(l => l.Titulo, f => f.Lorem.Sentence(3))
                      .RuleFor(l => l.Autor, f => f.Name.FullName())
                      .RuleFor(l => l.Editorial, f => f.Company.CompanyName())
                      .RuleFor(l => l.FechaPublicacion, f => f.Date.Past(10));

           
            var libros = faker.Generate(100);

           
            _context.Libros.AgregarColeccion(libros);
            await _context.GuardarCambiosAsync();
            return true;
        }
    }
}
