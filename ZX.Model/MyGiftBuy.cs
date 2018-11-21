using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MyGiftBuy
    /// <summary>
    /// MyGiftBuy
    /// </summary>
    [DataFieldAttribute("MyGiftBuy")]
    public class MyGiftBuy : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_UserId")]		public Int64? FK_UserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_ApprovalUserId")]		public Int64? FK_ApprovalUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Status")]		public Int32? Status		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AddTime")]		public DateTime? AddTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}
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
