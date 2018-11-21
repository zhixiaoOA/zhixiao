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
    public class ReportController : BaseController
    {
        #region 获取报表数量
        /// <summary>
        /// 获取报表数量
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="btime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetReportCount(string appId, string timestamp, string sign, string btime, string etime)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("btime", btime);
            pmts.Add("etime", etime);
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    btime += " 00:00:00";
                    etime += " 23:59:59";
                    ReportCount model = ReportBLL.GetReportCount(btime, etime);
                    result.Data = model;
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
