using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.ProfileManagement;
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
                ConfigureWindowsService(builder);

                //Build after all configuration happens first
                var app = builder.Build();

                app.MapGet("/", () => "Hello World!");

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

        //private static IWebHost CreateWebApplicationHost()
        //{



        //}

        public static void ConfigureWindowsService(WebApplicationBuilder builder) =>
            builder.Host.UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IDateTimeService, DateTimeService>();

                services.AddSingleton<IProfileSaver, ProfileSaver>();

                services.AddSingleton<IProfileManager, ProfileManager>((serviceProvider) =>
                {
                    var profileSaver = serviceProvider.GetService<IProfileSaver>();
                    var profileManager = profileSaver.Load();

                    return profileManager;
                });

                services.AddSingleton<IProfile, ProfileWatcher>((serviceProvider) =>
                {
                    var profileManager = serviceProvider.GetService<IProfileManager>();

                    return profileManager.Profile;
                });

                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<IAuditService, AuditService>();
                services.AddSingleton<ICleanUpService, CleanUpService>();

                //To run immediately for testing
                //services.AddSingleton<ISchedulingService, SchedulingServiceDummy>();

                //To run with proper scheduled timing
                services.AddSingleton<ISchedulingService, SchedulingService>();

                services.AddHostedService<WorkerService>();
            }).UseSerilog((hostContext, loggerConfiguration) =>
            {
                //Since this is configured here, don't do it in the JSON also otherwise the logging will appear twice
                loggerConfiguration
                    .ReadFrom.Configuration(hostContext.Configuration)
                    .WriteTo.Seq("http://localhost:5341")
                    .WriteTo.Console();
            });
    }
}