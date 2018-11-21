using System;
using System.Collections.Generic;
using System.Data;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class ProjectDAL : DBBase<Project>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<ProjectModel> GetProjectList(string key, int userId, int status, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetProjectList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("status", status);
            Pmts.Add("userId", userId);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<ProjectModel> list = Db.ExecuteProcToList<ProjectModel>(sql, Pmts.ToArray());
            DataList<ProjectModel> pageList = new DataList<ProjectModel>(list, Pmts.ListPmts[5].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 获取用户项目列表
        /// <summary>
        /// 获取用户项目列表 不包括已删除项目
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public List<Project> GetProjectCorrelationListByUserId(int userId)
        {
            string sql = "Proc_GetProjectCorrelationByUserIdList";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            List<Project> list = Db.ExecuteProcToList<Project>(sql, Pmts.ToArray());
            return list;
        }
        #endregion
    }
}
