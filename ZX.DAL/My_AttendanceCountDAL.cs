using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Tools;
using ZX.DAL;
using ZX.Model;
using System.Collections.Generic;

namespace ZX.DAL
{
    public class My_AttendanceCountDAL : DBBase<My_AttendanceCount>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<My_AttendanceCount> GetMy_AttendanceCountList(string key, string startTime,int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMy_AttendanceCountList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("startTime", startTime);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_AttendanceCount> list = Db.ExecuteProcToList<My_AttendanceCount>(sql, Pmts.ToArray());
            DataList<My_AttendanceCount> pageList = new DataList<My_AttendanceCount>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
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
        public List<My_AttendanceCount> GetList(string key, string startTime)
        {

            string sql = @"select * from My_AttendanceCount WHERE (@key='' OR URealName LIKE @key) AND (@startTime='' OR Date like @startTime)";          
            Pmts.ClearPmts();
            Pmts.Add("key", key.ToSqlLike());
            Pmts.Add("startTime", startTime);
            List<My_AttendanceCount> list = Db.ExecuteToList<My_AttendanceCount>(sql, Pmts.ToArray());
            return list;
        }
        #endregion
    }
}
