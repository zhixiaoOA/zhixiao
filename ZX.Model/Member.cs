using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Member
    /// <summary>
    /// Member
    /// </summary>
    [DataFieldAttribute("Member")]
    public class Member : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("OpenId")]		public string OpenId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Nickname")]		public string Nickname		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("HeadImage")]		public string HeadImage		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_UserId")]		public Int64? FK_UserId
        {			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AddTime")]		public DateTime? AddTime		{			get;			set;		}
    }
    #endregion
}
