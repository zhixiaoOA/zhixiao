using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Tools;
using ZX.DAL;
using ZX.Model;

namespace ZX.DAL
{
    public class MyClockDAL : DBBase<MyClock>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">事由</param>
        /// <param name="userId">申请人id</param>
        /// <param name="appUserId">审核人id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<MyClockModel> GetMyClockList(string key, long userId, long appUserId, string beginTime, string endTime, string status, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetMyClockList";
			Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("appUserId", appUserId);
            Pmts.Add("beginTime", beginTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("status", status);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<MyClockModel> list = Db.ExecuteProcToList<MyClockModel>(sql, Pmts.ToArray());
			DataList<MyClockModel> pageList = new DataList<MyClockModel>(list, Pmts.ListPmts[8].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
        #endregion

        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="userId">审核人id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<MyClockModel> GetMyClockList(string key, long userId, string beginTime, string endTime, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMyClockAppList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("beginTime", beginTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<MyClockModel> list = Db.ExecuteProcToList<MyClockModel>(sql, Pmts.ToArray());
            DataList<MyClockModel> pageList = new DataList<MyClockModel>(list, Pmts.ListPmts[6].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 根据id获取数据
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MyClockModel GetModelById(long id)
        {
            string sql = @"select a.*,b.RealName,DeptName = c.DName,PositionName = ISNULL(d.Name, '普通员工') 
            from MyClock as a
            left join Sys_User as b on A.FK_UserId=b.Id
            LEFT JOIN dbo.Sys_Dept AS c ON b.Fk_DeptId = c.Id
            LEFT JOIN dbo.CompanyPosition AS d ON b.FK_CompanyPositionId = d.Id
            where a.Id=@id";
            Pmts.ClearPmts();
            Pmts.Add("id", id);
            return Db.ExecuteToSingle<MyClockModel>(sql, Pmts.ToArray());
        }
        #endregion


        #region 根据条件获取数据
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="startTime">时间段-自</param>
        /// <param name="endTime">时间段-至</param>
        /// <returns>My_WorkModel</returns>
        public List<MyClockModel> GetModelListByWhere(int userId, string startTime, string endTime)
        {
            string sql = @"SELECT * FROM  dbo.MyClock 
WHERE FK_UserId=@userId AND Status=2 AND (@startTime='' OR AddTime>=@startTime) AND (@endTime='' OR AddTime<=@endTime) ORDER BY Id DESC";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            Pmts.Add("startTime", startTime);
            Pmts.Add("endTime", endTime);
            return Db.ExecuteToList<MyClockModel>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
