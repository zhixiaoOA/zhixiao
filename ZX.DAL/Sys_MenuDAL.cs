using System;
using System.Collections.Generic;
using System.Data;
using ZX.Tools;
using ZX.Model;

namespace ZX.DAL
{
    public class Sys_MenuDAL : DBBase<Sys_Menu>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Sys_MenuModel> GetSys_MenuList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetSys_MenuList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Sys_MenuModel> list = Db.ExecuteProcToList<Sys_MenuModel>(sql, Pmts.ToArray());
            DataList<Sys_MenuModel> pageList = new DataList<Sys_MenuModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }

        #endregion

        #region 存储过程获取菜单
        public List<Sys_Menu> GetUserMenu(int userId)
        {
            string sql = "Proc_GetUserMenu";
            Pmts.ClearPmts();
            Pmts.Add("UserId", userId);
            List<Sys_Menu> list = Db.ExecuteProcToList<Sys_Menu>(sql, Pmts.ToArray());
            return list;
        }
        #endregion
    }
}
