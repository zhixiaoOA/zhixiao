using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class Temporary_TaskDAL : DBBase<Temporary_Task>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Temporary_TaskModel> GetTemporary_TaskList(string key, int pageIndex, int pageSize, int userId, int tsuccess, int assigned, int isParent)
        {
            string sql = "Proc_GetTemporary_TaskList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("userId", userId);
            Pmts.Add("tsuccess", tsuccess);
            Pmts.Add("assigned", assigned);
            Pmts.Add("isParent", isParent);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Temporary_TaskModel> list = Db.ExecuteProcToList<Temporary_TaskModel>(sql, Pmts.ToArray());
            DataList<Temporary_TaskModel> pageList = new DataList<Temporary_TaskModel>(list, Pmts.ListPmts[7].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion


        public List<Temporary_TaskModel> GetTemporaryTaskListByParentId(int parentId)
        {
            string sql = "Proc_GetTemporaryTaskListByParentId";
            Pmts.ClearPmts();

            Pmts.Add("parentId", parentId);

            List<Temporary_TaskModel> list = Db.ExecuteProcToList<Temporary_TaskModel>(sql, Pmts.ToArray());

            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="counts">获取数例如:5; 值为-1获取全部</param>
        /// <param name="strWhere">where 后条件</param>
        /// <param name="orderBy">排序字段 例如: id desc</param>
        /// <returns></returns>
        public List<Temporary_TaskModel> GetTemporary_TaskBlockList(int counts, string strWhere, string orderBy)
        {
            string sql = "SELECT ";
            if (counts != -1)
            {
                sql += " TOP(" + counts + ") ";
            }

            sql += "* FROM (SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (SELECT Temporary_Task.*,ROW_NUMBER() OVER(ORDER BY Temporary_Task.Id DESC) AS RowIndex FROM dbo.[Temporary_Task] WHERE  1=1 and " + strWhere + " ) tampFirst LEFT JOIN dbo.Temporary_Task_Team AS B ON tampFirst.Id=B.FK_TemporaryTaskId) AS DT ";

            sql += " order by " + orderBy;

            Pmts.ClearPmts();
            Pmts.Add("counts", counts);
            Pmts.Add("strWhere", strWhere);
            Pmts.Add("orderBy", orderBy);

            List<Temporary_TaskModel> list = Db.ExecuteToList<Temporary_TaskModel>(sql, null);

            return list;
        }

        #region 获取用户未完成的任务数
        /// <summary>
        /// 获取用户未完成的任务数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetWwcCount(long userId)
        {
            string sql = @"select count(1) from Temporary_Task where TState IN(1,2,3) and Assigned=@userId and (ParentId=0 or ParentId is null)";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            return Db.GetSingle(sql, Pmts.ToArray()).ToInt();
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
		public DataList<Temporary_TaskModel> GetTemporaryTaskList(int pageIndex, int pageSize, string status, long userId)
        {
            string sql = "Proc_GetTemporaryTaskList";
            Pmts.ClearPmts();
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("status", status);
            Pmts.Add("userId", userId);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Temporary_TaskModel> list = Db.ExecuteProcToList<Temporary_TaskModel>(sql, Pmts.ToArray());
            DataList<Temporary_TaskModel> pageList = new DataList<Temporary_TaskModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
