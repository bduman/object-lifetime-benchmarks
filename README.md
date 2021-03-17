# Object lifetime management benchmarks

``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Apple M1 2.40GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.103
  [Host]     : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
  DefaultJob : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT


```
|                  Method |      Mean |     Error |    StdDev |     Gen 0 |   Gen 1 |   Gen 2 |  Allocated | Completed Work Items | Lock Contentions |
|------------------------ |----------:|----------:|----------:|----------:|--------:|--------:|-----------:|---------------------:|-----------------:|
|         WithNewInstance |  5.258 ms | 0.0633 ms | 0.0561 ms | 1333.3333 | 66.6667 | 66.6667 | 2989.25 KB |               9.0000 |                - |
|      WithSharedInstance | 21.697 ms | 0.0843 ms | 0.1432 ms |   62.5000 | 62.5000 | 62.5000 |  239.76 KB |              45.6875 |         616.0938 |
| WithThreadLocalInstance |  4.900 ms | 0.0482 ms | 0.0403 ms |   62.5000 | 62.5000 | 62.5000 |  232.98 KB |               8.9375 |                - |
|      WithPooledInstance | 10.087 ms | 0.1967 ms | 0.2020 ms |         - |       - |       - |  233.67 KB |               9.2308 |                - |
