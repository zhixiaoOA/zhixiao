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
    public class Sys_ButtonController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Sys_ButtonList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetSys_ButtonList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                string key = Request["key"] ?? "";
                DataList<Sys_ButtonModel> list = Sys_ButtonBLL.GetSys_ButtonList(key, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td><input type=\"checkbox\" class=\"cbox\" name=\"cbox\" value=\"" + item.Id + "\" /></td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Id + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BName + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.FK_MenuId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BDesc + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BSort + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");

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
        public ActionResult AddEdit(int? id,int? fk_menuId)
        {
            ViewBag.ButtonType= GetButtonType();
            Sys_Button model = new Sys_Button(){};
            try
            {
                if (id > 0)
                {
                    model = Sys_ButtonBLL.GetModel(id.ToInt());
                }
                else {
                    model.FK_MenuId = fk_menuId;
                }

                Sys_Menu menu = Sys_MenuBLL.GetModel(model.FK_MenuId);
                ViewBag.MenuName = menu.MName;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion


        #region Readonly
        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult Readonly(int? id)
        {
            ViewBag.ButtonType = GetButtonType();
            Sys_Button model = new Sys_Button() { };
            try
            {
                if (id > 0)
                {
                    model = Sys_ButtonBLL.GetModel(id.ToInt());
                    Sys_Menu menu = Sys_MenuBLL.GetModel(model.FK_MenuId);
                    ViewBag.MenuName = menu.MName;
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
            Sys_Button model = FormHelper.GetRequestForm<Sys_Button>();
            AjaxResult rest = new AjaxResult();
            try
            {
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    Sys_ButtonBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.UpdateTime = model.CreateTime;
                    model.UpdateUserId = UserId;
                    model.Id = Sys_ButtonBLL.AddModel(model);

                    if (model.Id > 0)
                    {
                        Sys_Role_Menu sys_Role_Menu = new Sys_Role_Menu()
                        {
                            FK_MenuButtonId = model.Id.ToInt(),
                            FK_RoleId = 1,
                            TypeId = 2
                        };
                        Sys_Role_MenuBLL.AddModel(sys_Role_Menu);
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
                int row = Sys_ButtonBLL.DelModel(" Id =" + id);
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


        #region 获取按钮的操作类型
        public List<SelectListItem> GetButtonType()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(ButtonType)))
            {             
                int bType= item.ToInt();    
                string typeDesc = Common.EnumGetDesc.GetEnumDescription((ButtonType)Enum.Parse(typeof(ProjectStatus), bType.ToString()));
                listItem.Add(new SelectListItem { Text = typeDesc, Value = bType.ToString() });
            }
            return listItem;
        }

        #endregion
    }
}
