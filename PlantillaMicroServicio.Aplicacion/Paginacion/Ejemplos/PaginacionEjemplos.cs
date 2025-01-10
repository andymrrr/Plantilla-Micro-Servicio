
using PlantillaMicroServicio.DAL.Core.Paginacion;
using PlantillaMicroServicio.DAL.Modelo;

namespace PlantillaMicroServicio.Aplicacion.Paginacion.Ejemplos
{
    public class PaginacionEjemplos : PaginacionBase<Ejemplo>
    {
        public PaginacionEjemplos(PaginacionEjemplosParametro parametro) : base(parametro.ConstruirFiltro())
        {
            // Incluir relaciones necesarias
           // AgregarIncluir(c => c.Subcategorias!);
           // AgregarIncluir(c => c.Artes!);

            // Configurar paginación
            AplicarPaginacion(parametro.Pagina, parametro.CantidadRegistroPorPagina);

            // Aplicar ordenamiento
            AplicarOrdenamiento(parametro.Ordenar);
        }

        private void AplicarOrdenamiento(string? orden)
        {
            if (string.IsNullOrEmpty(orden))
            {
                AgregarOrdenarPor(c => c.Id); // Ordenar por ID por defecto
                return;
            }

            // Aplicar ordenamiento basado en el parámetro
            var ordenamiento = orden.ToLower();
            if (ordenamiento == "nombreasc")
            {
                AgregarOrdenarPor(c => c.Nombre!);
            }
            else if (ordenamiento == "nombredesc")
            {
                AgregarOrdenarDescendiente(c => c.Nombre!);
            }
            else
            {
                AgregarOrdenarPor(c => c.Nombre); // Ordenar por ID si no coincide con ningún caso
            }
        }
    }


}
