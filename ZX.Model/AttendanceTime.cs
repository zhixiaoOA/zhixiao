using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
    [DataFieldAttribute("AttendanceTime")]
    public class AttendanceTime : BaseModel
    {

        [DataFieldAttribute("Date")]
        public string Date
        {
            get;
            set;
        }
        [DataFieldAttribute("ApplicableObject")]
        public string ApplicableObject
        {
            get;
            set;
        }


        [DataFieldAttribute("WorkDay")]
        public string WorkDay
        {
            get;
            set;
        }

        [DataFieldAttribute("AmStartTime")]
        public DateTime? AmStartTime {
            get;
            set;
        }

        [DataFieldAttribute("AmEndTime")]
        public DateTime? AmEndTime
        {
            get;
            set;
        }

        [DataFieldAttribute("PmStartTime")]
        public DateTime? PmStartTime
        {
            get;
            set;
        }

        [DataFieldAttribute("PmEndTime")]
        public DateTime? PmEndTime
        {
            get;
            set;
        }

        [DataFieldAttribute("WorkDays")]
        public string WorkDays
        {
            get;
            set;
        }


    }
}
