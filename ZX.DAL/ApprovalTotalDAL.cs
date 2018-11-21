using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Tools;
using ZX.DAL;
using ZX.Model;
using ZX.Model.Model;

namespace ZX.DAL
{
    public class ApprovalTotalDAL : DBBase<ApprovalsModel>
    {
        public DataList<ApprovalsModel> GetAllApprovls(string RealName, string beginTime, string endTime, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetAllApprovls";
            //Pmts.ClearPmts();
            //Pmts.Add("RealName", RealName);
            //Pmts.Add("beginTime", beginTime);
            //Pmts.Add("endTime", endTime);
            //Pmts.Add("pageIndex", pageIndex);
            //Pmts.Add("pageSize", pageSize);
            //Pmts.Add("count", -1, ParameterDirection.Output);
            List<ApprovalsModel> list = Db.ExecuteProcToList<ApprovalsModel>(sql, Pmts.ToArray());
            DataList<ApprovalsModel> pageList = new DataList<ApprovalsModel>(list, Pmts.ListPmts[5].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
    }
}
