/*
+-------------+--------------+------+-----+-------------------+-----------------------------+
| Field       | Type         | Null | Key | Default           | Extra                       |
+-------------+--------------+------+-----+-------------------+-----------------------------+
| biz_tag     | varchar(128) | NO   | PRI |                   |                             |
| max_id      | bigint(20)   | NO   |     | 1                 |                             |
| step        | int(11)      | NO   |     | NULL              |                             |
| desc        | varchar(256) | YES  |     | NULL              |                             |
| update_time | timestamp    | NO   |     | CURRENT_TIMESTAMP | on update CURRENT_TIMESTAMP |
+-------------+--------------+------+-----+-------------------+-----------------------------+

 */

using System;
using System.Collections.Concurrent;
using System.Threading;
using Nice.Leaf.Segment;

namespace Nice.Leaf
{
    /// <summary>
    /// 美团的Leaf Segment 方案
    /// </summary>
    public class LeafSegment
    {
        private long _currentStep = long.MaxValue >> 1;
        private readonly Func<DataVal> _idGetAction;
        private readonly ConcurrentQueue<long> _data = new ConcurrentQueue<long>();
        private readonly AutoResetEvent _autoReset = new AutoResetEvent(false);

        /// <summary>
        /// 美团的Leaf Segment 方案
        /// </summary>
        /// <param name="idGetAction">Id生成策略</param>
        /// <param name="prefill">是否立即初始化数据</param>
        public LeafSegment(Func<DataVal> idGetAction,bool prefill=false)
        {
            _idGetAction = idGetAction;
            if (prefill)
            {
                FillData();
            }
            Loop();
        }

        /// <summary>
        /// 获取下一个Id
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            _autoReset.Set();
            if (_data.TryDequeue(out var result))
            {
                return result;
            }

            throw new Exception("Resource not enough");
        }

        private void Loop()
        {
            (new Thread(_ =>
            {
                while (true)
                {
                    _autoReset.WaitOne();
                    FillData();
                }
            }) {IsBackground = true}).Start();


        }

        private void FillData()
        {
            //数量小于步长一半时触发拉新
            while (_data.Count < (_currentStep >> 1))
            {
                var tmp = _idGetAction.Invoke();
                _currentStep = tmp.Step;
                for (var i = tmp.MaxId - tmp.Step + 1; i <= tmp.MaxId; i++)
                {
                    _data.Enqueue(i);
                }
            }
        }
    }
}
