# Object lifetime management benchmarks

``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Apple M1 2.40GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.103
  [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
  Job-PUHLYN : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT

InvocationCount=50  UnrollFactor=1  

```
|                            Method |      Mean |     Error |    StdDev | Ratio | RatioSD |     Gen 0 | Gen 1 | Gen 2 |  Allocated | Completed Work Items | Lock Contentions |
|---------------------------------- |----------:|----------:|----------:|------:|--------:|----------:|------:|------:|-----------:|---------------------:|-----------------:|
|                   WithNewInstance |  3.486 ms | 0.0689 ms | 0.0645 ms |  1.14 |    0.02 | 1340.0000 |     - |     - | 2759.48 KB |               9.4400 |                - |
|                WithSharedInstance | 20.033 ms | 0.1893 ms | 0.1771 ms |  6.55 |    0.09 |         - |     - |     - |    6.46 KB |              27.6400 |         418.6400 |
|           WithThreadLocalInstance |  3.054 ms | 0.0311 ms | 0.0260 ms |  1.00 |    0.00 |         - |     - |     - |    3.38 KB |              10.5000 |                - |
|                WithPooledInstance |  8.765 ms | 0.1155 ms | 0.1080 ms |  2.87 |    0.05 |         - |     - |     - |    4.02 KB |               9.6200 |                - |
| WithThreadLocalOfPForEachInstance |  2.854 ms | 0.0536 ms | 0.0595 ms |  0.94 |    0.02 |         - |     - |     - |    4.22 KB |              10.2200 |                - |
