using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Assets_Type
    /// <summary>
    /// Assets_Type
    /// </summary>
    [DataFieldAttribute("Assets_Type")]
    public class Assets_Type : BaseModel
    {
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("TName")]
		public string TName
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("ParentId")]
		public Int32? ParentId
		{
			get;
			set;
		}
		/// <summary>
		/// 0：有效 10：挂起 20：完成 30：删除
		/// </summary>
		[DataFieldAttribute("TRemark")]
		public string TRemark
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("TSort")]
		public Int32? TSort
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("CreateUserId")]
		public Int32? CreateUserId
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("CreateTime")]
		public DateTime? CreateTime
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("UpdateUserId")]
		public Int32? UpdateUserId
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
		[DataFieldAttribute("CreateAccount")]
		public string CreateAccount
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("UpdateAccount")]
		public string UpdateAccount
		{
			get;
			set;
		}

    }
    #endregion
}
