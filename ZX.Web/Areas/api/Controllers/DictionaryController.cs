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
    public class DictionaryController : BaseController
    {
        #region 获取数据字典列表
        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="parentIds">父id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetList(string appId, string timestamp, string sign, string parentIds)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("parentIds", parentIds);
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    List<Dictionary> list = DictionaryBLL.GetDictionaryListByPid(parentIds);
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
