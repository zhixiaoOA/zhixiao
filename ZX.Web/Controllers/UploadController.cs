using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace ZX.Web.Controllers
{
    public class UploadController : Controller
    {

        public JsonResult UploadFile()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string uploadPath = "";
                if (hfc.Count > 0)
                {
                    string direName = "/UploadFile";
                    if (!Directory.Exists(Server.MapPath("~" + direName)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~" + direName));
                    }
                    string typeName = Path.GetExtension(hfc[0].FileName);
                    string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    string imgPath = Server.MapPath("~" + direName) + fileName + typeName;
                    hfc[0].SaveAs(imgPath);
                    uploadPath = "/UploadFile" + fileName + typeName;
                    result.Data = uploadPath;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }

        public JsonResult UploadFileByDire(string direName)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string uploadPath = "";
                if (hfc.Count > 0)
                {
                    if (direName.IsNullOrEmpty())
                    {
                        direName = "/UploadFile/Default";
                    }
                    else {
                        direName = "/UploadFile"+direName;
                    }
                    if (!Directory.Exists(Server.MapPath("~" + direName)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~" + direName));
                    }
                    string typeName = Path.GetExtension(hfc[0].FileName);
                    string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    string imgPath = Server.MapPath("~" + direName) + fileName + typeName;
                    hfc[0].SaveAs(imgPath);
                    uploadPath = direName + fileName + typeName;
                    result.Data = uploadPath;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }

        public JsonResult UploadAttach(string direName)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string uploadPath = "";
                if (hfc.Count > 0)
                {
                    if (direName.IsNullOrEmpty())
                    {
                        direName = "/UploadFile/Default";
                    }
                    else
                    {
                        direName = "/UploadFile" + direName;
                    }
                    if (!Directory.Exists(Server.MapPath("~" + direName)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~" + direName));
                    }
                    string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    int f = hfc[0].FileName.LastIndexOf(".");
                    string postfix = hfc[0].FileName.Substring(f, hfc[0].FileName.Length - f);
                    string imgPath = Server.MapPath("~" + direName) + fileName + postfix;
                   
                    hfc[0].SaveAs(imgPath);
                    uploadPath = direName + fileName + postfix;
                    result.Data = uploadPath;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }


        public JsonResult UploadUsginFile(string direName)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string uploadPath = "";
                if (hfc.Count > 0)
                {
                    if (direName.IsNullOrEmpty())
                    {
                        direName = "/UploadFile/Default";
                    }
                    else
                    {
                        direName = "/UploadFile" + direName;
                    }

                    if (!Directory.Exists(Server.MapPath("~" + direName)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~" + direName));
                    }
                    string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    string imgPath = Server.MapPath("~" + direName) + fileName + ".png";
                    hfc[0].SaveAs(imgPath);
                    uploadPath = direName+ fileName + ".png";
                    result.Data = uploadPath;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }


        public JsonResult UploadFileNotRename(string direName)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string uploadPath = "";
                if (hfc.Count > 0)
                {
                    if (direName.IsNullOrEmpty())
                    {
                        direName = "/UploadFile/Default";
                    }
                    else
                    {
                        direName = "/UploadFile" + direName;
                    }
                    if (!Directory.Exists(Server.MapPath("~" + direName)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~" + direName));
                    }
                    string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    int f = hfc[0].FileName.LastIndexOf(".");
                    string postfix = hfc[0].FileName.Substring(f, hfc[0].FileName.Length - f);
                    string imgPath = Server.MapPath("~" + direName) + fileName + postfix;

                    hfc[0].SaveAs(imgPath);
                    uploadPath = direName + fileName + postfix;
                    result.Data = uploadPath;
                    result.Remark = hfc[0].FileName;
                    result.Postfix = postfix;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
    }
}