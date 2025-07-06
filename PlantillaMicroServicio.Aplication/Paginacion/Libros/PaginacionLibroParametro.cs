using PlantillaMicroServicio.Dal.Core.Paginacion;
using PlantillaMicroServicio.Models;
using System.Linq.Expressions;

namespace PlantillaMicroServicio.Aplication.Paginacion.Libros
{
    public class PaginacionLibroParametro : PaginacionParametro
    {
        public Guid? LibroGuid { get; set; }
        public string? titulo { get; set; }

        // Método para construir el filtro de búsqueda
        public Expression<Func<Libro, bool>> ConstruirFiltro()
        {
            return c =>
                (!LibroGuid.HasValue || c.Id == LibroGuid) &&
                (string.IsNullOrEmpty(titulo) || c.Titulo!.Contains(titulo)) &&
                (string.IsNullOrEmpty(Busqueda) || c.Titulo!.ToLower().Contains(Busqueda.ToLower()));
        }
    }
}
