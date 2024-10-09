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

            // Registrar el servicio de gestión de memoria
            builder.Services.AddSingleton<IMemoryManagementService, MemoryManagementService>();

            // Agregar servicios al contenedor
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

            // Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configurar el pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection(); // Puedes comentar esto para desactivar HTTPS

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
