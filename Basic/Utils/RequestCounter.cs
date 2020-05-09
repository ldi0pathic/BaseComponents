using System;
using System.Collections.Generic;
using System.Linq;

using Extensions;

namespace Utils
{
    public class RequestCounter
    {
        private static readonly object m_lock = new object();

        private static volatile RequestCounter instance = null;
        public static RequestCounter getInstance(long MinuteLimit, long HourLimit = 0, long dayLimit = 0)
        {
            // DoubleLock 
            if (instance == null)
                lock (m_lock) { if (instance == null) instance = new RequestCounter(MinuteLimit, HourLimit, dayLimit); }

            return instance;
        }

        private RequestCounter(long MinuteLimit , long HourLimit = 0, long dayLimit = 0)
        {
            RequestLimitDay = dayLimit;
            RequestLimitMinute = MinuteLimit;
            RequestLimitHour = HourLimit;

            if (RequestLimitHour <= 0)
                RequestLimitHour = (long)(MinuteLimit * 59.5);

            if(RequestLimitDay <= 0)
                RequestLimitDay = (long)(RequestLimitHour * 23.5);

            RequestCount = new List<DateTime>();
        }

        private static long RequestLimitDay;
        private static long RequestLimitHour;
        private static long RequestLimitMinute;
        private static List<DateTime> RequestCount { get; set; }

        public event EventHandler IsLimitEvent;

        public void CheckRequestLimit(EventArgs e)
        {
            if (instance == null || RequestCount == null)
                throw new Exception("Singelton class needs to be initialized!");

            while (RequestCount.Count(t => t.IsBetween(DateTime.Now.AddDays(-1), DateTime.Now)) >= RequestLimitDay||
                   RequestCount.Count(t => t.IsBetween(DateTime.Now.AddHours(-1), DateTime.Now)) >= RequestLimitHour ||
                   RequestCount.Count(t => t.IsBetween(DateTime.Now.AddMinutes(-1), DateTime.Now)) >= RequestLimitMinute)
            {
                IsLimitEvent?.Invoke(this, e);
                Sleeper.Sleep(Random.GetRandomIntBetween(200, 1000), Sleeper.SleepType.z);
            }

            RequestCount.Add(DateTime.Now);

            //cleanup ;)
            RequestCount.RemoveAll(d => d.IsSmallerThan(DateTime.Now.AddDays(-1).AddMinutes(-10)));
        }
    }
}
