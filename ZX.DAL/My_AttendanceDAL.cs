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
    public class My_AttendanceDAL : DBBase<My_Attendance>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<My_AttendanceModel> GetMy_AttendanceList(string key, string startTime, string endTime,int pageIndex, int pageSize)
		{
			string sql = "Proc_GetMy_AttendanceList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
            Pmts.Add("startTime", startTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<My_AttendanceModel> list = Db.ExecuteProcToList<My_AttendanceModel>(sql, Pmts.ToArray());
			DataList<My_AttendanceModel> pageList = new DataList<My_AttendanceModel>(list, Pmts.ListPmts[5].Value.ToInt(), pageIndex, pageSize);
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
        public List<My_AttendanceModel> GetList(string key, string startTime, string endTime)
        {
            
            string sql = @"SELECT A.*,
                                ADate=CONVERT(VARCHAR(10),StartTime,20),
                                StrStartTime=CONVERT(VARCHAR(20),StartTime,20),
                                StrEndTime=CONVERT(VARCHAR(20),EndTime,20),
                                StrPmStartTime=CONVERT(VARCHAR(20),PmStartTime,20),
                                StrPmEndTime=CONVERT(VARCHAR(20),PmEndTime,20)
	                            FROM dbo.[My_Attendance] AS A
	                            WHERE (@key='' OR A.URealname LIKE @key)
                                AND (@startTime='' OR StartTime>=@startTime) AND (@endTime='' OR StartTime<=@endTime)";
            Pmts.ClearPmts();
            Pmts.Add("key", key.ToSqlLike());
            Pmts.Add("startTime", startTime);
            Pmts.Add("endTime", endTime);
            List<My_AttendanceModel> list = Db.ExecuteToList<My_AttendanceModel>(sql, Pmts.ToArray());
            return list;
        }
        #endregion


    }
}
