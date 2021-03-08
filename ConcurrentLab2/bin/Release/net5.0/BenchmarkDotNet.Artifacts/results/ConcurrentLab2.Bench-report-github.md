``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.20270
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|      Method |            method |       Mean |    Error |   StdDev |
|------------ |------------------ |-----------:|---------:|---------:|
| **ParallelFor** |    **MostCommonFile** |   **533.6 ms** |  **9.87 ms** |  **9.70 ms** |
|      Serial |    MostCommonFile |   352.4 ms |  6.93 ms | 12.14 ms |
|        Linq |    MostCommonFile |   741.7 ms | 14.20 ms | 16.91 ms |
|       Plinq |    MostCommonFile | 1,112.4 ms | 32.20 ms | 83.68 ms |
| **ParallelFor** | **MostFrequentWords** |   **215.8 ms** |  **4.24 ms** |  **7.65 ms** |
|      Serial | MostFrequentWords |   262.7 ms |  5.03 ms |  5.79 ms |
|        Linq | MostFrequentWords |   466.8 ms |  9.21 ms | 19.23 ms |
|       Plinq | MostFrequentWords |   289.9 ms |  5.75 ms | 12.25 ms |
| **ParallelFor** |      **MostLongWord** |   **110.7 ms** |  **2.19 ms** |  **2.15 ms** |
|      Serial |      MostLongWord |   169.5 ms |  3.20 ms |  3.14 ms |
|        Linq |      MostLongWord |   176.5 ms |  3.25 ms |  3.04 ms |
|       Plinq |      MostLongWord |   233.7 ms |  4.48 ms |  5.98 ms |
