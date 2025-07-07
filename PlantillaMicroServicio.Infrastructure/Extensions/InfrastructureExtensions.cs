using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroServicio.Infrastructure.Authentication;
using PlantillaMicroServicio.Models.Configuracion;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace PlantillaMicroServicio.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddHttpContextAccessor();
            servicios.AddJWT(configuracion);
            servicios.AddCors(configuracion);
            servicios.AddAuthenticationServices();

            return servicios;
        }

        private static void AddJWT(this IServiceCollection servicio, IConfiguration configuracion)
        {
            var jwtConfig = configuracion.GetSection("ConfiguracionJwt").Get<ConfiguracionJWT>();

            if (string.IsNullOrEmpty(jwtConfig?.Llave))
            {
                throw new InvalidOperationException("La configuración JWT no es válida. Verifique 'ConfiguracionJwt:Llave' en appsettings.json");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Llave));

            servicio.Configure<ConfiguracionJWT>(configuracion.GetSection("ConfiguracionJwt"));

            servicio.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opcion =>
                {
                    opcion.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = !string.IsNullOrEmpty(jwtConfig.Asunto),
                        ValidIssuer = jwtConfig.Asunto,
                        ValidateAudience = !string.IsNullOrEmpty(jwtConfig.Audiencia),
                        ValidAudience = jwtConfig.Audiencia,
                        ClockSkew = TimeSpan.FromMinutes(2),
                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };

                    opcion.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Error de autenticación JWT: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var claims = context.Principal?.Claims;
                            if (claims != null)
                            {
                                Console.WriteLine($"Token validado para usuario: {claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
                            }
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new { error = "Token de acceso requerido" });
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
        }

        public static void AddCors(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddCors(opcion =>
            {
                opcion.AddPolicy("CorsPolicy", constructor => constructor.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        private static void AddAuthenticationServices(this IServiceCollection servicios)
        {
            servicios.AddScoped<IJwtService, JwtService>();
        }
    }
}