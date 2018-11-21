using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    public class ProjecTask_Out
    {
        public string PName
        {
            get;
            set;
        }
        public string TName
        {
            get;
            set;
        }
        public string strTSucTime
        {
            get;
            set;
        }
        public string RealName { get; set; }
        public int TState { get; set; }

        public string strTState { get; set; }
        public string strCreateTime { get; set; }
    }
}
