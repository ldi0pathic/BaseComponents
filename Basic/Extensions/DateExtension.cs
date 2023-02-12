using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class DateExtension
    {
        public static DateTime ToDateTime(this int UnixTimestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(UnixTimestamp).ToLocalTime();
            return dateTime;
        }
    }
}
