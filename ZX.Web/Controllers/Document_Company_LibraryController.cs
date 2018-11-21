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
    public class Document_Company_LibraryController : BaseController
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
            List<Sys_Menu> menuList = Sys_MenuBLL.GetList(d => d.Where(t => t.MUrl.Contains("/Document_Company_Library/Index") && t.MParentId != 0));
            if (menuList.Count > 0)
            {
                sys_Menu = menuList.FirstOrDefault();
            }
            ViewBag.SecondMenuId = sys_Menu.Id;
            GetButtonListByMenuId(sys_Menu.Id.ToInt());
            return View();
        }
        #endregion


        #region 公司文档库操作

        /// <summary>
        /// 项目文档库菜单跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyLibs()
        {
            return View();
        }

        /// <summary>
        /// 分页获取项目文档库
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCompanyLibs()
        {
            AjaxResult result = new AjaxResult();
            DataList<Document_LibraryModel> list = new DataList<Document_LibraryModel>();
            StringBuilder builder = new StringBuilder();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";

                list = Document_LibraryBLL.GetDocument_LibraryList(name,20,pageIndex, pageSize);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        builder.Append("<div class='col-md-3'>");
                        builder.Append("<div class='libs-group-heading libs-project-heading' onclick=\"jumpCompanyLib(" + item.Id + ",0)\" style=\"height:34px;\">");
                        builder.Append("<a href='javascript:;' title='" + item.DName + "' onclick=\"jumpCompanyLib(" + item.Id + ",0)\"><span class='label label-primary'>公司文档库</span> " + item.DName + "</a>");
                        builder.Append("</div>");
                        builder.Append("<div class='libs-group clearfix'>");
                        builder.Append("<a class='lib w-lib-p50' title='公司文档库' href='javascript:;' onclick=\"jumpCompanyLib(" + item.Id + ",0)\">");
                        builder.Append("<i class='icon icon-2x icon-folder-open-alt'></i>");
                        builder.Append("<div class='lib-name' title='" + item.DName + "'>" + item.DName+"</div>");
                        builder.Append("</a>");

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
        public JsonResult GetCompanyLib()
        {
            AjaxResult result = new AjaxResult();
            DataList<Document_LibraryModel> list = new DataList<Document_LibraryModel>();
            StringBuilder builder = new StringBuilder();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";

                list = Document_LibraryBLL.GetDocument_LibraryList(name, 20, pageIndex, pageSize);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        builder.Append("<a class=\"lib \" title=\""+item.DName+ "\" href='javascript:;' data-id='8' onclick=\"jumpCompanyDocList(" + item.Id + ")\">");
                        builder.Append("<i class='icon icon-2x icon-folder-open-alt'></i>");
                        builder.Append("<div class='lib-name' title='@item.DName'>" + item.DName + "</div>");
                        builder.Append("</a>"); 
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
        /// 项目文档库编辑或添加跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyLibAddEdit()
        {
            Document_Library model = new Document_Library();
            try
            {
                //授权用户列表
                ViewBag.AuthUserList = GetUserSelectItem();

                //获取部门列表
                ViewBag.DeptList = Sys_DeptBLL.GetList();

                int libId = Request["id"].ToInt();
                int fk_Id = Request["FK_Id"].ToInt();
                if (libId > 0)
                {
                    model = Document_LibraryBLL.GetModel(libId);
                }
                else
                {
                    model.FK_Id = fk_Id;
                    model.DType = 0;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("编辑公司文档库异常", ex);
            }
            return View(model);
        }

        #endregion


        #region 公司文档操作

        public ActionResult CompanyDocList(int libraryId)
        {

            ////特殊处理,获取跳转到的面页对应菜单Id,供按钮权限控制
            //Sys_Menu sys_Menu = new Sys_Menu();
            //List<Sys_Menu> menuList = Sys_MenuBLL.GetList(d => d.Where(t => t.MUrl.Contains("/Document_Project_Library/Index") && t.MParentId != 0));
            //if (menuList.Count > 0)
            //{
            //    sys_Menu = menuList.FirstOrDefault();
            //}
            //ViewBag.SecondMenuId = sys_Menu.Id;

            Document_Library model = new Document_Library();
            model = Document_LibraryBLL.GetModel(libraryId);
            if (model != null)
            {
                ViewBag.Temporary_TaskModel = Temporary_TaskBLL.GetModel(model.FK_Id);
            }
            return View(model);
        }
        public JsonResult GetCompanyDocList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["key"] ?? "";
                int userId = Request["userId"].ToInt(-1);
                string roleId = Request["roleId"];
                int fk_LibraryId = Request["fk_LibraryId"].ToInt(-1);

                DataList<Document_ManagerModel> list = Document_ManagerBLL.GetDocument_ManagerList(key, userId, roleId, fk_LibraryId, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1;
                List<Document_Type> listType = Document_TypeBLL.GetList();

                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.DTitle + "'>" + item.DTitle + "</td>");
                    switch (item.DType)
                    {
                        case 0:
                            {
                                builder.Append("<td>文档</td>");
                                break;
                            }
                        case 10:
                            {
                                builder.Append("<td>链接</td>");
                                break;
                            }
                    }
                    List<Sys_User> userList = Sys_UserBLL.GetList();
                    Sys_User model = userList.Where(t => t.Id == item.CreateUserId).FirstOrDefault();
                    if (model != null)
                    {
                        builder.Append("<td class='text-left' title='" + model.RealName + "'>" + model.RealName + "</td>");
                    }
                    else
                    {
                        builder.Append("<td></td>");
                    }

                    builder.Append("<td class='visible-lg'>" + item.CreateTime.ToDateFormat("MM-dd HH:mm") + "</td>");

                    if (item.Attach.IsNotNullOrEmpty())
                    {
                        builder.Append("<td class='text-left'>");
                        if (RoleButtonList.Where(l => l.BName.Contains("下载")).Count() > 0)
                        {
                            builder.Append("<a href='" + item.Attach + "' target='_blank'>下载附件</a>");
                        }
                        builder.Append("</td>");
                    }
                    else
                    {
                        builder.Append("<td class='text-left'></td>");
                    }
                    builder.Append("<td class='actions'>");
                    int fkLibId = listType.Where(t => t.Id == item.FK_TypeId).FirstOrDefault().FK_LibraryId.ToInt();



                    //操作权限
                    if (RoleButtonList.Count() > 0)
                    {
                        foreach (var roleBtn in RoleButtonList)
                        {
                            if (roleBtn.BName.Contains("下载"))
                            {
                                continue;
                            }
                            builder.Append("<a href='javascript:;' onclick=\"" + roleBtn.BOptionFun + "\" val=\"" + item.Id + "," + item.FK_TypeId + "," + fk_LibraryId + "\">" + roleBtn.BName + "</a>");
                        }
                    }

                    builder.Append("</td>");
                    builder.Append("</tr>");
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
        /// 临时任务文档添加编辑跳转
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fk_TypeId"></param>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        public ActionResult CompanyDocAddEdit(int? id, int libraryId)
        {
            Document_Manager model = new Document_Manager();
            try
            {
                if (id > 0)
                {
                    model = Document_ManagerBLL.GetModel(id.ToInt());
                }
                else
                {
                    model.FK_LibraryId = libraryId;
                }

                //文档库是否设置为私密
                Document_Library modleLibrary = Document_LibraryBLL.GetModel(libraryId);

                ViewBag.IsPrivate = modleLibrary.IsPrivate;

                //List<Document_Type> dList = Document_TypeBLL.GetList(d => d.Where(t => t.FK_LibraryId == libraryId).OrderBy(t => t.DSort));
                //Document_Type dType = new Document_Type()
                //{
                //    Id = 0,
                //    DName = "顶级",
                //    ParentId = -1,
                //    DSort = -1
                //};
                //dList.Add(dType);
                ////获取文件分类树形,用于部门编辑input
                //var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                //ViewBag.TreeJson = items.ToJsonSerialize();
                //ViewBag.FK_TypeId = fk_TypeId;

                //ViewBag.DTypeName = dList.Where(t => t.Id == fk_TypeId).FirstOrDefault().DName;

                ////授权用户列表
                //ViewBag.AuthUserList = GetUserSelectItem();

                ////授权分组列表
                //ViewBag.RoleListList = Sys_RoleBLL.GetList();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        /// <summary>
        /// 根据文件类型获取文档列表
        /// </summary>
        /// <param name="fk_TypeId"></param>
        /// <returns></returns>
        public JsonResult GetDocumentListByTypeId(int fk_TypeId)
        {
            AjaxResult rest = new AjaxResult
            {
                Code = ResultCode.Failure
            };
            try
            {
                string mid = Request["mid"];
                string libraryId = Request["libraryId"];

                List<Document_Manager> listDoc = Document_ManagerBLL.GetList(d => d.Where(t => t.FK_TypeId == fk_TypeId));
                if (listDoc == null || listDoc.Count <= 0)
                {
                    rest.Data = "";
                    rest.Code = ResultCode.Succeed;
                    return Json(rest);
                }
                StringBuilder builder = new StringBuilder("");
                int i = 1;
                List<Sys_User> userList = Sys_UserBLL.GetList();
                foreach (var item in listDoc)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>");
                    builder.Append("<a href='javascript:;'>");
                    builder.Append(i);
                    builder.Append("</a>");
                    builder.Append("<td class='text-left' title='" + item.DTitle + "'>");
                    builder.Append("<nobr>");
                    builder.Append("<a href='javascript:;'>" + item.DTitle + "</a>");
                    builder.Append(" </nobr>");


                    builder.Append(" </td>");
                    switch (item.DType)
                    {
                        case 0:
                            {
                                builder.Append("<td>文档</td>");
                                break;
                            }
                        case 10:
                            {
                                builder.Append("<td>链接</td>");
                                break;
                            }
                    }
                    Sys_User model = userList.Where(t => t.Id == item.CreateUserId).FirstOrDefault();
                    if (model != null)
                    {
                        builder.Append("<td>" + model.RealName + "</td>");
                    }
                    else
                    {
                        builder.Append("<td></td>");
                    }

                    builder.Append("<td class='visible-lg'>" + item.CreateTime.ToDateFormat("MM-dd hh:mm") + "</td>");
                    builder.Append("<td class='text-left'>");
                    List<Sys_Button> btnList = RoleButtonList;
                    List<Sys_Button> currentBtn = new List<Sys_Button>();

                    if (btnList.Count > 0)
                    {
                        currentBtn = btnList.Where(l => l.BGroup == 28 && l.BIsUse == 0).ToList();
                    }

                    if (item.Attach.IsNotNullOrEmpty())
                    {
                        if (currentBtn.Count > 0)
                        {
                            foreach (var btnItem in currentBtn)
                            {
                                if (btnItem.BName.Contains("下载"))
                                {
                                    builder.Append("<a href='" + item.Attach + "' target='_blank'>下载附件</a>");
                                }
                            }

                        }
                    }
                    builder.Append("</td>");

                    builder.Append("<td class='actions'>");
                    if (currentBtn.Count > 0)
                    {
                        foreach (var btnItem in currentBtn)
                        {
                            if (btnItem.BName.Contains("下载"))
                            {
                                continue;
                            }
                            builder.Append("<a href='javascript:;' onclick='" + btnItem.BOptionFun + "' val=\"" + item.Id + "," + item.FK_TypeId + "," + libraryId + "\">" + btnItem.BName + "</a>&nbsp;&nbsp;");
                        }

                    }

                    builder.Append("</td>");
                    builder.Append("</tr>");
                    i++;
                }
                rest.Data = builder.ToString();
                rest.Code = ResultCode.Succeed;
            }
            catch (Exception ex)
            {
                rest.Message = ex.Message;
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError("查询文档信息时异常", ex);
            }
            return Json(rest);
        }

        #region 根据文档库Id获取文档列表
        public JsonResult GetDocumentListByLibraryId(int libraryId)
        {
            AjaxResult rest = new AjaxResult
            {
                Code = ResultCode.Failure
            };
            try
            {
                string mid = Request["mid"];

                List<Document_Manager> listDoc = Document_ManagerBLL.GetList(d => d.Where(t => t.FK_LibraryId == libraryId));
                if (listDoc == null || listDoc.Count <= 0)
                {
                    rest.Data = "";
                    rest.Code = ResultCode.Succeed;
                    return Json(rest);
                }
                StringBuilder builder = new StringBuilder("");
                int i = 1;
                foreach (var item in listDoc)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>");
                    builder.Append("<a href='javascript:;'>");
                    builder.Append(i);
                    builder.Append("</a>");
                    builder.Append("<td class='text-left' title='" + item.DTitle + "'>");
                    builder.Append("<nobr>");
                    builder.Append("<a href='javascript:;'>" + item.DTitle + "</a>");
                    builder.Append(" </nobr>");


                    builder.Append(" </td>");

                    builder.Append("<td>" + item.Postfix + "</td>");

                    builder.Append("<td class='visible-lg'>" + item.CreateTime.ToDateFormat("MM-dd HH:mm") + "</td>");

                    builder.Append("<td class='actions'>");

                    if (item.Attach.IsNotNullOrEmpty())
                    {
                        builder.Append("<a href='" + item.Attach + "' target='_blank'>下载</a>");
                    }

                    builder.Append("<a href = 'javascript:;' onclick=\"look(this)\" val=\"" + item.Id + "\"> 查看 </a>");

                    builder.Append("<a href='javascript:;' onclick='docDel(" + item.Id + ")'>删除</a>");



                    builder.Append("</td>");
                    builder.Append("</tr>");
                    i++;
                }
                rest.Data = builder.ToString();
                rest.Code = ResultCode.Succeed;
            }
            catch (Exception ex)
            {
                rest.Message = ex.Message;
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError("查询文档信息时异常", ex);
            }
            return Json(rest);
        }
        #endregion

        public JsonResult DocSaveData()
        {
            Document_Manager model = FormHelper.GetRequestForm<Document_Manager>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    row = Document_ManagerBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.Id = Document_ManagerBLL.AddModel(model);
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


        #region 查看文档
        public ActionResult Readonly(int id)
        {
            Document_Manager model = new Document_Manager();
            try
            {
                if (id > 0)
                {
                    model = Document_ManagerBLL.GetModel(id);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("查看公司文档异常", ex);
            }
            return View(model);
        }
        #endregion

        #endregion

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

            //授权用户列表
            ViewBag.AuthUserList = GetUserSelectItem();

            //获取参观者列表
            ViewBag.RoleListList = Sys_RoleBLL.GetList();

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
