using MediatR;
using PlantillaMicroServicio.Aplication.Feature.Libros.Vm;
using PlantillaMicroServicio.Dal.Core.Paginacion.Modelos;
using PlantillaMicroServicio.Dal.Core.Paginacion;


namespace PlantillaMicroServicio.Aplication.Feature.Libros.Consulta.BuscarLibroPaginacion
{
    public class BuscarLibroPaginacionConsulta : PaginacionParametro, IRequest<PaginacionVm<LibroVm>>
    {
        public Guid? LibroGuid { get; set; }
        public string? titulo { get; set; }
    }
}
