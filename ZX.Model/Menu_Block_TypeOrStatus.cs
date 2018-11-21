using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Menu_Block_TypeOrStatus
    /// <summary>
    /// Menu_Block_TypeOrStatus
    /// </summary>
    [DataFieldAttribute("Menu_Block_TypeOrStatus")]
    public class Menu_Block_TypeOrStatus : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_Menu_BlockId")]		public Int32? FK_Menu_BlockId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("TSName")]		public string TSName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("TypeOrStatus")]		public Int32? TypeOrStatus		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}
    }
    #endregion
}
