using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Project
    /// <summary>
    /// Project
    /// </summary>
    [DataFieldAttribute("Project")]
    public class Project : BaseModel
    {        /// <summary>        ///         /// </summary>        [DataFieldAttribute("PName")]
        public string PName
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("HeadUser")]
        public string HeadUser
        {
            get;
            set;
        }
        /// <summary>        /// 0：有效 10：挂起 20：完成 30：删除        /// </summary>        [DataFieldAttribute("PStatus")]
        public string PStatus
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("StartTime")]
        public DateTime? StartTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("EndTime")]
        public DateTime? EndTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("PDesc")]
        public string PDesc
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("Visitors")]
        public string Visitors
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
