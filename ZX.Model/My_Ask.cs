using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_Ask
    /// <summary>
    /// My_Ask
    /// </summary>
    [DataFieldAttribute("My_Ask")]
    public class My_Ask : BaseModel
    {
		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("AType")]		public Int32? AType		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StartTime")]		public DateTime? StartTime		{			get;			set;		}		/// <summary>		/// 0：项目文档库 10：自定义文档库		/// </summary>		[DataFieldAttribute("EndTime")]		public DateTime? EndTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("Status")]		public Int32? Status		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_ApprovalUserId")]		public Int64? FK_ApprovalUserId		{			get;			set;		}        /// <summary>		/// 		/// </summary>		[DataFieldAttribute("FK_ApprovalId")]		public Int64? FK_ApprovalId
        {			get;			set;		}


        /// <summary>        /// 附件        /// </summary>        [DataFieldAttribute("MAttach")]
        public string MAttach
        {
            get;
            set;
        }

    }
    #endregion
}
