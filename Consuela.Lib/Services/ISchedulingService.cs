using System;

namespace Consuela.Lib.Services
{
    public interface ISchedulingService
    {
        void ScheduleAction(Action method);
    }
}