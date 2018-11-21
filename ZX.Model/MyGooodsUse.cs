using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region MyGooodsUse
    /// <summary>
    /// MyGooodsUse
    /// </summary>
    [DataFieldAttribute("MyGooodsUse")]
    public class MyGooodsUse : BaseModel
    {


        /// <summary>        ///         /// </summary>        [DataFieldAttribute("FK_UserId")]
        public Int64? FK_UserId
        {
            get;
            set;
        }



        /// <summary>        /// 烟名称        /// </summary>        [DataFieldAttribute("YanName")]
        public string YanName
        {
            get;
            set;
        }



        /// <summary>        /// 烟数量        /// </summary>        [DataFieldAttribute("YanCount")]
        public Int32? YanCount
        {
            get;
            set;
        }



        /// <summary>        /// 单位名称        /// </summary>        [DataFieldAttribute("YanUnitName")]
        public string YanUnitName
        {
            get;
            set;
        }






        /// <summary>        /// 酒名称        /// </summary>        [DataFieldAttribute("JiuName")]
        public string JiuName
        {
            get;
            set;
        }





        /// <summary>        /// 酒数量        /// </summary>        [DataFieldAttribute("JiuCount")]
        public Int32? JiuCount
        {
            get;
            set;
        }



        /// <summary>        /// 单位名称        /// </summary>        [DataFieldAttribute("JiuUnitName")]
        public string JiuUnitName
        {
            get;
            set;
        }


        /// <summary>        /// 其他名称        /// </summary>        [DataFieldAttribute("OtherName")]
        public string OtherName
        {
            get;
            set;
        }



        /// <summary>        /// 其他数量        /// </summary>        [DataFieldAttribute("OtherCount")]
        public Int32? OtherCount
        {
            get;
            set;
        }

        /// <summary>        /// 单位名称        /// </summary>        [DataFieldAttribute("OrtherUnitName")]
        public string OrtherUnitName
        {
            get;
            set;
        }


        /// <summary>        ///         /// </summary>        [DataFieldAttribute("FK_ApprovalUserId")]
        public Int64? FK_ApprovalUserId
        {
            get;
            set;
        }

        /// <summary>        ///         /// </summary>        [DataFieldAttribute("Status")]
        public Int32? Status
        {
            get;
            set;
        }


        /// <summary>        ///         /// </summary>        [DataFieldAttribute("AddTime")]
        public DateTime? AddTime
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

        /// <summary>
        /// 流程id
        /// </summary>
        [DataFieldAttribute("FK_ApprovalId")]
        public Int64? FK_ApprovalId
        {
            get;
            set;
        }
    }
    #endregion
}
