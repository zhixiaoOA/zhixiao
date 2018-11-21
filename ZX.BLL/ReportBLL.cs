using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Model;
using ZX.DAL;
using ZX.Tools;

namespace ZX.BLL
{
    public class ReportBLL : BaseBLL<Sys_Menu, ReportDAL>
    {
        #region 获取报表数量
        /// <summary>
        /// 获取报表数量
        /// </summary>
        /// <param name="btime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns></returns>
        public static ReportCount GetReportCount(string btime, string etime)
        {
            return new ReportDAL().GetReportCount(btime, etime);
        }
        #endregion
    }
}
