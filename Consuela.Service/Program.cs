using Consuela.Service.Startup;
using Serilog;

namespace Consuela.Service
{
    //https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/host-and-deploy/windows-service/samples/3.x/BackgroundWorkerServiceSample
    public class Program
    {
        //https://github.com/serilog/serilog-extensions-hosting/blob/dev/samples/SimpleServiceSample/Program.cs
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            try
            {
                //WebApplication is the host
                var builder = WebApplication.CreateBuilder(args);

                //Hang a Windows Service off of the WebApplication host
                builder.Host.UseWindowsService();

                //Want to be able to reload the file when it is changed
                builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                //Configure the container
                ContainerConfig.Configure(builder.Host);

                //Build after all configuration happens first
                var app = builder.Build();

                WebAppConfig.Configure(app);

                app.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}