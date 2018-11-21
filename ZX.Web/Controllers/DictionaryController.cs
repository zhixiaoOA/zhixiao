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
    public class DictionaryController : BaseController
    {
        #region 初始化查询职位数据
        /// <summary>
        /// 初始化查询职位数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                List<Dictionary> list = DictionaryBLL.GetList(d=>d.OrderBy(t=>t.Sort));
                StringBuilder builder = new StringBuilder();
                string btnList = "";
                foreach (var item in list.Where(t => t.ParentId == 0))
                {
                    btnList = string.Format(CurrentBtnList28, item.Id + "," + item.ParentId);
                    builder.Append("<tr id=\"" + item.Id + "\" pid=\"" + item.ParentId + "\">");
                    builder.Append("<td class='text-left'>" + item.Name + "</td>");
                    builder.Append("<td class='text-left'>" + btnList);
                    builder.Append("<input type\"text\" name=\"Sort\" value=\"" + item.Sort + "\" style=\"width:65px;\" placeholder=\"排序\" autocomplete=\"off\" onblur=\"onSort(" + item.Id + ",this)\" onkeypress = \"$.admin.keyPress(this)\" onkeyup = \"$.admin.keyUp(this)\" onblur = \"$.admin.keyBlur(this)\"></td>");
                    builder.Append("</tr>");
                    Recursion(list, builder, item.Id);
                }
                ViewBag.List = builder;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            ViewBag.RealName = RealName;
            return View("DictionaryList");
        }
        #endregion

        #region 递归构造树形列表结构
        /// <summary>
        /// 递归构造树形列表结构
        /// </summary>
        /// <param name="lists">全部列表数据</param>
        /// <param name="builder">输出html数据</param>
        /// <param name="parentId">父节点id</param>
        void Recursion(List<Dictionary> lists, StringBuilder builder, long parentId)
        {
            string btnList = "";
            foreach (var item in lists)
            {
                btnList = string.Format(CurrentBtnList28, item.Id + "," + item.ParentId);
                if (item.ParentId == parentId)
                {
                    builder.Append("<tr id=\"" + item.Id + "\" pid=\"" + item.ParentId + "\">");
                    builder.Append("<td class='text-left'>" + item.Name + "</td>");
                    builder.Append("<td class='text-left'>" + btnList);
                    builder.Append("<input type\"text\" name=\"Sort\" value=\"" + item.Sort + "\" style=\"width:65px;\" placeholder=\"排序\" autocomplete=\"off\" onblur=\"onSort(" + item.Id + ",this)\" onkeypress = \"$.admin.keyPress(this)\" onkeyup = \"$.admin.keyUp(this)\" onblur = \"$.admin.keyBlur(this)\"></td>");
                    builder.Append("</tr>");
                    if (lists.Count(t => t.ParentId == item.Id) > 0)
                    {
                        Recursion(lists, builder, item.Id);
                    }
                }
            }
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
            Dictionary model = new Dictionary()
            {
                Id = id.ToInt(),
                ParentId = Request["pid"].ToInt()
            };
            try
            {
                List<Dictionary> list = DictionaryBLL.GetList();
                Dictionary dic = new Dictionary()
                {
                    Id = 0,
                    Name = "无",
                    ParentId = -1,
                    Sort = -1
                };
                list.Add(dic);
                var items = list.Select(t => new { id = t.Id.ToInt(), name = t.Name, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                ViewBag.TreeJson = items.ToJsonSerialize();
                if (id > 0)
                {
                    model = DictionaryBLL.GetModel(id.ToInt());
                }
                if (model.ParentId > 0)
                {
                    ViewBag.DictionaryName = list.FirstOrDefault(t => t.Id == model.ParentId).Name;
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
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveData(string data)
        {
            Dictionary model = FormHelper.GetRequestForm<Dictionary>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    row = DictionaryBLL.UpdateModel(model);
                }
                else
                {
                    model.AddTime = DateTime.Now;
                    row = DictionaryBLL.AddModel(model);
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
            try
            {
                bool bl = DictionaryBLL.CheckModel(t => t.Where(a => a.ParentId == id));
                if (bl)
                {
                    rest.Message = "请先删除子项";
                    rest.Code = ResultCode.Failure;
                }
                else
                {
                    int row = DictionaryBLL.DelModel(id);
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

        #region 修改排序
        /// <summary>
        /// 修改排序
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public JsonResult UpdateSort(int id, int? sort)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                DictionaryBLL.UpdateModel(new Dictionary() { Id = id, Sort = sort.ToInt() });
            }
            catch (Exception ex)
            {
                rest.Message = "操作失败";
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(rest);
        }
        #endregion
    }
}
