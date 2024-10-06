using MemorySimulatorBackend.Models;
using MemorySimulatorBackend.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar las políticas (pueden ser configurables)
var totalFrames = 100;
var replacementPolicy = ReplacementPolicy.LRU;
var fetchPolicy = FetchPolicy.DemandPaging;
var placementPolicy = PlacementPolicy.FirstFit;

// Registrar el servicio de gestión de memoria como singleton
builder.Services.AddSingleton(new MemoryManagementService(totalFrames, replacementPolicy, fetchPolicy, placementPolicy));

// Añadir servicios de controladores
builder.Services.AddControllers();

// Añadir configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Memory Simulator API",
        Version = "v1",
        Description = "API para simular gestión de memoria.",
        Contact = new OpenApiContact
        {
            Name = "Emilio",
            Email = "emilio@ejemplo.com"
        }
    });
});

var app = builder.Build();

// Configuración del middleware de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Memory Simulator API V1");
        options.RoutePrefix = string.Empty; // Para que Swagger esté en la raíz
    });
}

// Middleware estándar para routing y controladores
app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Iniciar la aplicación
app.Run();
