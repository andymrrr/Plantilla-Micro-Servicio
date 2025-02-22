

using PlantillaMicroServicio.Aplicacion.Servicios.Utilitario;

namespace PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm
{
    public class LibroVm
    {
        public string Titulo { get; set; }
        public string Editorial { get; set; }
        public string Autor { get; set; }
        [ExcluirDeLog]
        public DateTime FechaPublicacion { get; set; }
    }
}
