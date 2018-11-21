using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Document_Manager
    /// <summary>
    /// Document_Manager
    /// </summary>
    [DataFieldAttribute("Document_Manager")]
    public class Document_Manager : BaseModel
    {
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("FK_TypeId")]
		public Int32? FK_TypeId
		{
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_LibraryId")]
        public Int32? FK_LibraryId
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("AuthUserId")]
		public string AuthUserId
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("AuthRole")]
		public string AuthRole
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("DTitle")]
		public string DTitle
		{
			get;
			set;
		}

        /// <summary>
        /// 0-文档;10-链接
        /// </summary>
        [DataFieldAttribute("DType")]
        public int DType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("DContent")]
		public string DContent
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("DKey")]
		public string DKey
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("DAbs")]
		public string DAbs
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		[DataFieldAttribute("DUrl")]
		public string DUrl
		{
			get;
			set;
		}

        [DataFieldAttribute("Attach")]
        public string Attach {
            get;
            set;
        }

        [DataFieldAttribute("Postfix")]
        public string Postfix
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

    }
    #endregion
}
