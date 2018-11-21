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
    public class Temporary_TaskBLL : BaseBLL<Temporary_Task, Temporary_TaskDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Temporary_TaskModel> GetTemporary_TaskList(string key, int pageIndex, int pageSize, int userId, int tsuccess, int assigned, int isParent)
        {
            return new Temporary_TaskDAL().GetTemporary_TaskList(key, pageIndex, pageSize, userId, tsuccess, assigned, isParent);
        }
        #endregion

        #region 分页获取数据列表

        /// <summary>
        /// 
        /// </summary>
        /// <param name="counts">获取数例如:5; 值为-1获取全部</param>
        /// <param name="strWhere">where 后条件</param>
        /// <param name="orderBy">排序字段 例如: id desc</param>
        /// <returns></returns>
        public static List<Temporary_TaskModel> GetTemporary_TaskBlockList(int counts, string strWhere, string orderBy)
        {
            return new Temporary_TaskDAL().GetTemporary_TaskBlockList(counts, strWhere, orderBy);
        }
        #endregion

        public static List<Temporary_TaskModel> GetTemporaryTaskListByParentId(int parentId)
        {
            return new Temporary_TaskDAL().GetTemporaryTaskListByParentId(parentId);
        }

        #region 获取用户未完成的任务数
        /// <summary>
        /// 获取用户未完成的任务数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetWwcCount(long userId)
        {
            return new Temporary_TaskDAL().GetWwcCount(userId);
        }
        #endregion

        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="status">状态</param>
        /// <param name="userId">当前用户id</param>
        /// <returns></returns>
		public static DataList<Temporary_TaskModel> GetTemporaryTaskList(int pageIndex, int pageSize, string status, long userId)
        {
            return new Temporary_TaskDAL().GetTemporaryTaskList(pageIndex, pageSize, status, userId);
        }
        #endregion
    }
}
