using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region MyTravelReimbursementDetail
    /// <summary>
    /// MyTravelReimbursementDetail
    /// </summary>
    [DataFieldAttribute("MyTravelReimbursementDetail")]
    public class MyTravelReimbursementDetail : BaseModel
    {
		/// <summary>
		/// 起讫地点
		/// </summary>
		[DataFieldAttribute("Address")]
		public string Address
        {
			get;
			set;
		}
		/// <summary>
		/// 天数
		/// </summary>
		[DataFieldAttribute("days")]
		public Int32 days
        {
			get;
			set;
		}
		/// <summary>
		/// 机票费
		/// </summary>
		[DataFieldAttribute("JiPiaoFei")]
		public decimal? JiPiaoFei
        {
			get;
			set;
		}
		/// <summary>
		/// 车船费
		/// </summary>
		[DataFieldAttribute("CheChuanFei")]
		public decimal? CheChuanFei
        {
			get;
			set;
		}
		/// <summary>
		/// 交通费
		/// </summary>
		[DataFieldAttribute("JiaoTongFei")]
		public decimal? JiaoTongFei
        {
			get;
			set;
		}
		/// <summary>
		/// 住宿费
		/// </summary>
		[DataFieldAttribute("ZhuSuFei")]
		public decimal? ZhuSuFei
        {
			get;
			set;
		}
        /// <summary>
		/// 出差补助
		/// </summary>
		[DataFieldAttribute("ChuChaiBuZhu")]
		public decimal? ChuChaiBuZhu
        {
			get;
			set;
		}
		/// <summary>
		/// 住宿节约补助
		/// </summary>
		[DataFieldAttribute("ZhuSuJieYueBuZhu")]
		public decimal? ZhuSuJieYueBuZhu
        {
			get;
			set;
		}
		/// <summary>
		/// 其他
		/// </summary>
		[DataFieldAttribute("QiTa")]
		public decimal? QiTa
        {
			get;
			set;
		}
		/// <summary>
		/// 合计
		/// </summary>
		[DataFieldAttribute("TotalMoney")]
		public decimal? TotalMoney
        {
			get;
			set;
		}
		/// <summary>
		/// 日期
		/// </summary>
		[DataFieldAttribute("TravelTime")]
		public DateTime? TravelTime
        {
			get;
			set;
		}
        /// <summary>
		/// 差旅报销Id
		/// </summary>
		[DataFieldAttribute("TravelReimbursementId")]
        public Int64? TravelReimbursementId
        {
            get;
            set;
        }
    }
    #endregion
}
