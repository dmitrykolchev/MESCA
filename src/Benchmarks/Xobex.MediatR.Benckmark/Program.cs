// <copyright file="Program.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

namespace Xobex.MediatR.Benckmark;

internal class Program
{
    static void Main(string[] args)
    {
#if RELEASE
        IConfig config = DefaultConfig.Instance;

        config = config
            .AddDiagnoser(MemoryDiagnoser.Default)
            .AddColumn(StatisticColumn.OperationsPerSecond);
        BenchmarkRunner.Run<Benchmark>(config);
#else
        Benchmark b = new Benchmark();
        b.Initialize();
        b.TestMediatorAsync().GetAwaiter().GetResult();
#endif

    }
}
