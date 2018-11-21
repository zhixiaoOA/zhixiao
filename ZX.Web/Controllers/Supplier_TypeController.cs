using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;

namespace ZX.Web.Controllers
{
    [PermissionFilter]
    public class Supplier_TypeController : BaseController
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
                DataList<Supplier_TypeModel> list = Supplier_TypeBLL.GetSupplier_TypeList(name, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.TName + "'>" + item.TName + "</td>");
                    builder.Append("<td class='text-left' title='" + item.TSort + "'>" + item.TSort + "</td>");
                    builder.Append("<td class='text-left' title='" + item.TRemark + "'>" + item.TRemark + "</td>");
                    builder.Append("<td class='text-left'><a href='javascript:edit(" + item.Id + ")'>编辑</a>&nbsp;&nbsp;<a href='javascript:del(" + item.Id + ")'>删除</a></td>");
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
            Supplier_Type model = new Supplier_Type();
            try
            {
                List<Approval> list = ApprovalBLL.GetList();
                ViewBag.List = list;
                if (id > 0)
                {
                    model = Supplier_TypeBLL.GetModel(id.ToInt());
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
                Supplier_Type model = FormHelper.GetRequestForm<Supplier_Type>();
                if (model.Id > 0)
                {
                    Supplier_TypeBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = Supplier_TypeBLL.AddModel(model);
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
                int row = Supplier_TypeBLL.DelModelById(id);
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
