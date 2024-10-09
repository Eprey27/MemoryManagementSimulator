// Program.cs
using MMU.Simulator.Api.Models;
using MMU.Simulator.Api.Services;
using Microsoft.OpenApi.Models;

namespace MMU.Simulator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registrar el servicio de gestión de memoria como singleton usando la interfaz
            builder.Services.AddSingleton<IMemoryManagementService, MemoryManagementService>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MMU Simulator API",
                    Version = "v1",
                    Description = "API Memory Management Unit Simulator",
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
