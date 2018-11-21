using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Sys_Button
    /// <summary>
    /// Sys_Button
    /// </summary>
    [DataFieldAttribute("Sys_Button")]
    public class Sys_Button : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("BName")]
        public string BName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_MenuId")]
        public Int32? FK_MenuId
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("BDesc")]
        public string BDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("BSort")]
        public Int32? BSort
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("BGroup")]
        public Int32? BGroup
        {
            get;
            set;
        }

        /// <summary>
        /// 0-"",1-新增记录,2-编辑记录,3-删除记录,4-查看记录
        /// </summary>
        [DataFieldAttribute("BType")]
        public int BType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("BOptionFun")]
        public string BOptionFun
        {
            get;
            set;
        }
        /// <summary>
        /// 0-启用,1-禁用
        /// </summary>
        [DataFieldAttribute("BIsUse")]
        public int BIsUse
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
