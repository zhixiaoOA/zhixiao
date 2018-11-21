using System;
using System.Collections.Generic;
using System.Data;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class CompanyPositionDAL : DBBase<CompanyPosition>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<CompanyPositionModel> GetCompanyPositionList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetCompanyPositionList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<CompanyPositionModel> list = Db.ExecuteProcToList<CompanyPositionModel>(sql, Pmts.ToArray());
            DataList<CompanyPositionModel> pageList = new DataList<CompanyPositionModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
