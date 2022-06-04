using System;

namespace Consuela.Lib.Services.Dummy
{
    public class DateTimeServiceDummy
        : IDateTimeService
    {
        public DateTime Now => _dateTime;


        private DateTime _dateTime;

        public void SetDateTime(DateTime dateTime) => _dateTime = dateTime;
    }
}
