using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region Document_Type
    /// <summary>
    /// Document_Type
    /// </summary>
    [DataFieldAttribute("Document_Type")]
    public class Document_Type : BaseModel
    {






        /// <summary>        ///         /// </summary>        [DataFieldAttribute("DName")]
        public string DName
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

        [DataFieldAttribute("DDescribe")]
        public string DDescribe
        {
            get;
            set;
        }






        /// <summary>        ///         /// </summary>        [DataFieldAttribute("DSort")]
        public Int32? DSort
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


        [DataFieldAttribute("FK_LibraryId")]
        public Int32? FK_LibraryId
        {
            get;
            set;
        }
    }
    #endregion
}
