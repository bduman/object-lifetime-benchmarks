# Object lifetime management benchmarks

``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Apple M1 2.40GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.103
  [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
  Job-XLBCUE : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT

InvocationCount=50  UnrollFactor=1  

```
|                            Method |       Categories |      Mean |     Error |    StdDev | Ratio | RatioSD | Completed Work Items | Lock Contentions |     Gen 0 |     Gen 1 |    Gen 2 |   Allocated |
|---------------------------------- |----------------- |----------:|----------:|----------:|------:|--------:|---------------------:|-----------------:|----------:|----------:|---------:|------------:|
|           WithThreadLocalInstance | Parallel.ForEach |  3.102 ms | 0.0489 ms | 0.0409 ms |  1.00 |    0.00 |               9.0000 |                - |         - |         - |        - |     3.15 KB |
| WithThreadLocalOfPForEachInstance | Parallel.ForEach |  3.899 ms | 0.0667 ms | 0.0624 ms |  1.26 |    0.02 |              10.5600 |                - |         - |         - |        - |     4.29 KB |
|                                   |                  |           |           |           |       |         |                      |                  |           |           |          |             |
|       TaskWithThreadLocalInstance |         Task.Run | 26.858 ms | 0.5610 ms | 1.6540 ms |  1.00 |    0.00 |           58800.0400 |                - | 2860.0000 | 1160.0000 | 500.0000 | 10212.76 KB |