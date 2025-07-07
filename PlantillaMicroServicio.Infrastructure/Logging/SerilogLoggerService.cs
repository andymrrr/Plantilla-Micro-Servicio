using Serilog;
using Serilog.Events;
using System.Text.Json;

namespace PlantillaMicroServicio.Infrastructure.Logging
{
    /// <summary>
    /// Implementación de logging usando Serilog con logging estructurado
    /// </summary>
    public class SerilogLoggerService : ILoggerService
    {
        private readonly Serilog.ILogger _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public SerilogLoggerService(Serilog.ILogger logger)
        {
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public void LogInformation(string message, object? context = null)
        {
            if (context != null)
            {
                _logger.Information(message + " {@Context}", context);
            }
            else
            {
                _logger.Information(message);
            }
        }

        public void LogWarning(string message, object? context = null)
        {
            if (context != null)
            {
                _logger.Warning(message + " {@Context}", context);
            }
            else
            {
                _logger.Warning(message);
            }
        }

        public void LogError(string message, Exception? exception = null, object? context = null)
        {
            if (exception != null && context != null)
            {
                _logger.Error(exception, message + " {@Context}", context);
            }
            else if (exception != null)
            {
                _logger.Error(exception, message);
            }
            else if (context != null)
            {
                _logger.Error(message + " {@Context}", context);
            }
            else
            {
                _logger.Error(message);
            }
        }

        public void LogDebug(string message, object? context = null)
        {
            if (context != null)
            {
                _logger.Debug(message + " {@Context}", context);
            }
            else
            {
                _logger.Debug(message);
            }
        }

        public void LogCritical(string message, Exception? exception = null, object? context = null)
        {
            if (exception != null && context != null)
            {
                _logger.Fatal(exception, message + " {@Context}", context);
            }
            else if (exception != null)
            {
                _logger.Fatal(exception, message);
            }
            else if (context != null)
            {
                _logger.Fatal(message + " {@Context}", context);
            }
            else
            {
                _logger.Fatal(message);
            }
        }

        public void LogRequest(string method, string path, object? parameters = null, object? response = null, TimeSpan? duration = null)
        {
            var requestData = new
            {
                Method = method,
                Path = path,
                Parameters = parameters,
                Response = response,
                Duration = duration?.TotalMilliseconds
            };

            _logger.Information("HTTP Request: {@RequestData}", requestData);
        }

        public void LogBusinessOperation(string operation, object? data = null, bool success = true)
        {
            var operationData = new
            {
                Operation = operation,
                Data = data,
                Success = success,
                Timestamp = DateTime.UtcNow
            };

            if (success)
            {
                _logger.Information("Business Operation: {@OperationData}", operationData);
            }
            else
            {
                _logger.Warning("Business Operation Failed: {@OperationData}", operationData);
            }
        }
    }
}