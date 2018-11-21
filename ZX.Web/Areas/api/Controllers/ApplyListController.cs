using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Model;
using ZX.BLL;
using ZX.Tools;
using System.Text;

namespace ZX.Web.Areas.api.Controllers
{
    public class ApplyListController : BaseController
    {
        #region 获取申请单列表
        /// <summary>
        /// 获取申请单列表
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="typeId">类型id</param>
        /// <param name="userId">用户id</param>
        /// <param name="appUserId">审批人id</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetList(string appId, string timestamp, string sign, int typeId, long userId, long appUserId, string status, int pageIndex)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("typeId", typeId + "");
            pmts.Add("userId", userId + "");
            pmts.Add("status", status + "");
            pmts.Add("appUserId", appUserId + "");
            pmts.Add("pageIndex", pageIndex + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    DataList<ApplyAllModel> list = AllApplyNoticeBLL.GetMyApplyList(userId, appUserId, typeId, status + "", pageIndex, PageSize);
                    result.Data = list;
                    result.PageTotal = list.TotalPages;
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
