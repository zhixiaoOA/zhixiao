using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region MyTravelReimbursement
    /// <summary>
    /// MyTravelReimbursement
    /// </summary>
    [DataFieldAttribute("MyTravelReimbursement")]
    public class MyTravelReimbursement : BaseModel
    {
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_UserId")]
		public Int64? FK_UserId
        {
			get;
			set;
		}
		/// <summary>
		/// 职位
		/// </summary>
		[DataFieldAttribute("Position")]
		public string Position
        {
			get;
			set;
		}
		/// <summary>
		/// 事由
		/// </summary>
		[DataFieldAttribute("Reason")]
		public string Reason
        {
			get;
			set;
		}
		/// <summary>
		/// 出发时间
		/// </summary>
		[DataFieldAttribute("StartTime")]
		public DateTime? StartTime
        {
			get;
			set;
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		[DataFieldAttribute("EndTime")]
		public DateTime? EndTime
        {
			get;
			set;
		}
		/// <summary>
		/// 单据张数
		/// </summary>
		[DataFieldAttribute("Pages")]
		public Int32? Pages
        {
			get;
			set;
		}
		/// <summary>
		/// 总金额
		/// </summary>
		[DataFieldAttribute("TotalMoney")]
		public decimal? TotalMoney
        {
			get;
			set;
		}
        /// <summary>
		/// 总金额大写
		/// </summary>
		[DataFieldAttribute("TotalMoneyBig")]
		public string TotalMoneyBig
        {
			get;
			set;
		}
		/// <summary>
		/// 预支
		/// </summary>
		[DataFieldAttribute("Advance")]
		public decimal? Advance
        {
			get;
			set;
		}
		/// <summary>
		/// 退补
		/// </summary>
		[DataFieldAttribute("Retrieve")]
		public decimal? Retrieve
        {
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_ApprovalUserId")]
		public Int64? FK_ApprovalUserId
        {
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("Status")]
		public Int32? Status
        {
			get;
			set;
		}
        /// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("AddTime")]
        public DateTime? AddTime
        {
            get;
            set;
        }
        /// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("UpdateTime")]
        public DateTime? UpdateTime
        {
            get;
            set;
        }
        /// <summary>
		/// 
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
