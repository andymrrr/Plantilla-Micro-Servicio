using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text;

namespace PlantillaMicroServicio.Infrastructure.Logging
{
    /// <summary>
    /// Middleware para logging de requests HTTP con métricas de rendimiento
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var originalBodyStream = context.Response.Body;

            try
            {
                // Log del request
                var requestInfo = await LogRequest(context);

                // Ejecutar el pipeline
                using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;

                await _next(context);

                stopwatch.Stop();

                // Log del response
                await LogResponse(context, memoryStream, stopwatch.Elapsed, requestInfo);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _loggerService.LogError("Error procesando request", ex, new
                {
                    Path = context.Request.Path,
                    Method = context.Request.Method,
                    Duration = stopwatch.Elapsed.TotalMilliseconds
                });
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task<object> LogRequest(HttpContext context)
        {
            var requestInfo = new
            {
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                Headers = GetFilteredHeaders(context.Request.Headers),
                Body = await GetRequestBody(context.Request),
                Timestamp = DateTime.UtcNow
            };

            _loggerService.LogInformation("HTTP Request iniciado", requestInfo);
            return requestInfo;
        }

        private async Task LogResponse(HttpContext context, MemoryStream responseBody, TimeSpan duration, object requestInfo)
        {
            responseBody.Position = 0;
            var responseContent = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Position = 0;
            await responseBody.CopyToAsync(context.Response.Body);

            var responseInfo = new
            {
                StatusCode = context.Response.StatusCode,
                Duration = duration.TotalMilliseconds,
                ResponseSize = responseContent.Length,
                Headers = GetFilteredHeaders(context.Response.Headers),
                Body = GetFilteredResponseBody(responseContent, context.Response.ContentType)
            };

            _loggerService.LogRequest(
                context.Request.Method,
                context.Request.Path,
                requestInfo,
                responseInfo,
                duration);
        }

        private static Dictionary<string, string> GetFilteredHeaders(IHeaderDictionary headers)
        {
            var sensitiveHeaders = new[] { "authorization", "cookie", "x-api-key" };
            return headers
                .Where(h => !sensitiveHeaders.Contains(h.Key.ToLowerInvariant()))
                .ToDictionary(h => h.Key, h => h.Value.ToString());
        }

        private static async Task<string> GetRequestBody(HttpRequest request)
        {
            if (!request.Body.CanSeek)
                return string.Empty;

            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            return body;
        }

        private static object GetFilteredResponseBody(string content, string? contentType)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;

            // Si es JSON, intentar parsearlo para logging estructurado
            if (contentType?.Contains("application/json") == true)
            {
                try
                {
                    return System.Text.Json.JsonSerializer.Deserialize<object>(content) ?? content;
                }
                catch
                {
                    return content;
                }
            }

            // Para otros tipos, limitar el tamaño
            return content.Length > 1000 ? content[..1000] + "..." : content;
        }
    }
}