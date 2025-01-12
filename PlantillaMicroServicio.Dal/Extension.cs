using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroServicio.Dal.Contexto;
using PlantillaMicroServicio.Dal.Nucleo.Interfaces;
using PlantillaMicroServicio.Dal.Nucleo.Repositorios;
using PlantillaMicroServicio.Dal.Seguridad;
using System.Text;

namespace PlantillaMicroServicio.Dal
{
    public static class Extension
    {
        public static IServiceCollection AddServicioDatos(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddHttpContextAccessor();
            // Registrar el contexto de base de datos como scoped
            servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("Artes"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));

            // Registrar la unidad de trabajo como scoped
            servicio.AddScoped<IPlantillaMicroServicioUoW, PlantillaMicroServicioUoW>();

            // Registrar el repositorio genérico como scoped
            servicio.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
            servicio.AddScoped<IAutenticacion, Autenticacion>();

            servicio.Configure<ConfiguracionJWT>(configuracion.GetSection("ConfiguracionJwt"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["ConfiguracionJwt:Llave"]!));
            servicio.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opcion =>
            {
                opcion.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            return servicio;
        }
    }
}
