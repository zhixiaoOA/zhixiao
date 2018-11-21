using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model
{
    public class AssetsUseModel : AssetsUse
    {
        public string FlowName { get; set; }
        public string ApplyUserName { get; set; }
        public string RealName { get; set; }
        public string DeptName { get; set; }
        public string PositionName { get; set; }
    }
}
