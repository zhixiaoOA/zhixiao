using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model
{
    public class My_AttendanceModel : My_Attendance
    {
        public string StrStartTime { get; set; }

        public string StrEndTime { get; set; }

        public string StrPmStartTime { get; set; }

        public string StrPmEndTime { get; set; }
    }
}
