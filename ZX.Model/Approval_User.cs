using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZX.Model
{
    public class Approval_User : BaseModel
    {
        [DataFieldAttribute("FK_ApprovalId")]
        public long? FK_ApprovalId
        {
            get;
            set;
        }

        [DataFieldAttribute("FK_CompanyPositionId")]
        public long? FK_CompanyPositionId
        {
            get;
            set;
        }

        [DataFieldAttribute("FlowName")]
        public string FlowName
        {
            get;
            set;
        }
    }
}