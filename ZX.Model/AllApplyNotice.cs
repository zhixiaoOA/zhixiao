using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    public class AllApplyNotice : BaseModel
    {

        [DataFieldAttribute("Title")]
        public string Title
        {
            get;
            set;
        }

        [DataFieldAttribute("ApplyAction")]
        public string ApplyAction
        {
            get;
            set;
        }
        [DataFieldAttribute("ADesc")]
        public string ADesc
        {
            get;
            set;
        }

        [DataFieldAttribute("AUId")]
        public Int64? AUId
        {
            get;
            set;
        }
        [DataFieldAttribute("CreateTime")]
        public DateTime? CreateTime
        {
            get;
            set;
        }

        [DataFieldAttribute("CurrentState")]
        public Int32? CurrentState
        {
            get;
            set;
        }
        [DataFieldAttribute("FK_ApprovalId")]
        public Int64? FK_ApprovalId
        {
            get;
            set;
        }

        [DataFieldAttribute("FK_CompanyPositionId")]
        public Int64? FK_CompanyPositionId
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
        [DataFieldAttribute("RealName")]
        public string RealName
        {
            get;
            set;
        }
    }
}
