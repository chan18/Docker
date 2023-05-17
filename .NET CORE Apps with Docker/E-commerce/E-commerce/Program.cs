using E_commerce.Domain;
using E_commerce.Interfaces;
using E_commerce.Middleware;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace E_commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var name = typeof(Program).Assembly.GetName().Name;

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName() // from Serilog.Enrichers.Environment
                        .Enrich.WithProperty("Assembly", name)
                        // available sinks: https://github.com/serilog/serilog/wiki/Provided-Sinks
                        // Seq: https://datalust.co/seq
                        // Seq with Docker: https://docs.datalust.co/docs/getting-started-with-docker
                        .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
                        .WriteTo.Console()
                        .CreateLogger();

            try
            {
                Log.Information("Starting web host");

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddScoped<IProductLogic, ProductLogic>();
                builder.Services.AddScoped<IQuickOrderLogic, QuickOrderLogic>();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce.Api v1", Version = "v1" });
                });

                builder.Host.UseSerilog();

                var app = builder.Build();

                app.UseMiddleware<CustomExceptionHandlingMiddleware>();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce.Api v1"));
                }

                app.UseCustomRequestLogging();

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }            
        }
    }
}