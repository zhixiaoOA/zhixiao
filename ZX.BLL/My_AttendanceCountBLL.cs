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
    public class My_AttendanceCountBLL : BaseBLL<My_AttendanceCount, My_AttendanceCountDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<My_AttendanceCount> GetMy_AttendanceCountList(string key, string startTime, int pageIndex, int pageSize)
        {
            return new My_AttendanceCountDAL().GetMy_AttendanceCountList(key, startTime, pageIndex, pageSize);
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
        public static List<My_AttendanceCount> GetList(string key, string startTime)
        {
            return new My_AttendanceCountDAL().GetList(key, startTime);
        }
        #endregion
    }
}