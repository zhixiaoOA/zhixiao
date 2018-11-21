using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using ZX.Tools;

namespace ZX.Web
{
    /// <summary>
    /// 上传图片专用
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string restStr = UploadFile(context);
            context.Response.Write(restStr);
        }

        public string UploadFile(HttpContext context)
        {
            string rest = "";
            try
            {
                context.Request.ContentEncoding = Encoding.GetEncoding("utf-8");
                context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                context.Response.Charset = "utf-8";
                HttpPostedFile itemFile = context.Request.Files["Filedata"];
                string uploadPath = null;
                if (itemFile != null)
                {
                    string typeName = Path.GetExtension(itemFile.FileName);
                    if (typeName.Equals(".jpg") || typeName.Equals(".rar") || typeName.Equals(".zip") || typeName.Equals(".pdf"))
                    {
                        uploadPath = HttpContext.Current.Server.MapPath("~/UploadFile");
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }
                        Random ran = new Random();
                        int num = ran.Next(10000, 99999);
                        string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + num + Path.GetExtension(itemFile.FileName);//Path.GetExtension(itemFile.FileName);
                        uploadPath += "\\" + fileName;
                        string sfileName1 = "small-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ran.Next(10000, 99999) + Path.GetExtension(itemFile.FileName);
                        List<string> strpath = new List<string>();
                        itemFile.SaveAs(uploadPath);
                        System.Drawing.Image img = System.Drawing.Image.FromFile(uploadPath);
                        string size = img.PhysicalDimension.Width + "x" + img.PhysicalDimension.Height;
                        uploadPath = "/UploadFile/" + fileName;
                        img.Dispose();
                        //string url = context.Request.Url.ToString();+ url.Substring(0, url.IndexOf(":") + 1) + "//" + context.Request.Url.Host
                        rest = "{\"code\":\"200\",\"Message\":\"200\",\"path\":\"" + uploadPath + "\",\"size\":\"" + size + "\"}";
                    }
                    else
                    {
                        rest = "{\"code\":\"300\",\"Message\":\"上传文件格式不正确\",\"path\":\"" + uploadPath + "\",\"fileName\":\"" + itemFile.FileName + "\"}";
                    }
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return rest;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}