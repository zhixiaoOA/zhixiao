using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;
using System.Net;
using System.IO;
using System.Text;

namespace ZX.Web.Areas.api.Controllers
{
    public class LoginController : BaseController
    {
        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="nickname">昵称</param>
        /// <param name="headImage">头像</param>
        /// <param name="code">请求码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Index(string appId, string timestamp, string sign, string nickname, string headImage, string code)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("nickname", nickname);
            pmts.Add("headImage", headImage);
            pmts.Add("code", code);
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    Member model = new Member();
                    Sys_WxParameter wxParam = Sys_WxParameterBLL.GetModel(1);
                    string jsonMsg = HttpGet(wxParam.AppId, wxParam.AppSecret, code);
                    JObject json = JObject.Parse(jsonMsg);
                    if (json["openid"] == null)
                    {
                        result.Code = ResultCode.Failure;
                        result.Message = "没有获取到OpenId";
                    }
                    else
                    {
                        model.Nickname = nickname;
                        model.HeadImage = headImage;
                        model.OpenId = json["openid"].ToString();
                        model.AddTime = DateTime.Now;
                        Member member = MemberBLL.GetModel(t => t.Where(a => a.OpenId == model.OpenId));
                        if (member != null)
                        {
                            member.Nickname = nickname;
                            member.OpenId = json["openid"].ToString();
                            MemberBLL.UpdateModel(member);
                            MemberModel memberModel = new MemberModel()
                            {
                                Id = member.Id,
                                FK_UserId = member.FK_UserId,
                                HeadImage = headImage,
                                Nickname = nickname
                            };
                            Sys_User user = Sys_UserBLL.GetModel(member.FK_UserId.ToLong());
                            if (user != null)
                            {
                                memberModel.UserName = user.RealName;
                            }
                            result.Data = memberModel;
                        }
                        else
                        {
                            model.Id = MemberBLL.AddModel(model);
                            MemberModel memberModel = new MemberModel()
                            {
                                Id = model.Id,
                                FK_UserId = model.FK_UserId,
                                HeadImage = headImage,
                                Nickname = nickname
                            };
                            result.Data = memberModel;
                        }
                    }
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

        #region 获取get请求
        /// <summary>
        /// 获取get请求
        /// </summary>
        /// <param name="appid">appid</param>
        /// <param name="secret">appsecret</param>
        /// <param name="code">code</param>
        /// <returns></returns>
        public static String HttpGet(string appid, string secret, string code)
        {
            Log4Helper.WriteInfo("appid=" + appid + "secret=" + secret + "code=" + code);
            String url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", appid, secret, code);
            string strRet = "";
            string targeturl = url.Trim().ToString();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.UTF8);
                strRet = ser.ReadToEnd();
                Log4Helper.WriteInfo("strRet=" + strRet);
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
                strRet = "";
            }
            return strRet;
        }
        #endregion

        #region 绑定系统用户
        /// <summary>
        /// 绑定系统用户
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="memberId">微信用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BindUser(string appId, string timestamp, string sign, long memberId, string userName, string pwd)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("pwd", pwd);
            pmts.Add("memberId", memberId + "");
            pmts.Add("userName", userName);
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    pwd = EnDecrypt.SHA1Hash(pwd);
                    Sys_User model = Sys_UserBLL.GetUserLoginByPhone(userName, pwd);
                    if (model != null)
                    {
                        if (model.UserName != "admin")
                        {
                            bool bl = MemberBLL.CheckModel(t => t.Where(a => a.FK_UserId == model.Id));
                            if (!bl)
                            {
                                MemberBLL.UpdateModel(new Member { Id = memberId, FK_UserId = model.Id });
                                result.Data = model;
                            }
                            else
                            {
                                result.Code = ResultCode.Failure;
                                result.Message = "该账号已绑定";
                            }
                        }
                        else
                        {
                            result.Code = ResultCode.Failure;
                            result.Message = "账号或密码错误";
                        }
                    }
                    else
                    {
                        result.Code = ResultCode.Failure;
                        result.Message = "账号或密码错误";
                    }
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

        #region 解绑系统用户
        /// <summary>
        /// 解绑系统用户
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="memberId">微信用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UBindUser(string appId, string timestamp, string sign, long memberId)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("memberId", memberId + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    MemberBLL.UpdateModel(new Member() { FK_UserId = 0, Id = memberId });
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = "解绑失败";
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion
    }
}