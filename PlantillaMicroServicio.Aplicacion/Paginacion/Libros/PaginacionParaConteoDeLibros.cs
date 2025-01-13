using PlantillaMicroServicio.Dal.Nucleo.Paginacion;
using PlantillaMicroServicio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroServicio.Aplicacion.Paginacion.Libros
{
    public class PaginacionParaConteoDeLibros : PaginacionBase<Libro>
    {
        public PaginacionParaConteoDeLibros(PaginacionLibroParametro parametro)
         : base(parametro.ConstruirFiltro())
        { }
    }
}
