using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Tools
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 登陆超时
        /// </summary>
        NoLogin = 100,
        /// <summary>
        /// 200
        /// </summary>
        Succeed = 200,
        /// <summary>
        /// 300
        /// </summary>
        Failure = 300,
        /// <summary>
        /// 400
        /// </summary>
        PasswordError = 400
    }
    /// <summary>
    /// 微信小程序状态码
    /// </summary>
    public enum ResultWxCode
    {
        /// <summary>
        /// -1 参数不合法
        /// </summary>
        参数不合法 = -1,
        /// <summary>
        /// 0 请求通过
        /// </summary>
        通过 = 0,
        /// <summary>
        /// 500 请求错误
        /// </summary>
        请求错误 = 500,
        /// <summary>
        /// 1000 Appid为空
        /// </summary>
        Appid为空 = 1000,
        /// <summary>
        /// 1005 Appid不对
        /// </summary>
        Appid错误 = 1005,
        /// <summary>
        /// 1010 时间戳为空
        /// </summary>
        时间戳为空 = 1010,
        /// <summary>
        /// 1015 接口超时
        /// </summary>
        接口超时 = 1015,
        /// <summary>
        /// 1020 加密后的密钥不一致
        /// </summary>
        加密后的密钥不一致 = 1020,
        /// <summary>
        /// 1025 用户未绑定账号
        /// </summary>
        用户未绑定账号 = 1025
    }
    public class AjaxResult
    {
        public AjaxResult()
        {
            Code = ResultCode.Succeed;
            Message = "操作成功";
        }
        public object Data { get; set; }
        public int PageIndex { get; set; }
        public int PageTotal { get; set; }
        public long TotalCount { get; set; }
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }

        public string Postfix { get; set; }
    }

    public class MessageResult
    {
        public MessageResult()
        {
            Code = ResultCode.Succeed;
            Message = "成功";
        }
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public string CallbackUrl { get; set; }
    }

    public class AjaxApiResult
    {

        public AjaxApiResult()
        {
            Code = 0;
            Message = "操作成功";
        }
        public object Data { get; set; }
        public int PageIndex { get; set; }
        public int PageTotal { get; set; }
        public long TotalCount { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
