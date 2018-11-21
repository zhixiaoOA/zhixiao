using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region AssetsUseDetail
    /// <summary>
    /// AssetsUseDetail
    /// </summary>
    [DataFieldAttribute("AssetsUseDetail")]
    public class AssetsUseDetail : BaseModel
    {
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_AssetsUseId")]
		public Int64? FK_AssetsUseId
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_AssetsId")]
		public Int64? FK_AssetsId
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("DCount")]
		public Int32? DCount
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("NatureOfAssets")]
		public string NatureOfAssets
		{
			get;
			set;
		}
        /// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("AuthTime")]
		public DateTime? AuthTime
        {
			get;
			set;
		}
    }
    #endregion
}
