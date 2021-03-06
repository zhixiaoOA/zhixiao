﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Model;
using ZX.BLL;
using ZX.Tools;

namespace ZX.Web.Areas.api.Controllers
{
    public class HomeController : BaseController
    {
        #region 获取首页数据
        /// <summary>
        /// 获取首页数据
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetHomeData(string appId, string timestamp, string sign, long userId)
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
                    AllApplyCount authCount = AllApplyNoticeBLL.GetAuthApplyCount(userId);
                    int projectCount = Project_TaskBLL.GetWwcCount(userId);
                    int tempCount = Temporary_TaskBLL.GetWwcCount(userId);
                    result.Data = new
                    {
                        applyCount = applyCount.AgreementCount
                        + applyCount.ApplyCount
                        + applyCount.AskCount
                        + applyCount.BusinessTripCount
                        + applyCount.CartPublicCount
                        + applyCount.ClockCount
                        + applyCount.EntertainCount
                        + applyCount.GiftBuyCount
                        + applyCount.SealOutCount
                        + applyCount.SealUseCount
                        + applyCount.WorkCount
                        + applyCount.GooodsUseCount,
                        authCount = authCount.AgreementCount
                        + authCount.ApplyCount
                        + authCount.AskCount
                        + authCount.BusinessTripCount
                        + authCount.CartPublicCount
                        + authCount.ClockCount
                        + authCount.EntertainCount
                        + authCount.GiftBuyCount
                        + authCount.SealOutCount
                        + authCount.SealUseCount
                        + authCount.WorkCount
                        + authCount.GooodsUseCount,
                        projectCount,
                        tempCount
                    };
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
