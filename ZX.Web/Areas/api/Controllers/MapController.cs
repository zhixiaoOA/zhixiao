using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Model.Model;
using ZX.Tools;

namespace ZX.Web.Areas.api.Controllers
{
    public class MapController : BaseController
    {
        // GET: api/Map
        public ActionResult Index()
        {
            return View();
        }
        //添加位置信息
        public JsonResult AddPostion(MapPosition position, string appId, string timestamp, string sign) {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            AjaxResult result_A = CheckApiSign(pmts);
            MessageResult result = new MessageResult();
            if (result_A.Code == ResultCode.Succeed)
            {
                //MapPosition position = new MapPosition();
                position.CreateTime = DateTime.Now;
                int state = MapPositionBLL.AddModel(position);
                if (state > 0)
                {
                    result.Code = ResultCode.Succeed;
                }
                else
                {
                    result.Code = ResultCode.Failure;
                }
            }
            else
            {
                result.Code = ResultCode.Failure;
            }
            return Json(result);
        }
        //获取考勤列表
        public JsonResult GetPostionsByUser(int UserId, string month, string appId, string timestamp, string sign)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            AjaxResult result_A = CheckApiSign(pmts);
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (result_A.Code == ResultCode.Succeed)
            {
                List<MapPositionModel> days = MapPositionBLL.GetDayByMonth(UserId, month);

                string where = "CreateUserId = " + UserId + " ORDER BY CreateTime DESC";
                List<MapPosition> positions = MapPositionBLL.GetList(where);
                
                result.Add("days", days);
                result.Add("positions", positions);
            }
            return Json(result);
        }
        public JsonResult TestGet(string data)
        {
            return Json(data);
        }
    }
}