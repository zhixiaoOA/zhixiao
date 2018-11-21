using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using ZX.Model;
using ZX.DAL;

namespace ZX.BLL
{
    public class My_AttendanceBLL : BaseBLL<My_Attendance, My_AttendanceDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<My_AttendanceModel> GetMy_AttendanceList(string key, string startTime, string endTime, int pageIndex, int pageSize)
        {
            return new My_AttendanceDAL().GetMy_AttendanceList(key, startTime, endTime, pageIndex, pageSize);
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
        public static List<My_AttendanceModel> GetList(string key, string startTime, string endTime)
        {
            return new My_AttendanceDAL().GetList(key,  startTime,  endTime);
        }
        #endregion
    }
}
