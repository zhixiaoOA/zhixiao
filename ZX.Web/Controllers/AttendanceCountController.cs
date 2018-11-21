using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;
using System.Collections.Specialized;
using System.IO;
using Aspose.Cells;
using System.Data;

namespace ZX.Web.Controllers
{
    public class AttendanceCountController : BaseController
    {
        #region 统计跳转
        /// <summary>
        /// 统计跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("AttendanceCountList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMy_AttendanceCountList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["key"] ?? "";
                string startTime = Request["startTime"] ?? "";
                int index = 1;
                DataList<My_AttendanceCount> list = My_AttendanceCountBLL.GetMy_AttendanceCountList(key, startTime, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-left'>");
                    builder.Append("<td >" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td >" + item.URealName + "</td>");
                    builder.Append("<td>" + item.LateNo + "</td>");
                    builder.Append("<td>" + item.LeaveEarlyNo + "</td>");
                    builder.Append("<td>" + item.AskNO + "</td>");
                    builder.Append("<td>" + item.AbsenteeismNo + "</td>");
                    builder.Append("<td>" + item.ToleranceNo + "</td>");
                    builder.Append("<td>" + item.AttendanceOutNo + "</td>");
                    builder.Append("<td>" + item.DueDays + "</td>");
                    builder.Append("<td>" + item.ActualAttendanceNo + "</td>");
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

        #region 导入报表
        /// <summary>
        /// 导入报表
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult ImportReport()
        {
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                if (hfc.Count > 0)
                {
                    string savePath = Server.MapPath("~/TempFile");
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    string msg = null, tempPath = null;
                    try
                    {
                        for (int i = 0; i < hfc.Count; i++)
                        {
                            tempPath = savePath + "\\" + hfc[i].FileName;
                            hfc[i].SaveAs(tempPath);
                            ImportAllReport(tempPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg += ex.Message + ";";
                    }
                    if (msg != null)
                    {
                        return Json(new { message = msg, statusCode = 300 }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { message = "导入成功", statusCode = 200 }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "导入失败", statusCode = 300 }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
                return Json(new { message = ex.Message, statusCode = 300 }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region 导入全部报表
        /// <summary>
        /// 导入全部报表
        /// </summary>
        /// <param name="savePath">文档路径</param>
        protected void ImportAllReport(string savePath)
        {
            Workbook book = new Workbook();
            book.Open(savePath);
            Worksheets sheets = book.Worksheets;
            Worksheet sheet = sheets[0];

            Cells cells = sheet.Cells;
            DataTable dt = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxDataColumn + 1, true);
            List<My_AttendanceCount> list = new List<My_AttendanceCount>();
            int i = 1;
            foreach (DataRow item in dt.Rows)
            {
                My_AttendanceCount model = new My_AttendanceCount()
                {
                    URealName = item["姓名"].ConvToString(),
                    LateNo = item["迟到次数"].ConvToString(),
                    LeaveEarlyNo = item["早退次数"].ConvToString(),
                    AskNO = item["请假次数"].ConvToString(),
                    AbsenteeismNo = item["旷工次数"].ConvToString(),
                    ToleranceNo = item["公差次数"].ConvToString(),
                    AttendanceOutNo = item["外出考勤次数"].ConvToString(),
                    DueDays = item["应出勤天数"].ConvToString(),
                    ActualAttendanceNo = item["实际出勤天数"].ConvToString(),
                };
                list.Add(model);
                Log4Helper.WriteInfo(model.URealName + "   " + (i++) + "   " + item["姓名"]);

            }
            My_AttendanceCountBLL.MergeNotDelModel(list, "A.URealName=B.URealName");
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
            string startTime = Request["startTime"] ?? "";
            //认证
            Aspose.Cells.License lic = new Aspose.Cells.License();
            Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(License));
            lic.SetLicense(stream);
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/TempFile/tempAttendanceCount.xlsx"));

            List<My_AttendanceCount> list = My_AttendanceCountBLL.GetList(key, startTime);
            designer.SetDataSource("model", list);
            designer.Process();
            designer.Save(System.Web.HttpUtility.UrlEncode("考勤统计" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", System.Text.Encoding.UTF8),
            SaveType.OpenInExcel,
            FileFormatType.Excel2003,
            System.Web.HttpContext.Current.Response);
            Response.Flush();
            Response.Close();
            designer = null;
            return null;
        }
        #endregion
    }
}