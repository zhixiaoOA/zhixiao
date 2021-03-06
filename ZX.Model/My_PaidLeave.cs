﻿using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region My_PaidLeave
    /// <summary>
    /// My_PaidLeave
    /// </summary>
    [DataFieldAttribute("My_PaidLeave")]
    public class My_PaidLeave : BaseModel
    {
        /// <summary>
        /// 0：工作日加班 10：休息日加班 20：家假日加班 
        /// </summary>
        [DataFieldAttribute("FK_WorkId")]
        public Int32? FK_WorkId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("StartTime")]
        public DateTime? StartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("StartHour")]
        public string StartHour
        {
            get;
            set;
        }
        /// <summary>
        /// 0：项目文档库 10：自定义文档库
        /// </summary>
        [DataFieldAttribute("EndTime")]
        public DateTime? EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("EndHour")]
        public string EndHour
        {
            get;
            set;
        }
        /// <summary>
        /// 0：有效 10：挂起 20：完成 30：删除
        /// </summary>
        [DataFieldAttribute("ADesc")]
        public string ADesc
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ATotalLength")]
        public Int32? ATotalLength
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
        [DataFieldAttribute("Status")]
        public Int32 Status
        {
            get;
            set;
        }
        [DataFieldAttribute("FK_ApprovalUserId")]
        public Int64? FK_ApprovalUserId
        {
            get;
            set;
        }
        [DataFieldAttribute("AddWorkDesc")]
        public string AddWorkDesc
        {
            get;
            set;
        }
    }
    #endregion
}
