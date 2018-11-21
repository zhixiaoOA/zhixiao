using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MyAgreement
    /// <summary>
    /// MyAgreement
    /// </summary>
    [DataFieldAttribute("MyAgreement")]
    public class MyAgreement : BaseModel
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
		/// 合同编号
		/// </summary>
		[DataFieldAttribute("HtNo")]
		public string HtNo
		{
			get;
			set;
		}
		/// <summary>
		/// 合同名称
		/// </summary>
		[DataFieldAttribute("HtName")]
		public string HtName
		{
			get;
			set;
		}
		/// <summary>
		/// 甲方
		/// </summary>
		[DataFieldAttribute("JiaName")]
		public string JiaName
		{
			get;
			set;
		}
		/// <summary>
		/// 乙方
		/// </summary>
		[DataFieldAttribute("YiName")]
		public string YiName
		{
			get;
			set;
		}
		/// <summary>
		/// 合同金额
		/// </summary>
		[DataFieldAttribute("TotalMoney")]
		public decimal? TotalMoney
		{
			get;
			set;
		}
		/// <summary>
		/// 使用印章名称
		/// </summary>
		[DataFieldAttribute("YinzName")]
		public string YinzName
		{
			get;
			set;
		}
        /// <summary>
		/// 合同附件
		/// </summary>
		[DataFieldAttribute("HtFile")]
		public string HtFile
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
        [DataFieldAttribute("Description")]
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("Party")]
        public string Party
        {
            get;
            set;
        }

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
