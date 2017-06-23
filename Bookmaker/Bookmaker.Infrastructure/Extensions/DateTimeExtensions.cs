using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimeStamp(this DateTime value)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = value.Subtract(new TimeSpan(epoch.Ticks));

            // in minutes
            return time.Ticks / 10000;
        }
    }
}
