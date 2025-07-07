using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text.Json;
using PlantillaMicroServicio.Infrastructure.Logging.Utilities;

namespace PlantillaMicroServicio.Infrastructure.Logging.Filters
{
    /// <summary>
    /// Filtro para logging automático de requests y responses
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequestLoggingFilter : ActionFilterAttribute
    {
        private readonly ILoggerService _loggerService;

        public RequestLoggingFilter(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsApiController(context)) return;

            var requestInfo = new
            {
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path,
                User = GetUsuario(context),
                Parameters = context.ActionArguments,
                Timestamp = DateTime.UtcNow
            };

            _loggerService.LogInformation("Request iniciado", requestInfo);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!IsApiController(context)) return;

            var responseInfo = new
            {
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path,
                User = GetUsuario(context),
                StatusCode = context.HttpContext.Response.StatusCode,
                Response = GetResponseData(context.Result),
                Exception = context.Exception?.Message,
                Timestamp = DateTime.UtcNow
            };

            if (context.Exception != null)
            {
                _loggerService.LogError("Error en request", context.Exception, responseInfo);
            }
            else
            {
                _loggerService.LogInformation("Request completado", responseInfo);
            }
        }

        private static bool IsApiController(FilterContext context)
        {
            return context.ActionDescriptor.EndpointMetadata.Any(meta => meta is ApiControllerAttribute);
        }

        private static string GetUsuario(FilterContext context)
        {
            return context.HttpContext.User.Identity?.IsAuthenticated == true
                ? context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario desconocido"
                : "Usuario no autenticado";
        }

        private static object GetResponseData(IActionResult? result)
        {
            return result switch
            {
                ObjectResult objResult when objResult.Value != null => objResult.Value,
                JsonResult jsonResult when jsonResult.Value != null => jsonResult.Value,
                _ => "Sin respuesta"
            };
        }
    }
}