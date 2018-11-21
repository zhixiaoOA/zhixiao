using System;
using System.Collections.Generic;
using System.Data;
using ZX.Tools;
using ZX.Model;

namespace ZX.DAL
{
    public class Sys_RoleDAL : DBBase<Sys_Role>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Sys_RoleModel> GetSys_RoleList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetSys_RoleList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Sys_RoleModel> list = Db.ExecuteProcToList<Sys_RoleModel>(sql, Pmts.ToArray());
            DataList<Sys_RoleModel> pageList = new DataList<Sys_RoleModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 获取通用角色ID
        /// <summary>
        /// 获取通用角色id
        /// </summary>
        /// <returns>Sys_RoleModel</returns>
        public Sys_RoleModel getSysRoleID()
        {
            string sql = "select * from Sys_Role where RName='通用角色'";
            Sys_RoleModel role = Db.ExecuteToSingle<Sys_RoleModel>(sql);
            return role;
        }
        #endregion
    }
}
