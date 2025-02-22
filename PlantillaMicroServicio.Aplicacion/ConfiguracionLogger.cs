using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlantillaMicroServicio.Aplicacion.Servicios.Implementacion;
using PlantillaMicroServicio.Aplicacion.Servicios.Interfaz;
using Serilog;

namespace PlantillaMicroServicio.Aplicacion
{
    public static class ConfiguracionLogger
    {
        public static void AgregarLogging(WebApplicationBuilder builder)
        {
            // Configurar Serilog usando la configuración de appsettings.json
            Log.Logger = new LoggerConfiguration()
             .ReadFrom.Configuration(builder.Configuration)
              .CreateLogger();

            builder.Host.UseSerilog(Log.Logger);

            builder.Services.AddScoped<FiltroRegistroSolicitudes>(); // Agregamos el filtro
            // Inyectar el servicio de LoggerService
            builder.Services.AddSingleton<ILoggerService, LoggerService>();
        }
    }
}
