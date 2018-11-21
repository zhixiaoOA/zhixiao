using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;
using System.IO;
using Aspose.Cells;

namespace ZX.Web.Controllers
{
	[PermissionFilter]
    public class Assets_LogController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Assets_LogList");
        }
        #endregion

        #region 资产日志数据列表
        /// <summary>
        /// 资产分类
        /// </summary>
        /// <returns></returns>
        public JsonResult AssetsLogList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string aname = Request["anameKey"] ?? "";
                string createAccount = Request["createAccountKey"] ?? "";

                DataList<Assets_LogModel> list = Assets_LogBLL.GetAssets_LogList(aname, createAccount, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + item.ACode + "</td>");
                    builder.Append("<td>" + item.AName + "</td>");
                    builder.Append("<td>" + item.StatusName+ "</td>");
                    builder.Append("<td>" + item.CreateAccount + "</td>");
                    builder.Append("<td>" + item.CreateTime.ToShortDate() + "</td>");
                    builder.Append("<td>" + item.UpdateTime.ToShortDate() + "</td>");
                    builder.Append("<td>" + item.Remark+ "</td>");
                    //builder.Append("<td title='" + item.UpdateUserId + "'>" + item.UpdateUserId + "</td>");
                    //builder.Append("<td title='" + item.UpdateTime + "'>" + item.UpdateTime.ToShortDate() + "</td>");

                    //builder.Append("<td>");
                    //builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                    //builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + mid + "' >编辑</a>");
                    //builder.Append("</li>");
                    //builder.Append("</td>");

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

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            Assets_Log model = new Assets_Log();
            try
            {
                if (id > 0)
                {
                    model = Assets_LogBLL.GetModel(id.ToInt());
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
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
            Assets_Log model = FormHelper.GetRequestForm<Assets_Log>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    row = Assets_LogBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = Assets_LogBLL.AddModel(model);
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
                int row = Assets_LogBLL.DelModel("Id IN(" + id + ")");
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

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public ActionResult OutExcel()
        {
            string aname = Request["anameKey"] ?? "";
            string createAccount = Request["createAccountKey"] ?? "";
            //认证
            Aspose.Cells.License lic = new Aspose.Cells.License();
            Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(License));
            lic.SetLicense(stream);
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/Temp/AssetsLogTemp.xlsx"));

            List<Assets_LogModel> list = Assets_LogBLL.GetAssets_LogListNotPage(aname, createAccount);
            designer.SetDataSource("model", list);
            designer.Process();
            designer.Save(System.Web.HttpUtility.UrlEncode("资产日志" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", System.Text.Encoding.UTF8),
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
