using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using ZX.Model;
using ZX.DAL;
using ZX.Model.Model;

namespace ZX.BLL
{
    public class AttendanceTimeBLL:BaseBLL<AttendanceTime, AttendanceTImeDAL>
    {
        public static List<AttendanceTimeModel> GetMy_AttendanceTimeList(string key)
        {
            return new AttendanceTImeDAL().GetMy_AttendanceTimeList(key);
        }

        public static List<AttendanceTimeModel> GetExcelList(string key)
        {
            return new AttendanceTImeDAL().GetExcelList(key);
        }
    }
}
