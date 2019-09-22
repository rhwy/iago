namespace Iago
{
    using System;
    
    public class ApplicationTime
    {
        private static Func<DateTime> timeNow = ()=> DateTime.Now;

        public static void SetTimeProvider(Func<DateTime> time)
        {
            timeNow = time;
        }

        public static DateTime Now
        {
            get
            {
                return timeNow();
            }
        }

        private static DateTime start = timeNow();

        public static TimeSpan StopTime
        {
            get
            {
                return (timeNow() - start);
            }
        }

        private static Func<TimeSpan,string> timerFormat = (s) =>
        {

            return $"[{s.Seconds:00}:{s.Milliseconds:000}]";
        };

        public static void SetDefaultFormat(Func<TimeSpan,string> format)
        {
            timerFormat = format;
        }

        public static string FormatTimer(TimeSpan span)
        {
            return timerFormat(span);
        }
    }

}
