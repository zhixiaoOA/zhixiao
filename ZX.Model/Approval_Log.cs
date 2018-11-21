using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region Approval_Log
    /// <summary>
    /// Approval_Log
    /// </summary>
    [DataFieldAttribute("Approval_Log")]
    public class Approval_Log : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_ApplyFlowId")]
        public Int64? FK_ApplyFlowId
        {
            get;
            set;
        }
        /// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_TypeId")]
        public Int64? FK_TypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("DName")]
        public string DName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("LContent")]
        public string LContent
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
        /// <summary>
		/// 状态 0:通过  1:驳回
		/// </summary>
		[DataFieldAttribute("Status")]
        public Int32? Status
        {
            get;
            set;
        }
    }
    #endregion
}
