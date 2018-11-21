using ZX.DAL;
using ZX.Model;
using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ZX.BLL
{
    public class ApprovalStatisticalBLL
    {
        /// <summary>
        /// 根据时间及创建用户统计申请数
        /// </summary>
        /// <param name="startTime">自</param>
        /// <param name="endTime">至</param>
        /// <param name="realname">姓名,模糊查询</param>
        /// <param name="pageIndex">分页页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public static DataList<ApprovalStatistical> GetApprovalStatisticalList(string startTime, string endTime, string realname, int pageIndex, int pageSize)
        {
            return new Sys_UserDAL().GetApprovalStatisticalList(startTime, endTime, realname, pageIndex, pageSize);
        }

    }
}