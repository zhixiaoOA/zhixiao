using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;
using System.Text;

namespace ZX.Web.Controllers
{
    public class ApprovalTotal : BaseController
    {
        // GET: MyApply
        public ActionResult Index()
        {
            List<Approval_Type> listType = Approval_TypeBLL.GetList();
            ViewBag.ListType = listType;
            ViewBag.CurId = Request["tid"].ToInt() == 0 ? (listType != null ? listType.FirstOrDefault().Id : 0) : Request["tid"].ToInt();
            return View("MyApplyList");
        }


        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["key"] ?? "";
                string beginTime = Request["beginTime"] ?? "";
                string endTime = Request["endTime"] ?? "";
                int typeId = Request["typeId"].ToInt();
                int status = Request["status"].ToInt(-1);
                DataList<ApplyFlowModel> list = ApplyFlowBLL.GetApplyFlowList(typeId, key, beginTime, endTime, status, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1; string operName = "";
                foreach (var item in list)
                {
                    operName = "";
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.Title + "'>" + item.Title + "</td>");
                    builder.Append("<td class='text-left' title='" + item.StatusName + "'>" + item.StatusName + "</td>");
                    builder.Append("<td class='text-left' title='" + item.AddTime.ToDateFormat() + "'>" + item.AddTime.ToDateFormat() + "</td>");
                    if (item.Status == ApplyStatus.新申请.ToInt())
                    {
                        operName = "<a href='javascript:edit(" + item.Id + ")'>编辑</a>&nbsp;&nbsp;<a href='javascript:del(" + item.Id + ")'>删除</a>";
                    }
                    builder.Append("<td class='text-left'>" + operName + "</td>");
                    builder.Append("</tr>");
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = list.TotalCount;
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = "获取数据失败";
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
            ApplyFlow model = new ApplyFlow();
            try
            {
                if (id > 0)
                {
                    model = ApplyFlowBLL.GetModel(id.ToInt());
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
            AjaxResult rest = new AjaxResult();
            try
            {
                ApplyFlow model = FormHelper.GetRequestForm<ApplyFlow>();
                if (model.Id > 0)
                {
                    ApplyFlowBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = ApplyFlowBLL.AddModel(model);
                }
            }
            catch (Exception ex)
            {
                rest.Message = "保存失败";
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
                int row = ApplyFlowBLL.DelModelById(id);
                if (row == 0)
                {
                    rest.Message = "删除失败";
                    rest.Code = ResultCode.Failure;
                }
            }
            catch (Exception ex)
            {
                rest.Message = "删除失败";
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(rest);
        }
        #endregion
    }
}