using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_BaoXiao_Detail
    /// <summary>
    /// My_BaoXiao_Detail
    /// </summary>
    [DataFieldAttribute("My_BaoXiao_Detail")]
    public class My_BaoXiao_Detail : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("DDate")]		public DateTime? DDate		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_BaoXiaoId")]		public Int64? FK_BaoXiaoId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("EndHour")]		public string EndHour		{			get;			set;		}		/// <summary>		/// 0：有效 10：挂起 20：完成 30：删除		/// </summary>		[DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("BaoXiaoUserId")]		public string BaoXiaoUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}        [DataFieldAttribute("Status")]
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
