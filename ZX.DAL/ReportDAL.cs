using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class ReportDAL : DBBase<Sys_Menu>
    {
        #region 获取报表数量
        /// <summary>
        /// 获取报表数量
        /// </summary>
        /// <param name="btime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns></returns>
        public ReportCount GetReportCount(string btime, string etime)
        {
            string sql = "Proc_GetAllReport";
            Pmts.ClearPmts();
            Pmts.Add("btime", btime);
            Pmts.Add("etime", etime);
            return Db.ExecuteProcToSingle<ReportCount>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
