using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantillaMicroServicio.Dal.Contexto;
using PlantillaMicroServicio.Dal.Core.Interfaces;
using PlantillaMicroServicio.Dal.Core.Repositories;

namespace PlantillaMicroServicio.Dal
{
    public static class Extension
    {
        public static IServiceCollection AddServicioDatos(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddSQL(configuracion);

            servicio.AddScoped<IPlantillaMicroServicioUoW, PlantillaMicroServicioUoW>();
            servicio.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));

            return servicio;
        }

        private static void AddSQL(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("PlantillaMicroServicio"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));
        }

        private static void AddPostgres(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
                options.UseNpgsql(configuracion.GetConnectionString("PlantillaMicroServicio"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));
        }
    }
}
