using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using System.IO;
using Aspose.Cells;
using ZX.Model.Model;

namespace ZX.Web.Controllers
{

    /// <summary>
    /// 外出考勤
    /// </summary>
    public class My_Attendance_OutController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("My_Attendance_OutList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMy_Attendance_OutList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["key"] ?? "";
                string startTime = Request["startTime"]+" 00:00:00";
                string endTime = Request["endTime"] + " 23:59:59";
                int index = 1;
                DataList<MapPositionModel> list = MapPositionBLL.GetMy_Attendance_OutList(key, startTime, endTime, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-left'>");
                    builder.Append("<td >" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td>" + item.RealName + "</td>");                     
                    builder.Append("<td>" + item.Address+item.Title + "</td>");
                    builder.Append("<td>" + item.Cause + "</td>");
                    builder.Append("<td>" + item.ADate + "</td>");
                    builder.Append("</tr>");
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = list.TotalCount;
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion
        
        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public ActionResult OutExcel()
        {
            string key = Request["key"] ?? "";
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            //认证
            Aspose.Cells.License lic = new Aspose.Cells.License();
            Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(License));
            lic.SetLicense(stream);
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/TempFile/temp_out.xlsx"));

            List<MapPositionModel> list = MapPositionBLL.GetList(key,startTime,endTime);
            designer.SetDataSource("model", list);
            designer.Process();
            designer.Save(System.Web.HttpUtility.UrlEncode("外出考勤"+DateTime.Now.ToString("yyyy-MM-dd") + ".xls", System.Text.Encoding.UTF8),
            SaveType.OpenInExcel,
            FileFormatType.Excel2003,
            System.Web.HttpContext.Current.Response);
            Response.Flush();
            Response.Close();
            designer = null;
            //Response.End();
            return null;
        }
        #endregion
    }
}
