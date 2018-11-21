using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    /// <summary>
    /// model基类
    /// </summary>
    [Serializable]
    public class BaseModel
    {
        [DataFieldAttribute("Id", "pk")]
        public Int64 Id { get; set; }
    }
}
