using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServicioJobs.Aplication.Model;
using System.Net;
using System.Text.Json;

namespace PlantillaMicroServicio.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly bool _isDevelopment;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
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
            var logMessage = "Error en {Method} {Path}: {Message}";
            var logParams = new object[] { context.Request.Method, context.Request.Path, exception.Message };

            switch (logLevel)
            {
                case LogLevel.Information:
                    _logger.LogInformation(exception, logMessage, logParams);
                    break;
                case LogLevel.Warning:
                    _logger.LogWarning(exception, logMessage, logParams);
                    break;
                case LogLevel.Error:
                    _logger.LogError(exception, logMessage, logParams);
                    break;
                default:
                    _logger.LogError(exception, logMessage, logParams);
                    break;
            }
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