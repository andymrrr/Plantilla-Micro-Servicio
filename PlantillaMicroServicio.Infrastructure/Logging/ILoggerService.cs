namespace PlantillaMicroServicio.Infrastructure.Logging
{
    /// <summary>
    /// Servicio de logging estructurado para la aplicación
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Registra información general
        /// </summary>
        void LogInformation(string message, object? context = null);

        /// <summary>
        /// Registra advertencias
        /// </summary>
        void LogWarning(string message, object? context = null);

        /// <summary>
        /// Registra errores con excepción opcional
        /// </summary>
        void LogError(string message, Exception? exception = null, object? context = null);

        /// <summary>
        /// Registra información de debug
        /// </summary>
        void LogDebug(string message, object? context = null);

        /// <summary>
        /// Registra información crítica
        /// </summary>
        void LogCritical(string message, Exception? exception = null, object? context = null);

        /// <summary>
        /// Registra información de solicitudes HTTP
        /// </summary>
        void LogRequest(string method, string path, object? parameters = null, object? response = null, TimeSpan? duration = null);

        /// <summary>
        /// Registra información de operaciones de negocio
        /// </summary>
        void LogBusinessOperation(string operation, object? data = null, bool success = true);
    }
}