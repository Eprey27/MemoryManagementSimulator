using MemorySimulatorBackend.Models;
using MemorySimulatorBackend.Services;
using Microsoft.OpenApi.Models;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

namespace MMU.Simulator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar las políticas (pueden ser configurables)
            var totalFrames = 100;
            var replacementPolicy = ReplacementPolicy.LRU;
            var fetchPolicy = FetchPolicy.DemandPaging;
            var placementPolicy = PlacementPolicy.FirstFit;

            // Registrar el servicio de gestión de memoria como singleton
            builder.Services.AddSingleton(new MemoryManagementService(totalFrames, replacementPolicy, fetchPolicy, placementPolicy));

            // Add services to the container.

            builder.Services.AddControllers();           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MMU Simulator API",
                    Version = "v1",
                    Description = "API Memory Management Unity Simulator",
                    Contact = new OpenApiContact
                    {
                        Name = "EPR",
                        Email = "eprey27@gmail.com"
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
