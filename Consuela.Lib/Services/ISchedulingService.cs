using System;
using System.Threading.Tasks;

namespace Consuela.Lib.Services
{
    public interface ISchedulingService
    {
        Task ScheduleAction(Action method);

        bool TryExecuteAction();

        DateTime GetEndDate();

        DateTime? GetLastExecution();
    }
}