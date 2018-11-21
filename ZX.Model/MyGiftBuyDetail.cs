using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MyGiftBuyDetail
    /// <summary>
    /// MyGiftBuyDetail
    /// </summary>
    [DataFieldAttribute("MyGiftBuyDetail")]
    public class MyGiftBuyDetail : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_MyGiftBuyId")]		public Int64? FK_MyGiftBuyId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Name")]		public string Name		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Brand")]		public string Brand		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Count")]		public Int32? Count		{			get;			set;		}        public string GifUnitName {
            get;
            set;
        }

  /// <summary>  ///   /// </summary>      [DataFieldAttribute("Price")]		public decimal? Price		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("BuyDeptName")]		public string BuyDeptName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ZChanType")]		public string ZChanType		{			get;			set;		}        [DataFieldAttribute("FK_ApprovalId")]        public Int32? FK_ApprovalId
        {
            get;
            set;
        }
    }
    #endregion
}
