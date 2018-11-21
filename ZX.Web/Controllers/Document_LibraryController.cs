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
    public class Document_LibraryController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获取当前首页对应菜单Id,供按钮权限控制
            Sys_Menu sys_Menu = new Sys_Menu();
            try
            {
                List<Sys_Menu> menuList = Sys_MenuBLL.GetList(d => d.Where(t => t.MUrl.Contains("/Document_Library/Index") && t.MParentId != 0));
                if (menuList.Count > 0)
                {
                    sys_Menu = menuList.FirstOrDefault();
                }
                ViewBag.SecondMenuId = sys_Menu.Id;
                GetButtonListByMenuId(sys_Menu.Id.ToInt());

                //获取所有菜单
                List<Sys_Menu> menuAllList = Sys_MenuBLL.GetList();

                int mid = Request["mid"].ToInt();
                //获取菜单所属的一级菜单Id

                int firstMenuId = RecursionGetFirstMenu(menuAllList, ViewBag.CurrentMid);

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

                ViewBag.Role_MenuList = role_menuList;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("首页加载异常",ex);
            }


            return View();
        }
        #endregion


        #region 项目文档库操作

        /// <summary>
        /// 项目文档库菜单跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectLibs()
        {
            return View();
        }

        /// <summary>
        /// 分页获取项目文档库
        /// </summary>
        /// <returns></returns>
        public JsonResult GetProjectLibs()
        {
            AjaxResult result = new AjaxResult();
            DataList<ProjectModel> list = new DataList<ProjectModel>();
            StringBuilder builder = new StringBuilder();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";

                list = ProjectBLL.GetProjectList(name, -1, -1, pageIndex, pageSize);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        builder.Append("<div class='col-md-3'>");
                        builder.Append("<div class='libs-group-heading libs-project-heading' onclick=\"jumpProjectLib(" + item.Id + ",0)\" style=\"height:34px;\">");
                        builder.Append("<a href='javascript:;' title='" + item.PName + "' onclick=\"jumpProjectLib(" + item.Id + ",0)\"><span class='label label-success'>项目文档库</span> " + item.PName + "</a>");
                        builder.Append("</div>");
                        builder.Append("<div class='libs-group clearfix'>");
                        builder.Append("<a class='lib w-lib-p50' title='项目主库' href='javascript:;' onclick=\"jumpProjectLib(" + item.Id + ",0)\">");
                        builder.Append("<i class='icon icon-2x icon-folder-open-alt'></i>");
                        builder.Append("<div class='lib-name' title='项目主库'>项目主库</div>");
                        builder.Append("</a>");

                        //判断该项目下的所有任务是否有附件,如果有,显示附件库
                        List<Project_Task> taskList = Project_TaskBLL.GetList(d => d.Where(t => t.FK_ProjectId == item.Id));
                        bool bl = false;
                        foreach (var taskItem in taskList)
                        {
                            if (taskItem.Attach.IsNotNullOrEmpty())
                            {
                                bl = true;
                                break;
                            }
                        }

                        if (bl)
                        {
                            builder.Append("<a class='lib w-lib-p50' title='附件库' href='javascript:;' onclick=\"jumpProjectLib(" + item.Id + ",0)\">");
                            builder.Append("<i class='icon icon-2x icon-folder-open-alt'></i>");
                            builder.Append("<div class='lib-name' title='附件库'>附件库</div>");
                            builder.Append("</a>");
                        }

                        builder.Append("</div>");
                        builder.Append("</div>");
                    }
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = list.TotalCount;
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }

        /// <summary>
        /// 获取单个项目的文档库
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="libType"></param>
        /// <returns></returns>
        public ActionResult ProjectLib(int projectId, int libType)
        {
            ViewBag.LibType = libType;
            Project pro = new Project();
            pro = ProjectBLL.GetModel(projectId);
            return View(pro);
        }

        /// <summary>
        /// 项目文档库编辑或添加跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectLibAddEdit()
        {
            Document_Library model = new Document_Library();
            try
            {
                //项目列表
                ViewBag.ProjectList = GetProjectSelectItem();

                //授权用户列表
                ViewBag.AuthUserList = GetUserSelectItem();

                //获取参观者列表
                ViewBag.RoleListList = Sys_RoleBLL.GetList();

                int libId = Request["id"].ToInt();
                int fk_Id = Request["FK_Id"].ToInt();
                if (libId > 0)
                {
                    model = Document_LibraryBLL.GetModel(libId);
                }
                else {
                    model.FK_Id = fk_Id;
                    model.DType = 0;
                }                
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("编辑项目文档库异常", ex);
            }
            return View(model);
        }

        #endregion


        #region 临时任务文档库操作
        /// <summary>
        /// 项目文档库菜单跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Temporary_TaskLibs()
        {
            return View();
        }
        /// <summary>
        /// 分页获取项目文档库
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTemporary_TaskLibs()
        {
            AjaxResult result = new AjaxResult();
            DataList<Temporary_TaskModel> list = new DataList<Temporary_TaskModel>();
            StringBuilder builder = new StringBuilder();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";

                list = Temporary_TaskBLL.GetTemporary_TaskList(name, pageIndex, pageSize, UserId, UserId, UserId, 1);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        builder.Append("<div class='col-md-3'>");
                        builder.Append("<div class='libs-group-heading libs-project-heading' onclick=\"jumpTemporaryTaskLib(" + item.Id + ")\" style=\"height:34px;\">");
                        builder.Append("<a href='javascript:;' title='" + item.TName + "' onclick=\"jumpProjectLib(" + item.Id + ",0)\"><span class='label label-info lable-custom'>临时任务文档库</span> " + item.TName + "</a>");
                        builder.Append("</div>");
                        builder.Append("<div class='libs-group clearfix'>");
                        builder.Append("<a class='lib w-lib-p50' title='临时任务文档库主库' href='javascript:;' onclick=\"jumpTemporaryTaskLib(" + item.Id + ")\">");
                        builder.Append("<i class='icon icon-2x icon-folder-open-alt'></i>");
                        builder.Append("<div class='lib-name' title='临时任务文档库主库'>主库</div>");
                        builder.Append("</a>");

                        //判断该项目下的所有任务是否有附件,如果有,显示附件库
                        List<Temporary_Task> taskList = Temporary_TaskBLL.GetList(d => d.Where(t => t.ParentId == item.Id));
                        bool bl = false;
                        foreach (var taskItem in taskList)
                        {
                            if (taskItem.Attach.IsNotNullOrEmpty())
                            {
                                bl = true;
                                break;
                            }
                        }

                        if (bl)
                        {
                            builder.Append("<a class='lib w-lib-p50' title='附件库' href='javascript:;' onclick=\"jumpProjectLib(" + item.Id + ",0)\">");
                            builder.Append("<i class='icon icon-2x icon-folder-open-alt'></i>");
                            builder.Append("<div class='lib-name' title='附件库'>附件库</div>");
                            builder.Append("</a>");
                        }

                        builder.Append("</div>");
                        builder.Append("</div>");
                    }
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = list.TotalCount;
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }

        /// <summary>
        /// 获取单个项目的文档库
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="libType"></param>
        /// <returns></returns>
        public ActionResult Temporary_TaskLib()
        {
            int taskId = Request["taskId"].ToInt();
            Temporary_Task task = new Temporary_Task();
            task = Temporary_TaskBLL.GetModel(taskId);
            return View(task);
        }

        /// <summary>
        /// 项目文档库编辑或添加跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult TemporaryTaskLibAddEdit()
        {
            Document_Library model = new Document_Library();
            try
            {
                //临时列表
                ViewBag.TemporaryTaskList = GetTemporaryTaskList();
                int libId = Request["id"].ToInt();
                if (libId > 0)
                {
                    model = Document_LibraryBLL.GetModel(libId);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("编辑项目文档库异常", ex);
            }
            return View(model);
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public ActionResult DocLibMain(int mid)
        {
            //获取项目列表
            List<Project> listProject = new List<Project>();
            ViewBag.CrrentMid = mid;
            if (UserId == 1)
            {
                listProject = ProjectBLL.GetList();
            }
            else
            {
                listProject = ProjectBLL.GetList(d => d.Where(t => t.CreateUserId == UserId));
            }
            ViewBag.ListProject = listProject;
            return View();
        }

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int id)
        {
            Document_Library model = new Document_Library();

            //项目列表
            ViewBag.ProjectList = GetProjectSelectItem();
            //临时列表
            ViewBag.TemporaryTaskList = GetTemporaryTaskList();

            //授权用户列表
            ViewBag.AuthUserList = GetUserSelectItem();

            //获取参观者列表
            ViewBag.RoleListList = Sys_RoleBLL.GetList();

            //获取部门列表
            ViewBag.DeptList = Sys_DeptBLL.GetList();

            try
            {
                int fk_Id = Request["fk_Id"].ToInt();
                int libType = Request["libType"].ToInt();
                if (id > 0)
                {
                    model = Document_LibraryBLL.GetModel(id.ToInt());
                }
                else
                {
                    model.FK_Id = fk_Id;
                    model.DType = libType.ToInt();
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        /// <summary>
        /// 获取项目下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetProjectSelectItem()
        {
            List<Project> listProject = ProjectBLL.GetList();
            List<SelectListItem> projectSelectItem = new List<SelectListItem>();

            if (listProject != null)
            {
                foreach (var item in listProject)
                {
                    projectSelectItem.Add(new SelectListItem { Text = item.PName, Value = item.Id.ToString() });
                }
            }
            return projectSelectItem;
        }

        /// <summary>
        /// 获取临时任务下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetTemporaryTaskList()
        {
            int userId = UserId;
            List<Temporary_Task> listTemporaryTask = Temporary_TaskBLL.GetList(d => d.Where(t => t.CreateUserId == userId || t.Assigned == userId + "" || t.TSuccess == userId + ""));
            List<SelectListItem> taskSelectItem = new List<SelectListItem>();

            if (listTemporaryTask != null)
            {
                foreach (var item in listTemporaryTask)
                {
                    //过滤子任务
                    if (item.ParentId.ToInt() > 0)
                    {
                        continue;
                    }
                    taskSelectItem.Add(new SelectListItem { Text = item.TName, Value = item.Id.ToString() });
                }
            }
            return taskSelectItem;
        }

        /// <summary>
        /// 获取用户下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetUserSelectItem()
        {
            List<Sys_User> uList = Sys_UserBLL.GetList();
            List<SelectListItem> userSelectItem = new List<SelectListItem>();

            if (uList != null)
            {
                foreach (var item in uList)
                {
                    userSelectItem.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                }
            }
            return userSelectItem;
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
            Document_Library model = FormHelper.GetRequestForm<Document_Library>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    row = Document_LibraryBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.Id = Document_LibraryBLL.AddModel(model);
                }
                rest.Message = "保存成功";
                rest.Code = ResultCode.Succeed;
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
                int row = Document_LibraryBLL.DelModelById(id);
                if (row == 0)
                {
                    rest.Message = "删除失败";
                    rest.Code = ResultCode.Failure;
                }
                else
                {
                    rest.Message = "删除成功";
                    rest.Code = ResultCode.Succeed;
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
