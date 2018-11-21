using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Model;
using ZX.DAL;
using ZX.Model.Model;
using ZX.Tools;

namespace ZX.BLL
{
    public class MapPositionBLL : BaseBLL<MapPosition,MapPositionDAL>
    {
        public static List<MapPositionModel> GetDayByMonth(int UserId, string Month)
        {
            return new MapPositionDAL().GetDayByMonth(UserId, Month);
        }
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<MapPositionModel> GetMy_Attendance_OutList(string key, string startTime, string endTime, int pageIndex, int pageSize)
        {
            return new MapPositionDAL().GetMy_Attendance_OutList(key, startTime, endTime, pageIndex, pageSize);
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
        public static List<MapPositionModel> GetList(string key, string startTime, string endTime)
        {
            return new MapPositionDAL().GetList(key, startTime, endTime);
        }
        #endregion
    }
}
