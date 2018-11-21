using System;

namespace ZX.Model
{
    #region Sys_Role_Menu
    /// <summary>
    /// Sys_Role_Menu
    /// </summary>
    [DataFieldAttribute("Sys_Role_Menu")]
    public class Sys_Role_Menu : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_RoleId")]
        public Int32? FK_RoleId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_MenuButtonId")]
        public Int32? FK_MenuButtonId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("TypeId")]
        public Int32? TypeId
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
