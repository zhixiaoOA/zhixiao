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
    public class MyCostBillController : BaseController
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
                    MyCostBillModel model = MyCostBillBLL.GetModelById(id);
                    int typeId = ApplyType.费用申请单.ToInt();                    
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
        public JsonResult AuthCostBill(string appId, string timestamp, string sign, long id, long userId, int status, string msg)
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
                    MyCostBillModel model = MyCostBillBLL.GetModel(id);
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
                            FK_TypeId = ApplyType.费用申请单.ToInt(),
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
                            FK_TypeId = ApplyType.费用申请单.ToInt(),
                            Status = 0,
                            LContent = approval_User.FlowName + "[同意]，" + msg
                        };
                        Approval_LogBLL.AddModel(log);
                    }
                    MyCostBillBLL.UpdateModel(model);
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

        #region 保存数据
        [ValidateInput(false)]
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData(string timestamp, string sign, string appId, MyCostBillModel model, string userId, string positionId)
        {

            //MyCostBill model = FormHelper.GetRequestForm<MyCostBill>();
            AjaxResult rest = new AjaxResult();
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("userId", userId);
            pmts.Add("positionId", positionId);
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if((result.Code == ResultCode.Succeed))
                {
                    long aid = model.FK_ApprovalId.ToLong();
                    List<Approval_User> listApplyUser = Approval_UserBLL.GetList(t => t.Where(a => a.FK_ApprovalId == aid));
                    int row = 0;
                    if (model.Id > 0)
                    {
                        model.UpdateTime = DateTime.Now;
                        if (model.Status == ApplyStatus.驳回.ToInt())
                        {
                            model.Status = ApplyStatus.审核中.ToInt();
                            //List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.费用报销单.ToInt(), DeptId);
                            if (listApplyUser.Count > 0)
                            {
                                model.FK_ApprovalUserId = listApplyUser[0].Id;
                                //判断当前职位是否与流程职位相等,如果相等则跳转到下一个步骤,如果没有下一个步骤,则为自己审批,直接审核通过
                                if (long.Parse(positionId) == listApplyUser[0].FK_CompanyPositionId)
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
                                row = MyCostBillBLL.UpdateModel(model);
                            }
                            else
                            {
                                rest.Message = "当前申请未配置审批流程";
                                rest.Code = ResultCode.Failure;
                            }
                        }
                        else
                        {
                            row = MyCostBillBLL.UpdateModel(model);
                        }
                    }
                    else
                    {
                        model.AddTime = DateTime.Now;
                        model.FK_UserId = long.Parse(userId);
                        model.UpdateTime = model.AddTime;

                        //List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.费用报销单.ToInt(), DeptId);
                        if (listApplyUser.Count > 0)
                        {
                            model.FK_ApprovalUserId = listApplyUser[0].Id;
                            if (model.FK_ApprovalUserId.ToInt() == 0)
                            {
                                model.FK_ApprovalUserId = listApplyUser[0].Id;
                                //判断当前职位是否与流程职位相等,如果相等则跳转到下一个步骤,如果没有下一个步骤,则为自己审批,直接审核通过
                                if (long.Parse(positionId) == listApplyUser[0].FK_CompanyPositionId)
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
                            model.Id = MyCostBillBLL.AddModel(model);
                        }
                        else
                        {
                            rest.Message = "当前申请未配置审批流程";
                            rest.Code = ResultCode.Failure;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rest.Message = ex.Message;
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(rest);
        }
        #endregion

        #region 获取费用申请单工作流程
        /// <summary>
        /// 获取费用申请单工作流程
        /// </summary>
        [HttpPost]
        public JsonResult GetFlow(string appId, string timestamp, string sign, string userId, long DeptId)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("userId", userId);
            pmts.Add("deptId", DeptId.ToString());
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    int typeId = ApplyType.费用申请单.ToInt();
                    List<Approval> listApproval = ApprovalBLL.GetListByTypeId(typeId, DeptId);
                    result.Data = listApproval;
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
    }
}
