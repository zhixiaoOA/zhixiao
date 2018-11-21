using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Approval
    /// <summary>
    /// Approval
    /// </summary>
    [DataFieldAttribute("Approval")]
    public class Approval : BaseModel
    {
		/// <summary>		/// 		/// </summary>		//[DataFieldAttribute("FK_ApprovalTypeId")]		//public Int32? FK_ApprovalTypeId		//{		//	get;		//	set;		//}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("DName")]		public string DName		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StepId")]		public Int32? StepId		{			get;			set;		}		/// <summary>		/// 0：有效 10：挂起 20：完成 30：删除		/// </summary>		[DataFieldAttribute("AuthUserAccount")]		public string AuthUserAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AuthRole")]		public string AuthRole		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}        /// <summary>
        /// 是否手动
        /// </summary>
        [DataFieldAttribute("IsSdong")]
        public Int32? IsSdong
        {
            get;
            set;
        }
        
        /// <summary>
        /// 关联部门id
        /// </summary>
        [DataFieldAttribute("FK_DeptId")]
        public Int64? FK_DeptId
        {
            get;
            set;
        }
        /// <summary>
        /// 申请单类型id
        /// </summary>
        [DataFieldAttribute("FK_TypeId")]
        public Int64? FK_TypeId
        {
            get;
            set;
        }
    }
    #endregion
}
