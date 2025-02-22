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
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                // Configuración de Serilog para registrar solo lo que se maneje en los filtros.
                configuration
                    .ReadFrom.Configuration(context.Configuration) // Lee la configuración de appsettings.json
                    .WriteTo.Console() // Puedes ajustar el destino de log según tus necesidades.
                    .WriteTo.File(
                        context.Configuration["SerilogConfig:Path"] ?? "logs/log.txt", // Ruta del archivo de logs desde la configuración, o valor por defecto
                        rollingInterval: RollingInterval.Day); // Logs se rotarán diariamente
            });

            builder.Services.AddScoped<FiltroRegistroSolicitudes>(); // Agregamos el filtro
            // Inyectar el servicio de LoggerService
            builder.Services.AddSingleton<ILoggerService, LoggerService>();
        }
    }
}
