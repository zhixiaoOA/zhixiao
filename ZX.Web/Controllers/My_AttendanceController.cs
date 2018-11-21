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
using ZX.Model.Model;

namespace ZX.Web.Controllers
{

    /// <summary>
    /// 考勤
    /// </summary>
    public class My_AttendanceController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("My_AttendanceList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMy_AttendanceList()
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
                DataList<My_AttendanceModel> list = My_AttendanceBLL.GetMy_AttendanceList(key,startTime,endTime,pageIndex, pageSize);

                DateTime dt = Convert.ToDateTime(startTime);
                string attenKey = dt.Year + "年" + dt.Month + "月";
                
                List <AttendanceTimeModel> attenTimeList = AttendanceTimeBLL.GetMy_AttendanceTimeList(attenKey);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    //迟到
                    int i = 0;
                    //早退
                    int j = 0;
                    builder.Append("<tr class='text-left'>");
                    builder.Append("<td >" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td>" + item.URealname + "</td>");
                    builder.Append("<td>" + item.ADate + "</td>");                     
                    builder.Append("<td>" + item.StartTime + "</td>");
                    builder.Append("<td>" + item.EndTime + "</td>");
                    builder.Append("<td>" + item.PmStartTime + "</td>");
                    builder.Append("<td>" + item.PmEndTime + "</td>");
                    if (attenTimeList.Count > 0)
                    {
                        DateTime? DTStrStartTime = Convert.ToDateTime(item.StrStartTime);
                        DateTime? DTStrEndTime = Convert.ToDateTime(item.StrStartTime);
                        DateTime? DTStrPmStartTime = Convert.ToDateTime(item.StrPmStartTime);
                        DateTime? DTStrPmEndTime = Convert.ToDateTime(item.StrPmEndTime);

                        TimeSpan ts1 = Convert.ToDateTime(attenTimeList[0].AmStartTime.ToDateFormat("HH:mm")).Subtract(Convert.ToDateTime(DTStrStartTime.ToDateFormat("HH:mm")));
                        TimeSpan ts2 = Convert.ToDateTime(attenTimeList[0].AmEndTime.ToDateFormat("HH:mm")).Subtract(Convert.ToDateTime(DTStrEndTime.ToDateFormat("HH:mm")));
                        TimeSpan ts3 = Convert.ToDateTime(attenTimeList[0].PmStartTime.ToDateFormat("HH:mm")).Subtract(Convert.ToDateTime(DTStrPmStartTime.ToDateFormat("HH:mm")));
                        TimeSpan ts4 = Convert.ToDateTime(attenTimeList[0].PmEndTime.ToDateFormat("HH:mm")).Subtract(Convert.ToDateTime(DTStrPmEndTime.ToDateFormat("HH:mm")));

                        if (ts1.Hours >= 0 && ts3.Hours >= 0)
                        {
                            if (ts1.Minutes >= 0 && ts3.Minutes >= 0)
                                i = 0;
                            else if (ts1.Minutes < 0 && ts3.Minutes > 0)
                                i = 1;
                            else if (ts1.Minutes > 0 && ts3.Minutes < 0)
                                i = 1;
                            else if (ts1.Minutes < 0 && ts3.Minutes < 0)
                                i = 2;
                        }
                        else if (ts1.Hours > 0 && ts3.Hours < 0)
                            i = 1;
                        else if (ts1.Hours < 0 && ts3.Hours > 0)
                            i = 1;
                        else if (ts1.Hours < 0 && ts3.Hours < 0)
                            i = 2;

                        if (ts2.Hours <= 0 && ts4.Hours <= 0)
                        {
                            if (ts2.Minutes > 0 && ts4.Minutes > 0)
                                j = 2;
                            if (ts2.Minutes > 0 && ts4.Minutes < 0)
                                j = 1;
                            if (ts2.Minutes < 0 && ts4.Minutes > 0)
                                j = 1;
                            if (ts2.Minutes <= 0 && ts4.Minutes <= 0)
                                j = 0;
                        }
                        else if (ts2.Hours > 0 && ts4.Hours < 0)
                            j = 1;
                        else if (ts2.Hours < 0 && ts4.Hours > 0)
                            j = 1;
                        else if (ts2.Hours > 0 && ts4.Hours > 0)
                            j = 2;
                    }
                    builder.Append("<td>" + i + "</td>");
                    builder.Append("<td>" + j + "</td>");
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
            List<My_Attendance> list = new List<My_Attendance>();
            int i = 1;
            foreach (DataRow item in dt.Rows)
            {
                My_Attendance model = new My_Attendance()
                {
                    URealname = item["姓名"].ConvToString(),
                    StartTime = Convert.ToDateTime(item["上午上班打卡时间"].ConvToString()),
                    EndTime = Convert.ToDateTime(item["上午下班打卡时间"].ConvToString()),
                    PmStartTime = Convert.ToDateTime(item["下午上班打卡时间"].ConvToString()),
                    PmEndTime = Convert.ToDateTime(item["下午下班打卡时间"].ConvToString()),
                    ADate = item["日期"].ConvToString(),
                    CreateUserId = UserId,
                    CreateTime = DateTime.Now,
                    CreateAccount=UserAccount
                };
                list.Add(model);
                Log4Helper.WriteInfo(model.URealname+"   "+(i++)+"   "+ item["上午上班打卡时间"]);
                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 2)
                {
                    break;
                }
            }
            My_AttendanceBLL.MergeNotDelModel(list, "A.URealname=B.URealname AND A.StartTime=B.StartTime AND A.CardNo=B.CardNo");
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
            designer.Open(Server.MapPath("~/TempFile/temp.xlsx"));
            endTime = endTime + " 23:59:59";
            List<My_AttendanceModel> list = My_AttendanceBLL.GetList(key,startTime,endTime);
            designer.SetDataSource("model", list);
            designer.Process();
            designer.Save(System.Web.HttpUtility.UrlEncode("考勤"+DateTime.Now.ToString("yyyy-MM-dd") + ".xls", System.Text.Encoding.UTF8),
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
