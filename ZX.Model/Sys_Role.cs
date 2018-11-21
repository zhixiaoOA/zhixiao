using System;

namespace ZX.Model
{
    #region Sys_Role
    /// <summary>
    /// Sys_Role
    /// </summary>
    [DataFieldAttribute("Sys_Role")]
    public class Sys_Role : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("RName")]
        public string RName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("RDesc")]
        public string RDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("RSort")]
        public Int32? RSort
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

    }
    #endregion
}
