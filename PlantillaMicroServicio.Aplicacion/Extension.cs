using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantillaMicroServicio.Aplicacion.Mapper;
using System.Reflection;

namespace PlantillaMicroServicio.Aplicacion
{
    public static class Extension
    {
        public static void AddNegocio(this IServiceCollection servicio, IConfiguration config)
        {
            servicio.AddMediatR(opcion => opcion.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            var cofiguracionMapeo = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapeoPerfil());
            });
            IMapper mapeo = cofiguracionMapeo.CreateMapper();
            servicio.AddSingleton(mapeo);
        }
    }
}
