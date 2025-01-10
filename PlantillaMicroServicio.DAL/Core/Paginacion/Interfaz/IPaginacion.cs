using System.Linq.Expressions;

namespace  PlantillaMicroServicio.DAL.Core.Paginacion.Interfaz
{
    public interface IPaginacion<T>
    {
        Expression<Func<T, bool>>? Filtro { get; } 
        List<Expression<Func<T, object>>>? Inclusiones { get; } 
        Expression<Func<T, object>>? OrdenarPor { get; } 
        bool OrdenarDescendente { get; } 
        int Pagina { get; } 
        int CantidadRegistro { get; } 
        bool HabilitarPaginacion { get; } 
    }

}
