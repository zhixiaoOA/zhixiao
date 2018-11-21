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
    public class ApplyTypeController : BaseController
    {
        #region 获取我的申请单类型
        /// <summary>
        /// 获取我的申请单类型
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMyApplyType(string appId, string timestamp, string sign, long userId)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("userId", userId + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed) 
                {
                    AllApplyCount applyCount = AllApplyNoticeBLL.GetMyApplyCount(userId);
                    List<DictionaryModel> list = DictionaryBLL.GetApplyTypeList();
                    foreach (var item in list)
                    {
                        switch (item.Id)
                        {
                            case (int)ApplyType.出差申请单:
                                item.Count = applyCount.BusinessTripCount;
                                break;
                            case (int)ApplyType.加班及调休申请单:
                                item.Count = applyCount.WorkCount;
                                break;
                            case (int)ApplyType.印章使用审批申请单:
                                item.Count = applyCount.SealUseCount;
                                break;
                            case (int)ApplyType.印章外带审批申请单:
                                item.Count = applyCount.SealOutCount;
                                break;
                            case (int)ApplyType.合同协议审批申请单:
                                item.Count = applyCount.AgreementCount;
                                break;
                            case (int)ApplyType.员工请假申请单:
                                item.Count = applyCount.AskCount;
                                break;
                            case (int)ApplyType.所需物品领用申请单:
                                item.Count = applyCount.GooodsUseCount;
                                break;
                            case (int)ApplyType.招待审批申请单:
                                item.Count = applyCount.EntertainCount;
                                break;
                            case (int)ApplyType.未打卡证明申请单:
                                item.Count = applyCount.ClockCount;
                                break;
                            case (int)ApplyType.申请单:
                                item.Count = applyCount.ApplyCount;
                                break;
                            case (int)ApplyType.物资采购申请单:
                                item.Count = applyCount.GiftBuyCount;
                                break;
                            
                        }
                    }
                    result.Data = list;
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

        #region 获取我的审批单类型
        /// <summary>
        /// 获取我的申请单类型
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMyAuthType(string appId, string timestamp, string sign, long userId)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("userId", userId + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    AllApplyCount authCount = AllApplyNoticeBLL.GetAuthApplyCount(userId);
                    List<DictionaryModel> list = DictionaryBLL.GetApplyTypeList();
                    foreach (var item in list)
                    {
                        switch (item.Id)
                        {
                            case (int)ApplyType.出差申请单:
                                item.Count = authCount.BusinessTripCount;
                                break;
                            case (int)ApplyType.加班及调休申请单:
                                item.Count = authCount.WorkCount;
                                break;
                            case (int)ApplyType.印章使用审批申请单:
                                item.Count = authCount.SealUseCount;
                                break;
                            case (int)ApplyType.印章外带审批申请单:
                                item.Count = authCount.SealOutCount;
                                break;
                            case (int)ApplyType.合同协议审批申请单:
                                item.Count = authCount.AgreementCount;
                                break;
                            case (int)ApplyType.员工请假申请单:
                                item.Count = authCount.AskCount;
                                break;
                            case (int)ApplyType.所需物品领用申请单:
                                item.Count = authCount.GooodsUseCount;
                                break;
                            case (int)ApplyType.招待审批申请单:
                                item.Count = authCount.EntertainCount;
                                break;
                            case (int)ApplyType.未打卡证明申请单:
                                item.Count = authCount.ClockCount;
                                break;
                            case (int)ApplyType.申请单:
                                item.Count = authCount.ApplyCount;
                                break;
                            case (int)ApplyType.物资采购申请单:
                                item.Count = authCount.GiftBuyCount;
                                break;
                           
                        }
                    }
                    result.Data = list;
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
