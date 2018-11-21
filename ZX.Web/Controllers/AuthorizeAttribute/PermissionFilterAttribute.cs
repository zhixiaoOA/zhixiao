using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZX.Tools;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZX.Web.Controllers
{
    public class PermissionFilterAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public string FunctionCode
        {
            get;
            set;
        }

        /// <summary>
        /// Action 执行前执行,验证是否已经登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (SessionHelper.Get("UserId").ToInt()==0)
                {
                    #region 判断是否登录
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult
                        {
                            Data = new AjaxResult { Code = ResultCode.NoLogin, Message = "对不起，您还没有登录！" },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult("Default",
                                                                     new System.Web.Routing.RouteValueDictionary(
                                                                         new { action = "Index", controller = "Login" }));
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                //HXDH.Common.LogHelper.LogHelper.Error("权限判断运行出错", ex);
            }
        }
    }
}