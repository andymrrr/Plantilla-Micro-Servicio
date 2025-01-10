using AutoMapper;
using PlantillaMicroServicio.Aplicacion.Funcionalidad.Ejemplos.DTO;
using PlantillaMicroServicio.DAL.Modelo;



namespace PlantillaMicroServicio.Aplicacion.Mapper
{
    public class MapeoPerfil : Profile
    {
        public MapeoPerfil()
        {
            CreateMap<EjemploDTO, Ejemplo>().ReverseMap();
        }
    }
}
