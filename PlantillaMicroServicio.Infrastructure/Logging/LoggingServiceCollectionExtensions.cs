using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace PlantillaMicroServicio.Infrastructure.Logging
{
  
    public static class LoggingServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
            services.AddSingleton<ILoggerService, SerilogLoggerService>();

            return services;
        }
    }
}