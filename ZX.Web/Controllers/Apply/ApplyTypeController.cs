using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ZX.Web.Controllers
{
    public class ApplyTypeController : BaseController
    {
        // GET: ApplyType
        public ActionResult Index()
        {
            return View("ApplyTypeList");
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
                string name = Request["name"] ?? "";
                DataList<Approval_TypeModel> list = Approval_TypeBLL.GetApproval_TypeList(name, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.AName + "'>" + item.AName + "</td>");
                    builder.Append("<td class='text-left' title='" + (item.IsSdong == 1 ? "手动" : "自动") + "'>" + (item.IsSdong == 1 ? "手动" : "自动") + "</td>");
                    builder.Append("<td class='text-left' title='" + item.DName + "'>" + item.DName + "</td>");
                    builder.Append("<td class='text-left'>" + string.Format(CurrentBtnList28, item.Id) + "</td>");
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
            Approval_Type model = new Approval_Type();
            try
            {
                List<Approval> list = ApprovalBLL.GetList();
                ViewBag.List = list;
                if (id > 0)
                {
                    model = Approval_TypeBLL.GetModel(id.ToInt());
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 查看跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult Readonly(int? id)
        {
            Approval_Type model = new Approval_Type();
            try
            {
                List<Approval> list = ApprovalBLL.GetList();
                ViewBag.List = list;
                if (id > 0)
                {
                    model = Approval_TypeBLL.GetModel(id.ToInt());
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
                Approval_Type model = FormHelper.GetRequestForm<Approval_Type>();
                if (model.Id > 0)
                {
                    Approval_TypeBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = Approval_TypeBLL.AddModel(model);
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
                int row = Approval_TypeBLL.DelModelById(id);
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