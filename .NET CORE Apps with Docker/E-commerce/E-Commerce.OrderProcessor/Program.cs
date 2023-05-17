using Serilog.Events;
using Serilog;

namespace E_Commerce.OrderProcessor
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

                IHost host = Host.CreateDefaultBuilder(args)
                   .ConfigureServices(services =>
                   {
                       services.AddHostedService<Worker>();
                   })
                   .UseSerilog()
                   .Build();

                host.Run();
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