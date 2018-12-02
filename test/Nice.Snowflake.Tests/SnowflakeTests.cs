using System;
using Xunit;

namespace Nice.Snowflake.Tests
{
    public class SnowflakeTests
    {
        [Fact]
        public void BaseTest()
        {
            var snowflake=new Snowflake(0,0,0);
            var val = snowflake.NextId();
            var val2 = snowflake.NextId();
            Console.WriteLine($"val:{val} val2:{val2}");
            Assert.InRange(val,0L,long.MaxValue);
            Assert.InRange(val2,0L,long.MaxValue);

            Assert.True(val<val2);

        }
    }
}
