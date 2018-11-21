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
    public class My_PaidLeaveBLL : BaseBLL<My_PaidLeave, My_PaidLeaveDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">事由</param>
        /// <param name="userId">申请人id</param>
        /// <param name="appUserId">审核人id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<My_PaidLeaveModel> GetMy_PaidLeaveList(string key, long userId, long appUserId, string beginTime, string endTime, int status, int pageIndex, int pageSize)
        {
			return new My_PaidLeaveDAL().GetMy_PaidLeaveList(key, userId, appUserId, beginTime, endTime, status, pageIndex, pageSize);
		}
		#endregion
    }
}
