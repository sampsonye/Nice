using System;

namespace Nice.Snowflake.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var snowflake = new Snowflake(1, 1, 0);
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(snowflake.NextId());
            }

            Console.ReadLine();
        }
    }
}
