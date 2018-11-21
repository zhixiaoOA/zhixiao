using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model
{
    public class ApplyFlowModel : ApplyFlow
    {
        /// <summary>
        /// 申请人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 当前审核人
        /// </summary>
        public string CurrAuthName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName { get; set; }
    }
}
