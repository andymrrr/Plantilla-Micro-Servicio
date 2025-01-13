using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlantillaMicroServicio.Dal.Contexto;
using PlantillaMicroServicio.Dal.Nucleo.Interfaces;
using PlantillaMicroServicio.Dal.Nucleo.Repositorios;
using PlantillaMicroServicio.Dal.Seguridad;
using System;
using System.Text;

namespace PlantillaMicroServicio.Dal
{
    public static class Extension
    {
        public static IServiceCollection AddServicioDatos(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddHttpContextAccessor();
            
            //servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
            //    options.UseSqlServer(configuracion.GetConnectionString("PlantillaMicroServicio"),
            //        sqlOptions => sqlOptions.MigrationsAssembly(typeof(ContextPlantillaMicroServicio).Assembly.FullName)));
            
            servicio.AddDbContext<ContextPlantillaMicroServicio>(options =>
                    options.UseInMemoryDatabase("PlantillaMicroServicio"));

            servicio.AddScoped<IPlantillaMicroServicioUoW, PlantillaMicroServicioUoW>();

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
        public static IServiceCollection AddCors(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddCors(opcion =>
            {
                opcion.AddPolicy("CorsPoolicy", constructor => constructor.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            return servicios;
        }
    }
}
