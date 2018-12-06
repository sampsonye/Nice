using System;
using System.Collections.Generic;
using System.Text;

namespace Nice.Leaf.Segment
{
    /// <summary>
    /// 数据单元
    /// </summary>
    public class DataVal
    {
        /// <summary>
        /// 当前最大Id
        /// </summary>
        public long MaxId { get; set; } = 1;
        /// <summary>
        /// 当前步长
        /// </summary>
        public int Step { get; set; } = 1000;
    }
}
