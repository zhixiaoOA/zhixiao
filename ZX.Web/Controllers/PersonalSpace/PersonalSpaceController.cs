using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ZX.Web.Controllers
{
    public class PersonalSpaceController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Sys_User> userList = Sys_UserBLL.GetList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in userList)
            {
                list.Add(new SelectListItem { Value=item.Id.ToString(),Text=item.RealName });
            }
            ViewBag.UserList = list;
            //亮灯判断所需要的系统时间
            ViewBag.CurrentTime = DateTime.Now;
            return View();
        }
        #endregion 初始跳转
        public ActionResult MyCalendar()
        {
            ViewBag.RealName = RealName;
            return View();
        }

        public JsonResult GetMyCalendar()
        {
            AjaxResult result = new AjaxResult();
            //待办显示(待办+待办任务<项目任务和临时任务> 默认按所剩时间升序排序)

            return Json(result);
        }

        //我的项目任务
        public ActionResult MyProject_Task()
        {
            return View();
        }

        //我的临时任务
        public ActionResult MyTemporary_Task() {
            return View();
        }
    }
}