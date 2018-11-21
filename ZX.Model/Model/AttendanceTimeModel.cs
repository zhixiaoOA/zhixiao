using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model.Model
{
    public class AttendanceTimeModel:AttendanceTime
    {
        public int Count { get; set; }

        public string StrAmStartTime { get; set; }
        public string StrAmEndTime { get; set; }
        public string StrPmStartTime { get; set; }
        public string StrPmEndTime { get; set; }
    }
}
