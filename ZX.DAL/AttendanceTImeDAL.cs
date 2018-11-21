using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Tools;
using ZX.DAL;
using ZX.Model;
using ZX.Model.Model;

namespace ZX.DAL
{
    public class AttendanceTImeDAL: DBBase<AttendanceTime>
    {
        public List<AttendanceTimeModel> GetMy_AttendanceTimeList(string key)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                sql= @"SELECT Id,Date,ApplicableObject,WorkDay,AmStartTime,AmEndTime,PmStartTime,PmEndTime,WorkDays FROM AttendanceTime";
            }
            else
            {
                sql = @"SELECT Id,Date,ApplicableObject,WorkDay,AmStartTime,AmEndTime,PmStartTime,PmEndTime,WorkDays FROM AttendanceTime where Date='" + key + "';";
            }
            List<AttendanceTimeModel> list = Db.ExecuteToList<AttendanceTimeModel>(sql);
            return list;
        }


        public List<AttendanceTimeModel> GetExcelList(string key)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                sql = @"
                    SELECT Id,Date,ApplicableObject,WorkDay,
                    CONVERT(VARCHAR(20),AmStartTime,24) AS StrAmStartTime,
                    CONVERT(VARCHAR(20),AmEndTime,24) AS StrAmEndTime,
                    CONVERT(VARCHAR(20),PmStartTime,24) AS StrPmStartTime,
                    CONVERT(VARCHAR(20),PmEndTime,24) AS StrPmEndTime,WorkDays FROM AttendanceTime";
            }
            else
            {
                sql = @" SELECT Id,Date,ApplicableObject,WorkDay,
                    CONVERT(VARCHAR(20),AmStartTime,24) AS StrAmStartTime,
                    CONVERT(VARCHAR(20),AmEndTime,24) AS StrAmEndTime,
                    CONVERT(VARCHAR(20),PmStartTime,24) AS StrPmStartTime,
                    CONVERT(VARCHAR(20),PmEndTime,24) AS StrPmEndTime,WorkDays FROM AttendanceTime where Date='" + key + "';";
            }
            List<AttendanceTimeModel> list = Db.ExecuteToList<AttendanceTimeModel>(sql);
            return list;
        }
    }
}
