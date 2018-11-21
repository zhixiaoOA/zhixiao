using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Tools;

namespace ZX.Web.Areas.api.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// appid
        /// </summary>
        public static string Appid = "wxca91d7b182f164b1";
        /// <summary>
        /// app密钥
        /// </summary>
        public static string AppidKey = "6ffb9a80950e2cdd61ea6e7fc25a2075";

        public static int PageSize = 10;

        /// <summary>
        /// 检测api接口安全
        /// </summary>
        /// <param name="pmts">参数集合</param>
        /// <returns></returns>
        protected AjaxResult CheckApiSign(ApiPmts pmts)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                string appId = pmts.Get("appId");
                string sign = pmts.Get("sign").ToUpper();
                string timestamp = pmts.Get("timestamp");
                string apiSign = pmts.GetSige(AppidKey);
                Log4Helper.WriteInfo("请求的签名:" + sign);
                Log4Helper.WriteInfo("接口的签名:" + apiSign);
                DateTime reqDt = timestamp.Length == 10 ? timestamp.ToTimeStamp10() : timestamp.ToTimeStamp13();
                if (appId != Appid)
                {
                    result.Code = ResultCode.Failure;
                    result.Message = "AppId不存在";
                }
                else if ((DateTime.Now - reqDt).Minutes > 1)
                {
                    result.Code = ResultCode.Failure;
                    result.Message = "请求失效";
                }
                //else if (sign != apiSign)
                //{
                //    result.Code = ResultCode.Failure;
                //    result.Message = "签名无效";
                //}
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
                result.Code = ResultCode.Failure;
                result.Message = "参数错误";
            }
            return result;
        }
    }
}