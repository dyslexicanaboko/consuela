using Consuela.Entity;
using System;
using System.Threading.Tasks;

namespace Consuela.Lib.Services.Dummy
{
    /// <summary>
    /// Over-simplistic scheduling service. Not sophisticated enough to know if a schedule has run or not already.
    /// Avoiding having persistent storage, so this will have to do for now.
    /// </summary>
    public class SchedulingServiceDummy
        : ISchedulingService
    {
        public SchedulingServiceDummy(IProfile profile)
        {

        }

        public DateTime GetEndDate()
        {
            //This doesn't actually matter in this context
            return DateTime.Now;
        }

        public DateTime? GetLastExecution()
        {
            throw new NotImplementedException();
        }

        public async Task ScheduleAction(Action method)
        { 
            method();

            await Task.CompletedTask;
        }

        public bool TryExecuteAction()
        {
            throw new NotImplementedException();
        }
    }
}
