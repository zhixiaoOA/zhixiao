using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZX.Model;
using ZX.Tools;

namespace ZX.Web.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.Title = ZXConfig.SYSNAME;
            ViewBag.RealName = RealName;
            ViewBag.CurrentTime = DateTime.Now;
            ViewBag.LeftMenuList = GetUserLeftMenu(UserId);
            
            return View();
        }
        /// <summary>
        /// 获取左侧所有菜单栏数据
        /// </summary>
        /// <param name="userId">用户ID（用来做权限判断）</param>
        /// <returns></returns>
        private string GetUserLeftMenu(int userId)
        {
            List<Sys_Menu> userAllMenuList = BLL.Sys_MenuBLL.GetUserMenu(userId).Where(d => d.MParentId == 0).ToList();
            List<LeftMenu> leftMenuList = new List<LeftMenu>();
            foreach (var item in userAllMenuList)
            {
                var IsAllApplicationId = item.Id;
                var secondMenuId = "";
                if ("企业架构".Equals(item.MName))
                {
                    List<Sys_Menu> deptMenus = BLL.Sys_MenuBLL.GetUserMenu(userId).Where(d => (d.MParentId == item.Id && "公司架构".Equals(d.MName))).ToList();
                    secondMenuId = deptMenus[0].Id.ToString();
                }else if ("系统设置".Equals(item.MName))
                {
                    List<Sys_Menu> systemMenus = BLL.Sys_MenuBLL.GetUserMenu(userId).Where(d => (d.MParentId == item.Id && "菜单管理".Equals(d.MName))).ToList();
                    secondMenuId = systemMenus[0].Id.ToString();

                }
                else if ("首页".Equals(item.MName))
                {
                    IsAllApplicationId = item.Id;
                    ViewBag.HomeManu = item;
                }
                LeftMenu leftMenu = new LeftMenu()
                {
                    id = item.Id.ToString(),
                    code = item.BlockSign,
                    name = item.MName,
                    url = item.MUrl + "?mid=" + IsAllApplicationId + "&secondMenuId=" + secondMenuId,
                    open = "iframe",
                    desc = item.Mesc,
                    size = "max",
                    icon = item.MIcon,
                    control = "simple",
                    position = "default",
                    menu = "all",
                    display = "fixed",
                    abbr = item.MName,
                    order = item.MSort.ToString(),
                    sys = "1",
                    category = "0"
                };
                if (item.MName.Equals("首页"))
                {
                    leftMenuList.Insert(0, leftMenu);
                }
                else
                {
                    leftMenuList.Add(leftMenu);
                }
                
            }
            return leftMenuList.ToJsonSerialize();
        }
    }
}