using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using PlantillaMicroServicio.Aplication;
using PlantillaMicroServicio.Dal;
using PlantillaMicroServicio.Extensions;
using PlantillaMicroServicio.Infrastructure.Extensions;
using PlantillaMicroServicio.Infrastructure.Logging;
using PlantillaMicroServicio.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
SerilogConfiguration.ConfigureSerilog(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.AddControllers(opcion =>
{
    var politica = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opcion.Filters.Add(new AuthorizeFilter(politica));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJWT(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddServicioDatos(builder.Configuration);
builder.Services.AddServicioAplicacion(builder.Configuration);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseRequestLogging();
app.UseErrorLogging(); // Middleware personalizado para errores importantes
app.UseExceptionHandling();
app.MapControllers();

// Configurar Serilog para el cierre de la aplicación
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();
