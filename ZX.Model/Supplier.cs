using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Supplier
    /// <summary>
    /// Supplier
    /// </summary>
    [DataFieldAttribute("Supplier")]
    public class Supplier : BaseModel
    {
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SName")]
        public string SName
        {
            get;
            set;
        }
        /// <summary>        /// 0：公司公告 10：新闻        /// </summary>        [DataFieldAttribute("SCode")]
        public string SCode
        {
            get;
            set;
        }
        /// <summary>        /// 0：有效 10：挂起 20：完成 30：删除        /// </summary>        [DataFieldAttribute("SRemark")]
        public string SRemark
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SAddress")]
        public string SAddress
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SLinkName")]
        public string SLinkName
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SPhone")]
        public string SPhone
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("STel")]
        public string STel
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SEMail")]
        public string SEMail
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SOpenName")]
        public string SOpenName
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SOpenBank")]
        public string SOpenBank
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SOpenBankAccount")]
        public string SOpenBankAccount
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SOpenMake")]
        public string SOpenMake
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("STaxpayer")]
        public string STaxpayer
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("CreateUserId")]
        public Int32? CreateUserId
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("CreateTime")]
        public DateTime? CreateTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("UpdateUserId")]
        public Int32? UpdateUserId
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("UpdateTime")]
        public DateTime? UpdateTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("CreateAccount")]
        public string CreateAccount
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("UpdateAccount")]
        public string UpdateAccount
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SSize")]
        public string SSize
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SIndustry")]
        public string SIndustry
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SQQ")]
        public string SQQ
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("SWeiXin")]
        public string SWeiXin
        {
            get;
            set;
        }
        /// <summary>        /// 供货合格率        /// </summary>        [DataFieldAttribute("Qualified")]
        public decimal? Qualified
        {
            get;
            set;
        }
    }
    #endregion
}
