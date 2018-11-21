using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model.Model
{
    public class ApprovalsModel : BaseModel
    {
        public string Title { get; set; }
        public string ApplyAction { get; set; }
        public string ADesc { get; set; }
        public string Status { get; set; }
        public DateTime? AddTime { get; set; }
        public string FlowName { get; set; }
        public string RealName { get; set; }
    }
}
