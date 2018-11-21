using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Project_Block
    /// <summary>
    /// Project_Block
    /// </summary>
    [DataFieldAttribute("Project_Block")]
    public class Project_Block : BaseModel
    {
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("BType")]
        public Int32? BType
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("BName")]
        public string BName
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("BWidth")]
        public Int32? BWidth
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("BColor")]
        public string BColor
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("PTType")]
        public Int32? PTType
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("RowCounts")]
        public Int32? RowCounts
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>       [DataFieldAttribute("BOrderBy")]
        public string BOrderBy
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("PTState")]
        public string PTState
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
