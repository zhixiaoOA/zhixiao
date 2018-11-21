using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    public class Project_TaskModel : Project_Task
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

        public string ProjectName
        {
            get;
            set;
        }

        /// <summary>
        /// 项目截止时间
        /// </summary>
        public DateTime? PEndTime
        {
            get;
            set;
        }
    }
}
