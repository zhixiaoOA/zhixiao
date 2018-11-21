using System;

namespace ZX.Model
{
    #region Sys_Dept
    /// <summary>
    /// Sys_Dept
    /// </summary>
    [DataFieldAttribute("Sys_Dept")]
    public class Sys_Dept : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("DName")]
        public string DName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("DParentId")]
        public Int32? DParentId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_UserId")]
        public Int32? FK_UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("DDesc")]
        public string DDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("DSort")]
        public Int32? DSort
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
