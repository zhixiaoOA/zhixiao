using System;
using System.Text;
using ZX.Model;
using System.Collections.Generic;

namespace ZX.Model
{
    public class MyTravelReimbursementModel : MyTravelReimbursement
    {
        public List<MyTravelReimbursementDetail> Details
        {
            get;
            set;
        }
        public string FlowName { get; set; }
        public string ApplyUserName { get; set; }
        public string RealName { get; set; }
        public string DeptName { get; set; }
        public string PositionName { get; set; }
        public long PositionId { get; set; }
    }
}
