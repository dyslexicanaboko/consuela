using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using System;
using System.Timers;

namespace Consuela.Lib.Services
{
    /// <summary>
    /// Over-simplistic scheduling service. Not sophisticated enough to know if a schedule has run or not already.
    /// Avoiding having persistent storage, so this will have to do for now.
    /// </summary>
    public class SchedulingService 
        : ISchedulingService
    {
        private const double OneDay = 86400000;
        private readonly IProfile _profile;
        private readonly Timer _timer;
        private Action _method;
        private DateTime _endDate;

        public SchedulingService(IProfile profile)
        {
            _profile = profile;

            _timer = new Timer();
        }

        public void ScheduleAction(Action method)
        {
            _method = method;

            _endDate = GetEndDate(_profile.Delete.Schedule);

            //Interval is going to be set to one day so that each day the timer will check if today is the target date
            //If not, it waits another day
            //If it is, then the action is performed and the timer is stopped
            _timer.Interval = OneDay; //Milliseconds
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Date != _endDate.Date) return;

            _method();
            
            _timer.Stop();
        }

        private DateTime GetEndDate(Schedule schedule)
        {
            var dtmNow = DateTime.Now.Date;

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
