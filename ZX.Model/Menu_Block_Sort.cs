using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Menu_Block_Sort
    /// <summary>
    /// Menu_Block_Sort
    /// </summary>
    [DataFieldAttribute("Menu_Block_Sort")]
    public class Menu_Block_Sort : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_Menu_BlockId")]		public Int32? FK_Menu_BlockId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("SName")]		public string SName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("SortField")]		public string SortField		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}
    }
    #endregion
}
