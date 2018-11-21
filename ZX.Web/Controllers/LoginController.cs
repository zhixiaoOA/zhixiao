using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;
    
namespace ZX.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (System.IO.File.Exists(Server.MapPath("~/Data/IsNew.txt")))
            {
                return Redirect("Admin/Index");
            }
                ViewBag.BarName = ZXConfig.SYSNAME;
            ViewBag.DesignBy = ZXConfig.DESIGNBY;
            return View();
        }
        [HttpPost]
        //登陆动作
        public JsonResult AdminLogin()
        {
            string userName = Request.Form["userName"];
            string pwd = Request.Form["pwd"];
            Sys_User model = new Sys_User();

            if (userName == null && pwd == null)
            {
                Stream reqStream = Request.InputStream;
                StreamReader streamReader = new StreamReader(reqStream);
                string json = streamReader.ReadToEnd();
                model = (Sys_User)JsonConvert.DeserializeObject(json, model.GetType());
            }
            AjaxResult ajrs = new AjaxResult();
            try
            {
                // 查询用户
                model = Sys_UserBLL.GetUserLogin(userName == null ? model.UserName : userName, EnDecrypt.SHA1Hash(pwd == null ? model.Pwd : pwd));
                if (model.IsNullOrEmpty())
                {
                    ajrs.Message = "用户不存在或密码错误!";
                    ajrs.Code = ResultCode.Failure;
                }
                else
                {
                    SessionHelper.WriteLoginSession(model);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("登陆异常,异常信息:" + ex.Message, ex);
                ajrs.Code = ResultCode.Failure;
                ajrs.Message = "服务繁忙!";
            }

            return Json(ajrs);
        }
        #region 退出登录
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public RedirectResult Login_Out()
        {
            Session.Clear();
            Session.Abandon();
            return Redirect("/Login/Index");
        }
        #endregion
    }
}