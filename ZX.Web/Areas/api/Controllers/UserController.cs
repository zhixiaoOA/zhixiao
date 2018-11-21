using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Model;
using ZX.BLL;

namespace ZX.Web.Areas.api.Controllers
{
    public class UserController : BaseController
    {
        #region 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUserInfo(string appId, string timestamp, string sign, long userId)
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
                    Sys_UserModel user = Sys_UserBLL.GetUserById(userId);
                    DateTime dt = DateTime.Now;
                    result.Data = new
                    {
                        Model = user,
                        bdate = dt.ToString("yyyy-MM-dd"),
                        edate = dt.AddDays(1).ToString("yyyy-MM-dd"),
                        btime = dt.ToString("HH:mm"),
                        etime = dt.ToString("HH:mm")
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