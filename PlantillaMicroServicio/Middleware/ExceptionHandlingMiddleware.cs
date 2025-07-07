using FluentValidation;
using PlantillaMicroServicio.Models.Api;
using PlantillaMicroServicio.Infrastructure.Logging;
using System.Net;
using System.Text.Json;

namespace PlantillaMicroServicio.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly bool _isDevelopment;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerService loggerService, IWebHostEnvironment environment)
        {
            _next = next;
            _loggerService = loggerService;
            _isDevelopment = environment.IsDevelopment();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message, errorTecnico, logLevel) = GetExceptionDetails(exception);

            // Logging con nivel apropiado
            LogException(exception, context, logLevel);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            SetCorsHeaders(context);

            var respuesta = RespuestaServicio<object>.Fallo(
                mensaje: message,
                errorTecnico: _isDevelopment ? errorTecnico : null
            );

            // Agregar errores de validación si es ValidationException
            if (exception is ValidationException validationEx)
            {
                respuesta.Errores = validationEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .SelectMany(g => g.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
                    .ToList();
            }

            var jsonResponse = JsonSerializer.Serialize(respuesta, _jsonOptions);
            await context.Response.WriteAsync(jsonResponse);
        }

        private (int statusCode, string message, string errorTecnico, LogLevel logLevel) GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                ValidationException valEx =>
                    ((int)HttpStatusCode.BadRequest, "Uno o más errores de validación ocurrieron.", valEx.Message, LogLevel.Warning),

                ArgumentException argEx =>
                    ((int)HttpStatusCode.BadRequest, "Parámetro inválido", argEx.Message, LogLevel.Warning),

                InvalidOperationException invOpEx =>
                    ((int)HttpStatusCode.BadRequest, "Operación inválida", invOpEx.Message, LogLevel.Warning),

                UnauthorizedAccessException unauthEx =>
                    ((int)HttpStatusCode.Unauthorized, "Acceso no autorizado", unauthEx.Message, LogLevel.Warning),

                KeyNotFoundException keyEx =>
                    ((int)HttpStatusCode.NotFound, "Recurso no encontrado", keyEx.Message, LogLevel.Information),

                TimeoutException timeoutEx =>
                    ((int)HttpStatusCode.RequestTimeout, "Tiempo de espera agotado", timeoutEx.Message, LogLevel.Warning),

                _ => ((int)HttpStatusCode.InternalServerError,
                      "Ocurrió un error interno del servidor",
                      exception.ToString(),
                      LogLevel.Error)
            };
        }

        private void LogException(Exception exception, HttpContext context, LogLevel logLevel)
        {
            var contextInfo = new
            {
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                User = context.User.Identity?.IsAuthenticated == true
                    ? context.User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value
                    : "No autenticado",
                IP = GetClientIP(context),
                Timestamp = DateTime.UtcNow
            };

            // Usar nuestro sistema de logging profesional
            _loggerService.LogError($"Error en {context.Request.Method} {context.Request.Path}: {exception.Message}",
                exception, contextInfo);
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

        private static void SetCorsHeaders(HttpContext context)
        {
            if (context.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
            {
                return;
            }

            context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization";
            context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}