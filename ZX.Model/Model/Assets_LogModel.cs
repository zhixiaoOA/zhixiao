using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Model
{
    public class Assets_LogModel : Assets_Log
    {
        public string AName { get; set; }

        public string ACode { get; set; }


        public string StrCreateTime { get; set; }

        public string StrUpdateTime { get; set; }
    }
}
