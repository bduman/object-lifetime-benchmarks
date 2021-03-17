using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.ObjectPool;

namespace DateTimeConvertBenchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class Benchmarks
    {
        private List<string> _listOfString;

        [GlobalSetup]
        public void Setup()
        {
            this._listOfString = new List<string>();
            var years = Enumerable.Range(2000, 2100);
            var days = Enumerable.Range(1, 28);

            foreach (var dates in years.Select(year => days.Select(day => "01/" + day + "/" + year)))
            {
                this._listOfString.AddRange(dates);
            }
        }

        [Benchmark]
        public int[] WithNewInstance()
        {
            int[] buffer = new int[this._listOfString.Count];
            var i = 0;

            Parallel.ForEach(this._listOfString, str =>
            {
                var parser = new DateTimeParser();
                var dateTime = parser.Parse(str);
                buffer[i++] = dateTime.Day;
            });

            return buffer;
        }

        [Benchmark]
        public int[] WithSharedInstance()
        {
            int[] buffer = new int[this._listOfString.Count];
            var i = 0;

            var parser = new DateTimeParser();

            Parallel.ForEach(this._listOfString, str =>
            {
                var dateTime = parser.Parse(str);
                buffer[i++] = dateTime.Day;
            });

            return buffer;
        }

        [Benchmark]
        public int[] WithThreadLocalInstance()
        {
            int[] buffer = new int[this._listOfString.Count];
            var i = 0;

            Parallel.ForEach(this._listOfString, str =>
            {
                var dateTime = ThreadedDateTimeParser.Parser.Value.Parse(str);
                buffer[i++] = dateTime.Day;
            });

            return buffer;
        }

        [Benchmark]
        public int[] WithPooledInstance()
        {
            var provider = new DefaultObjectPoolProvider();
            var pool = provider.Create(new DefaultPooledObjectPolicy<DateTimeParser>());

            int[] buffer = new int[this._listOfString.Count];
            var i = 0;

            Parallel.ForEach(this._listOfString, str =>
            {
                var parser = pool.Get();

                var dateTime = parser.Parse(str);
                buffer[i++] = dateTime.Day;

                pool.Return(parser);
            });

            return buffer;
        }
    }
}