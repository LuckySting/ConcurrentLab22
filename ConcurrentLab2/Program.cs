using System;
using System.Diagnostics;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ConcurrentLab2
{
    public class Bench
    {
        private const string path = "C:\\Users\\Luckysting\\RiderProjects\\ConcurrentLab2\\ConcurrentLab2\\books";
        private string[] filePaths;
        
        [Params("MostLongWord", "MostFrequentWords", "MostCommonFile")]
        public string method;

        public Bench()
        {
            filePaths = Directory.GetFiles(path);
        }

        [Benchmark]
        public void ParallelFor()
        {
            var task = new ParallelForTextTask();
            if (method == "MostLongWord")
            {
                task.MostLongWord(filePaths);
            }
            else if (method == "MostFrequentWords")
            {
                task.MostFrequentWords(filePaths);
            }
            else if (method == "MostCommonFile")
            {
                task.MostCommonFile(filePaths);
            }
        }

        [Benchmark]
        public void Serial()
        {
            var task = new SerialTextTask();
            if (method == "MostLongWord")
            {
                task.MostLongWord(filePaths);
            }
            else if (method == "MostFrequentWords")
            {
                task.MostFrequentWords(filePaths);
            }
            else if (method == "MostCommonFile")
            {
                task.MostCommonFile(filePaths);
            }
        }
        
        [Benchmark]
        public void Linq()
        {
            var task = new LinqTextTask();
            if (method == "MostLongWord")
            {
                task.MostLongWord(filePaths);
            }
            else if (method == "MostFrequentWords")
            {
                task.MostFrequentWords(filePaths);
            }
            else if (method == "MostCommonFile")
            {
                task.MostCommonFile(filePaths);
            }
        }
        
        [Benchmark]
        public void Plinq()
        {
            var task = new PlinqTextTask();
            if (method == "MostLongWord")
            {
                task.MostLongWord(filePaths);
            }
            else if (method == "MostFrequentWords")
            {
                task.MostFrequentWords(filePaths);
            }
            else if (method == "MostCommonFile")
            {
                task.MostCommonFile(filePaths);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}