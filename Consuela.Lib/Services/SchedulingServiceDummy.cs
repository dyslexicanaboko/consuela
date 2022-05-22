using Consuela.Entity;
using System;

namespace Consuela.Lib.Services
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

        public void ScheduleAction(Action method) => method();
    }
}
