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
    public class Project_TaskBLL : BaseBLL<Project_Task, Project_TaskDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Project_TaskModel> GetProject_TaskList(string key, int pageIndex, int pageSize, int status, int userId, int tsuccess, int tcancel, int tclosed, int assigned)
        {
            return new Project_TaskDAL().GetProject_TaskList(key, pageIndex, pageSize, status, userId, tsuccess, tcancel, tclosed, assigned);
        }
        #endregion
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表 L 
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public static List<Project_TaskModel> GetProject_TaskList_C(string key, int pageIndex, int pageSize, string conditions)
        {
            return new Project_TaskDAL().GetProject_TaskList_C(key, pageIndex, pageSize, conditions);
        }
        #endregion

        #region 根据条件获取条件符合的全部数据
        /// <summary>
        /// 根据条件获取条件符合的全部数据 L ExecuteToList
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public static List<Project_Task> GetProjectTaskList(string conditions)
        {
            return new Project_TaskDAL().GetProjectTaskList(conditions);
        }
        #endregion


        #region 分页获取数据列表 返回List类型
        /// <summary>
        /// 分页获取数据列表 L
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static List<Project_TaskModel> GetProject_TaskToList(string key, int pageIndex, int pageSize)
        {
            return new Project_TaskDAL().GetProject_TaskToList(key, pageIndex, pageSize);
        }
        #endregion

        public List<Project_TaskModel> GetModelList()
        {
            List<Project_TaskModel> list = new List<Project_TaskModel>();

            return list;
        }
        /// <summary>
        /// 个人空间，个人任务查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <param name="tsuccess"></param>
        /// <param name="assigned"></param>
        /// <param name="isMain">1—查主任务，-1—查所有</param>
        /// <returns></returns>
        public static DataList<Project_TaskModel> GetPersonalTask(string key, int pageIndex, int pageSize, int userId, int tsuccess, int assigned, int isMain)
        {
            return new Project_TaskDAL().GetPersonalTask(key, pageIndex, pageSize, userId, tsuccess, assigned, isMain);
        }
        public static List<Project_TaskModel> GetProjectTaskListByParentId(int parentId)
        {
            return new Project_TaskDAL().GetProjectTaskListByParentId(parentId);
        }

        public static List<Project_TaskModel> GetProjectTaskListByProjectId(int parentId)
        {
            return new Project_TaskDAL().GetProjectTaskListByProjectId(parentId);
        }


        /// <summary>
        /// 获取指定行数记录
        /// </summary>
        /// <param name="counts">获取数例如:5; 值为-1获取全部</param>
        /// <param name="strWhere">where 后条件</param>
        /// <param name="orderBy">排序字段 例如: id desc</param>
        /// <returns></returns>
        public static List<Project_TaskModel> GetProjectTaskTopList(int counts, string strWhere, string orderBy)
        {
            return new Project_TaskDAL().GetProjectTaskTopList(counts, strWhere, orderBy);
        }


        /// <summary>
        /// 获取用户未完成的top任务
        /// </summary>
        /// <param name="counts">获取数例如:5 ; 值为-1获取全部</param>
        /// <returns></returns>
        public static List<Project_TaskModel> GetProjectTaskNotFinish(int counts, int userId)
        {
            return new Project_TaskDAL().GetProjectTaskNotFinish(counts, userId);
        }

        #region 获取用户未完成的任务数
        /// <summary>
        /// 获取用户未完成的任务数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetWwcCount(long userId)
        {
            return new Project_TaskDAL().GetWwcCount(userId);
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
        public static DataList<Project_TaskModel> GetProjectTaskList(int pageIndex, int pageSize, string status, long userId)
        {
            return new Project_TaskDAL().GetProjectTaskList(pageIndex, pageSize, status, userId);
        }
        #endregion

        public static List<ProjecTask_Out> getExcelList()
        {
            return new Project_TaskDAL().getExcelList();
        }

    }
}
