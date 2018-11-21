using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    #region Assets
    /// <summary>
    /// Assets
    /// </summary>
    [DataFieldAttribute("Assets")]
    public class Assets : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ACode")]
        public string ACode
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("AName")]
        public string AName
        {
            get;
            set;
        }
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
        /// 0：有效 10：挂起 20：完成 30：删除
        /// </summary>
        [DataFieldAttribute("TRemark")]
        public string TRemark
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("TSort")]
        public Int32? TSort
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
        [DataFieldAttribute("QrCodeUrl")]
        public string QrCodeUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ImageUrl")]
        public string ImageUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ANum")]
        public Int32? ANum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("APrice")]
        public decimal? APrice
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UsePeriod")]
        public Int32? UsePeriod
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UnitName")]
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("NatureOfAssets")]
        public string NatureOfAssets
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("IsWhether")]
        public Int32? IsWhether
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
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("Degree")]
        public string Degree
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("UsePeriodUnit")]
        public string UsePeriodUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("FK_SupplierId")]
        public Int64? FK_SupplierId
        {
            get;
            set;
        }

        /// <summary>
        /// 购买日期
        /// </summary>
        [DataFieldAttribute("PurchaseDay")]
        public DateTime? PurchaseDay
        {
            get;
            set;
        }

        /// <summary>
        /// 使用部门
        /// </summary>
        [DataFieldAttribute("DeptName")]
        public string DeptName
        {
            get;
            set;
        }
    }
    #endregion
}
