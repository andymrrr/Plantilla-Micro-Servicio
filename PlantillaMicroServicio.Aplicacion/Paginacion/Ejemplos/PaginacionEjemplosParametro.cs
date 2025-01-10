
using PlantillaMicroServicio.DAL.Core.Paginacion;
using PlantillaMicroServicio.DAL.Modelo;
using System.Linq.Expressions;

namespace PlantillaMicroServicio.Aplicacion.Paginacion.Ejemplos
{
    public class PaginacionEjemplosParametro : PaginacionParametro
    {
        public Guid? EjemploId { get; set; }
        public string? Nombre { get; set; }

        // Método para construir el filtro de búsqueda
        public Expression<Func<Ejemplo, bool>> ConstruirFiltro()
        {
            return c =>
                (!EjemploId.HasValue || c.Id == EjemploId) &&
                (string.IsNullOrEmpty(Nombre) || c.Nombre!.Contains(Nombre)) &&
                (string.IsNullOrEmpty(Busqueda) || c.Nombre!.ToLower().Contains(Busqueda.ToLower()));
        }
    }
}
