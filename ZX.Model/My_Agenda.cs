using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region My_Agenda
    /// <summary>
    /// My_Agenda
    /// </summary>
    [DataFieldAttribute("My_Agenda")]
    public class My_Agenda : BaseModel
    {






        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ADate")]
        public DateTime? ADate
        {
            get;
            set;
        }






        /// <summary>
        /// 0-待定，1-非待定
        /// </summary>
        [DataFieldAttribute("AIsUndetermined")]
        public Int32? AIsUndetermined
        {
            get;
            set;
        }






        /// <summary>
        /// 类型 0-自定义，10-项目任务，
        /// </summary>
        [DataFieldAttribute("AType")]
        public Int32? AType
        {
            get;
            set;
        }






        /// <summary>
        /// 字典值 一般，最高，较高，最低
        /// </summary>
        [DataFieldAttribute("APriority")]
        public Int32? APriority
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
        [DataFieldAttribute("AAssigned")]
        public Int64? AAssigned
        {
            get;
            set;
        }






        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ADesc")]
        public string ADesc
        {
            get;
            set;
        }






        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ARemark")]
        public string ARemark
        {
            get;
            set;
        }







        /// <summary>
        /// 字典数据 :待办状态
        /// </summary>
        [DataFieldAttribute("AStatus")]
        public Int32? AStatus
        {
            get;
            set;
        }






        /// <summary>
        /// 06:30
        /// </summary>
        [DataFieldAttribute("AStartmmhh")]
        public string AStartmmhh
        {
            get;
            set;
        }






        /// <summary>
        /// 06:30
        /// </summary>
        [DataFieldAttribute("AEndmmhh")]
        public string AEndmmhh
        {
            get;
            set;
        }






        /// <summary>
        /// 0-设定起止时间，1-暂时不设定起止时间
        /// </summary>
        [DataFieldAttribute("AIsNotSet")]
        public Int32? AIsNotSet
        {
            get;
            set;
        }






        /// <summary>
        /// 0-私人事务，1-非私人事务
        /// </summary>
        [DataFieldAttribute("AIsPlivate")]
        public Int32? AIsPlivate
        {
            get;
            set;
        }













        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("AFinishUserId")]
        public Int64? AFinishUserId
        {
            get;
            set;
        }












        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("AFinishTime")]
        public DateTime? AFinishTime
        {
            get;
            set;
        }












        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ACloseUserId")]
        public Int64? ACloseUserId
        {
            get;
            set;
        }












        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ACloseTime")]
        public DateTime? ACloseTime
        {
            get;
            set;
        }












        /// <summary>
        /// 
        /// </summary>
        [DataFieldAttribute("ACloseReason")]
        public string ACloseReason
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
        [DataFieldAttribute("CreateUserId")]
        public Int32? CreateUserId
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
        [DataFieldAttribute("UpdateTime")]
        public DateTime? UpdateTime
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
        [DataFieldAttribute("UpdateAccount")]
        public string UpdateAccount
        {
            get;
            set;
        }






        /// <summary>
        /// 外表Id,例如 类型为：项目任务，则此字段存项目任务的id
        /// </summary>
        [DataFieldAttribute("FK_Id")]
        public Int64? FK_Id
        {
            get;
            set;
        }

    }
    #endregion
}
