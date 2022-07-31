using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using Consuela.Lib.Services.ProfileManagement;
using System;
using System.Threading.Tasks;

namespace Consuela.Lib.Services
{
    /// <summary>
    /// Over-simplistic scheduling service. Not sophisticated enough to know if a schedule has run or not already.
    /// Avoiding having persistent storage, so this will have to do for now.
    /// </summary>
    public class SchedulingService 
        : ISchedulingService
    {
        private const int OneDay = 86400000; //Milliseconds
        private readonly IProfileSaver _profileSaver;
        private readonly IDateTimeService _dateTimeService;
        private DateTime _endDate;
        private object _lock = new object();

        private IProfile Profile => _profileSaver.Get();

        public SchedulingService(IProfileSaver profileSaver, IDateTimeService dateTimeService)
        {
            _profileSaver = profileSaver;
            _profileSaver.Changed += ProfileChanged;

            _dateTimeService = dateTimeService;
        }

        //If the profile changes the end date needs to be updated
        private void ProfileChanged(object sender, EventArgs e)
        {
            //If the profile is changing, don't allow the service to execute the clean up method
            lock (_lock)
            {
                SetEndDate(); 
            }
        }

        public DateTime GetEndDate() => _endDate;

        private void SetEndDate() => _endDate = CalculateEndDate(Profile.Delete.Schedule);

        public async Task ScheduleAction(Action method)
        {
            SetEndDate();

            //Interval is going to be set to one day so that each day the timer will check if today is the target date
            //If not, it waits another day
            //If it is, then the action is performed and the timer is stopped
            var keepWaiting = true;

            while (keepWaiting)
            {
                await Task.Delay(OneDay);

                if (!IsElapsed()) continue;

                keepWaiting = false;

                //If the clean up method is being executed, do not allow the profile change to update anything
                lock (_lock)
                {
                    method(); 
                }
            }
        }

        private bool IsElapsed()
        {
            var isElapsed = _dateTimeService.Now.Date == _endDate.Date;

            return isElapsed;
        }

        private DateTime CalculateEndDate(Schedule schedule)
        {
            var dtmNow = _dateTimeService.Now.Date;

            switch (schedule.Frequency)
            {
                case ScheduleFrequency.Daily:
                    //Will have to default to tomorrow relative to now
                    var tomorrow = dtmNow.AddDays(1);

                    return tomorrow;
                case ScheduleFrequency.Weekly:
                    //Starts at the beginning of each week, so the next Sunday
                    var dtm = dtmNow.Date;

                    //Edge case - today is Sunday
                    if (dtm.DayOfWeek == DayOfWeek.Sunday) dtm = dtm.AddDays(1);

                    while (dtm.DayOfWeek != DayOfWeek.Sunday)
                    {
                        dtm = dtm.AddDays(1);
                    }

                    return dtm;
                case ScheduleFrequency.Monthly:
                default:
                    //Go to next month
                    var nextMonth = dtmNow.Date.AddMonths(1);

                    return nextMonth;
            }
        }
    }
}
