using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MyEntertain
    /// <summary>
    /// MyEntertain
    /// </summary>
    [DataFieldAttribute("MyEntertain")]
    public class MyEntertain : BaseModel
    {
		/// <summary>		/// 招待事项		/// </summary>		[DataFieldAttribute("ESxiang")]		public string ESxiang		{			get;			set;		}		/// <summary>		/// 招待级别		/// </summary>		[DataFieldAttribute("ELevel")]		public string ELevel		{			get;			set;		}		/// <summary>		/// 事由		/// </summary>		[DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}		/// <summary>		/// 招待费用		/// </summary>		[DataFieldAttribute("TotalMoney")]		public decimal? TotalMoney		{			get;			set;		}        		/// <summary>		/// 招待费用		/// </summary>		[DataFieldAttribute("EntertainUnitName")]		public string EntertainUnitName		{			get;			set;		}		/// <summary>		/// 客人单位		/// </summary>		[DataFieldAttribute("KeCompanyName")]		public string KeCompanyName		{			get;			set;		}		/// <summary>		/// 客人人数		/// </summary>		[DataFieldAttribute("KeCount")]		public Int32? KeCount		{			get;			set;		}        /// <summary>		/// 陪餐人员		/// </summary>		[DataFieldAttribute("AccompanyDinner")]		public string AccompanyDinner
        {			get;			set;		}        
/// <summary>/// /// </summary>        [DataFieldAttribute("FK_UserId")]		public Int64? FK_UserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_ApprovalUserId")]		public Int64? FK_ApprovalUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Status")]		public Int32? Status		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AddTime")]		public DateTime? AddTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}

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
