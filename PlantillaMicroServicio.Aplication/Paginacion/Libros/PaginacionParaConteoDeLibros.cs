using PlantillaMicroServicio.Dal.Core.Paginacion;
using PlantillaMicroServicio.Models;

namespace PlantillaMicroServicio.Aplication.Paginacion.Libros
{
    public class PaginacionParaConteoDeLibros : PaginacionBase<Libro>
    {
        public PaginacionParaConteoDeLibros(PaginacionLibroParametro parametro)
         : base(parametro.ConstruirFiltro())
        { }
    }
}
