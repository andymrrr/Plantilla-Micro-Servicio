using PlantillaMicroServicio.Aplicacion;
using PlantillaMicroServicio.Dal;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de logging
ConfiguracionLogger.AgregarLogging(builder);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServicioDatos(builder.Configuration);
builder.Services.AddServicioAplicacion(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
