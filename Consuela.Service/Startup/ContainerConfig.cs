using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.ProfileManagement;
using Serilog;

namespace Consuela.Service.Startup
{
    public static class ContainerConfig
    {
        public static void Configure(IHostBuilder host)
        {
            host.ConfigureServices((hostContext, services) =>
            {
                //Web setup
                services.AddRazorPages();

                //Dependency injection below
                services.AddSingleton<IAppSettingsService, AppSettingsService>();
                
                services.AddTransient<IDateTimeService, DateTimeService>();

                services.AddSingleton<IProfileSaver, ProfileSaver>();

                services.AddTransient<IProfile, Profile>((serviceProvider) =>
                {
                    var profileSaver = serviceProvider.GetService<IProfileSaver>();

                    return (Profile)profileSaver.Get();
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
                    .WriteTo.Console();

                if (hostContext.HostingEnvironment.IsDevelopment())
                {
                    loggerConfiguration.WriteTo.Seq("http://localhost:5341");
                }
            });
        }
    }
}
