using GX.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZX.Tools;

namespace ZX.Web.Areas.api
{
    public class ApiPmts
    {
        protected HttpContext HttpContext;

        /// <summary>
        /// 请求的参数
        /// </summary>
        protected Hashtable Parameters;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContext"></param>
        public ApiPmts(HttpContext httpContext)
        {
            Parameters = new Hashtable();
            this.HttpContext = httpContext ?? HttpContext.Current;

        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key">key值</param>
        /// <param name="value">value值</param>
        public void Add(string key, string value)
        {
            if (value.IsNullOrEmpty()) return;
            Parameters.Add(key, value);
        }
        /// <summary>
        /// 根据key获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            try
            {
                return Parameters[key] + "";
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="appKey">app密钥</param>
        /// <returns></returns>
        public string GetSige(string appKey)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList arrayKey = new ArrayList(Parameters.Keys);
            arrayKey.Sort();
            foreach (string key in arrayKey)
            {
                string v = (string)Parameters[key];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(key) != 0 && "key".CompareTo(key) != 0)
                {
                    sb.Append(key + "=" + HttpUtility.UrlEncode(v).ToUpper() + "&");
                }
            }
            sb.Append("key=" + appKey);
            Log4Helper.WriteInfo("签名参数:" + sb.ToString());
            return MD5Util.GetMD5(sb.ToString(), GetCharset()).ToUpper();
        }
        /// <summary>
        /// 获取Charset
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCharset()
        {
            if (this.HttpContext == null)
            {
                return "utf-8";
            }
            return this.HttpContext.Request.ContentEncoding.BodyName;
        }
    }
}