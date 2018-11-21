using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;

namespace ZX.Web.Controllers
{
    public class Sys_RoleController : BaseController
    {
        #region 角色列表初始页
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("Sys_RoleList");
        }
        #endregion

        #region 根据id获取角色信息
        /**
         * 根据角色id获取角色信息
         * 
         * <param name="id">角色id</param>
         */
        public ActionResult Readonly(int? id)
        {
            Sys_Role model = new Sys_Role();
            try
            {
                if (id > 0)
                {
                    model = Sys_RoleBLL.GetModel(id.ToInt());
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 分页获取角色列表
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetSys_RoleList(int mid)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["name"] ?? "";
                DataList<Sys_RoleModel> list = Sys_RoleBLL.GetSys_RoleList(key, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1;

                //行编号
                foreach (var item in list)
                {
                    if (item.RName != "通用角色")
                    {
                        builder.Append("<tr class='text-center'>");
                        builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                        builder.Append("<td>" + item.RName + "</td>");
                        builder.Append("<td>" + item.RDesc + "</td>");
                        builder.Append("<td>" + item.RSort + "</td>");
                        builder.Append("<td class='actions'>");

                        builder.Append(string.Format(CurrentBtnList28, item.Id));
                        builder.Append("</td>");
                        builder.Append("</tr>");
                    }
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = list.TotalCount;
            }
            catch (Exception ex)
            {
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
            Sys_Role model = new Sys_Role();
            try
            {
                if (id > 0)
                {
                    model = Sys_RoleBLL.GetModel(id.ToInt());

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
            Sys_Role model = FormHelper.GetRequestForm<Sys_Role>();
            AjaxResult rest = new AjaxResult();

            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    row = Sys_RoleBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.UpdateTime = model.CreateTime;
                    model.UpdateUserId = UserId;
                    row = Sys_RoleBLL.AddModel(model);

                    Sys_RoleModel roleModel = Sys_RoleBLL.getSysRoleID();
                    List<Sys_Role_Menu> roleMenuLists = Sys_Role_MenuBLL.GetList(t => t.Where(a => a.FK_RoleId == roleModel.Id));
                    if (roleMenuLists.Count > 0)
                    {
                        foreach (var item in roleMenuLists)
                        {
                            item.FK_RoleId = row;
                        }
                        Sys_Role_MenuBLL.AddModel(roleMenuLists);
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
                Sys_Role role = Sys_RoleBLL.GetModel(id.ToInt());
                if (id.ToInt() == 1 || role.RName.Contains("超级管理员"))
                {
                    rest.Code = ResultCode.Failure;
                    rest.Message = "当前权限不足以删除超级管理员角色";
                    return Json(rest);
                }
                int row = Sys_RoleBLL.DelModelById(id);
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