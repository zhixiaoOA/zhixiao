using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    public class FormJsonModel
    {
        public int Quarter { get; set; }
        public string Group { get; set; }
        public List<DataBase> Streses { get; set; }
    }
}
