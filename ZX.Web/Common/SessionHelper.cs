using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZX.Model;

namespace ZX.Web
{
    public class SessionHelper
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        static int Timeout = 30;
        #region 写入登录session
        /// <summary>
        /// 写入登录session
        /// </summary>
        /// <param name="model"></param>
        public static void WriteLoginSession(Sys_User model)
        {
            Add("UserId", model.Id + "");
            Add("UserName", model.RealName);
            Add("UserRoleId", model.Fk_RoleId.ToString());
            Add("UserAccount", model.UserName);
            Add("RealName", model.RealName);
            Add("DeptId", model.Fk_DeptId + "");
            Add("PositionId", model.FK_CompanyPositionId + "");
        }
        #endregion
        /// <summary>
        /// 读取某个Session的值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns></returns>
        public static string Get(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName].ToString();
            }
        }
        /// <summary>   
        /// 添加Session，调动有效期为30分钟   
        /// </summary>   
        /// <param name="strSessionName">Session对象名称</param>   
        /// <param name="strValue">Session值</param>   
        public static void Add(string strSessionName, string strValue)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = Timeout;
        }
    }
}