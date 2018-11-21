using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region ApplyFlow
    /// <summary>
    /// ApplyFlow
    /// </summary>
    [DataFieldAttribute("ApplyFlow")]
    public class ApplyFlow : BaseModel
    {
		/// <summary>		/// 申请用户		/// </summary>		[DataFieldAttribute("FK_UserId")]		public Int64? FK_UserId		{			get;			set;		}		/// <summary>		/// 申请类型		/// </summary>		[DataFieldAttribute("FK_Approval_TypeId")]		public Int64? FK_Approval_TypeId		{			get;			set;		}		/// <summary>		/// 当前审核人		/// </summary>		[DataFieldAttribute("FK_Approval_UserId")]		public Int64? FK_Approval_UserId		{			get;			set;		}		/// <summary>		/// 标题		/// </summary>		[DataFieldAttribute("Title")]		public string Title		{			get;			set;		}		/// <summary>		/// 申请事由		/// </summary>		[DataFieldAttribute("AContent")]		public string AContent		{			get;			set;		}		/// <summary>		/// 开始时间		/// </summary>		[DataFieldAttribute("BeginTime")]		public DateTime? BeginTime		{			get;			set;		}		/// <summary>		/// 结束时间		/// </summary>		[DataFieldAttribute("EndTime")]		public DateTime? EndTime		{			get;			set;		}		/// <summary>		/// 申请时间		/// </summary>		[DataFieldAttribute("AddTime")]		public DateTime? AddTime		{			get;			set;		}		/// <summary>		/// 0:新申请  1:审核中  2:审核通过  3:驳回		/// </summary>		[DataFieldAttribute("Status")]		public Int32? Status		{			get;			set;		}
    }
    #endregion
}
