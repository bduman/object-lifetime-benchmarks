using System.Threading;

namespace DateTimeConvertBenchmarks
{
    public class ThreadedDateTimeParser
    {
        public static ThreadLocal<DateTimeParser> Parser = new ThreadLocal<DateTimeParser>(() => new DateTimeParser());
    }
}
