using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Document_Library
    /// <summary>
    /// Document_Library
    /// </summary>
    [DataFieldAttribute("Document_Library")]
    public class Document_Library : BaseModel
    {
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
        [DataFieldAttribute("FK_Id")]
        public Int32? FK_Id
        {
            get;
            set;
        }
        /// <summary>
        /// 0-项目文档库,10-临时任务文档库,20-公司文档库,30-个人文档库
        /// </summary>
        [DataFieldAttribute("DType")]
        public Int32? DType
        {
            get;
            set;
        }
        /// <summary>
        /// 0：有效 10：挂起 20：完成 30：删除
        /// </summary>
        [DataFieldAttribute("AuthUserAccount")]
        public string AuthUserAccount
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("AuthRole")]
        public string AuthRole
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("AuthDept")]
        public string AuthDept
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("IsPrivate")]
        public Int32? IsPrivate
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
