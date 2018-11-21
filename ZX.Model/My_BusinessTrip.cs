using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_BusinessTrip
    /// <summary>
    /// My_BusinessTrip
    /// </summary>
    [DataFieldAttribute("My_BusinessTrip")]
    public class My_BusinessTrip : BaseModel
    {
		/// <summary>		/// 出差人员		/// </summary>		[DataFieldAttribute("Persons")]		public string Persons		{			get;			set;		}		/// <summary>		/// 开始时间		/// </summary>		[DataFieldAttribute("StartTime")]		public DateTime? StartTime		{			get;			set;		}		/// <summary>		/// 结束时间		/// </summary>		[DataFieldAttribute("EndTime")]		public DateTime? EndTime		{			get;			set;		}		/// <summary>		/// 出差时长(天)		/// </summary>		[DataFieldAttribute("BLength")]		public Int32? BLength		{			get;			set;		}		/// <summary>		/// 出差事由		/// </summary>		[DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}		/// <summary>		/// 拟出差路线		/// </summary>		[DataFieldAttribute("Luxian")]		public string Luxian		{			get;			set;		}		/// <summary>		/// 申请费用		/// </summary>		[DataFieldAttribute("ApplyMoney")]		public decimal? ApplyMoney		{			get;			set;		}        		/// <summary>		/// 申请费用单位		/// </summary>		[DataFieldAttribute("ApplyMoneyUnitName")]        public string ApplyMoneyUnitName {
            get;
            set;
        }

/// <summary>/// 拟乘交通工具/// </summary>        [DataFieldAttribute("Jtgj")]		public string Jtgj		{			get;			set;		}		/// <summary>		/// 是否需要代购车票		/// </summary>		[DataFieldAttribute("IsDgou")]		public Int32? IsDgou		{			get;			set;		}		/// <summary>		/// 代购车票提供个人资料		/// </summary>		[DataFieldAttribute("DgouInfo")]		public string DgouInfo		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Status")]		public Int32? Status		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_ApprovalUserId")]		public Int64? FK_ApprovalUserId		{			get;			set;		}

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
