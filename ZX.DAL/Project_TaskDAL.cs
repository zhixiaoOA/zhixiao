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
    public class Project_TaskDAL : DBBase<Project_Task>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Project_TaskModel> GetProject_TaskList(string key, int pageIndex, int pageSize, int status, int userId, int tsuccess, int tcancel, int tclosed, int assigned)
        {

            string sql = "Proc_GetProject_TaskList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("status", status);
            Pmts.Add("userId", userId);
            Pmts.Add("tsuccess", tsuccess);
            Pmts.Add("tcancel", tcancel);
            Pmts.Add("tclosed", tclosed);
            Pmts.Add("assigned", assigned);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());
            DataList<Project_TaskModel> pageList = new DataList<Project_TaskModel>(list, Pmts.ListPmts[9].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 分页获取数据列表 带条件查询
        /// <summary>
        /// 分页获取数据列表 带条件查询 L
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public List<Project_TaskModel> GetProject_TaskList_C(string key, int pageIndex, int pageSize, string conditions)
        {
            string sql = "Proc_GetProject_TaskList_C";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            Pmts.Add("conditions", conditions);//传入查询条件
            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());
            List<Project_TaskModel> pageList = new DataList<Project_TaskModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 分页获取数据列表 带条件查询
        /// <summary>
        /// 分页获取数据列表 带条件查询 L
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public List<Project_Task> GetProjectTaskList(string conditions)
        {
            string sql = @"SELECT TOP 1000 [Id]
                          ,[FK_ProjectId]
                          ,[TName]
                          ,[Assigned]
                          ,[Priority]
                          ,[TExpected]
                          ,[AsTime]
                          ,[PDesc]
                          ,[TState]
                          ,[TCC]
                          ,[Attach]
                          ,[TRemark]
                          ,[TSuccess]
                          ,[TSucTime]
                          ,[TCancel]
                          ,[TCancelTime]
                          ,[TClosed]
                          ,[TClosedTime]
                          ,[TClosedWhy]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[ParentId]
                          ,[EditCount]
                      FROM [ShanDongOA].[dbo].[Project_Task]
                      WHERE 1=1" + conditions;
            Pmts.ClearPmts();
            Pmts.Add("count", -1, ParameterDirection.Output);
            Pmts.Add("conditions", conditions);//传入查询条件
            List<Project_Task> list = Db.ExecuteToList<Project_Task>(sql, Pmts.ToArray());
            List<Project_Task> pageList = new DataList<Project_Task>(list, Pmts.ListPmts[0].Value.ToInt());
            return pageList;
        }
        #endregion

        #region 分页获取数据列表 返回List类型数据
        /// <summary>
        /// 分页获取数据列表 L
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public List<Project_TaskModel> GetProject_TaskToList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetProject_TaskList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());
            List<Project_TaskModel> pageList = new DataList<Project_TaskModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
        public List<Project_TaskModel> GetModelList()
        {
            string sql = "Proc_GetProject_TaskModelList";
            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());
            return list;
        }
        public DataList<Project_TaskModel> GetPersonalTask(string key, int pageIndex, int pageSize, int userId, int tsuccess, int assigned, int isMain)
        {
            string sql = "Proc_GetPersonalTaskList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("userId", userId);
            Pmts.Add("tsuccess", tsuccess);
            Pmts.Add("assigned", assigned);
            Pmts.Add("isMain", isMain);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());
            DataList<Project_TaskModel> pageList = new DataList<Project_TaskModel>(list, Pmts.ListPmts[7].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        public List<Project_TaskModel> GetProjectTaskListByParentId(int parentId)
        {
            string sql = "Proc_GetProjectTaskListByParentId";
            Pmts.ClearPmts();

            Pmts.Add("parentId", parentId);

            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());

            return list;
        }

        public List<Project_TaskModel> GetProjectTaskListByProjectId(int projectId)
        {
            string sql = "Proc_GetProjectTaskListByProjectId";
            Pmts.ClearPmts();

            Pmts.Add("projectId", projectId);

            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());

            return list;
        }


        /// <summary>
        /// 获取指定行数记录
        /// </summary>
        /// <param name="counts">获取数例如:5 ; 值为-1获取全部</param>
        /// <param name="strWhere">where 后条件</param>
        /// <param name="orderBy">排序字段 例如: id desc</param>
        /// <returns></returns>
        public List<Project_TaskModel> GetProjectTaskTopList(int counts, string strWhere, string orderBy)
        {
            string sql = "SELECT TOP(" + counts + ") DT.*,C.PName AS ProjectName,C.EndTime AS PEndTime FROM (SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (SELECT A.*FROM dbo.[Project_Task] AS A WHERE (A.ParentId IS NULL OR A.ParentId <= 0) ";

            if (counts == -1)
            {
                sql = "SELECT DT.*,C.PName AS ProjectName,C.EndTime AS PEndTime FROM (SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (SELECT A.*FROM dbo.[Project_Task] AS A WHERE (A.ParentId IS NULL OR A.ParentId <= 0) ";
            }

            if (strWhere.IsNotNullOrEmpty())
            {
                sql += " AND " + strWhere;
            }

            sql += " ) tampFirst LEFT JOIN dbo.Project_Task_Team AS B ON tampFirst.Id = B.FK_TaskId ) AS DT LEFT JOIN dbo.Project C ON DT.FK_ProjectId=C.Id ";

            if (orderBy.IsNotNullOrEmpty())
            {
                sql += " ORDER BY " + orderBy;
            }
            Pmts.ClearPmts();
            Pmts.Add("counts", counts);
            Pmts.Add("strWhere", strWhere);
            Pmts.Add("orderBy", orderBy);

            List<Project_TaskModel> list = Db.ExecuteToList<Project_TaskModel>(sql, null);

            return list;
        }


        /// <summary>
        /// 获取用户未完成的top任务
        /// </summary>
        /// <param name="counts">获取数例如:5 ; 值为-1获取全部</param>
        /// <returns></returns>
        public List<Project_TaskModel> GetProjectTaskNotFinish(int counts, int userId)
        {
            string sql = "SELECT ";
            if (counts != -1)
            {
                sql += " TOP(" + counts + ")";
            }

            sql += " DT.*,C.PName AS ProjectName,C.EndTime AS PEndTime FROM (SELECT tampFirst.*,B.ConsumTime,b.TheTime FROM (SELECT A.*FROM dbo.[Project_Task] AS A WHERE (A.ParentId IS NULL OR A.ParentId <= 0) AND TState IN (1,2,3) AND (Assigned=@userId OR TCC LIKE '%@userId%') ) tampFirst LEFT JOIN dbo.Project_Task_Team AS B ON tampFirst.Id = B.FK_TaskId ) AS DT LEFT JOIN dbo.Project C ON DT.FK_ProjectId=C.Id ORDER BY AsTime DESC";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);

            List<Project_TaskModel> list = Db.ExecuteToList<Project_TaskModel>(sql, Pmts.ToArray());
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
            string sql = @"select count(1) from Project_Task where TState IN(1,2,3) and Assigned=@userId and (ParentId=0 or ParentId is null)";
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
        /// <param name="userId">当前用户</param>
        /// <returns></returns>
        public DataList<Project_TaskModel> GetProjectTaskList(int pageIndex, int pageSize, string status, long userId)
        {
            string sql = "Proc_GetProjectTaskList";
            Pmts.ClearPmts();
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("status", status);
            Pmts.Add("userId", userId);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Project_TaskModel> list = Db.ExecuteProcToList<Project_TaskModel>(sql, Pmts.ToArray());
            DataList<Project_TaskModel> pageList = new DataList<Project_TaskModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        public List<ProjecTask_Out> getExcelList()
        {

            string sql = string.Empty;
            sql = @"select b.PName as PName,a.TName as TName,
            strTSucTime=CONVERT(VARCHAR(10),a.TSucTime,20),
                s.RealName as RealName,a.TState as TState,strCreateTime=CONVERT(VARCHAR(10),a.CreateTime,20),a.CreateAccount 
                from Project_Task a left join Project b on 
                a.FK_ProjectId=b.Id left join Sys_User s on a.TSuccess = s.Id";
            List<ProjecTask_Out> list = Db.ExecuteToList<ProjecTask_Out>(sql);
            return list;
        }
    }
}
