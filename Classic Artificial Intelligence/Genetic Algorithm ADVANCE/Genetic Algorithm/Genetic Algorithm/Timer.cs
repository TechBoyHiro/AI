using System;
using System.Collections.Generic;
using System.Text;

namespace Genetic_Algorithm
{
    public static class Timer
    {
        private static DateTime time { get; set; }
        public static void Start()
        {
            time = DateTime.UtcNow;
        }

        public static string Stop()
        {
            TimeSpan timeSpan = DateTime.UtcNow - time;
            return timeSpan.TotalMilliseconds.ToString();
        }

        public static double GetTime()
        {
            return time.Millisecond;
        }
    }
}
