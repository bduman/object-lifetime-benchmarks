using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.ObjectPool;

namespace DateTimeConvertBenchmarks
{
    [SimpleJob(invocationCount: 50)]
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
        public void WithNewInstance()
        {
            Parallel.ForEach(this._listOfString, str =>
            {
                var parser = new DateTimeParser();
                var dateTime = parser.Parse(str);
            });
        }

        [Benchmark]
        public void WithSharedInstance()
        {
            var parser = new DateTimeParser();

            Parallel.ForEach(this._listOfString, str =>
            {
                var dateTime = parser.Parse(str);
            });
        }

        [Benchmark(Baseline = true)]
        public void WithThreadLocalInstance()
        {
            Parallel.ForEach(this._listOfString, str =>
            {
                var dateTime = ThreadedDateTimeParser.Parser.Value.Parse(str);
            });
        }

        [Benchmark]
        public void WithPooledInstance()
        {
            var provider = new DefaultObjectPoolProvider();
            var pool = provider.Create(new DefaultPooledObjectPolicy<DateTimeParser>());

            Parallel.ForEach(this._listOfString, str =>
            {
                var parser = pool.Get();
                var dateTime = parser.Parse(str);
                pool.Return(parser);
            });
        }

        [Benchmark]
        public void WithThreadLocalOfPForEachInstance()
        {
            Parallel.ForEach(this._listOfString,
            () => new DateTimeParser(),
            (str, loopstate, index, parser) =>
            {
                var dateTime = parser.Parse(str);
                return parser;
            },
            (parser) =>
            {
                // do nothing
            });
        }
    }
}