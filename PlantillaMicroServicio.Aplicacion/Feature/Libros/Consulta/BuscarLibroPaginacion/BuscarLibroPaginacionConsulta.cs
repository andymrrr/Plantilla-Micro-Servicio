using MediatR;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroServicio.Dal.Core.Paginacion.Modelos;
using PlantillaMicroServicio.Dal.Core.Paginacion;


namespace PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroPaginacion
{
    public class BuscarLibroPaginacionConsulta : PaginacionParametro, IRequest<PaginacionVm<LibroVm>>
    {
        public Guid? LibroGuid { get; set; }
        public string? titulo { get; set; }
    }
}
