using System;
using System.Collections.Generic;
using System.Data;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class Sys_DeptDAL : DBBase<Sys_Dept>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<Sys_DeptModel> GetSys_DeptList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetSys_DeptList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Sys_DeptModel> list = Db.ExecuteProcToList<Sys_DeptModel>(sql, Pmts.ToArray());
            DataList<Sys_DeptModel> pageList = new DataList<Sys_DeptModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
