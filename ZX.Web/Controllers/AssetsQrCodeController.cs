using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZX.Web.Controllers
{
    public class AssetsQrCodeController : Controller
    {
        // GET: AssetsQrCode
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 扫描二维码进入验证页面
        /// </summary>
        /// <param name="id">资产编号</param>
        /// <returns></returns>
        public ActionResult QrCode(int id)
        {
            ViewBag.AssetId = id;
            ViewBag.Dept = Request["dept"];
            ViewBag.Name = Request["name"];
            return View("QrCodeReturnData");
        }
        /// <summary>
        /// 验证用户并插入一条日志信息
        /// </summary>
        /// <param name="userName">需要验证的账号</param>
        /// <param name="userPwd">需要验证的密码</param>
        /// <param name="id">资产编号</param>
        /// <returns></returns>
        public JsonResult Authentication()
        {
            AjaxApiResult result = new AjaxApiResult();

            int AssetId = Request["AssetId"].ToInt();
            string userName = Request["userName"] + "";
            string userPwd = Request["userPwd"] + "";
            string remark = Request["Remark"] + "";
            //根据用户输入的账号及加密后的密码查询
            Sys_User model = Sys_UserBLL.GetUserLogin(userName, EnDecrypt.SHA1Hash(userPwd));
            if (model != null)
            {
                //根据id资产信息
                Assets assets = AssetsBLL.GetModel(AssetId);
                Assets_Log aslog = new Assets_Log();
                aslog.FK_AssetsId = AssetId;
                var datetime = DateTime.Now;
                aslog.CreateTime = datetime;
                aslog.CreateUserId = (int)model.Id;
                aslog.UpdateTime = datetime;
                aslog.UpdateUserId = (int)model.Id;
                aslog.CreateAccount = model.UserName;
                aslog.UpdateAccount = model.UserName;
                aslog.Remark = "已借出" + (remark != "-" ? remark : "");
                aslog.StatusId = 0;
                aslog.StatusName = 0;

                //根据资产id添加使用日志信息
                var assetslog = Assets_LogBLL.AddLogBasedOnId(aslog);
                if (assetslog > 0)
                {
                    result.Code = 200;
                    result.Message = "登记成功！";
                }
            }
            else
            {
                result.Message = "用户不存在或密码错误!";
                result.Code = 300;
            }
            //return Content(string.Format("<script type='text/javascript'>alert('返回状态码：{0}\r\n 返回结果：{1}')</script>", result.Code, result.Message));
            return Json(result);
            //return View("QrCodeReturnData");
        }
    }
}