using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroServicio.Dal.Contexto;
using PlantillaMicroServicio.Dal.Enums;
using PlantillaMicroServicio.Dal.Modelos;
using PlantillaMicroServicio.Dal.Modelos.Seguridad;
using PlantillaMicroServicio.Dal.Nucleo.Interfaces;
using PlantillaMicroServicio.Dal.Nucleo.Repositorios;
using PlantillaMicroServicio.Dal.Proveedor;
using System.Text;

namespace PlantillaMicroServicio.Dal
{
    public static class Extension
    {
        public static IServiceCollection AddServicioDatos(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddHttpContextAccessor();
            servicio.AddJWT(configuracion);

            var dbProvider = configuracion["DatabaseProvider"] ?? Proveedor.sqlserver;

            switch (dbProvider.ToLower())
            {
                case Proveedor.sqlserver:
                    servicio.AddSQL(configuracion);
                    break;
                case Proveedor.postgres:
                    servicio.AddPostgres(configuracion);
                    break;
                default:
                    throw new InvalidOperationException($"Proveedor de base de datos no soportado: {dbProvider}");
            }

            servicio.AddScoped<IPlantillaMicroServicioUoW, PlantillaMicroServicioUoW>();
            servicio.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
            servicio.AddScoped<IAutenticacion, Autenticacion>();

            return servicio;
        }

        private static void AddSQL(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("PlantillaMicroServicio"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));
        }

        private static void AddPostgres(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
                options.UseNpgsql(configuracion.GetConnectionString("PlantillaMicroServicio"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));
        }

        private static void AddJWT(this IServiceCollection servicio, IConfiguration configuracion)
        {
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
        }
        public static void  AddCors(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddCors(opcion =>
            {
                opcion.AddPolicy("CorsPoolicy", constructor => constructor.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }
    }
}
