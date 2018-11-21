using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_Holiday
    /// <summary>
    /// My_Holiday
    /// </summary>
    [DataFieldAttribute("My_Holiday")]
    public class My_Holiday : BaseModel
    {
		/// <summary>		/// 0：假期 10：补班		/// </summary>		[DataFieldAttribute("HTypeId")]		public Int32? HTypeId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("HName")]		public string HName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StartTime")]		public DateTime? StartTime		{			get;			set;		}		/// <summary>		/// 0：有效 10：挂起 20：完成 30：删除		/// </summary>		[DataFieldAttribute("EndTime")]		public DateTime? EndTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("DDesc")]		public string HDesc		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}
        [DataFieldAttribute("Status")]
        public Int32? Status
        {
            get;
            set;
        }        [DataFieldAttribute("FK_ApprovalUserId")]
        public Int64? FK_ApprovalUserId
        {
            get;
            set;
        }
    }
    #endregion
}
