using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Tools;
using ZX.DAL;
using ZX.Model;

namespace ZX.DAL
{
    public class ApplyFlowDAL : DBBase<ApplyFlow>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="typeId">申请类型</param>
        /// <param name="name">申请人姓名</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<ApplyFlowModel> GetApplyFlowList(long typeId, string name, string beginTime, string endTime, int status, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetApplyFlowList";
            Pmts.ClearPmts();
            Pmts.Add("typeId", typeId);
            Pmts.Add("name", name);
            Pmts.Add("beginTime", beginTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("status", status);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<ApplyFlowModel> list = Db.ExecuteProcToList<ApplyFlowModel>(sql, Pmts.ToArray());
            DataList<ApplyFlowModel> pageList = new DataList<ApplyFlowModel>(list, Pmts.ListPmts[7].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
