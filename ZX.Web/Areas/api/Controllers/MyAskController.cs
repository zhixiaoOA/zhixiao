using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Model;
using ZX.BLL;
using ZX.Tools;

namespace ZX.Web.Areas.api.Controllers
{
    public class MyAskController : BaseController
    {
        #region 根据id获取数
        /// <summary>
        /// 根据id获取数
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetModel(string appId, string timestamp, string sign, long id)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("id", id + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    My_AskModel model = My_AskBLL.GetModelById(id);
                    string appLen = "";
                    int totalHh = (Convert.ToDateTime(model.EndTime) - Convert.ToDateTime(model.StartTime)).TotalHours.ToInt();
                    int dd = totalHh / 24;
                    int hh = totalHh % 24;
                    if (hh != 0)
                    {
                        if (dd > 0)
                        {
                            appLen = "共请假" + dd + "天" + hh + "小时";
                        }
                        else
                        {
                            appLen = "共请假" + hh + "小时";
                        }
                    }
                    else
                    {
                        appLen = "共请假" + dd + "天";
                    }
                    model.TotalDay = appLen;
                    int typeId = ApplyType.员工请假申请单.ToInt();
                    List<Approval_Log> listLog = Approval_LogBLL.GetList(t => t.Where(a => a.FK_ApplyFlowId == id && a.FK_TypeId == typeId));                    
                    result.Data = new { Model = model, ListLog = listLog };
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = "获取数据失败";
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion

        #region 审批
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="id">申请单id</param>
        /// <param name="userId">审批人id</param>
        /// <param name="status">0:通过 1:驳回</param>
        /// <param name="msg">审批意见</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AuthApply(string appId, string timestamp, string sign, long id, long userId, int status, string msg)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("id", id + "");
            pmts.Add("userId", userId + "");
            pmts.Add("status", status + "");
            pmts.Add("msg", msg);
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    My_Ask model = My_AskBLL.GetModel(id);
                    Sys_User autoUser = Sys_UserBLL.GetModel(userId);
                    //获取所有审批流程
                    List<Approval_User> listApplyUser = Approval_UserBLL.GetList(t => t.Where(a => a.FK_ApprovalId == model.FK_ApprovalId));
                    Approval_User approval_User = listApplyUser.FirstOrDefault(t => t.FK_CompanyPositionId == autoUser.FK_CompanyPositionId);
                    if (status == ApplyStatus.驳回.ToInt())
                    {
                        Approval_Log log = new Approval_Log()
                        {
                            CreateAccount = autoUser.Id + "",
                            CreateTime = DateTime.Now,
                            CreateUserId = autoUser.Id.ToInt(),
                            FK_ApplyFlowId = model.Id,
                            FK_TypeId = ApplyType.员工请假申请单.ToInt(),
                            Status = 1,
                            LContent = approval_User.FlowName + "[驳回]，" + msg
                        };
                        Approval_LogBLL.AddModel(log);
                        model.Status = ApplyStatus.驳回.ToInt();
                    }
                    else
                    {

                        Approval_User approvalUserNext = listApplyUser.FirstOrDefault(t => t.Id > approval_User.Id);
                        //判断是否有下一步流程
                        if (approvalUserNext != null)
                        {
                            model.Status = ApplyStatus.审核中.ToInt();
                            model.FK_ApprovalUserId = approvalUserNext.Id;
                        }
                        else
                        {
                            model.Status = ApplyStatus.已审核.ToInt();
                        }
                        Approval_Log log = new Approval_Log()
                        {
                            CreateAccount = autoUser.Id + "",
                            CreateTime = DateTime.Now,
                            CreateUserId = autoUser.Id.ToInt(),
                            FK_ApplyFlowId = model.Id,
                            FK_TypeId = ApplyType.员工请假申请单.ToInt(),
                            Status = 0,
                            LContent = approval_User.FlowName + "[同意]，" + msg
                        };
                        Approval_LogBLL.AddModel(log);
                    }
                    My_AskBLL.UpdateModel(model);
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = "获取数据失败";
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion

        #region 根据请假类型获取工作流
        /// <summary>
        /// 根据请假类型获取工作流
        /// </summary>     
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="askType">请假类型</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public JsonResult GetAskFlow(string appId, string timestamp, string sign, int askType, long userId)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("askType", askType + "");
            pmts.Add("userId", userId + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    Sys_User user = Sys_UserBLL.GetModel(userId);
                    List<Approval> listApproval = new List<Approval>();
                    switch (askType)
                    {
                        case (int)AskType.事假:
                            listApproval = ApprovalBLL.GetListByTypeId(ApplyType.员工请假申请单.ToInt(), user.Fk_DeptId.ToLong());
                            break;
                        case (int)AskType.婚假:
                            listApproval = ApprovalBLL.GetListByTypeId(ApplyType.员工请假申请单_婚假.ToInt(), user.Fk_DeptId.ToLong());
                            break;
                        case (int)AskType.产假:
                            listApproval = ApprovalBLL.GetListByTypeId(ApplyType.员工请假申请单_产假.ToInt(), user.Fk_DeptId.ToLong());
                            break;
                        case (int)AskType.丧假:
                            listApproval = ApprovalBLL.GetListByTypeId(ApplyType.员工请假申请单_丧假.ToInt(), user.Fk_DeptId.ToLong());
                            break;
                        case (int)AskType.病假:
                            listApproval = ApprovalBLL.GetListByTypeId(ApplyType.员工请假申请单_病假.ToInt(), user.Fk_DeptId.ToLong());
                            break;
                        case (int)AskType.工伤假:
                            listApproval = ApprovalBLL.GetListByTypeId(ApplyType.员工请假申请单_工伤假.ToInt(), user.Fk_DeptId.ToLong());
                            break;
                    }
                    result.Data = listApproval;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion

        #region 新增申请单
        /// <summary>
        /// 新增申请单
        /// </summary>        
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="model">申请单实体</param>
        /// <returns></returns>
        public JsonResult SaveData(string appId, string timestamp, string sign, My_Ask model)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    Sys_User user = Sys_UserBLL.GetModel(model.CreateUserId.ToLong());
                    long aid = model.FK_ApprovalId.ToLong();
                    List<Approval_User> listApplyUser = Approval_UserBLL.GetList(t => t.Where(a => a.FK_ApprovalId == aid));
                    if (model.Id > 0)
                    {
                        model.UpdateUserId = model.CreateUserId;
                        model.UpdateTime = DateTime.Now;
                        if (model.Status == ApplyStatus.驳回.ToInt())
                        {
                            model.Status = ApplyStatus.审核中.ToInt();
                            if (listApplyUser.Count > 0)
                            {
                                model.FK_ApprovalUserId = listApplyUser[0].Id;
                                //判断当前职位是否与流程职位相等,如果相等则跳转到下一个步骤,如果没有下一个步骤,则为自己审批,直接审核通过
                                if (user.FK_CompanyPositionId == listApplyUser[0].FK_CompanyPositionId)
                                {
                                    Approval_User appUser = listApplyUser.FirstOrDefault(t => t.Id > listApplyUser[0].Id);
                                    if (appUser == null)
                                    {
                                        model.Status = ApplyStatus.已审核.ToInt();
                                    }
                                    else
                                    {
                                        model.FK_ApprovalUserId = appUser.Id;
                                    }
                                }
                                My_AskBLL.UpdateModel(model);
                            }
                            else
                            {
                                result.Message = "当前申请未配置审批流程";
                                result.Code = ResultCode.Failure;
                            }
                        }
                        else
                        {
                            My_AskBLL.UpdateModel(model);
                        }
                    }
                    else
                    {
                        model.CreateTime = DateTime.Now;
                        model.CreateUserId = model.CreateUserId;
                        model.UpdateTime = model.CreateTime;
                        model.UpdateUserId = model.CreateUserId;
                        if (listApplyUser.Count > 0)
                        {
                            if (model.FK_ApprovalUserId.ToInt() == 0)
                            {
                                model.FK_ApprovalUserId = listApplyUser[0].Id;
                                //判断当前职位是否与流程职位相等,如果相等则跳转到下一个步骤,如果没有下一个步骤,则为自己审批,直接审核通过
                                if (user.FK_CompanyPositionId == listApplyUser[0].FK_CompanyPositionId)
                                {
                                    Approval_User appUser = listApplyUser.FirstOrDefault(t => t.Id > listApplyUser[0].Id);
                                    if (appUser == null)
                                    {
                                        model.Status = ApplyStatus.已审核.ToInt();
                                    }
                                    else
                                    {
                                        model.FK_ApprovalUserId = appUser.Id;
                                    }
                                }
                            }
                            model.Id = My_AskBLL.AddModel(model);
                        }
                        else
                        {
                            result.Message = "当前申请未配置审批流程";
                            result.Code = ResultCode.Failure;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion
    }
}
