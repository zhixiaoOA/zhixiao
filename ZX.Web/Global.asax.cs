using ZX.Tools;
using ZX.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZX.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log4Helper.SetConfig(new System.IO.FileInfo(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Log4Net.config"));
            AreaRegistration.RegisterAllAreas();
           // WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            //捕获整个解决方案下的所有异常  
            try
            {
            }
            catch (Exception ex)
            {
            }
        }
    }
}
