using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Sys_User
    /// <summary>
    /// Sys_User
    /// </summary>
    [DataFieldAttribute("Sys_User")]
    public class Sys_User : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UserName")]
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("RealName")]
        public string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// 1-男;2-女
        /// </summary>
        [DataFieldAttribute("USex")]
        public string USex
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("Fk_DeptId")]
        public Int32? Fk_DeptId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("Fk_RoleId")]
        public Int32? Fk_RoleId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("Pwd")]
        public string Pwd
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UEmail")]
        public string UEmail
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UPhone")]
        public string UPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UQQ")]
        public string UQQ
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UAddress")]
        public string UAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("IPAddress")]
        public string IPAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 1-生效;0-失效
        /// </summary>
        [DataFieldAttribute("IsDel")]
        public Int32? IsDel
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
		[DataFieldAttribute("OpenId")]
        public string OpenId
        {
            get;
            set;
        }

        /// <summary>
        /// 个人签名图片
        /// </summary>
        [DataFieldAttribute("USgin")]
        public string USgin
        {
            get;
            set;
        }

        /// <summary>
        /// 职位id
        /// </summary>
        [DataFieldAttribute("FK_CompanyPositionId")]
        public Int64? FK_CompanyPositionId
        {
            get;
            set;
        }
    }
    #endregion
}