using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    #region My_AttendanceCount
    /// <summary>
    /// My_AttendanceCount
    /// </summary>
    [DataFieldAttribute("My_AttendanceCount")]
    public class My_AttendanceCount : BaseModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        [DataFieldAttribute("Date")]
        public string Date
        {
            get;
            set;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataFieldAttribute("URealName")]
        public string URealName
        {
            get;
            set;
        }
                /// <summary>        /// 迟到次数        /// </summary>        [DataFieldAttribute("LateNo")]
        public string LateNo
        {
            get;
            set;
        }

        /// <summary>        /// 早退次数        /// </summary>        [DataFieldAttribute("LeaveEarlyNo")]
        public string LeaveEarlyNo
        {
            get;
            set;
        }

        /// <summary>
        /// 请假次数
        /// </summary>        [DataFieldAttribute("AskNO")]        public string AskNO
        {
            get;
            set;
        }

        /// <summary>
        /// 旷工次数
        /// </summary>
        [DataFieldAttribute("AbsenteeismNo")]
        public string AbsenteeismNo
        {
            get;
            set;
        }

        /// <summary>        /// 公差次数        /// </summary>        [DataFieldAttribute("ToleranceNo")]
        public string ToleranceNo
        {
            get;
            set;
        }

        /// <summary>
        /// 外出考勤次数
        /// </summary>
        [DataFieldAttribute("AttendanceOutNo")]
        public string AttendanceOutNo
        {
            get;
            set;
        }

        /// <summary>
        /// 应出勤天数
        /// </summary>
        [DataFieldAttribute("DueDays")]
        public string DueDays
        {
            get;
            set;
        }

        /// <summary>
        /// 实际出勤天数
        /// </summary>
        [DataFieldAttribute("ActualAttendanceNo")]
        public string ActualAttendanceNo
        {
            get;
            set;
        }
#endregion
    }
}