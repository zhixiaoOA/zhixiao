using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Menu_Block
    /// <summary>
    /// Menu_Block
    /// </summary>
    [DataFieldAttribute("Menu_Block")]
    public class Menu_Block : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_MenuId")]		public Int32? FK_MenuId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("MBName")]		public string MBName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ToTableName")]		public string ToTableName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}
    }
    #endregion
}
