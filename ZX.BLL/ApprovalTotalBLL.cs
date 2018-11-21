using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using ZX.Model;
using ZX.DAL;
using ZX.Model.Model;

namespace ZX.BLL
{
    public class ApprovalTotalBLL : BaseBLL<ApprovalsModel, ApprovalTotalDAL>
    {
        public static DataList<ApprovalsModel> GetAllApprovls(string RealName, string beginTime, string endTime, int pageIndex, int pageSize)
        {
            return new ApprovalTotalDAL().GetAllApprovls(RealName, beginTime, endTime, pageIndex, pageSize);
        }
    }
}
