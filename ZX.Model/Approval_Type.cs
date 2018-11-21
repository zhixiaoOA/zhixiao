using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region Approval_Type
    /// <summary>
    /// Approval_Type
    /// </summary>
    [DataFieldAttribute("Approval_Type")]
    public class Approval_Type : BaseModel
    {

        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_ApprovalId")]
        public long? FK_ApprovalId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("AName")]
        public string AName
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

        /// <summary>
        /// 是否手动
        /// </summary>
        [DataFieldAttribute("IsSdong")]
        public Int32? IsSdong
        {
            get;
            set;
        }
    }
    #endregion
}
