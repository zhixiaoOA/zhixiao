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
    public class Approval_LogBLL : BaseBLL<Approval_Log, Approval_LogDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Approval_LogModel> GetApproval_LogList(string key, int pageIndex, int pageSize)
        {
            return new Approval_LogDAL().GetApproval_LogList(key, pageIndex, pageSize);
        }
        #endregion

        /// <summary>
        /// 通过审批日志获取审批用户
        /// </summary>
        /// <param name="counts">获取数例如:5; 值为-1获取全部</param>
        /// <param name="fk_TypeId">审批类型</param>
        /// <param name="fk_ApplyFlowId">审批单Id</param>
        /// <returns></returns>
        public static List<Sys_User> GetUserListByApprovalLog(int counts, int fk_TypeId, int fk_ApplyFlowId)
        {
            return new Approval_LogDAL().GetUserListByApprovalLog(counts, fk_TypeId, fk_ApplyFlowId);
        }

        #region 获取审批日志
        /// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="applyFlowId">申请单id</param>
        /// <param name="typeId">申请单类型id</param>
        /// <returns></returns>
        public static List<Approval_LogModel> GetApproval_LogList(long? applyFlowId, int typeId)
        {
            return new Approval_LogDAL().GetApproval_LogList(applyFlowId, typeId);
        }
        #endregion
    }
}
