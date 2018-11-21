using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Project_Team
    /// <summary>
    /// Project_Team
    /// </summary>
    [DataFieldAttribute("Project_Team")]
    public class Project_Team : BaseModel
    {
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("FK_ProjectId")]
        public Int32? FK_ProjectId
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("FK_UserId")]
        public Int32? FK_UserId
        {
            get;
            set;
        }
        /// <summary>        /// 0：默认 10：管理员 20：受限        /// </summary>        [DataFieldAttribute("Permissions")]
        public Int32? Permissions
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
