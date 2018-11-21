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
    public class Sys_Role_MenuController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("Sys_Role_MenuList");
        }
        #endregion

        #region 角色权限
        public JsonResult GetSys_RolePower(int rid)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                StringBuilder builder = new StringBuilder();
                List<Sys_Menu> listMenu = Sys_MenuBLL.GetList(t => t.OrderBy(a => a.MSort));
                //获取所有按钮列表
                List<Sys_Button> bList = new List<Sys_Button>();
                bList = Sys_ButtonBLL.GetList();

                //获取已有权限
                List<Sys_Role_Menu> roleMenuLists = Sys_Role_MenuBLL.GetList(t => t.Where(a => a.FK_RoleId == rid));

                //循环菜单生成html
                foreach (var item in listMenu.Where(t => t.MParentId == 0))
                {

                    builder.Append("<div class='item'>");
                    builder.Append("<div class='item-content'>");
                    builder.Append("<table class='table table-hover table-bordered table-priv'>");
                    builder.Append("<tr>");

                    int nListCounts = listMenu.Where(t => t.MParentId == item.Id).Count();
                    builder.Append("<th rowspan='" + nListCounts + "' class='w-120px'>");
                    builder.Append("<label class='checkbox-inline'>" + item.MName);
                    builder.Append("<input type='checkbox' class='checkApp' name='menu' onclick='checkAppClick(this)'");
                    if (roleMenuLists.Count(d => d.FK_MenuButtonId == item.Id && d.TypeId == 1) > 0)
                    {
                        builder.Append(" checked ");
                    }
                    builder.Append(" value='" + item.Id + "' />");

                    builder.Append("</label>");
                    if (nListCounts > 0)
                    {
                        Recursion(listMenu, item.Id, bList, roleMenuLists, ref builder);
                    }
                    else //添加空td
                    {
                        builder.Append("<th class='text-right w-120px'>");
                        builder.Append("<label class='checkbox-inline'>");
                        builder.Append("</label>");
                        builder.Append("</th>");
                        builder.Append("<td>");

                        builder.Append("<label class='checkbox-inline'>");
                        builder.Append("</label>");

                        builder.Append("</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</th>");
                    builder.Append("</tr>");
                    builder.Append("</table>");
                    builder.Append("</div>");
                    builder.Append("</div>");
                }
                builder.Append("<div class='panel'>");
                builder.Append("<div class='panel-footer text-center'>");

                //按钮权限可见性控制
                List<Sys_Button> btnList = RoleButtonList;

                builder.Append("<button id='submit' type='button' class='btn btn-primary' onclick='saveDate()'>保存</button>");

                builder.Append("<a href='javascript:;' class='btn btn-back ' onclick='goBackToList()'>返回</a>");
                builder.Append("<input type='hidden' name='noChecked' id='noChecked' value='' />");
                builder.Append("</div>");
                builder.Append("</div>");
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }

            return Json(result);
        }

        #region 递归查询子集
        /// <summary>
        /// 递归查询子集
        /// </summary>
        /// <param name="nLists">子集</param>
        /// <param name="parentID">父id</param>
        /// <param name="builder">返回字符</param>
        public void Recursion(List<Sys_Menu> nLists, long parentID, List<Sys_Button> bList, List<Sys_Role_Menu> roleMenuLists, ref StringBuilder builder)
        {
            int i = 0; //记录循环第几次
            foreach (var item in nLists.Where(t => t.MParentId == parentID))
            {
                if (i == 0)
                {
                    builder.Append("<th class='text-right w-120px'>");
                    builder.Append("<label class='checkbox-inline'>" + item.MName);
                    builder.Append("<input type='checkbox' pid='" + parentID + "' name='menu' class='checkModule' onclick='checkModuleClick(this)' value='" + item.Id + "'");
                    if (roleMenuLists.Count(d => d.FK_MenuButtonId == item.Id && d.TypeId == 1) > 0)
                    {
                        builder.Append(" checked ");
                    }
                    builder.Append("/>");
                    builder.Append("</label>");
                    builder.Append("</th>");
                    builder.Append("<td id='" + item.BlockSign + "'>");
                    //菜单所拥有按钮
                    foreach (var btnItem in bList.Where(d => d.FK_MenuId == item.Id).OrderBy(d => d.BSort))
                    {
                        builder.Append("<label class='checkbox-inline'>");
                        builder.Append("<input type='checkbox' pid='" + parentID + "' btnpid='" + item.Id + "' name='btn' value='" + btnItem.Id + "'");
                        if (roleMenuLists.Count(d => d.FK_MenuButtonId == btnItem.Id && d.TypeId == 2) > 0)
                        {
                            builder.Append(" checked ");
                        }
                        builder.Append("/>" + btnItem.BName);
                        builder.Append("</label>");
                    }

                    builder.Append("</td>");
                    builder.Append("</tr>");

                    continue;
                }
                builder.Append("<tr>");
                builder.Append("<th class='text-right w-120px'>");
                builder.Append("<label class='checkbox-inline'>" + item.MName);
                builder.Append("<input type='checkbox' pid='" + parentID + "' name='menu' class='checkModule' onclick='checkModuleClick(this)' value='" + item.Id + "'");
                if (roleMenuLists.Count(d => d.FK_MenuButtonId == item.Id && d.TypeId == 1) > 0)
                {
                    builder.Append(" checked ");
                }
                builder.Append("/>");
                builder.Append("</label>");
                builder.Append("</th>");
                builder.Append("<td id='" + item.BlockSign + "'>");
                //菜单所拥有按钮
                foreach (var btnItem in bList.Where(d => d.FK_MenuId == item.Id).OrderBy(d => d.BSort))
                {
                    builder.Append("<label class='checkbox-inline'>");
                    builder.Append("<input type='checkbox' pid='" + parentID + "' btnpid='" + item.Id + "' name='btn' value='" + btnItem.Id + "'");
                    if (roleMenuLists.Count(d => d.FK_MenuButtonId == btnItem.Id && d.TypeId == 2) > 0)
                    {
                        builder.Append(" checked ");
                    }
                    builder.Append("/>" + btnItem.BName);
                    builder.Append("</label>");
                }

                builder.Append("</td>");
                builder.Append("</tr>");

                i++;
            }

        }
        #endregion
        #endregion

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            Sys_Role_Menu model = new Sys_Role_Menu();
            try
            {
                if (id > 0)
                {
                    model = Sys_Role_MenuBLL.GetModel(id.ToInt());
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
        public JsonResult SaveData(string jsonData, int rid)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                List<Sys_Role_Menu> list = jsonData.ToJsonDeserialize<List<Sys_Role_Menu>>();
                Sys_Role_MenuBLL.MergeModel(list, "A.FK_RoleId=B.FK_RoleId and A.FK_MenuButtonId=B.FK_MenuButtonId and A.TypeId=B.TypeId", "A.FK_RoleId=" + rid);
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
                int row = Sys_Role_MenuBLL.DelModel("Id IN(" + id + ")");
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
