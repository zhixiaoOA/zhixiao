using ZX.Model;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using ZX.Model.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class MapPositionDAL : DBBase<MapPosition>
    {
        public List<MapPositionModel> GetDayByMonth(int UserId, string Month)
        {
            string sql = "SELECT DISTINCT CONVERT(varchar(100), CreateTime, 23) as SignDate from MapPosition where CreateUserId = @UserId and CONVERT(VarChar(7), CreateTime, 120) = @Day ORDER BY SignDate DESC";
            SqlParameter[] param = {
                new SqlParameter("@UserId",SqlDbType.Int),
                new SqlParameter("@Day",SqlDbType.VarChar)
            };
            param[0].Value = UserId;
            param[1].Value = Month;
            List<MapPositionModel> days = Db.ExecuteToList<MapPositionModel>(sql, param.ToArray());
            return days;
        }
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<MapPositionModel> GetMy_Attendance_OutList(string key, string startTime, string endTime, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMapPostionList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("startTime", startTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<MapPositionModel> list = Db.ExecuteProcToList<MapPositionModel>(sql, Pmts.ToArray());
            DataList<MapPositionModel> pageList = new DataList<MapPositionModel>(list, Pmts.ListPmts[5].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion


        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="key">xingming</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<MapPositionModel> GetList(string key, string startTime, string endTime)
        {

            string sql = @"SELECT A.*,StrStartTime=CONVERT(VARCHAR(20),A.CreateTime,20),u.RealName FROM dbo.[MapPosition] AS A join Sys_User u on A.CreateUserId = u.Id WHERE (@key='' OR u.RealName LIKE '%'+@key+'%') AND (@startTime='' OR A.CreateTime>=@startTime) AND (@endTime='' OR A.CreateTime<=@endTime)";
            //string sql = @"select A.*,StrStartTime=CONVERT(VARCHAR(20),A.CreateTime,20),u.RealName from dbo.MapPosition A join Sys_User u on A.CreateUserId = u.Id where (@startTime='' OR A.CreateTime>=@startTime) AND (@startTime='' OR A.CreateTime<=@startTime)";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("startTime", startTime);
            Pmts.Add("endTime", endTime);
            List<MapPositionModel> list = Db.ExecuteToList<MapPositionModel>(sql, Pmts.ToArray());
            return list;
        }
        #endregion
    }
}
