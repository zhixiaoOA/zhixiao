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

    public class Menu_Block_SortController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("Menu_Block_SortList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMenu_Block_SortList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                string key = Request["key"] ?? "";
                DataList<Menu_Block_SortModel> list = Menu_Block_SortBLL.GetMenu_Block_SortList(key, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td><input type=\"checkbox\" class=\"cbox\" name=\"cbox\" value=\"" + item.Id + "\" /></td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Id + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.FK_Menu_BlockId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.SName + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.SortField + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                    builder.Append("</tr>");
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.PageTotal = list.TotalPages;
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
            Menu_Block_Sort model = new Menu_Block_Sort();
            try
            {
                if (id > 0)
                {
                    model = Menu_Block_SortBLL.GetModel(id.ToInt());
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
            Menu_Block_Sort model = FormHelper.GetRequestForm<Menu_Block_Sort>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    row = Menu_Block_SortBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = Menu_Block_SortBLL.AddModel(model);
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
                int row = Menu_Block_SortBLL.DelModel("Id IN(" + id + ")");
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
