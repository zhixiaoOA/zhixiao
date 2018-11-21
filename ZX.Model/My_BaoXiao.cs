using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_BaoXiao
    /// <summary>
    /// My_BaoXiao
    /// </summary>
    [DataFieldAttribute("My_BaoXiao")]
    public class My_BaoXiao : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("BName")]		public string BName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_DeptId")]		public Int32? FK_DeptId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Subject")]		public string Subject		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("SubjectType")]		public string SubjectType		{			get;			set;		}		/// <summary>		/// 币种		/// </summary>		[DataFieldAttribute("Currency")]		public string Currency		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("EndHour")]		public string EndHour		{			get;			set;		}		/// <summary>		/// 0：有效 10：挂起 20：完成 30：删除		/// </summary>		[DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Attachment")]		public string Attachment		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}
        [DataFieldAttribute("Status")]
        public Int32? Status
        {
            get;
            set;
        }
        [DataFieldAttribute("FK_ApprovalUserId")]
        public Int64? FK_ApprovalUserId
        {
            get;
            set;
        }
    }
    #endregion
}
