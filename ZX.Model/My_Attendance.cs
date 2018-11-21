using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region My_Attendance
    /// <summary>
    /// My_Attendance
    /// </summary>
    [DataFieldAttribute("My_Attendance")]
    public class My_Attendance : BaseModel
    {
		        [DataFieldAttribute("URealname")]		public string URealname		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("StartTime")]		public DateTime? StartTime		{			get;			set;		}		/// <summary>		/// 0：项目文档库 10：自定义文档库		/// </summary>		[DataFieldAttribute("EndTime")]		public DateTime? EndTime		{			get;			set;		}        /// <summary>
        /// 下午上班时间
        /// </summary>        [DataFieldAttribute("PmStartTime")]        public DateTime? PmStartTime
        {
            get;
            set;
        }
        //考勤日期
        public string ADate { get; set; }

        /// <summary>
        /// 下午下班时间
        /// </summary>
        [DataFieldAttribute("PmEndTime")]
        public DateTime? PmEndTime
        {
            get;
            set;
        }

        /// <summary>        ///         /// </summary>        [DataFieldAttribute("ADesc")]		public string ADesc		{			get;			set;		}


        [DataFieldAttribute("ANumber")]
        public string ANumber
        {
            get;
            set;
        }


        [DataFieldAttribute("ComparisonType")]
        public string ComparisonType
        {
            get;
            set;
        }
        [DataFieldAttribute("CardNo")]
        public string CardNo
        {
            get;
            set;
        }
        

        /// <summary>        ///         /// </summary>        [DataFieldAttribute("CreateUserId")]		public Int32? CreateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateTime")]		public DateTime? CreateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateUserId")]		public Int32? UpdateUserId		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateTime")]		public DateTime? UpdateTime		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("CreateAccount")]		public string CreateAccount		{			get;			set;		}		/// <summary>		/// 		/// </summary>		[DataFieldAttribute("UpdateAccount")]		public string UpdateAccount		{			get;			set;		}

        /// <summary>        /// 0：正常 10：补录 20：请假 30：加班 40：调休 50：出差 60：外出        /// </summary>        [DataFieldAttribute("Status")]
        public Int32? Status
        {
            get;
            set;
        }
    }
    #endregion
}
