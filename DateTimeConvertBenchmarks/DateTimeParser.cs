using System;

namespace DateTimeConvertBenchmarks
{
    public class DateTimeParser
    {
        private object _lock = new object();

        public DateTime Parse(string s)
        {
            lock (_lock)
            {
                return DateTime.Parse(s);
            }
        }
    }
}
