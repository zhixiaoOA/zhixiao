using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;
using System.IO;

namespace ZX.Web.Controllers
{
    public class Document_ManagerController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int libraryId)
        {
            Document_Library model = new Document_Library();
            model = Document_LibraryBLL.GetModel(libraryId);
            if (model != null)
            {
                ViewBag.Project = ProjectBLL.GetModel(model.FK_Id);
                return View("Document_ManagerList", model);
            }
            return View("Document_ManagerList");
        }

        public JsonResult GetTree(int libraryId)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                List<Document_Type> document_TypeTree = new List<Document_Type>();
                StringBuilder builder = new StringBuilder();
                List<Document_Type> listDocument = Document_TypeBLL.GetList(d => d.Where(t => t.FK_LibraryId == libraryId));
                foreach (var item in listDocument.Where(t => t.ParentId == 0))
                {
                    //?m=user&f=admin&deptID=820056

                    builder.Append("<li><a href='javascript:;' onclick='GetDocumentListByTypeId(" + item.Id + ")'>" + item.DName + "</a>"); ;

                    ////按钮权限可见性控制
                    //string btnHtml = string.Format(CurrentBtnList30, item.Id);
                    //builder.Append(btnHtml);

                    if (listDocument.Where(t => t.ParentId == item.Id).Count() > 0)
                    {
                        builder.Append("<ul>");
                        Recursion(listDocument, item.Id, ref builder);
                        builder.Append("</ul>");
                    }
                    builder.Append("</li>");
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("查询文档异常", ex);
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
        public void Recursion(List<Document_Type> nLists, long parentID, ref StringBuilder builder)
        {
            foreach (var item in nLists.Where(t => t.ParentId == parentID))
            {
                builder.Append("<li><a href='javascript:;' onclick='GetDocumentListByTypeId(" + item.Id + ")'>" + item.DName + "</a>");
                ////按钮权限可见性控制
                //string btnHtml = string.Format(CurrentBtnList30, item.Id);
                //builder.Append(btnHtml);

                if (nLists.Where(t => t.ParentId == item.Id).Count() > 0)
                {
                    builder.Append("<ul>");
                    Recursion(nLists, item.Id, ref builder);
                    builder.Append("</ul>");
                }
                builder.Append("</li>");
            }
        }
        #endregion
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetDocument_ManagerList()
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

                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.DTitle + "'>" + item.DTitle + "</td>");
                    builder.Append("<td class='text-left' title='" + item.Postfix + "'>" + item.Postfix + "</td>");
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

                    builder.Append("<td class='actions'>");


                    //操作权限
                    if (RoleButtonList.Count() > 0)
                    {
                        foreach (var roleBtn in RoleButtonList.Where(l => l.BGroup == 28 && l.BIsUse == 0))
                        {
                            if (roleBtn.BName.Contains("下载"))
                            {
                                if (item.Attach.IsNullOrEmpty())
                                {
                                    continue;
                                }
                                else
                                {
                                    builder.Append("&nbsp;<a href='" + item.Attach + "' target='_blank'>下载</a>");
                                    continue;
                                }
                            }
                            builder.Append("<a href='javascript:;' onclick=\"" + roleBtn.BOptionFun + "\" val=\"" + item.Id + "," + item.FK_TypeId + "," + fk_LibraryId + "\">&nbsp;&nbsp;" + roleBtn.BName + "</a>");
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
        #endregion

        #region 项目文档
        public ActionResult ProjectDocList(int libraryId)
        {

            //特殊处理,获取跳转到的面页对应菜单Id,供按钮权限控制
            Sys_Menu sys_Menu = new Sys_Menu();
            List<Sys_Menu> menuList = Sys_MenuBLL.GetList(d => d.Where(t => t.MUrl.Contains("/Document_Project_Library/Index") && t.MParentId != 0));
            if (menuList.Count > 0)
            {
                sys_Menu = menuList.FirstOrDefault();
            }
            ViewBag.SecondMenuId = sys_Menu.Id;

            Document_Library model = new Document_Library();
            model = Document_LibraryBLL.GetModel(libraryId);
            if (model != null)
            {
                ViewBag.Project = ProjectBLL.GetModel(model.FK_Id);
            }
            GetButtonListByMenuId(sys_Menu.Id.ToInt());
            ViewBag.RoleButtonList = RoleButtonList;
            return View(model);
        }
        public JsonResult GetProjectDocList()
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
        #endregion


        #region 根据文件类型获取文档列表
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

                    builder.Append("<td class='visible-lg'>" + item.CreateTime.ToDateFormat("MM-dd HH:mm") + "</td>");

                    if (item.Attach.IsNotNullOrEmpty())
                    {
                        builder.Append("<td class='text-left'><a href='" + item.Attach + "' target='_blank'>下载附件</a></td>");
                    }
                    else
                    {
                        builder.Append("<td class='text-left'></td>");
                    }

                    builder.Append("<td class='actions'>");
                    if (RoleButtonList.Where(l => l.BType == 1 || l.BType == 2).Count() > 0)
                    {
                        builder.Append("<a href = 'AddEdit?id=" + item.Id + "&mid=" + mid + "&fk_TypeId=" + item.FK_TypeId + "&libraryId=" + libraryId + "&secondMenuId=" + SecondMenuId + "'> 编辑 </a>");
                    }
                    if (RoleButtonList.Where(l => l.BType == 1 || l.BType == 2).Count() > 0)
                    {
                        builder.Append("<a href='javascript:;' onclick='docDel(" + item.Id + "," + item.FK_TypeId + ")'>删除</a>");
                    }

                    if (RoleButtonList.Where(l => l.BType == 4).Count() > 0)
                    {
                        builder.Append("<a href = 'AddEdit?id=" + item.Id + "&mid=" + mid + "&fk_TypeId=" + item.FK_TypeId + "&libraryId=" + libraryId + "&secondMenuId=" + SecondMenuId + "'> 查看 </a>");
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
        #endregion

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

                    builder.Append("<td>" + item.Postfix + "</td>");

                    Sys_User model = userList.Where(t => t.Id == item.CreateUserId).FirstOrDefault();
                    if (model != null)
                    {
                        builder.Append("<td>" + model.RealName + "</td>");
                    }
                    else
                    {
                        builder.Append("<td></td>");
                    }

                    builder.Append("<td class='visible-lg'>" + item.CreateTime.ToDateFormat("MM-dd HH:mm") + "</td>");

                    builder.Append("<td class='actions'>");

                    if (item.Attach.IsNotNullOrEmpty())
                    {
                        builder.Append("<a href='" + item.Attach + "' target='_blank'>下载</a>");
                    }

                    builder.Append("<a href = 'AddEdit?id=" + item.Id + "&mid=" + mid + "&fk_TypeId=" + item.FK_TypeId + "&libraryId=" + libraryId + "&secondMenuId=" + SecondMenuId + "'> 查看 </a>");

                    builder.Append("<a href='javascript:;' onclick='docDel(" + item.Id + "," + item.FK_TypeId + ")'>删除</a>");



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

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id, int? fk_TypeId, int libraryId)
        {
            Document_Manager model = new Document_Manager();
            try
            {
                if (id > 0)
                {
                    model = Document_ManagerBLL.GetModel(id.ToInt());
                    fk_TypeId = model.FK_TypeId.ToInt();
                    Document_Type docTypeModel = Document_TypeBLL.GetModel(fk_TypeId);
                    Document_Library libModel = Document_LibraryBLL.GetModel(docTypeModel.FK_LibraryId);
                    libraryId = libModel.Id.ToInt();
                }
                else
                {
                    model.FK_TypeId = fk_TypeId;
                }
                List<Document_Type> dList = Document_TypeBLL.GetList(d => d.Where(t => t.FK_LibraryId == libraryId).OrderBy(t => t.DSort));
                Document_Type dType = new Document_Type()
                {
                    Id = 0,
                    DName = "顶级",
                    ParentId = -1,
                    DSort = -1
                };
                dList.Add(dType);
                //获取文件分类树形,用于部门编辑input
                var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                ViewBag.TreeJson = items.ToJsonSerialize();
                ViewBag.FK_TypeId = fk_TypeId;

                ViewBag.DTypeName = dList.Where(t => t.Id == fk_TypeId).FirstOrDefault().DName;

                //文档库是否设置为私密
                Document_Library modleLibrary = Document_LibraryBLL.GetModel(libraryId);

                ViewBag.IsPrivate = modleLibrary.IsPrivate;

                //授权用户列表
                ViewBag.AuthUserList = GetUserSelectItem();

                //授权分组列表
                ViewBag.RoleListList = Sys_RoleBLL.GetList();

            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }


        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Readonly()
        {
            Document_Manager model = new Document_Manager();
            try
            {

                int fk_TypeId = Request["fk_TypeId"].ToInt();
                int libraryId = Request["libraryId"].ToInt();
                int id = Request["id"].ToInt();

                if (id > 0)
                {
                    model = Document_ManagerBLL.GetModel(id);
                    fk_TypeId = model.FK_TypeId.ToInt();
                    Document_Type docTypeModel = Document_TypeBLL.GetModel(fk_TypeId);
                    Document_Library libModel = Document_LibraryBLL.GetModel(docTypeModel.FK_LibraryId);
                    libraryId = libModel.Id.ToInt();

                    List<Document_Type> dList = Document_TypeBLL.GetList(d => d.Where(t => t.FK_LibraryId == libraryId).OrderBy(t => t.DSort));
                    Document_Type dType = new Document_Type()
                    {
                        Id = 0,
                        DName = "顶级",
                        ParentId = -1,
                        DSort = -1
                    };
                    dList.Add(dType);
                    //获取文件分类树形,用于部门编辑input
                    var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                    ViewBag.TreeJson = items.ToJsonSerialize();


                    ViewBag.FK_TypeId = fk_TypeId;
                    ViewBag.DTypeName = dList.Where(t => t.Id == fk_TypeId).FirstOrDefault().DName;

                    //文档库是否设置为私密
                    Document_Library modleLibrary = Document_LibraryBLL.GetModel(libraryId);

                    ViewBag.IsPrivate = modleLibrary.IsPrivate;

                    //授权用户列表
                    ViewBag.AuthUserList = GetUserSelectItem();

                    //授权分组列表
                    ViewBag.RoleListList = Sys_RoleBLL.GetList();
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
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
        #endregion


        #region 直接上传文件形式保存
        public JsonResult SaveUploadData(string docItem)
        {
            Document_Manager model = docItem.ToJsonDeserialize<Document_Manager>();

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
            Document_Manager model = new Document_Manager();
            rest.Message = "删除失败";
            rest.Code = ResultCode.Failure;
            try
            {
                if (id.ToInt() > 0)
                {
                    model = Document_ManagerBLL.GetModel(id.ToInt());

                    int row = Document_ManagerBLL.DelModelById(model.Id.ToString());
                    if (row == 0)
                    {
                        rest.Message = "删除失败";
                        rest.Code = ResultCode.Failure;
                    }
                    else
                    {
                        //删除对应文件
                        //string filePath = File.Exists(@"");

                        rest.Message = "删除成功";
                        rest.Code = ResultCode.Succeed;
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

        #region 个人文件
        public ActionResult MyDoc()
        {
            ViewBag.UserId = UserId;
            ViewBag.RoleId = UserRoleId;
            return View();
        }
        #endregion
        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMyDocList()
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

                    builder.Append("<td class='visible-lg'>" + item.CreateTime.ToDateFormat("MM-dd hh:mm") + "</td>");

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
                    //int fkLibId = listType.Where(t => t.Id == item.FK_TypeId).FirstOrDefault().FK_LibraryId.ToInt();



                    //操作权限
                    if (RoleButtonList.Count() > 0)
                    {
                        foreach (var roleBtn in RoleButtonList.Where(l => l.BGroup == 28 && l.BIsUse == 0))
                        {
                            if (roleBtn.BName.Contains("下载"))
                            {
                                continue;
                            }
                            builder.Append("<a href='javascript:;' onclick=\"" + roleBtn.BOptionFun + "\" val=\"" + item.Id + "," + item.FK_TypeId + "," + fk_LibraryId + "\">" + roleBtn.BName + "</a>&nbsp;&nbsp;");
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
        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult MyDocReadonly()
        {
            Document_Manager model = new Document_Manager();
            try
            {

                int id = Request["id"].ToInt();
                if (id > 0)
                {
                    model = Document_ManagerBLL.GetModel(id);

                    Document_Type cType = new Document_Type();
                    cType = Document_TypeBLL.GetModel(model.FK_TypeId);

                    List<Document_Type> dList = Document_TypeBLL.GetList(d => d.Where(t => t.FK_LibraryId == cType.FK_LibraryId).OrderBy(t => t.DSort));
                    Document_Type dType = new Document_Type()
                    {
                        Id = 0,
                        DName = "顶级",
                        ParentId = -1,
                        DSort = -1
                    };
                    dList.Add(dType);
                    //获取文件分类树形,用于部门编辑input
                    var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                    ViewBag.TreeJson = items.ToJsonSerialize();


                    ViewBag.FK_TypeId = cType.Id;
                    ViewBag.DTypeName = cType.DName;
                    //文档库是否设置为私密
                    Document_Library modleLibrary = Document_LibraryBLL.GetModel(cType.FK_LibraryId);

                    ViewBag.IsPrivate = modleLibrary.IsPrivate;

                    //授权用户列表
                    ViewBag.AuthUserList = GetUserSelectItem();

                    //授权分组列表
                    ViewBag.RoleListList = Sys_RoleBLL.GetList();
                }

            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        public ActionResult MyDocEdit()
        {
            Document_Manager model = new Document_Manager();
            try
            {
                int id = Request["id"].ToInt();
                if (id > 0)
                {
                    model = Document_ManagerBLL.GetModel(id);
                    Document_Type cType = new Document_Type();
                    cType = Document_TypeBLL.GetModel(model.FK_TypeId);

                    List<Document_Type> dList = Document_TypeBLL.GetList(d => d.Where(t => t.FK_LibraryId == cType.FK_LibraryId).OrderBy(t => t.DSort));
                    Document_Type dType = new Document_Type()
                    {
                        Id = 0,
                        DName = "顶级",
                        ParentId = -1,
                        DSort = -1
                    };
                    dList.Add(dType);
                    //获取文件分类树形,用于部门编辑input
                    var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                    ViewBag.TreeJson = items.ToJsonSerialize();


                    ViewBag.FK_TypeId = cType.Id;
                    ViewBag.DTypeName = cType.DName;
                    //文档库是否设置为私密
                    Document_Library modleLibrary = Document_LibraryBLL.GetModel(cType.FK_LibraryId);

                    ViewBag.IsPrivate = modleLibrary.IsPrivate;

                    //授权用户列表
                    ViewBag.AuthUserList = GetUserSelectItem();

                    //授权分组列表
                    ViewBag.RoleListList = Sys_RoleBLL.GetList();
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        #region 项目文档
        public ActionResult ProjectDoc(int fk_LibraryId)
        {
            ViewBag.FK_LibraryId = fk_LibraryId;
            ViewBag.UserId = UserId;
            ViewBag.RoleId = UserRoleId;
            return View();
        }
        #endregion

        public ActionResult AllList()
        {
            return View();
        }
    }
}
