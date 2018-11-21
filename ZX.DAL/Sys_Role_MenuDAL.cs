using System;
using System.Collections.Generic;
using System.Data;
using ZX.Tools;
using ZX.Model;

namespace ZX.DAL
{
    public class Sys_Role_MenuDAL : DBBase<Sys_Role_Menu>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Sys_Role_MenuModel> GetSys_Role_MenuList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetSys_Role_MenuList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Sys_Role_MenuModel> list = Db.ExecuteProcToList<Sys_Role_MenuModel>(sql, Pmts.ToArray());
            DataList<Sys_Role_MenuModel> pageList = new DataList<Sys_Role_MenuModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
