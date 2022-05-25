using System;

namespace Consuela.Lib.Services
{
    public class DateTimeService
        : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
