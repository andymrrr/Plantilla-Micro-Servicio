using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace PlantillaMicroServicio.Infrastructure.Logging
{
    /// <summary>
    /// Extensiones para registrar servicios de logging
    /// </summary>
    public static class LoggingServiceCollectionExtensions
    {
        /// <summary>
        /// Registra los servicios de logging en el contenedor de dependencias
        /// </summary>
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            // Registra el logger de Serilog como singleton
            services.AddSingleton(Log.Logger);

            // Registra el servicio de logging personalizado
            services.AddScoped<ILoggerService, SerilogLoggerService>();

            return services;
        }
    }
}