using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MySealOut
    /// <summary>
    /// MySealOut
    /// </summary>
    [DataFieldAttribute("MySealOut")]
    public class MySealOut : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("YinzName")]		public string YinzName		{			get;			set;		}		/// <summary>		/// 保管人		/// </summary>		[DataFieldAttribute("BgName")]		public string BgName		{			get;			set;		}		/// <summary>		/// 返还时间		/// </summary>		[DataFieldAttribute("ReturnTime")]		public DateTime? ReturnTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_UserId")]		public Int64? FK_UserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_ApprovalUserId")]		public Int64? FK_ApprovalUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Status")]		public Int32? Status		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AddTime")]		public DateTime? AddTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}

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
