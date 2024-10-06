using MemorySimulatorBackend.Models;
using MemorySimulatorBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar las políticas (pueden ser configurables)
var totalFrames = 100;
var replacementPolicy = ReplacementPolicy.LRU;
var fetchPolicy = FetchPolicy.DemandPaging;
var placementPolicy = PlacementPolicy.FirstFit;

// Registrar el servicio de gestión de memoria como singleton
builder.Services.AddSingleton(new MemoryManagementService(totalFrames, replacementPolicy, fetchPolicy, placementPolicy));

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
