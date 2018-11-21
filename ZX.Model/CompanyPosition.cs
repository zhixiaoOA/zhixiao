using System;

namespace ZX.Model
{
    #region CompanyPosition
    /// <summary>
    /// CompanyPosition
    /// </summary>
    [DataFieldAttribute("CompanyPosition")]
    public class CompanyPosition : BaseModel
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        [DataFieldAttribute("Name")]
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("Sort")]
        public Int32? Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataFieldAttribute("AddTime")]
        public DateTime? AddTime
        {
            get;
            set;
        }
        /// <summary>
		/// 父id
		/// </summary>
		[DataFieldAttribute("ParentId")]
        public Int64? ParentId
        {
            get;
            set;
        }

    }
    #endregion
}
