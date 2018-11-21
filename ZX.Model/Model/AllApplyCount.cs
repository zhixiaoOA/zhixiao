using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    public class AllApplyCount
    {
        /// <summary>
        /// 申请单数量
        /// </summary>
        public int ApplyCount { get; set; }
        /// <summary>
        /// 请假单数量
        /// </summary>
        public int AskCount { get; set; }
        /// <summary>
        /// 出差单数量
        /// </summary>
        public int BusinessTripCount { get; set; }
        /// <summary>
        /// 加班单数量
        /// </summary>
        public int WorkCount { get; set; }
        /// <summary>
        /// 合同单数量
        /// </summary>
        public int AgreementCount { get; set; }
        /// <summary>
        /// 私车公用单数量
        /// </summary>
        public int CartPublicCount { get; set; }
        /// <summary>
        /// 未打卡证明数量
        /// </summary>
        public int ClockCount { get; set; }
        /// <summary>
        /// 招待申请单数量
        /// </summary>
        public int EntertainCount { get; set; }
        /// <summary>
        /// 物资采购单数量
        /// </summary>
        public int GiftBuyCount { get; set; }
        /// <summary>
        /// 商品领用单数量
        /// </summary>
        public int GooodsUseCount { get; set; }
        /// <summary>
        /// 印章借出单数量
        /// </summary>
        public int SealOutCount { get; set; }
        /// <summary>
        /// 印章使用单数量
        /// </summary>
        public int SealUseCount { get; set; }

        /// <summary>
        /// 费用申请单数量
        /// </summary>
        public int MyCostCount { get; set; }
    }
}
