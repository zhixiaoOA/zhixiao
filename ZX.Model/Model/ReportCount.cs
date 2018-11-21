using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model
{
    public class ReportCount
    {
        /// <summary>
        /// 闭环任务（完成任务）
        /// </summary>
        public int SCount { get; set; }
        /// <summary>
        /// 新增任务
        /// </summary>
        public int AddCount { get; set; }
        /// <summary>
        /// 新增员工
        /// </summary>
        public int AddUCount { get; set; }
        /// <summary>
        /// 新增资产
        /// </summary>
        public int AddACount { get; set; }
        /// <summary>
        /// 资产开销
        /// </summary>
        public decimal ZcMoney { get; set; }
    }
}
