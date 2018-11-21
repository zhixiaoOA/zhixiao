using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    public class Temporary_TaskModel : Temporary_Task
    {
        public int ConsumTime
        {
            get;
            set;
        }
        public int TheTime
        {
            get;
            set;
        }
        public string StatusName { get; set; }
    }
}
