using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.DAL;
using ZX.Model;
using ZX.Tools;

namespace ZX.BLL
{
    public class AllApplyNoticeBLL : BaseBLL<AllApplyNotice, AllApplyNoticeDAL>
    {
        /// <summary>
        /// 获取审批站内消息
        /// </summary>
        /// <param name="fk_CompanyPositionId">职位Id,-1获取所有站内通知</param>
        /// <returns></returns>
        public static List<AllApplyNotice> GetApplyNoticeList(int fk_CompanyPositionId, string beginTime, string endTime, int status)
        {
            return new AllApplyNoticeDAL().GetApplyNoticeList(fk_CompanyPositionId, beginTime, endTime, status);
        }

        /// <summary>
        /// 获取审批站内消息
        /// </summary>
        /// <param name="fk_CompanyPositionId">职位Id,-1获取所有站内通知</param>
        /// <returns></returns>
        public static List<AllApplyNotice> GetAllApplyNoticeList(int fk_CompanyPositionId)
        {
            return new AllApplyNoticeDAL().GetAllApplyNoticeList(fk_CompanyPositionId);
        }
        /// <summary>
        /// 获取我提交的审批
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public static List<AllApplyNotice> GetMyCreateApplyNoticeList(int userId)
        {
            return new AllApplyNoticeDAL().GetMyCreateApplyNoticeList(userId);
        }

        #region 获取我的申请单未审核数量
        /// <summary>
        /// 获取我的申请单未审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static AllApplyCount GetMyApplyCount(long userId)
        {
            return new AllApplyNoticeDAL().GetMyApplyCount(userId);
        }
        #endregion

        #region 获取我的审批单未审核数量
        /// <summary>
        /// 获取我的审批单未审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static AllApplyCount GetAuthApplyCount(long userId)
        {
            return new AllApplyNoticeDAL().GetAuthApplyCount(userId);
        }
        #endregion

        #region 获取申请单类型获取申请单或审批单列表
        /// <summary>
        /// 获取申请单类型获取申请单或审批单列表
        /// </summary>
        /// <param name="userId">申请人id</param>
        /// <param name="appUserId">审批人id</param>
        /// <param name="typeId">类型id</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<ApplyAllModel> GetMyApplyList(long userId, long appUserId, int typeId, string status, int pageIndex, int pageSize)
        {
            return new AllApplyNoticeDAL().GetMyApplyList(userId, appUserId, typeId, status, pageIndex, pageSize);
        }
        #endregion

        public static List<AllApplyNoticeModel> GetAllApplyNoticeListExcel(int fk_CompanyPositionId)
        {
            return new AllApplyNoticeDAL().GetAllApplyNoticeListExcel(fk_CompanyPositionId);
        }
    }
}
