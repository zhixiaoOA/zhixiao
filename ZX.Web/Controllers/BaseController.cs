using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System.Text;

namespace ZX.Web.Controllers
{
    
    public class BaseController : Controller
    {
        #region
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId
        {
            get
            {
                return SessionHelper.Get("UserId").ToInt(0);
            }
        }
        /// <summary>
        /// 用户角色id
        /// </summary>
        public int UserRoleId
        {
            get
            {
                return Session["UserRoleId"].ToInt(-1);
            }
        }
        /// <summary>
        /// 用户部门id
        /// </summary>
        public long DeptId
        {
            get
            {
                return Session["DeptId"].ToLong(0);
            }
        }

        /// <summary>
        /// 用户职位id
        /// </summary>
        public long PositionId
        {
            get
            {
                return Session["PositionId"].ToLong(-1);
            }
        }
        protected string UserName
        {
            get
            {
                return SessionHelper.Get("UserName");
            }
        }
        protected string RealName
        {
            get
            {
                return SessionHelper.Get("RealName");
            }
        }
        #endregion
        protected string UserAccount
        {
            get
            {
                return SessionHelper.Get("UserAccount");
            }
        }

        protected List<Sys_Button> RoleButtonList;

        /// <summary>
        /// 列表组按钮string
        /// </summary>
        protected string CurrentBtnList28;

        /// <summary>
        /// 树形维护组string
        /// </summary>
        protected string CurrentBtnList30;

        /// <summary>
        /// 编辑页组按钮html
        /// </summary>
        protected string CurrentBtnList29;

        protected int SecondMenuId = 0;

        protected int PageSize = 20;


        /// <summary>
        /// 导出报表许可证
        /// </summary>
        public static string License = @"<License><Data><SerialNumber>aed83727-21cc-4a91-bea4-2607bf991c21</SerialNumber><EditionType>Enterprise</EditionType><Products><Product>Aspose.Total</Product></Products></Data><Signature>CxoBmxzcdRLLiQi1kzt5oSbz9GhuyHHOBgjTf5w/wJ1V+lzjBYi8o7PvqRwkdQo4tT4dk3PIJPbH9w5Lszei1SV/smkK8SCjR8kIWgLbOUFBvhD1Fn9KgDAQ8B11psxIWvepKidw8ZmDmbk9kdJbVBOkuAESXDdtDEDZMB/zL7Y=</Signature></License>";


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = (filterContext.RouteData.Values["controller"]).ToString().ToLower();
            string actionName = (filterContext.RouteData.Values["action"]).ToString().ToLower();
            string areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString().ToLower();

            int menuId = Request["mid"].ToInt();
            int rid = Request["rid"].ToInt();

            int secondMenuId = Request["secondMenuId"].ToInt();

            ViewBag.Rid = rid;
            ViewBag.CurrentMid = menuId;

            ViewBag.SecondMenuId = secondMenuId;

            SecondMenuId = secondMenuId;

            GetSysAdminMainNavbarByUserId(menuId);//获取顶部菜单;
            GetSysAdminMiddleMenu(menuId);

            GetButtonListByMenuId(secondMenuId);//获取角色的当前菜单所具备的信息操作权限


            ViewBag.webRoot = Request.Url.Host + ":" + Request.Url.Port;
            ViewBag.router = areaName.IsNotNullOrEmpty() ? "\\/" + areaName + "\\/" + controllerName + "\\/" + actionName : "\\/" + controllerName + "\\/" + actionName;
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (UserId <= 0)
                {
                    filterContext.Result = RedirectToAction("index", "login", new { returnUrl = Request["returnUrl"] });
                }
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 获取当前登录用户所具备传入菜单的二级菜单HTML
        /// </summary>
        /// <param name="pMenuId">任意菜单Id</param>
        /// <returns>拼凑的HTML串</returns>
        public void GetSysAdminMainNavbarByUserId(int pMenuId)
        {
            //标题头HTML
            StringBuilder builderMainNavbarr = new StringBuilder("");

            try
            {
                if (pMenuId <= 0)
                {
                    return;
                }
                int secondMenuId = Request["secondMenuId"].ToInt();
                ViewBag.mid = pMenuId;
                //获取所有菜单
                List<Sys_Menu> menuAllList = Sys_MenuBLL.GetList();

                //获取菜单所属的一级菜单Id
                int firstMenuId = RecursionGetFirstMenu(menuAllList, pMenuId);

                //获取所传入Id对应的菜单
                Sys_Menu menuCurrent = menuAllList.Where(d => d.Id == firstMenuId).FirstOrDefault();

                //子菜单Id串
                StringBuilder builderMenuIds = new StringBuilder();
                builderMenuIds.Append("");
                int i = 0;
                //遍历下级菜单
                foreach (var item in menuAllList.Where(d => d.MParentId == firstMenuId))
                {
                    builderMenuIds.Append(item.Id);
                    if (i < menuAllList.Where(d => d.MParentId == firstMenuId).Count() - 1)
                    {
                        builderMenuIds.Append(",");
                    }
                    i++;
                }
                //获取当前用户所拥有二级菜单
                List<Sys_Role_Menu> role_menuList = Sys_Role_MenuBLL.GetList("FK_RoleId=" + UserRoleId + " and TypeId=1 and FK_MenuButtonId in (" + builderMenuIds.ToString() + ")");
                builderMainNavbarr.Append("<nav class='navbar navbar-fixed-top top-navbar-index' role='navigation' id='mainNavbar'>");
                builderMainNavbarr.Append("<div class='navbar-header'>");
                menuCurrent.MUrl = (menuCurrent.MUrl == null || menuCurrent.MUrl == "") ? "javascript:;" : menuCurrent.MUrl;
                builderMainNavbarr.Append("<a href = '" + menuCurrent.MUrl + "?mid=" + firstMenuId + "&secondMenuId=" + menuCurrent.Id + "' class='navbar-brand'>" + menuCurrent.MName + "</a>");
                builderMainNavbarr.Append("</div>");
                builderMainNavbarr.Append("<ul class='nav navbar-nav'>");

                List<Sys_Menu> menuList = new List<Sys_Menu>();
                foreach (var item in role_menuList)
                {
                    Sys_Menu model = menuAllList.Where(d => d.Id == item.FK_MenuButtonId).FirstOrDefault();
                    menuList.Add(model);
                }
                //排序后输出
                foreach (var item in menuList.OrderBy(t => t.MSort))
                {
                    item.MUrl = (item.MUrl == null || item.MUrl == "") ? "javascript:;" : item.MUrl;
                    builderMainNavbarr.Append("<li class='" + (item.Id == secondMenuId ? "active" : "") + "'><a href = '" + item.MUrl + "?mid=" + firstMenuId + "&secondMenuId=" + item.Id + "'> " + item.MName + " </a></ li >");
                }
                builderMainNavbarr.Append("</ul>");
                builderMainNavbarr.Append("</nav>");
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("获取当前登录用户所具备传入菜单的下一级菜单HTML异常,异常信息:" + ex.Message, ex);
                builderMainNavbarr.ToString();
            }

            ViewBag.SysAdminMainNavbar = builderMainNavbarr.ToString();
        }

        /// <summary>
        /// 获取当前登录用户所具备传入菜单的二级菜单HTML
        /// </summary>
        /// <param name="pMenuId">任意菜单Id</param>
        /// <returns>拼凑的HTML串</returns>
        public void GetSysAdminMiddleMenu(int pMenuId)
        {
            //标题头HTML
            StringBuilder builderMiddleNavbarr = new StringBuilder("");

            try
            {
                //获取下一级菜单
                List<Sys_Menu> childMenuList = Sys_MenuBLL.GetList(" MParentId =" + pMenuId);

                //子菜单Id串
                StringBuilder builderMenuIds = new StringBuilder();
                builderMenuIds.Append("");
                int i = 0;
                //遍历下级菜单
                foreach (var item in childMenuList)
                {
                    builderMenuIds.Append(item.Id);
                    if (i < childMenuList.Count() - 1)
                    {
                        builderMenuIds.Append(",");
                    }
                    i++;
                }
                string strWhere = "FK_RoleId=" + UserRoleId.ToInt() + " and TypeId=1 ";
                if (builderMenuIds.ToString().IsNotNullOrEmpty())
                {
                    strWhere += " and FK_MenuButtonId in (" + builderMenuIds.ToString() + ")";
                }
                //获取当前用户所拥有子级菜单
                List<Sys_Role_Menu> role_menuList = Sys_Role_MenuBLL.GetList(strWhere);
                //样式
                string[] styleClassName = { "shortcut organization", "shortcut employee", "shortcut role-system", "shortcut role-permission", "shortcut menu-admin" };


                List<Sys_Menu> menuList = new List<Sys_Menu>();
                foreach (var item in role_menuList)
                {
                    Sys_Menu model = childMenuList.Where(d => d.Id == item.FK_MenuButtonId).FirstOrDefault();
                    menuList.Add(model);
                }
                int j = 0;
                foreach (var item in menuList.OrderBy(t => t.MSort))
                {
                    item.MUrl = (item.MUrl == null || item.MUrl == "") ? "javascript:;" : item.MUrl;
                    builderMiddleNavbarr.Append(" <div class='col-md-4 col-sm-6' style='padding-top:15px;'>");
                    builderMiddleNavbarr.Append(" <div class='" + styleClassName[j] + "'>");
                    if (j == styleClassName.Length - 1) j = 0;
                    builderMiddleNavbarr.Append(" <a href = '" + item.MUrl + "?mid=" + pMenuId + "&secondMenuId=" + item.Id + "' ><h3> " + item.MName + " </h3></a>");
                    builderMiddleNavbarr.Append(" </div>");
                    builderMiddleNavbarr.Append(" </div>");
                    if (j == styleClassName.Length - 1)
                    {
                        j = -1;
                    }
                    j++;
                }

            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("获取当前登录用户所具备传入菜单的下一级菜单HTML异常,异常信息:" + ex.Message, ex);
            }

            ViewBag.SysAdminMiddleMenu = builderMiddleNavbarr.ToString();
        }

        /// <summary>
        /// 获取一级菜单
        /// </summary>
        /// <param name="menuList">菜单集合</param>
        /// <param name="mid">任意一菜单id</param>
        /// <returns>一级菜单</returns>
        public int RecursionGetFirstMenu(List<Sys_Menu> menuList, int mid)
        {
            int mParentId = menuList.Where(d => d.Id == mid).FirstOrDefault().MParentId.ToInt();
            if (mParentId == 0)
            {
                return mid;
            }
            else
            {
                return RecursionGetFirstMenu(menuList, mParentId);
            }
        }

        /// <summary>
        /// 获取单一菜单角色所具备按钮
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>所具备按钮集合</returns>
        public void GetButtonListByMenuId(int menuId)
        {
            List<Sys_Button> buttonList = new List<Sys_Button>();
            try
            {
                //获取当前用户所拥有按钮集合
                List<Sys_Role_Menu> role_ButtonList = Sys_Role_MenuBLL.GetList("FK_RoleId=" + UserRoleId + " and TypeId=2 ");

                //获取当前菜单所关联的按钮集合
                List<Sys_Button> butonListOfMenuId = Sys_ButtonBLL.GetList(d => d.Where(t => t.FK_MenuId == menuId));

                //遍历筛选角色按钮
                foreach (var item in butonListOfMenuId)
                {
                    int counts = 0;
                    try
                    {
                        counts = role_ButtonList.Where(l => l.FK_MenuButtonId == item.Id).ToList().Count();
                        if (counts > 0)
                        {
                            buttonList.Add(item);
                        }
                    }
                    catch { }
                }

                //RoleButtonList = buttonList;
                StringBuilder builder = new StringBuilder("");

                //按组筛选树形组按钮
                //if (buttonList.Count > 0)
                //{
                RoleButtonList = buttonList;
                ViewBag.RoleButtonList = buttonList;
                //表格右上角小钮组
                List<Sys_Button> currentBtnList27 = new List<Sys_Button>();
                currentBtnList27 = buttonList.Where(l => l.BGroup == 27 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                StringBuilder builder27 = new StringBuilder("");
                foreach (var btnItem in currentBtnList27)
                {
                    builder27.Append("<a href='javascript:;' onclick=\"" + btnItem.BOptionFun + "\" class='btn btn-primary btn-xs'>" + btnItem.BName + "</a>");
                }
                ViewBag.CurrentBtnList27 = builder27.ToString();


                //表格右上角大按钮组
                List<Sys_Button> currentBtnList32 = new List<Sys_Button>();
                currentBtnList32 = buttonList.Where(l => l.BGroup == 32 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                StringBuilder builder32 = new StringBuilder("");
                foreach (var btnItem in currentBtnList32)
                {
                    builder32.Append("<a href='javascript:;' onclick=\"" + btnItem.BOptionFun + "\" class='btn btn-primary' val=\"{0}\">" + btnItem.BName + "</a>");
                }
                ViewBag.CurrentBtnList32 = builder32.ToString();
                //表行按钮组
                List<Sys_Button> currentBtnList28 = new List<Sys_Button>();
                currentBtnList28 = buttonList.Where(l => l.BGroup == 28 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                StringBuilder builder28 = new StringBuilder("");
                foreach (var btnItem in currentBtnList28)
                {
                    builder28.Append("<a href='javascript:;' onclick='" + btnItem.BOptionFun + "' val=\"{0}\">" + btnItem.BName + "</a>&nbsp;&nbsp;");
                }
                CurrentBtnList28 = builder28.ToString();


                //编辑页按钮组
                List<Sys_Button> currentBtnList29 = new List<Sys_Button>();
                currentBtnList29 = buttonList.Where(l => l.BGroup == 29 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                StringBuilder builder29 = new StringBuilder("");
                foreach (var btnItem in currentBtnList29)
                {
                    string className = "btn btn-primary";
                    if (btnItem.BName.Equals("返回") || btnItem.BName.Contains("返回"))
                    {
                        className = "btn btn-back";
                    }
                    builder29.Append("<a href='javascript:;' class='" + className + "' onclick='" + btnItem.BOptionFun + "'>" + btnItem.BName + "</a>");
                }
                CurrentBtnList29 = builder29.ToString();
                ViewBag.CurrentBtnList29 = builder29.ToString();

                //树形维护组
                List<Sys_Button> currentBtnList30 = new List<Sys_Button>();
                currentBtnList30 = buttonList.Where(l => l.BGroup == 30 && l.BIsUse == 0).OrderByDescending(l => l.BSort).ToList();
                StringBuilder builder30 = new StringBuilder("");
                foreach (var btnItem in currentBtnList30)
                {
                    builder30.Append("<a href='javascript:;' onclick='" + btnItem.BOptionFun + "' val=\"{0}\" class='a_btn_menu'>" + btnItem.BName + "</a>");
                }
                CurrentBtnList30 = builder30.ToString();

                //树形左下角组
                List<Sys_Button> currentBtnList31 = new List<Sys_Button>();
                currentBtnList31 = buttonList.Where(l => l.BGroup == 31 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                StringBuilder builder31 = new StringBuilder("");
                foreach (var btnItem in currentBtnList31)
                {
                    builder31.Append("<a href='javascript:;' class='btn btn-primary btn-xs' onclick=\"" + btnItem.BOptionFun + "\" val=\"{0}\">" + btnItem.BName + "</a>");
                }
                ViewBag.CurrentBtnList31 = builder31.ToString();

                //文档库图形添加按钮组
                List<Sys_Button> currentBtnList33 = new List<Sys_Button>();
                currentBtnList33 = buttonList.Where(l => l.BGroup == 33 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                StringBuilder builder33 = new StringBuilder("");
                foreach (var btnItem in currentBtnList33)
                {
                    builder33.Append("<a href='javascript:;' class='lib addbtn' title='" + btnItem.BName + "' onclick=\"" + btnItem.BOptionFun + "\" val=\"{0}\"><i class='icon icon-plus'></i></a>");
                }
                ViewBag.CurrentBtnList33 = builder33.ToString();

                //文档库右上角菜单栏内按钮组
                List<Sys_Button> currentBtnList34 = new List<Sys_Button>();
                currentBtnList34 = buttonList.Where(l => l.BGroup == 34 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                StringBuilder builder34 = new StringBuilder("");
                foreach (var btnItem in currentBtnList34)
                {
                    builder34.Append("<a href=\"javascript:;\" class=\"btn btn-primary\" onclick=\"" + btnItem.BOptionFun + "\">" + btnItem.BName + "</a>");
                }
                ViewBag.CurrentBtnList34 = builder34.ToString();
                //}
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("获取按钮异常", ex);
            }
        }
    }
}