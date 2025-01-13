

using AutoMapper;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroServicio.Modelos;

namespace PlantillaMicroServicio.Aplicacion.Mapeo
{
    internal class MapeoPerfil : Profile
    {
        public MapeoPerfil()
        {
            CreateMap<Libro,LibroVm>().ReverseMap();
        }
    }
}
