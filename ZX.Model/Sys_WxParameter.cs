using System;
using System.Text;
using ZX.Model;

namespace ZX.Model
{
	#region Sys_WxParameter
    /// <summary>
    /// Sys_WxParameter
    /// </summary>
    [DataFieldAttribute("Sys_WxParameter")]
    public class Sys_WxParameter : BaseModel
    {
		/// <summary>		/// 微信AppId		/// </summary>		[DataFieldAttribute("AppId")]		public string AppId		{			get;			set;		}		/// <summary>		/// 微信AppSecret		/// </summary>		[DataFieldAttribute("AppSecret")]		public string AppSecret		{			get;			set;		}		/// <summary>		/// 微信商户号		/// </summary>		[DataFieldAttribute("MchId")]		public string MchId		{			get;			set;		}		/// <summary>		/// 微信商户支付密钥		/// </summary>		[DataFieldAttribute("Paykey")]		public string Paykey		{			get;			set;		}
    }
    #endregion
}
