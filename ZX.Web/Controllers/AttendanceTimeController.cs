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
    public class AttendanceTimeController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("AttendanceTimeList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMy_AttendanceTimeList()
        {
            AjaxResult result = new AjaxResult();
            try
            {               
                string key = Request["key"];
                List<AttendanceTimeModel> list = AttendanceTimeBLL.GetMy_AttendanceTimeList(key);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-left'>");
                    builder.Append("<td rowspan='2'>" + item.Date + "</td>");
                    builder.Append("<td rowspan='2'>" + item.ApplicableObject + "</td>");
                    builder.Append("<td rowspan='2'>" + item.WorkDay + "</td>");
                    builder.Append("<td>" + item.AmStartTime.ToDateFormat("HH:mm")+"-"+ item.AmEndTime.ToDateFormat("HH:mm")+"</td>");
                    builder.Append("<td rowspan='2'>" + item.WorkDays + "</td>");
                    builder.Append("<td rowspan='2'>");
                    builder.Append("<a href='Javascript:;' onclick='edit(" + item.Id + ")' >编辑</a>&nbsp;&nbsp;");
                    builder.Append("<a href='Javascript:;' onclick='del(" + item.Id + ")' >删除</a>&nbsp;&nbsp;");
                    builder.Append("</td>");
                    builder.Append("</tr>");

                    builder.Append("<tr class='text-left'>");
                    builder.Append("<td>" + item.PmStartTime.ToDateFormat("HH:mm")+"-"+ item.PmEndTime.ToDateFormat("HH:mm") + "</td>");
                    builder.Append("</tr>");
                }
                result.Data = builder.ToString();
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

        #region 编辑添加
        public ActionResult AddEdit(int? id)
        {
            AttendanceTime model = new AttendanceTime();
            try
            {
                Sys_UserModel user = Sys_UserBLL.GetUserById(UserId);
                ViewBag.User = user;
                if (id > 0)
                {
                    model = AttendanceTimeBLL.GetModel(id.ToInt());
                }
                Dictionary dict = DictionaryBLL.GetModel(t => t.Where(a => a.Name == "考勤设置单双休"));
                //获取单双休
                List<Dictionary> list = DictionaryBLL.GetList(t => t.Where(a => a.ParentId == dict.Id).OrderBy(a => a.Sort));
                ViewBag.ListApplyMoneyUnit = list;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
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
            List<AttendanceTime> list = new List<AttendanceTime>();
            int i = 1;
            foreach (DataRow item in dt.Rows)
            {
                AttendanceTime model = new AttendanceTime()
                {
                    Date = item["日期"].ConvToString(),
                    ApplicableObject = item["适用对象"].ConvToString(),
                    WorkDay = item["工作日"].ConvToString(),
                    AmStartTime = Convert.ToDateTime(item["上午上班时间"].ConvToString()),
                    AmEndTime = Convert.ToDateTime(item["上午下班时间"].ConvToString()),
                    PmStartTime = Convert.ToDateTime(item["下午上班时间"].ConvToString()),
                    PmEndTime = Convert.ToDateTime(item["下午下班时间"].ConvToString()),
                    WorkDays = item["本月应出勤天数"].ToString(),
                };
                list.Add(model);
                Log4Helper.WriteInfo(model.Date + "   " + (i++) + "   " + item["日期时间"]);

            }
            AttendanceTimeBLL.MergeNotDelModel(list, "A.Date=B.Date");
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
            //认证
            Aspose.Cells.License lic = new Aspose.Cells.License();
            Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(License));
            lic.SetLicense(stream);
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/TempFile/tempAttendanceTime.xlsx"));

            List<AttendanceTimeModel> list = AttendanceTimeBLL.GetExcelList(key);
            designer.SetDataSource("model", list);
            designer.Process();
            designer.Save(System.Web.HttpUtility.UrlEncode("考勤设置" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", System.Text.Encoding.UTF8),
            SaveType.OpenInExcel,
            FileFormatType.Excel2003,
            System.Web.HttpContext.Current.Response);
            Response.Flush();
            Response.Close();
            designer = null;
            return null;
        }
        #endregion


        #region 保存数据
        [ValidateInput(false)]
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            AttendanceTime model = FormHelper.GetRequestForm<AttendanceTime>();
            AjaxResult rest = new AjaxResult();
            try
            {
                if (model.Id > 0)
                {
                    AttendanceTimeBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = AttendanceTimeBLL.AddModel(model);
                }
            }
            catch (Exception ex)
            {
                rest.Message = ex.Message;
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(rest);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public JsonResult DeleteData(string id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = AttendanceTimeBLL.DelModelById(id);
                if (row == 0)
                {
                    rest.Message = "删除失败";
                    rest.Code = ResultCode.Failure;
                }
            }
            catch (Exception ex)
            {
                rest.Message = ex.Message;
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(rest);
        }
        #endregion
    }
}

