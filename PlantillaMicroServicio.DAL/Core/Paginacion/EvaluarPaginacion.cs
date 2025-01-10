using Microsoft.EntityFrameworkCore;
using PlantillaMicroServicio.DAL.Core.Paginacion.Interfaz;


namespace PlantillaMicroServicio.DAL.Core.Paginacion
{
    public class EvaluarPaginacion<T> where T : class
    {
        public static IQueryable<T> MostrarConsulta(IQueryable<T> consulta, IPaginacion<T> especificacion)
        {
            
            if (especificacion.Filtro != null)
            {
                consulta = consulta.Where(especificacion.Filtro);
            }

           
            if (especificacion.OrdenarPor != null)
            {
                consulta = especificacion.OrdenarDescendente
                    ? consulta.OrderByDescending(especificacion.OrdenarPor)
                    : consulta.OrderBy(especificacion.OrdenarPor);
            }

          
            if (especificacion.HabilitarPaginacion)
            {
                consulta = consulta.Skip((especificacion.Pagina - 1) * especificacion.CantidadRegistro)
                                   .Take(especificacion.CantidadRegistro);
            }

           
            if (especificacion.Inclusiones != null)
            {
                consulta = especificacion.Inclusiones.Aggregate(consulta,
                    (current, include) => current.Include(include));
            }

            return consulta.AsSplitQuery().AsNoTracking();
        }
    }


}
