using System;
using System.Text;

namespace ZX.Model
{
    /// <summary>
    /// 
    /// </summary>
   [DataFieldAttribute("Sys_Menu")]
    public class Sys_Menu : BaseModel
    {
        /// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("MName")]
        public string MName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("MParentId")]
        public Int32? MParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("MIcon")]
        public string MIcon
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("Mesc")]
        public string Mesc
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("MUrl")]
        public string MUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("MSort")]
        public Int32? MSort
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
        /// 英文
        /// </summary>
        [DataFieldAttribute("BlockSign")]
        public string BlockSign
        {
            get;
            set;
        }
    }
}
