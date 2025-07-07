namespace PlantillaMicroServicio.Infrastructure.Logging
{
    
    public interface ILoggerService
    {
        void LogInformation(string message, object? context = null);
        void LogWarning(string message, object? context = null);
        void LogError(string message, Exception? exception = null, object? context = null);
        void LogDebug(string message, object? context = null);
        void LogCritical(string message, Exception? exception = null, object? context = null);
        void LogRequest(string method, string path, object? parameters = null, object? response = null, TimeSpan? duration = null);
        void LogBusinessOperation(string operation, object? data = null, bool success = true);
    }
}