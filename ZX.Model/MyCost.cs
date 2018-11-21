using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region MyCost
    /// <summary>
    /// MyCost
    /// </summary>
    [DataFieldAttribute("MyCost")]
    public class MyCost : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_UserId")]		public Int64? FK_UserId
        {			get;			set;		}		/// <summary>		/// 用途		/// </summary>		[DataFieldAttribute("YongTu")]		public string YongTu
        {			get;			set;		}		/// <summary>		/// 合同总价表		/// </summary>		[DataFieldAttribute("Ht_TotalMoney")]		public decimal? Ht_TotalMoney
        {			get;			set;		}		/// <summary>		/// 已支付		/// </summary>		[DataFieldAttribute("YiZhiFu")]		public decimal? YiZhiFu
        {			get;			set;		}		/// <summary>		/// 本次申请付款金额		/// </summary>		[DataFieldAttribute("This_Money")]		public decimal? This_Money
        {			get;			set;		}		/// <summary>		/// 付款方式		/// </summary>		[DataFieldAttribute("Payment")]		public string Payment
        {			get;			set;		}		/// <summary>		/// 开户行		/// </summary>		[DataFieldAttribute("KaiHuHang")]		public string KaiHuHang
        {			get;			set;		}        /// <summary>		/// 银行账号		/// </summary>		[DataFieldAttribute("Number")]		public string Number
        {			get;			set;		}		/// <summary>		/// 收款单位		/// </summary>		[DataFieldAttribute("ShouKuanDanWei")]		public string ShouKuanDanWei
        {			get;			set;		}
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("FK_ApprovalUserId")]
        public Int64? FK_ApprovalUserId
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("Status")]		public Int32? Status		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AddTime")]		public DateTime? AddTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}

        /// <summary>
        /// 流程id
        /// </summary>
        [DataFieldAttribute("FK_ApprovalId")]
        public Int64? FK_ApprovalId
        {
            get;
            set;
        }
    }
    #endregion
}
