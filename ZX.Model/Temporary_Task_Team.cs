using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Temporary_Task_Temp
    /// <summary>
    /// Temporary_Task_Team
    /// </summary>
    [DataFieldAttribute("Temporary_Task_Team")]
    public class Temporary_Task_Team : BaseModel
    {
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("FK_TemporaryTaskId")]
        public Int32? FK_TemporaryTaskId
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
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("ExpectHours")]
        public Int32? ExpectHours
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("ConsumTime")]
        public Int32? ConsumTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TheTime")]
        public Int32? TheTime
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
