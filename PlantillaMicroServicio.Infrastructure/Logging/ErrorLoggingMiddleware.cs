using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace PlantillaMicroServicio.Infrastructure.Logging
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public ErrorLoggingMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogErrorAsync(context, ex);
                throw;
            }
        }

        private void LogErrorAsync(HttpContext context, Exception exception)
        {
            var errorInfo = new
            {
                ErrorType = exception.GetType().Name,
                Message = exception.Message,
                StackTrace = GetCleanStackTrace(exception),

                Request = new
                {
                    Method = context.Request.Method,
                    Path = context.Request.Path,
                    QueryString = context.Request.QueryString.ToString(),
                    UserAgent = context.Request.Headers["User-Agent"].ToString(),
                    IP = GetClientIP(context)
                },

                User = context.User.Identity?.IsAuthenticated == true
                        ? context.User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value
                    : "No autenticado",

                Timestamp = DateTime.UtcNow
            };

            _loggerService.LogError("ERROR IMPORTANTE DETECTADO", exception, errorInfo);
        }

        private static string GetCleanStackTrace(Exception exception)
        {
            if (exception.StackTrace == null) return "No stack trace disponible";

            var lines = exception.StackTrace.Split('\n')
                .Where(line =>
                    !line.Contains("Microsoft.AspNetCore") &&
                    !line.Contains("System.Threading") &&
                    !line.Contains("System.Runtime") &&
                    !line.Contains("--- End of stack trace") &&
                    line.Trim().Length > 0)
                .Take(10)
                .ToArray();

            return string.Join("\n", lines);
        }

        private static string GetClientIP(HttpContext context)
        {
            var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedHeader))
            {
                return forwardedHeader.Split(',')[0].Trim();
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? "IP desconocida";
        }
    }

    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}