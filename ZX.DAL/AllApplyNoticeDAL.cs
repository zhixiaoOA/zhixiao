using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class AllApplyNoticeDAL : DBBase<AllApplyNotice>
    {
        /// <summary>
        /// 获取审批站内消息
        /// </summary>
        /// <param name="fk_CompanyPositionId">职位Id,-1获取所有站内通知</param>
        /// <returns></returns>
        public List<AllApplyNotice> GetApplyNoticeList(int fk_CompanyPositionId, string beginTime, string endTime, int status)
        {
            string sql = "Proc_GetApplyNoticeList";
            Pmts.ClearPmts();
            Pmts.Add("fk_CompanyPositionId", fk_CompanyPositionId);
            Pmts.Add("beginTime", beginTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("status", status);
            List<AllApplyNotice> list = Db.ExecuteProcToList<AllApplyNotice>(sql, Pmts.ToArray());
            return list;
        }

        /// <summary>
        /// 获取审批站内消息
        /// </summary>
        /// <param name="fk_CompanyPositionId">职位Id,-1获取所有站内通知</param>
        /// <returns></returns>
        public List<AllApplyNotice> GetAllApplyNoticeList(int fk_CompanyPositionId)
        {
            string sql = "Proc_GetAllApplyNoticeList";
            Pmts.ClearPmts();
            Pmts.Add("fk_CompanyPositionId", fk_CompanyPositionId);
            List<AllApplyNotice> list = Db.ExecuteProcToList<AllApplyNotice>(sql, Pmts.ToArray());
            return list;
        }
        /// <summary>
        /// 获取我提交的审批
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public List<AllApplyNotice> GetMyCreateApplyNoticeList(int userId)
        {
            string sql = "Proc_GetMyCreateApplyNoticeList";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            List<AllApplyNotice> list = Db.ExecuteProcToList<AllApplyNotice>(sql, Pmts.ToArray());
            return list;
        }

        #region 获取我的申请单未审核数量
        /// <summary>
        /// 获取我的申请单未审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AllApplyCount GetMyApplyCount(long userId)
        {
            string sql = "Proc_GetMyApplyCount";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            return Db.ExecuteProcToSingle<AllApplyCount>(sql, Pmts.ToArray());
        }
        #endregion

        #region 获取我的审批单未审核数量
        /// <summary>
        /// 获取我的审批单未审核数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AllApplyCount GetAuthApplyCount(long userId)
        {
            string sql = "Proc_GetAuthApplyCount";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            return Db.ExecuteProcToSingle<AllApplyCount>(sql, Pmts.ToArray());
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
        public DataList<ApplyAllModel> GetMyApplyList(long userId, long appUserId, int typeId, string status, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMyApplyList";
            Pmts.ClearPmts();
            Pmts.Add("userId", userId);
            Pmts.Add("appUserId", appUserId);
            Pmts.Add("typeId", typeId);
            Pmts.Add("status", status);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<ApplyAllModel> list = Db.ExecuteProcToList<ApplyAllModel>(sql, Pmts.ToArray());
            DataList<ApplyAllModel> pageList = new DataList<ApplyAllModel>(list, Pmts.ListPmts[6].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="fk_CompanyPositionId"></param>
        /// <returns></returns>
        public List<AllApplyNoticeModel> GetAllApplyNoticeListExcel(int fk_CompanyPositionId)
        {
            string sql = "Proc_GetAllApplyNoticeList";
            Pmts.ClearPmts();
            Pmts.Add("fk_CompanyPositionId", fk_CompanyPositionId);
            List<AllApplyNoticeModel> list = Db.ExecuteProcToList<AllApplyNoticeModel>(sql, Pmts.ToArray());
            return list;
        }
    }
}
