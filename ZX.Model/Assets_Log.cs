using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Assets_Log
    /// <summary>
    /// Assets_Log
    /// </summary>
    [DataFieldAttribute("Assets_Log")]
    public class Assets_Log : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_AssetsId")]		public Int32? FK_AssetsId		{			get;			set;		}		/// <summary>		/// 0：有效 10：挂起 20：完成 30：删除		/// </summary>		[DataFieldAttribute("Remark")]		public string Remark		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StatusId")]		public Int32? StatusId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StatusName")]		public Int32? StatusName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}
    }
    #endregion
}
