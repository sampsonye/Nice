using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Nice.Snowflake.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SnowflakeTest>();
            Console.ReadLine();
        }
    }

    public class SnowflakeTest
    {
        readonly Snowflake _snowflake=new Snowflake(1,1,0); 
        [Benchmark]
        public void TestGenerate()
        {
           var id= _snowflake.NextId();
        }
    }
}
