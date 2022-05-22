using Consuela.Entity;
using Consuela.Lib.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Consuela.Service
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
                //This should happen forever
                _schedulingService.ScheduleAction(() => 
                {
                    //Undecided as to whether or not to use the returned clean up results for something more
                    var results = _cleanUpService.CleanUp(_profile, false);

                    //For now putting summary of results on the screen
                    _logger.LogInformation(results.ToString());
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw;
            }
            
            await Task.CompletedTask;
        }
    }
}
