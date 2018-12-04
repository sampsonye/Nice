using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Nice.Leaf.Segment;

namespace Nice.Leaf.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<LeafSegmentTest>();
            Console.ReadLine();
        }
    }

    public class LeafSegmentTest
    {
        private readonly LeafSegment _leafSegment=new LeafSegment(DataFun,true);
        private static long maxId = 0;
        private const int step = 50000;
        private static DataVal DataFun()
        {
            maxId += step;
            return new DataVal() { Step = step, MaxId = maxId };
        }

        [Benchmark]
        public void TestGenerate()
        {
            var id = _leafSegment.NextId();
        }
    }
}
