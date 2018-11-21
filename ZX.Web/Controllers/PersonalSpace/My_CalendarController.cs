using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Tools;

namespace ZX.Web.Controllers
{
    public class My_CalendarController : BaseController
    {
        // GET: MyCalendar
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMyCalendar()
        {
            AjaxResult result = new AjaxResult();
            //待办显示(待办+待办任务<项目任务和临时任务> 默认按所剩时间升序排序)

            return Json(result);
        }
    }
}