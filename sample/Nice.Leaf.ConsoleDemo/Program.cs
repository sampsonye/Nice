using System;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using Nice.Leaf.Segment;

namespace Nice.Leaf.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var segment = new LeafSegment(DataFun/*DataFunByDb*/, true);
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine(segment.NextId());
            }

            Console.ReadLine();
        }

        
        //Data by Mock
        private static long maxId = 1;
        private const int step = 2000;
        private static DataVal DataFun()
        {
            maxId += step;
            return new DataVal() { Step = step, MaxId = maxId };
        }

        //Data by Mysql
        private static DataVal DataFunByDb()
        {
            var conn = new MySqlConnection(
                "server=localhost;port=3316;Initial Catalog=mtx;user id=root;password=123456;SslMode=None");
           
            using (conn)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new
                    {
                        bizTag = "test",
                    };
                    var rows = conn.Execute("UPDATE leafsegment SET max_id=max_id+step,update_time=now() WHERE biz_tag=@bizTag;", param,
                        tran);
                    var data = conn.QueryFirstOrDefault("SELECT biz_tag, max_id, step FROM leafsegment WHERE biz_tag=@bizTag;", param, tran);
                    tran.Commit();
                    return new DataVal() { Step = data.step, MaxId = data.max_id };
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw e;
                }

            }
        }
    }
}
