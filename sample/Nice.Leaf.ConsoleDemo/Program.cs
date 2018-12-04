using System;
using System.Threading;
using Nice.Leaf.Segment;

namespace Nice.Leaf.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var segment = new LeafSegment(DataFun, true);
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine(segment.NextId());
            }

            Console.ReadLine();
        }

        private static long maxId = 1;
        private const int step = 2000;
        private static DataVal DataFun()
        {
            maxId += step;
            return new DataVal() { Step = step, MaxId = maxId };
        }
    }
}
