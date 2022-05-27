﻿using Consuela.Entity;
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
}
