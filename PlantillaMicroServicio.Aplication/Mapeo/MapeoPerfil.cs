

using AutoMapper;
using PlantillaMicroServicio.Aplication.Feature.Libros.Vm;
using PlantillaMicroServicio.Models;

namespace PlantillaMicroServicio.Aplication.Mapeo
{
    internal class MapeoPerfil : Profile
    {
        public MapeoPerfil()
        {
            CreateMap<Libro,LibroVm>().ReverseMap();
        }
    }
}
