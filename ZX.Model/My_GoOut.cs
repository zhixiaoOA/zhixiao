using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_GoOut
    /// <summary>
    /// My_GoOut
    /// </summary>
    [DataFieldAttribute("My_GoOut")]
    public class My_GoOut : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("BName")]		public string BName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("BKGName")]		public string BKGName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StartTime")]		public DateTime? StartTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StartHour")]		public string StartHour		{			get;			set;		}		/// <summary>		/// 0：项目文档库 10：自定义文档库		/// </summary>		[DataFieldAttribute("EndTime")]		public DateTime? EndTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("EndHour")]		public string EndHour		{			get;			set;		}		/// <summary>		/// 0：有效 10：挂起 20：完成 30：删除		/// </summary>		[DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("EndCity")]		public string EndCity		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}        [DataFieldAttribute("Status")]
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
