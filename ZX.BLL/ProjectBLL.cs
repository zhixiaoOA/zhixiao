using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.DAL;
using ZX.Model;
using ZX.Tools;

namespace ZX.BLL
{
    public class ProjectBLL : BaseBLL<Project, ProjectDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<ProjectModel> GetProjectList(string key, int userId, int status, int pageIndex, int pageSize)
        {
            return new ProjectDAL().GetProjectList(key, userId, status, pageIndex, pageSize);
        }
        #endregion

        /// <summary>
        /// 获取用户项目列表 不包括已删除项目
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public static List<Project> GetProjectCorrelationListByUserId(int userId)
        {
            return new ProjectDAL().GetProjectCorrelationListByUserId(userId);
        }
    }
}
