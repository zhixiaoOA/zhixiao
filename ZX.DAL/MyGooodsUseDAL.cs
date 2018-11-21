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
    public class MyGooodsUseDAL : DBBase<MyGooodsUse>
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
        public DataList<MyGooodsUseModel> GetMyGooodsUseList(string key, long userId, long appUserId, string beginTime, string endTime, string status, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMyGooodsUseList";
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
            List<MyGooodsUseModel> list = Db.ExecuteProcToList<MyGooodsUseModel>(sql, Pmts.ToArray());
            DataList<MyGooodsUseModel> pageList = new DataList<MyGooodsUseModel>(list, Pmts.ListPmts[8].Value.ToInt(), pageIndex, pageSize);
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
        public DataList<MyGooodsUseModel> GetMyGooodsUseList(string key, long userId, string beginTime, string endTime, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMyGooodsUseAppList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("beginTime", beginTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<MyGooodsUseModel> list = Db.ExecuteProcToList<MyGooodsUseModel>(sql, Pmts.ToArray());
            DataList<MyGooodsUseModel> pageList = new DataList<MyGooodsUseModel>(list, Pmts.ListPmts[6].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 根据id获取数据
        /// <summary>
        /// 获取Model
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>MyGooodsUseModel</returns>
        public MyGooodsUseModel GetModelById(int id)
        {
            string sql = @"SELECT A.*,b1.RealName,DeptName = e.DName,PositionName = ISNULL(f.Name, '普通员工') 
            FROM dbo.MyGooodsUse AS A
            left join Sys_User as b1 on A.FK_UserId=b1.Id
            LEFT JOIN dbo.Sys_Dept AS e ON b1.Fk_DeptId = e.Id
            LEFT JOIN dbo.CompanyPosition AS f ON b1.FK_CompanyPositionId = f.Id
            WHERE A.Id =@id ";
            Pmts.ClearPmts();
            Pmts.Add("id", id);
            return Db.ExecuteToSingle<MyGooodsUseModel>(sql, Pmts.ToArray());
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
        public List<MyGooodsUseModel> GetModelListByWhere(int userId, string startTime, string endTime)
        {
            string sql = @"SELECT tmp.*,RealName=b1.RealName,PositionName = ISNULL(f.Name, '普通员工'),DeptName = e.DName FROM
                        (
                        SELECT * FROM  dbo.MyGooodsUse 
                        WHERE FK_UserId=@userId AND Status=2 AND (@startTime='' OR AddTime>=@startTime) AND (@endTime='' OR AddTime<=@endTime)) tmp 
            LEFT JOIN Sys_User as b1 on tmp.FK_UserId=b1.Id
            LEFT JOIN dbo.Sys_Dept AS e ON b1.Fk_DeptId = e.Id
            LEFT JOIN dbo.CompanyPosition AS f ON b1.FK_CompanyPositionId = f.Id
            ORDER BY Id DESC";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            Pmts.Add("startTime", startTime);
            Pmts.Add("endTime", endTime);
            return Db.ExecuteToList<MyGooodsUseModel>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
