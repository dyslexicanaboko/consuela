﻿using Consuela.Entity;
using Consuela.Lib.Services;

namespace Consuela.Service.Startup
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly ICleanUpService _cleanUpService;
        private readonly ISchedulingService _schedulingService;
        private readonly IProfile _profile;

        public WorkerService(
            ILogger<WorkerService> logger,
            ICleanUpService cleanUpService,
            ISchedulingService schedulingService,
            IProfile profile)
        {
            _logger = logger;

            _cleanUpService = cleanUpService;

            _schedulingService = schedulingService;

            _profile = profile;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("======= Consuela Clean Up Service Started =======");

            stoppingToken.Register(() =>
            {
                _logger.LogInformation("======= Consuela Clean Up Service Stopped =======");
            });

            try
            {
                //This process should happen endlessly until manually stopped
                while (true)
                {
                    await _schedulingService.ScheduleAction(() =>
                    {
                        //TODO: A cloned copy of the profile needs to be provided here so that if the management
                        //interface changes something, it doesn't affect the clean up simultaneously

                        var results = _cleanUpService.CleanUp(_profile, false);

                        //Visible to console
                        _logger.LogInformation($"Clean up summary:{Environment.NewLine}{results}");

                    });

                    //Just so the loop doesn't cause a run away train scenario, wait between runs
                    await Task.Delay(5000);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw;
            }
        }
    }
}
