using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ZGN.Web.Scripts.uploadify.jquery.uploadify
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string useropt = context.Request["opt"].ToString();
            string restStr = UploadFile(context,useropt);
            context.Response.Write(restStr);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="context"></param>
        string UploadFile(HttpContext context,string useropt)
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
                    if (useropt == "cert")
                    {
                        uploadPath = HttpContext.Current.Server.MapPath("~/UploadFile/Credentials");
                    }
                    else
                    {
                        uploadPath = HttpContext.Current.Server.MapPath("~/UploadFile/User/headimage");
                    }
                    
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    Random ran = new Random();
                    int num = ran.Next(10000, 99999);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + num + ".png";//Path.GetExtension(itemFile.FileName);
                    uploadPath += "\\" + fileName;

                    itemFile.SaveAs(uploadPath);
                    System.Drawing.Image img = System.Drawing.Image.FromFile(uploadPath);
                    string size = img.PhysicalDimension.Width + "x" + img.PhysicalDimension.Height;
                    if (useropt == "cert")
                    {
                        uploadPath = "/UploadFile/Credentials/" + fileName;
                    }
                    else
                    {
                        uploadPath = "/UploadFile/User/headimage/" + fileName;
                    }

                    img.Dispose();
                    rest = "{\"path\":\"" + uploadPath + "\",\"size\":\"" + size + "\",\"fileName\":\"" + itemFile.FileName.Substring(0, itemFile.FileName.LastIndexOf(".")) + "\"}";
                }
            }
            catch (Exception ex)
            {
                // Log4Helper.WriteError(ex.Message, ex);
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