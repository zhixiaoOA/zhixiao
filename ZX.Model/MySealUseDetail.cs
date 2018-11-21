using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MySealUseDetail
    /// <summary>
    /// MySealUseDetail
    /// </summary>
    [DataFieldAttribute("MySealUseDetail")]
    public class MySealUseDetail : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_MySealUseId")]		public Int64? FK_MySealUseId
        {			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ZlName")]		public string ZlName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ZlCount")]		public Int32? ZlCount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("HtNo")]		public string HtNo		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("YJianCount")]		public Int32? YJianCount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FYJianCount")]		public Int32? FYJianCount		{			get;			set;		}
    }
    #endregion
}
