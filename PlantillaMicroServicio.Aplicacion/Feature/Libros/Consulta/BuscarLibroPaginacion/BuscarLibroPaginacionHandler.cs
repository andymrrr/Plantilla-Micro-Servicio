using MediatR;
using PlantillaMicroServicio.Dal.Core.Paginacion.Modelos;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroServicio.Aplicacion.Paginacion.Libros;
using PlantillaMicroServicio.Dal.Core.Interfaces;
using AutoMapper;

namespace PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroPaginacion
{
    public class BuscarLibroPaginacionHandler : IRequestHandler<BuscarLibroPaginacionConsulta, PaginacionVm<LibroVm>>
    {
        private readonly IPlantillaMicroServicioUoW _contex;
        private readonly IMapper _mapper;
        public BuscarLibroPaginacionHandler(IPlantillaMicroServicioUoW context, IMapper mapper)
        {
            _contex = context;
            _mapper = mapper;
        }
        public async Task<PaginacionVm<LibroVm>> Handle(BuscarLibroPaginacionConsulta request, CancellationToken cancellationToken)
        {
            var libroParametro = new PaginacionLibroParametro
            {
                Pagina = request.Pagina,
                CantidadRegistroPorPagina = request.CantidadRegistroPorPagina,
                Busqueda = request.Busqueda,
                Ordenar = request.Ordenar,
                LibroGuid = request.LibroGuid,
                titulo = request.titulo
            };

            var especificaciones = new PaginacionLibro(libroParametro);
            var categorias = await _contex.Libros.BuscarTodaEspecificificaciones(especificaciones);

            var cantidadEspecificaciones = new PaginacionParaConteoDeLibros(libroParametro);
            var totalCategorias = await _contex.Libros.CantidadAsincrona(cantidadEspecificaciones);

            var categoriasVm = _mapper.Map<IReadOnlyList<LibroVm>>(categorias);

            return new PaginacionVm<LibroVm>
            {
                TotalRegistros = totalCategorias,
                PaginaActual = request.Pagina,
                CantidadRegistroPorPagina = request.CantidadRegistroPorPagina,
                Datos = categoriasVm
            };
        }
    }
}
