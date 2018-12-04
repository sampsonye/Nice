using System;
using System.Threading;
using Nice.Leaf.Segment;
using Xunit;

namespace Nice.Leaf.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var segment=new LeafSegment(DataFun,true);
            Thread.Sleep(100);
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine(segment.NextId());
            }

        }

        private long maxId = 1;
        private const int step = 100;
        private DataVal DataFun()
        {
            maxId += step;
            return new DataVal(){Step = step,MaxId = maxId};
        }
    }
}
