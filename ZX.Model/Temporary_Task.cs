using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Temporary_Task
    /// <summary>
    /// Temporary_Task
    /// </summary>
    [DataFieldAttribute("Temporary_Task")]
    public class Temporary_Task : BaseModel
    {
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TName")]
        public string TName
        {
            get;
            set;
        }        
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("Assigned")]
        public string Assigned
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("Priority")]
        public Int32? Priority
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TExpected")]
        public Int32? TExpected
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("AsTime")]
        public DateTime? AsTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TDesc")]
        public string TDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 1-未完成 2-未开始 3-进行中 4-已完成 5-已取消 6-已关闭,7-已删除
        /// </summary>        [DataFieldAttribute("TState")]
        public Int32? TState
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TCC")]
        public string TCC
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("Attach")]
        public string Attach
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TRemark")]
        public string TRemark
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TSuccess")]
        public string TSuccess
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TSucTime")]
        public DateTime? TSucTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TCancel")]
        public string TCancel
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TCancelTime")]
        public DateTime? TCancelTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TClosed")]
        public string TClosed
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TClosedTime")]
        public DateTime? TClosedTime
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("TClosedWhy")]
        public string TClosedWhy
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
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("ParentId")]
        public Int32? ParentId
        {
            get;
            set;
        }
        /// <summary>        ///         /// </summary>        [DataFieldAttribute("EditCount")]
        public Int32? EditCount
        {
            get;
            set;
        }
    }
    #endregion
}
