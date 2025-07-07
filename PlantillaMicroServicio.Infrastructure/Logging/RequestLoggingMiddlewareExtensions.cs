using Microsoft.AspNetCore.Builder;

namespace PlantillaMicroServicio.Infrastructure.Logging
{
    /// <summary>
    /// Extensiones para el middleware de logging de requests
    /// </summary>
    public static class RequestLoggingMiddlewareExtensions
    {
        /// <summary>
        /// Agrega el middleware de logging de requests HTTP
        /// </summary>
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}