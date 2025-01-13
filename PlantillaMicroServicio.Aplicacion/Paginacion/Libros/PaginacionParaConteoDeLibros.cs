using PlantillaMicroServicio.Dal.Nucleo.Paginacion;
using PlantillaMicroServicio.Modelos;

namespace PlantillaMicroServicio.Aplicacion.Paginacion.Libros
{
    public class PaginacionParaConteoDeLibros : PaginacionBase<Libro>
    {
        public PaginacionParaConteoDeLibros(PaginacionLibroParametro parametro)
         : base(parametro.ConstruirFiltro())
        { }
    }
}
