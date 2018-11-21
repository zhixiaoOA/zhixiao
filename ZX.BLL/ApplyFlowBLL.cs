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
    public class ApplyFlowBLL : BaseBLL<ApplyFlow, ApplyFlowDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="typeId">申请类型</param>
        /// <param name="name">申请人姓名</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<ApplyFlowModel> GetApplyFlowList(long typeId, string name, string beginTime, string endTime, int status, int pageIndex, int pageSize)
        {
            return new ApplyFlowDAL().GetApplyFlowList(typeId, name, beginTime, endTime, status, pageIndex, pageSize);
        }
        #endregion
    }
}
