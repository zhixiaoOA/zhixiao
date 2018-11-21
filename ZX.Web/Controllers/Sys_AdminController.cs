using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZX.Web.Controllers
{
    public class Sys_AdminController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("Sys_AdminIndex");
        }
    }
}