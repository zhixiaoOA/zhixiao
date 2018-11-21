using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Dictionary
    /// <summary>
    /// Dictionary
    /// </summary>
    [DataFieldAttribute("Dictionary")]
    public class Dictionary : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Name")]		public string Name		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ParentId")]		public Int64? ParentId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Sort")]		public Int32? Sort		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AddTime")]		public DateTime? AddTime		{			get;			set;		}
    }
    #endregion
}
