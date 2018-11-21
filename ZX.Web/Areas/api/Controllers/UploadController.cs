using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Model;
using ZX.Tools;
using ZX.BLL;
using System.Drawing;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ZX.Web.Areas.api.Controllers
{
    public class UploadController : BaseController
    {

        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UploadFile()
        {
            string url = "";
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                if (hfc.Count > 0)
                {
                    string direName = "/UploadFile/WX";
                    if (!Directory.Exists(Server.MapPath("~" + direName)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~" + direName));
                    }
                    string typeName = Path.GetExtension(hfc[0].FileName);
                    string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    string savePath = Server.MapPath("~" + direName) + fileName + typeName;
                    hfc[0].SaveAs(savePath);
                    url = "/UploadFile/WX/" + fileName + typeName;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            Log4Helper.WriteInfo("url:" + url);
            return url;
        }
        #endregion

    }
}
