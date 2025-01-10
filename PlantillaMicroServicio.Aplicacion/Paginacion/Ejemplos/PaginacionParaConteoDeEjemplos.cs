using PlantillaMicroServicio.DAL.Core.Paginacion;
using PlantillaMicroServicio.DAL.Modelo;


namespace PlantillaMicroServicio.Aplicacion.Paginacion.Ejemplos
{
    public class PaginacionParaConteoDeEjemplos : PaginacionBase<Ejemplo>
    {
        public PaginacionParaConteoDeEjemplos(PaginacionEjemplosParametro parametro)
         : base(parametro.ConstruirFiltro())
        { }
    }
}
