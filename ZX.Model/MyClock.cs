using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region MyClock
    /// <summary>
    /// MyClock
    /// </summary>
    [DataFieldAttribute("MyClock")]
    public class MyClock : BaseModel
    {
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("OneDate")]
		public DateTime? OneDate
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("OneDesc")]
		public string OneDesc
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("TwoDate")]
		public DateTime? TwoDate
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("TwoDesc")]
		public string TwoDesc
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("ThreeDate")]
		public DateTime? ThreeDate
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("ThreeDesc")]
		public string ThreeDesc
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("OutThreeDate")]
		public DateTime? OutThreeDate
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("OutThreeDesc")]
		public string OutThreeDesc
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_UserId")]
		public Int64? FK_UserId
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_ApprovalUserId")]
		public Int64? FK_ApprovalUserId
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("Status")]
		public Int32? Status
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("AddTime")]
		public DateTime? AddTime
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("UpdateTime")]
		public DateTime? UpdateTime
		{
			get;
			set;
		}
        /// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("NoClockCount")]
		public Int32? NoClockCount
        {
			get;
			set;
		}
        
        /// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_ApprovalId")]
		public Int64? FK_ApprovalId
        {
			get;
			set;
		}
    }
    #endregion
}
