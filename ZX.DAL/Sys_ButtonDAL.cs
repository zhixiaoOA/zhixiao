using System;
using System.Collections.Generic;
using System.Data;
using ZX.Tools;
using ZX.Model;

namespace ZX.DAL
{
    public class Sys_ButtonDAL : DBBase<Sys_Button>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Sys_ButtonModel> GetSys_ButtonList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetSys_ButtonList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Sys_ButtonModel> list = Db.ExecuteProcToList<Sys_ButtonModel>(sql, Pmts.ToArray());
            DataList<Sys_ButtonModel> pageList = new DataList<Sys_ButtonModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
