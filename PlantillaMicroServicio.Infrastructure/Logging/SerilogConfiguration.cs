using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

namespace PlantillaMicroServicio.Infrastructure.Logging
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(IConfiguration configuration, string environment)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId();

           
            if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
            {
                ConfigureDevelopmentLogging(loggerConfiguration, configuration);
            }
            else
            {
                ConfigureProductionLogging(loggerConfiguration, configuration);
            }

            
            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static void ConfigureDevelopmentLogging(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
        {
            loggerConfiguration
                .MinimumLevel.Information() // Solo Information y superior
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Solo warnings y errores de Microsoft
                .MinimumLevel.Override("System", LogEventLevel.Warning) // Solo warnings y errores de System
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error) // Solo errores de ASP.NET
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning) // Solo warnings y errores de EF
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Error) // Solo errores de routing
                .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Error) // Solo errores de archivos estáticos
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Error) // Solo errores de hosting
                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.File(
                    path: configuration["Serilog:FilePath"] ?? "logs/app-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    formatter: new CompactJsonFormatter(),
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1));
        }

        private static void ConfigureProductionLogging(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
        {
            loggerConfiguration
                .MinimumLevel.Warning() 
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error) 
                .MinimumLevel.Override("System", LogEventLevel.Error) 
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error) 
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error) 
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Error) 
                .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Error) 
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Error)
                .WriteTo.Console(new JsonFormatter())
                .WriteTo.File(
                    path: configuration["Serilog:FilePath"] ?? "logs/app-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    fileSizeLimitBytes: 10485760, // 10MB
                    rollOnFileSizeLimit: true,
                    formatter: new JsonFormatter(),
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(5));

          
            var seqUrl = configuration["Serilog:SeqUrl"];
            if (!string.IsNullOrEmpty(seqUrl))
            {
                loggerConfiguration.WriteTo.Seq(seqUrl);
            }
        }
    }
}