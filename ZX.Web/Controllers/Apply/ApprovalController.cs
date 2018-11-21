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
    public class ApprovalController : BaseController
    {
        #region 跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("ApprovalList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetApprovalList()
        {
            AjaxResult result = new AjaxResult();
            try
            {

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

        #region 获取数据无分页
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetApprovalNotPageList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                string beginTime = Request["beginTime"] ?? "";
                string endTime = Request["endTime"] ?? "";
                int status = Request["status"].ToInt(-1);
                //获取相关审批
                List<AllApplyNotice> allApplyNoticeList = new List<AllApplyNotice>();
                int fk_CompanyPositionId = PositionId.ToInt();
                if (fk_CompanyPositionId != 0)
                {
                    allApplyNoticeList = AllApplyNoticeBLL.GetApplyNoticeList(fk_CompanyPositionId,beginTime,endTime,status);

                    if (allApplyNoticeList.Count > 0)
                    {
                        int rowIndex = 1;
                        StringBuilder builder = new StringBuilder();
                        foreach (var applyNotice in allApplyNoticeList)
                        {
                            string tStatusName = "未知";
                            switch (applyNotice.CurrentState.ToInt())
                            {
                                case 0:
                                    {
                                        tStatusName = "新申请";
                                        break;
                                    }
                                case 1:
                                    {
                                        tStatusName = "审批中";

                                        break;
                                    }
                            }

                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + rowIndex + "</td>");
                            builder.Append("<td class=\"text-left\" title=\""+applyNotice.ADesc + "\"><a href=\""+applyNotice.ApplyAction+"\">"+applyNotice.Title+"</a></td>");
                            builder.Append("<td class='text-left' title='" + applyNotice.ADesc + "'>" + applyNotice.ADesc + "</td>");
                            builder.Append("<td class='text-left' title='" + applyNotice.FlowName + "'>" + applyNotice.FlowName + "</td>");
                            builder.Append("<td>"+ tStatusName + "</td>");

                            builder.Append("</tr>");
                            rowIndex++;
                        }
                        result.Data = builder.ToString();
                    }
                }
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
            Approval model = new Approval();
            try
            {
                if (id > 0)
                {
                    model = ApprovalBLL.GetModel(id.ToInt());
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
            Approval model = FormHelper.GetRequestForm<Approval>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    row = ApprovalBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = ApprovalBLL.AddModel(model);
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
                int row = ApprovalBLL.DelModel("Id IN(" + id + ")");
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
            List<AllApplyNotice> list = new List<AllApplyNotice>();
            int fk_CompanyPositionId = PositionId.ToInt();
            string beginTime = Request["beginTime"] ?? "";
            string endTime = Request["endTime"] ?? "";
            int status = Request["status"].ToInt(-1);
            //认证
            Aspose.Cells.License lic = new Aspose.Cells.License();
            Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(License));
            lic.SetLicense(stream);
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/Temp/Approval_out.xlsx"));

            list = AllApplyNoticeBLL.GetApplyNoticeList(fk_CompanyPositionId, beginTime, endTime, status);
            designer.SetDataSource("model", list);
            designer.Process();
            designer.Save(System.Web.HttpUtility.UrlEncode("审批单汇总" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", System.Text.Encoding.UTF8),
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
