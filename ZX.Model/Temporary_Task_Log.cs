using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Temporary_Task_Log
    /// <summary>
    /// Temporary_Task_Log
    /// </summary>
    [DataFieldAttribute("Temporary_Task_Log")]
    public class Temporary_Task_Log : BaseModel
    {
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("FK_TemporaryTaskId")]
        public Int32? FK_TemporaryTaskId
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("Remark")]
        public string Remark
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("StatusId")]
        public Int32? StatusId
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("StatusName")]
        public string StatusName
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
        /// <summary>        /// 实际开始 目前针对于记录任务的实际开始时间,项目暂时未用上        /// </summary>        [DataFieldAttribute("ActualStart")]
        public DateTime? ActualStart
        {
            get;
            set;
        }
    }
    #endregion
}
