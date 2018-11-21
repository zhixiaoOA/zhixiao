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
    public class ApprovalBLL : BaseBLL<Approval, ApprovalDAL>
    {
        #region 根据用户id 分页获取审批流数据列表
        public static DataList<ApprovalModel> GetApprovalList(string key, int pageIndex, int pageSize)
        {
            return new ApprovalDAL().GetApprovalList(key, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据用户id 分页获取审批流数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public static DataList<ApprovalModel> GetApprovalList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetApprovalList(key, pageIndex, pageSize, conditions);
        }

        public static DataList<My_AskModel> GetMy_AskList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_AskList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_AttendanceModel> GetMy_AttendanceList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_AttendanceList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_GoOutModel> GetMy_GoOutList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_GoOutList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_WorkModel> GetMy_WorkList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_WorkList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_PaidLeaveModel> GetMy_PaidLeaveList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_PaidLeaveList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_BusinessTripModel> GetMy_BusinessTripList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_BusinessTripList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_BaoXiaoModel> GetMy_BaoXiaoList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_BaoXiaoList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_BaoXiao_DetailModel> GetMy_BaoXiao_DetailList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_BaoXiao_DetailList(key, pageIndex, pageSize, conditions);
        }
        public static DataList<My_HolidayModel> GetMy_HolidayList(string key, int pageIndex, int pageSize, int conditions)
        {
            return new ApprovalDAL().GetMy_HolidayList(key, pageIndex, pageSize, conditions);
        }
        #endregion

        #region 根据审批节点获取审批流程
        /// <summary>
        /// 根据节点获取类型
        /// </summary>
        /// <param name="FK_ApprovalId"></param>
        /// <returns></returns>
        public static Approval_TypeModel GetApprovalType(int FK_ApprovalId)
        {
            return new ApprovalDAL().GetApprovalType(FK_ApprovalId);
        }
        /// <summary>
        /// 根据节点获取审批用户
        /// </summary>
        /// <param name="FK_ApprovalId"></param>
        /// <returns></returns>
        public static Approval_UserModel GetApprovalUser(int FK_ApprovalId)
        {
            return new ApprovalDAL().GetApprovalUser(FK_ApprovalId);
        }
        /// <summary>
        /// 获取当前审批类型的所有审批用户
        /// </summary>
        /// <param name="FK_ApprovalId">节点类型</param>
        /// <returns></returns>
        public static List<Approval_UserModel> GetCurrentApprovalUser(int FK_ApprovalId)
        {
            return new ApprovalDAL().GetCurrentApprovalUser(FK_ApprovalId);
        }
        /// <summary>
        /// 获取关于当前用户的审批流
        /// </summary>
        /// <param name="FK_ApprovalId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<Approval_UserModel> GetTheCurrentApprovalUser(int FK_ApprovalId, int UserId)
        {
            return new ApprovalDAL().GetTheCurrentApprovalUser(FK_ApprovalId, UserId);
        }
        #endregion

        #region 根据类型id和部门id获取流程
        /// <summary>
        /// 根据类型id和部门id获取流程
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="deptId">部门id</param>
        /// <returns></returns>
        public static List<Approval> GetListByTypeId(int typeId, long deptId)
        {
            return new ApprovalDAL().GetListByTypeId(typeId, deptId);
        }
        #endregion
    }
}
