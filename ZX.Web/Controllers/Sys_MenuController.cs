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
    //[PermissionFilter]
    public class Sys_MenuController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Sys_MenuList");
        }
        #endregion

        public JsonResult GetList()
        {
            int mid = Request["mid"].ToInt();
            AjaxResult result = new AjaxResult();
            try
            {
                StringBuilder builder = new StringBuilder();
                List<Sys_Menu> listMenu = Sys_MenuBLL.GetList(t => t.OrderBy(a => a.MSort));
                foreach (var item in listMenu.Where(t => t.MParentId == 0))
                {
                    builder.Append("<li><a href='javascript:;' onclick='showSysMenuBtn(" + item.Id + ")'>" + item.MName + "</a>");
                    //按钮权限可见性控制
                    string btnHtml = string.Format(CurrentBtnList30, item.Id);
                    builder.Append(btnHtml);
                    builder.Append("<input value='" + item.MSort + "' onblur='onSort(" + item.Id + ",this)' style='width:50px' placeholder='排序' />");
                    if (listMenu.Where(t => t.MParentId == item.Id).Count() > 0)
                    {
                        builder.Append("<ul>");
                        Recursion(listMenu, item.Id, mid, ref builder);
                        builder.Append("</ul>");
                    }
                    builder.Append("</li>");
                }
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
        public void Recursion(List<Sys_Menu> nLists, long parentID, int mid, ref StringBuilder builder)
        {
            foreach (var item in nLists.Where(t => t.MParentId == parentID))
            {
                builder.Append("<li><a href='javascript:;' onclick='showSysMenuBtn(" + item.Id + ")'>" + item.MName + "</a>");
                //按钮权限可见性控制
                string btnHtml = string.Format(CurrentBtnList30, item.Id);
                builder.Append(btnHtml);
                builder.Append("<input value='" + item.MSort + "' onblur='onSort(" + item.Id + ",this)' style='width:50px' placeholder='排序' />");
                if (nLists.Where(t => t.MParentId == item.Id).Count() > 0)
                {
                    builder.Append("<ul>");
                    Recursion(nLists, item.Id, mid, ref builder);
                    builder.Append("</ul>");
                }
                builder.Append("</li>");
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
            int mPId = Request["pid"].ToInt();
            Sys_Menu model = new Sys_Menu()
            {
                Id = id.ToInt(),
                MParentId = mPId
            };
            try
            {
                List<Sys_Menu> list = Sys_MenuBLL.GetList(t => t.OrderBy(a => a.MSort));
                Sys_Menu menu = new Sys_Menu()
                {
                    Id = 0,
                    MName = "顶级",
                    MParentId = -1,
                    MSort = -1
                };
                list.Add(menu);
                var items = list.Select(t => new { id = t.Id.ToInt(), name = t.MName, pId = t.MParentId.ToInt(), open = t.MParentId == -1 });
                ViewBag.TreeJson = items.ToJsonSerialize();
                if (id > 0)
                {
                    model = Sys_MenuBLL.GetModel(id.ToInt());
                }
                ViewBag.MenuName = list.FirstOrDefault(t => t.Id == model.MParentId).MName;
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
            Sys_Menu model = FormHelper.GetRequestForm<Sys_Menu>();
            AjaxResult rest = new AjaxResult();
            try
            {
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    Sys_MenuBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.UpdateTime = model.CreateTime;
                    model.UpdateUserId = UserId;
                    model.Id = Sys_MenuBLL.AddModel(model);
                    if (model.Id > 0)
                    {
                        Sys_Role_Menu sys_Role_Menu = new Sys_Role_Menu()
                        {
                            FK_MenuButtonId = model.Id.ToInt(),
                            FK_RoleId = 1,
                            TypeId = 1
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
        public JsonResult DeleteData(int id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                bool bl = Sys_MenuBLL.CheckModel(t => t.Where(a => a.MParentId == id));
                if (bl)
                {
                    rest.Message = "请先删除子菜单";
                    rest.Code = ResultCode.Failure;
                }
                else
                {
                    int row = Sys_MenuBLL.DelModel(id);
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


        #region 获取菜单按钮
        /// <summary>
        /// 获取菜单按钮
        /// </summary>
        /// <param name="mid">菜单id</param>
        /// <returns></returns>
        public JsonResult GetButtonList(int mid)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                List<Sys_Button> list = Sys_ButtonBLL.GetList(t => t.Where(a => a.FK_MenuId == mid));
                List<Dictionary> ditList = new List<Dictionary>();

                if (list.Count > 0)
                {
                    ditList = DictionaryBLL.GetList(d => d.Where(t => t.ParentId == 26));
                }

                //按组筛选树形组按钮  字典中 行列组对应Id-28

                foreach (var item in list)
                {
                    rest.Data += "<tr class='text-center'>";
                    rest.Data += "<td>" + item.Id + "</td>";
                    rest.Data += "<td>" + item.BName + "</td>";
                    rest.Data += "<td>" + Common.EnumGetDesc.GetEnumDescription((ButtonType)Enum.Parse(typeof(ProjectStatus), item.BType.ToString())) + "</td>";
                    string groupName = "";
                    Dictionary dit = new Dictionary();
                    dit = ditList.Where(l => l.Id == item.BGroup).FirstOrDefault();
                    if (dit != null)
                    {
                        groupName = dit.Name;
                    }
                    rest.Data += "<td>" + groupName + "</td>";
                    rest.Data += "<td>" + item.BOptionFun + "</td>";

                    string isUserName = "";
                    switch (item.BIsUse)
                    {
                        case 0:
                            {
                                isUserName = "启用";
                                break;
                            }
                        case 1:
                            {
                                isUserName = "禁用";
                                break;
                            }
                        default:
                            isUserName = "未知";
                            break;
                    }
                    rest.Data += "<td>" + isUserName + "</td>";
                    rest.Data += "<td>" + item.BDesc + "</td>";
                    rest.Data += "<td>" + item.BSort + "</td>";
                    rest.Data += "<td class='operate'>";

                    //功能按钮
                    string btnHtml = string.Format(CurrentBtnList28, item.Id);
                    rest.Data += btnHtml;

                    rest.Data += "</td> ";
                    rest.Data += "</tr>";
                }
            }
            catch (Exception ex)
            {
                rest.Code = ResultCode.Failure;
                rest.Message = ex.Message;
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
                Sys_MenuBLL.UpdateModel(new Sys_Menu() { Id = id, MSort = sort.ToInt() });
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