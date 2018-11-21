using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region News
    /// <summary>
    /// News
    /// </summary>
    [DataFieldAttribute("News")]
    public class News : BaseModel
    {
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("DName")]
        public string DName
        {
            get;
            set;
        }
        
        /// <summary>        /// 0：公司公告 10：新闻        /// </summary>        [DataFieldAttribute("DType")]
        public Int32? DType
        {
            get;
            set;
        }

        /// <summary>        /// 0：有效 10：挂起 20：完成 30：删除        /// </summary>        [DataFieldAttribute("DContent")]
        public string DContent
        {
            get;
            set;
        }

        /// <summary>        ///         /// </summary>        [DataFieldAttribute("DImageUrl")]
        public string DImageUrl
        {
            get;
            set;
        }

        /// <summary>        ///         /// </summary>        [DataFieldAttribute("DSort")]
        public Int32? DSort
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
    }
    #endregion
}
