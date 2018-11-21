using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    public class Sys_UserModel : Sys_User
    {
        public string DeptName { get; set; }
        public string PositionName { get; set; }

        public decimal GreenPercent { get; set; }//绿灯占百分比

        public decimal RedPercent { get; set; }//红灯占百分比
    }
}
