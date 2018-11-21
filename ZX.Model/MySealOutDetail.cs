using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MySealOutDetail
    /// <summary>
    /// MySealOutDetail
    /// </summary>
    [DataFieldAttribute("MySealOutDetail")]
    public class MySealOutDetail : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_MySealOutId")]		public Int64? FK_MySealOutId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ZlName")]		public string ZlName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ZlCount")]		public Int32? ZlCount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("HtNo")]		public string HtNo		{			get;			set;		}
    }
    #endregion
}
