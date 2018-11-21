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
    public class Assets_TypeController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Assets_TypeList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetAssets_TypeList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                string key = Request["key"] ?? "";
                DataList<Assets_TypeModel> list = Assets_TypeBLL.GetAssets_TypeList(key, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td><input type=\"checkbox\" class=\"cbox\" name=\"cbox\" value=\"" + item.Id + "\" /></td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Id + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.TName + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ParentId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.TRemark + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.TSort + "</td>");
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
            Assets_Type model = new Assets_Type();
            try
            {
                if (id > 0)
                {
                    model = Assets_TypeBLL.GetModel(id.ToInt());
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
            Assets_Type model = FormHelper.GetRequestForm<Assets_Type>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    row = Assets_TypeBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = Assets_TypeBLL.AddModel(model);
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
        public JsonResult DeleteData(int id)
        {
            AjaxResult rest = new AjaxResult();
            int row = 0;
            try
            {
                bool bl = Assets_TypeBLL.CheckModel(t => t.Where(a => a.ParentId == id));
                if (bl)
                {
                    rest.Message = "请先删除子分类";
                    rest.Code = ResultCode.Failure;
                }
                else
                {
                    row = Sys_MenuBLL.DelModelById(id.ToString());
                    if (row == 0)
                    {
                        rest.Message = "删除失败";
                        rest.Code = ResultCode.Failure;
                    }
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
