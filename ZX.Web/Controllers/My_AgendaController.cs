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
    public class My_AgendaController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("My_AgendaAll");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMy_AgendaList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["name"] ?? "";
                //string beginTime = Request["beginTime"] ?? "";
                //string endTime = Request["endTime"] ?? "";
                int status = Request["status"].ToInt(-1);
                int index = 1;
                int userId = -1;

                if (UserId != 1)
                {
                    userId = UserId;
                }
                DataList<My_AgendaModel> list = My_AgendaBLL.GetMy_AgendaList(key, userId, status, pageIndex, PageSize);
                List<Sys_User> userList = new List<Sys_User>();
                List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                Dictionary dictModel = new Dictionary();
                if (list.Count > 0)
                {
                    userList = Sys_UserBLL.GetList();
                }
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-center'>" + item.ADate.ToShortDate() + "</td>");
                    builder.Append("<td class='text-center'>" + (item.AType.ToInt() == 0 ? "自定义" : "项目任务") + "</td>");


                    builder.Append("<td class='text-left' title='" + item.AName + "'>");
                    if (dictList.Count > 0)
                    {
                        try
                        {
                            builder.Append(dictList.Where(l => l.Id == item.APriority).FirstOrDefault().Name);
                        }
                        catch { }
                    }
                    builder.Append("</td>");

                    builder.Append("<td class='text-center'>" + item.AName + "</td>");

                    //获取真实名
                    string assignedName = "";
                    try
                    {
                        assignedName = userList.Where(l => l.Id == item.AAssigned.ToInt()).FirstOrDefault().RealName;
                    }
                    catch 
                    {
                    }


                    builder.Append("<td class='text-center'>" + assignedName + "</td>");
                    builder.Append("<td class='text-center'>" + item.AStartmmhh + "</td>");
                    builder.Append("<td class='text-center'>" + item.AEndmmhh + "</td>");
                    string statusName = "";
                    try
                    {
                        statusName = dictList.Where(l => l.Id == item.AStatus).FirstOrDefault().Name;
                    }
                    catch { }
                    builder.Append("<td class='text-center'>" + statusName + "</td>");

                    builder.Append("<td class='w-290px'>");

                    //按钮权限可见性控制
                    List<Sys_Button> btnList = RoleButtonList;
                    //表行按钮组
                    List<Sys_Button> currentBtnList28 = new List<Sys_Button>();

                    currentBtnList28 = btnList.Where(l => l.BGroup == 28 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();


                    if (currentBtnList28.Count > 0)
                    {
                        foreach (var btnItem in currentBtnList28)
                        {
                            //如果是进行中,那么禁用开始 如果已完成,禁用完成 
                            if (((statusName == "进行中" || statusName == "未开始") && btnItem.BName.Contains("激活")) || (statusName == "已完成" && (btnItem.BName.Contains("完成") || btnItem.BName.Contains("关闭"))) || (statusName == "已关闭" && (btnItem.BName.Contains("关闭") || btnItem.BName.Contains("完成"))))
                            {
                                builder.Append("<a href = 'javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                continue;
                            }
                            builder.Append("<a href = 'javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + item.Id + "\" class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                        }
                    }


                    builder.Append("</td>");
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


        #region 分页获取未完成待办
        /// <summary>
        /// 未完成初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult UnfinishedList()
        {
            return View();
        }

        public JsonResult GetMy_AgendaUnFinishList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["name"] ?? "";

                int index = 1;
                int userId = -1;

                if (UserId != 1)
                {
                    userId = UserId;
                }
                DataList<My_AgendaModel> list = My_AgendaBLL.GetMy_AgendaUnFinishList(key, userId, pageIndex, PageSize);
                List<Sys_User> userList = new List<Sys_User>();
                List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                Dictionary dictModel = new Dictionary();
                if (list.Count > 0)
                {
                    userList = Sys_UserBLL.GetList();
                }
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-center'>" + item.ADate.ToShortDate() + "</td>");
                    builder.Append("<td class='text-center'>" + (item.AType.ToInt() == 0 ? "自定义" : "项目任务") + "</td>");


                    builder.Append("<td class='text-left' title='" + item.AName + "'>");
                    if (dictList.Count > 0)
                    {
                        try
                        {
                            builder.Append(dictList.Where(l => l.Id == item.APriority).FirstOrDefault().Name);
                        }
                        catch { }
                    }
                    builder.Append("</td>");

                    builder.Append("<td class='text-center'>" + item.AName + "</td>");

                    //获取真实名
                    string assignedName = "";
                    try
                    {
                        assignedName = userList.Where(l => l.Id == item.AAssigned.ToInt()).FirstOrDefault().RealName;
                    }
                    catch
                    {
                    }

                    builder.Append("<td class='text-center'>" + assignedName + "</td>");
                    builder.Append("<td class='text-center'>" + item.AStartmmhh + "</td>");
                    builder.Append("<td class='text-center'>" + item.AEndmmhh + "</td>");
                    string statusName = "";
                    try
                    {
                        statusName = dictList.Where(l => l.Id == item.AStatus).FirstOrDefault().Name;
                    }
                    catch { }
                    builder.Append("<td class='text-center'>" + statusName + "</td>");

                    builder.Append("<td class='w-290px'>");

                    //按钮权限可见性控制
                    List<Sys_Button> btnList = RoleButtonList;
                    //表行按钮组
                    List<Sys_Button> currentBtnList28 = new List<Sys_Button>();

                    currentBtnList28 = btnList.Where(l => l.BGroup == 28 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();


                    if (currentBtnList28.Count > 0)
                    {
                        foreach (var btnItem in currentBtnList28)
                        {
                            //如果是进行中,那么禁用开始 如果已完成,禁用完成 
                            if (((statusName == "进行中" || statusName == "未开始") && btnItem.BName.Contains("激活")) || (statusName == "已完成" && (btnItem.BName.Contains("完成") || btnItem.BName.Contains("关闭"))) || (statusName == "已关闭" && (btnItem.BName.Contains("关闭") || btnItem.BName.Contains("完成"))))
                            {
                                builder.Append("<a href = 'javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                continue;
                            }
                            builder.Append("<a href = 'javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + item.Id + "\" class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                        }
                    }


                    builder.Append("</td>");
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


        #region 分页获取待定待办
        /// <summary>
        /// 未完成初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult IsUndeterminedList()
        {
            return View();
        }

        public JsonResult GetMy_AgendaAIsUndeterminedList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["name"] ?? "";

                int index = 1;
                int userId = -1;

                if (UserId != 1)
                {
                    userId = UserId;
                }
                DataList<My_AgendaModel> list = My_AgendaBLL.GetMy_AgendaAIsUndeterminedList(key, userId, pageIndex, PageSize);
                List<Sys_User> userList = new List<Sys_User>();
                List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                Dictionary dictModel = new Dictionary();
                if (list.Count > 0)
                {
                    userList = Sys_UserBLL.GetList();
                }
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-center'>" + item.ADate.ToShortDate() + "</td>");
                    builder.Append("<td class='text-center'>" + (item.AType.ToInt() == 0 ? "自定义" : "项目任务") + "</td>");


                    builder.Append("<td class='text-left' title='" + item.AName + "'>");
                    if (dictList.Count > 0)
                    {
                        try
                        {
                            builder.Append(dictList.Where(l => l.Id == item.APriority).FirstOrDefault().Name);
                        }
                        catch { }
                    }
                    builder.Append("</td>");

                    builder.Append("<td class='text-center'>" + item.AName + "</td>");

                    //获取真实名
                    string assignedName = "";
                    try
                    {
                        assignedName = userList.Where(l => l.Id == item.AAssigned.ToInt()).FirstOrDefault().RealName;
                    }
                    catch
                    {
                    }

                    builder.Append("<td class='text-center'>" + assignedName + "</td>");
                    builder.Append("<td class='text-center'>" + item.AStartmmhh + "</td>");
                    builder.Append("<td class='text-center'>" + item.AEndmmhh + "</td>");
                    string statusName = "";
                    try
                    {
                        statusName = dictList.Where(l => l.Id == item.AStatus).FirstOrDefault().Name;
                    }
                    catch { }
                    builder.Append("<td class='text-center'>" + statusName + "</td>");

                    builder.Append("<td class='w-290px'>");

                    //按钮权限可见性控制
                    List<Sys_Button> btnList = RoleButtonList;
                    //表行按钮组
                    List<Sys_Button> currentBtnList28 = new List<Sys_Button>();

                    currentBtnList28 = btnList.Where(l => l.BGroup == 28 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();


                    if (currentBtnList28.Count > 0)
                    {
                        foreach (var btnItem in currentBtnList28)
                        {
                            //如果是进行中,那么禁用开始 如果已完成,禁用完成 
                            if (((statusName == "进行中" || statusName == "未开始") && btnItem.BName.Contains("激活")) || (statusName == "已完成" && (btnItem.BName.Contains("完成") || btnItem.BName.Contains("关闭"))) || (statusName == "已关闭" && (btnItem.BName.Contains("关闭") || btnItem.BName.Contains("完成"))))
                            {
                                builder.Append("<a href = 'javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                continue;
                            }
                            builder.Append("<a href = 'javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + item.Id + "\" class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                        }
                    }


                    builder.Append("</td>");
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


        #region 分页获取指派给我的待办
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult AssignedMyList()
        {
            return View();
        }

        public JsonResult GetMy_AgendaAssignedMyList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["name"] ?? "";

                int index = 1;
                int userId = UserId;
                DataList<My_AgendaModel> list = My_AgendaBLL.GetMy_AgendaAssignedMyList(key, userId, pageIndex, PageSize);
                List<Sys_User> userList = new List<Sys_User>();
                List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                Dictionary dictModel = new Dictionary();
                if (list.Count > 0)
                {
                    userList = Sys_UserBLL.GetList();
                }
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-center'>" + item.ADate.ToShortDate() + "</td>");
                    builder.Append("<td class='text-center'>" + (item.AType.ToInt() == 0 ? "自定义" : "项目任务") + "</td>");


                    builder.Append("<td class='text-left' title='" + item.AName + "'>");
                    if (dictList.Count > 0)
                    {
                        try
                        {
                            builder.Append(dictList.Where(l => l.Id == item.APriority).FirstOrDefault().Name);
                        }
                        catch { }
                    }
                    builder.Append("</td>");

                    builder.Append("<td class='text-center'>" + item.AName + "</td>");

                    //获取真实名
                    string assignedName = "";
                    try
                    {
                        assignedName = userList.Where(l => l.Id == item.AAssigned.ToInt()).FirstOrDefault().RealName;
                    }catch{ }

                    builder.Append("<td class='text-center'>" + assignedName + "</td>");
                    builder.Append("<td class='text-center'>" + item.AStartmmhh + "</td>");
                    builder.Append("<td class='text-center'>" + item.AEndmmhh + "</td>");
                    string statusName = "";
                    try
                    {
                        statusName = dictList.Where(l => l.Id == item.AStatus).FirstOrDefault().Name;
                    }
                    catch { }
                    builder.Append("<td class='text-center'>" + statusName + "</td>");

                    builder.Append("<td class='w-290px'>");

                    //按钮权限可见性控制
                    List<Sys_Button> btnList = RoleButtonList;
                    //表行按钮组
                    List<Sys_Button> currentBtnList28 = new List<Sys_Button>();

                    currentBtnList28 = btnList.Where(l => l.BGroup == 28 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();


                    if (currentBtnList28.Count > 0)
                    {
                        foreach (var btnItem in currentBtnList28)
                        {
                            //如果是进行中,那么禁用开始 如果已完成,禁用完成 
                            if ((statusName == "进行中" && btnItem.BName.Contains("激活")) || (statusName == "已完成" && (btnItem.BName.Contains("完成") || btnItem.BName.Contains("关闭"))) || (statusName == "已关闭" && (btnItem.BName.Contains("关闭") || btnItem.BName.Contains("完成"))))
                            {
                                builder.Append("<a href = 'javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                continue;
                            }
                            builder.Append("<a href = 'javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + item.Id + "\" class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                        }
                    }


                    builder.Append("</td>");
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


        #region 分页获取指派给我的待办
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult AssignedOtherList()
        {
            return View();
        }

        public JsonResult GetMy_AgendaAssignedOtherList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["name"] ?? "";

                int index = 1;
                int userId =UserId;
                DataList<My_AgendaModel> list = My_AgendaBLL.GetMy_AgendaAssignedOtherList(key, userId, pageIndex, PageSize);
                List<Sys_User> userList = new List<Sys_User>();
                List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                Dictionary dictModel = new Dictionary();
                if (list.Count > 0)
                {
                    userList = Sys_UserBLL.GetList();
                }
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-center'>" + item.ADate.ToShortDate() + "</td>");
                    builder.Append("<td class='text-center'>" + (item.AType.ToInt() == 0 ? "自定义" : "项目任务") + "</td>");


                    builder.Append("<td class='text-left' title='" + item.AName + "'>");
                    if (dictList.Count > 0)
                    {
                        try
                        {
                            builder.Append(dictList.Where(l => l.Id == item.APriority).FirstOrDefault().Name);
                        }
                        catch { }
                    }
                    builder.Append("</td>");

                    builder.Append("<td class='text-center'>" + item.AName + "</td>");


                    //获取真实名
                    string assignedName = "";
                    try
                    {
                        assignedName = userList.Where(l => l.Id == item.AAssigned.ToInt()).FirstOrDefault().RealName;
                    }
                    catch { }

                    builder.Append("<td class='text-center'>" + assignedName + "</td>");
                    builder.Append("<td class='text-center'>" + item.AStartmmhh + "</td>");
                    builder.Append("<td class='text-center'>" + item.AEndmmhh + "</td>");
                    string statusName = "";
                    try
                    {
                        statusName = dictList.Where(l => l.Id == item.AStatus).FirstOrDefault().Name;
                    }
                    catch { }
                    builder.Append("<td class='text-center'>" + statusName + "</td>");

                    builder.Append("<td class='w-290px'>");

                    //按钮权限可见性控制
                    List<Sys_Button> btnList = RoleButtonList;
                    //表行按钮组
                    List<Sys_Button> currentBtnList28 = new List<Sys_Button>();

                    currentBtnList28 = btnList.Where(l => l.BGroup == 28 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();


                    if (currentBtnList28.Count > 0)
                    {
                        foreach (var btnItem in currentBtnList28)
                        {
                            //如果是进行中,那么禁用开始 如果已完成,禁用完成 
                            if (((statusName == "进行中" || statusName == "未开始") && btnItem.BName.Contains("激活")) || (statusName == "已完成" && (btnItem.BName.Contains("完成") || btnItem.BName.Contains("关闭"))) || (statusName == "已关闭" && (btnItem.BName.Contains("关闭") || btnItem.BName.Contains("完成"))))
                            {
                                builder.Append("<a href = 'javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                continue;
                            }
                            builder.Append("<a href = 'javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + item.Id + "\" class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                        }
                    }


                    builder.Append("</td>");
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

        /// <summary>
        /// 获取个人无分页数据,默认获取当前月份的个人所有相关待办
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAgendaList()
        {
            AjaxResult result = new AjaxResult();

            try
            {
                DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                DateTime endTime = DateTime.Now.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                List<My_Agenda> list = My_AgendaBLL.GetList(d => d.Where(t => t.ADate >= startTime && t.ADate <= endTime && t.AIsUndetermined != 0 && (t.CreateUserId==UserId||t.AAssigned==UserId)));
                result.Data = list;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("查询待办异常", ex);
            }

            return Json(result);
        }
        /// <summary>
        /// 列表添加日程
        /// </summary>
        /// <returns></returns>
        public ActionResult My_AgendaListAdd()
        {
            List<Sys_User> uList = new List<Sys_User>();
            List<SelectListItem> teamUser = new List<SelectListItem>(); //指派列表
            List<Dictionary> dicnrChildList = new List<Dictionary>();
            List<SelectListItem> aPrioritySItem = new List<SelectListItem>(); //优先级

            List<SelectListItem> projectSItem = new List<SelectListItem>(); //项目列表
            try
            {
                uList = Sys_UserBLL.GetList();
                foreach (var item in uList)
                {
                    teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                }
                try
                {
                    List<Dictionary> parentList = DictionaryBLL.GetList(d => d.Where(l => l.Name == "待办优先级"));
                    Dictionary dictModel = parentList.FirstOrDefault();

                    int parentId = dictModel.Id.ToInt();

                    dicnrChildList = DictionaryBLL.GetList(d => d.Where(l => l.ParentId == parentId));
                    foreach (var childItem in dicnrChildList)
                    {
                        aPrioritySItem.Add(new SelectListItem { Text = childItem.Name, Value = childItem.Id.ToString() });
                    }
                    int userId = UserId.ToInt();
                    //获取与自己相关的项目
                    List<Project> projectList = ProjectBLL.GetProjectCorrelationListByUserId(userId);

                    foreach (var projectItem in projectList)
                    {
                        projectSItem.Add(new SelectListItem { Text = projectItem.PName, Value = projectItem.Id.ToString() });
                    }

                }
                catch { }

                ViewBag.ListTeam = teamUser;

                ViewBag.TPrioritySItem = aPrioritySItem;

                ViewBag.ProjectSItem = projectSItem;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("批量新增待办异常", ex);
            }

            return View();
        }
        public JsonResult My_AgendaListAddSave(string jsonData)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                List<My_Agenda> list = jsonData.ToJsonDeserialize<List<My_Agenda>>();
                List<My_Agenda> standardList = new List<My_Agenda>();

                List<My_Agenda_Log> logList = new List<My_Agenda_Log>();
                if (list.Count > 0)
                {
                    List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                    List<Dictionary> agendaStatus = new List<Dictionary>();
                    Dictionary dictModel = new Dictionary();

                    dictModel = dictList.Where(d => d.Name == "待办状态").FirstOrDefault();
                    agendaStatus = dictList.Where(l => l.ParentId == dictModel.Id).ToList();

                    Dictionary agendaModel = agendaStatus.Where(l => l.Name == "未开始").FirstOrDefault();
                    foreach (var item in list)
                    {
                        item.CreateTime = DateTime.Now;
                        item.CreateUserId = UserId;
                        item.CreateAccount = UserAccount;
                        item.AStatus = agendaModel.Id.ToInt();

                        //standardList.Add(item);

                        int id = My_AgendaBLL.AddModel(item);

                        //日志
                        My_Agenda_Log log = new My_Agenda_Log();
                        log.FK_AgendaId = id;
                        log.Remark = "创建";
                        log.CreateTime = DateTime.Now;
                        log.CreateUserId = UserId;
                        log.CreateAccount = UserAccount;
                        log.AStatus = "为开始";
                        log.ADoFun = "批量新增";

                        My_Agenda_LogBLL.AddModel(log);
                    }
                    //My_AgendaBLL.MergeNotDelModel(standardList, "A.AType=B.AType AND A.AName=B.AName AND A.ADate=B.ADate AND A.AAssigned=B.AAssigned AND A.AStartmmhh=B.AStartmmhh AND A.AEndmmhh=B.AEndmmhh");

                    //保存日志

                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("保存日程异常", ex);
            }
            return Json(result);
        }

        public ActionResult EventAgenda()
        {
            My_Agenda agenda = new My_Agenda();
            try
            {
                int id = Request["Id"].ToInt();
                if (id > 0)
                {
                    agenda = My_AgendaBLL.GetModel(id);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("跳转异常", ex);
            }
            return View(agenda);
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Readonly(int? id)
        {
            My_Agenda model = new My_Agenda();
            try
            {
                if (id > 0)
                {
                    model = My_AgendaBLL.GetModel(id.ToInt());

                    List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                    //待办状态
                    List<SelectListItem> aStatusSItem = new List<SelectListItem>();
                    List<Dictionary> agendaStatus = new List<Dictionary>();
                    Dictionary dictModel = new Dictionary();

                    dictModel = dictList.Where(d => d.Name == "待办状态").FirstOrDefault();
                    agendaStatus = dictList.Where(l => l.ParentId == dictModel.Id).ToList();

                    foreach (var itemStatus in agendaStatus)
                    {
                        aStatusSItem.Add(new SelectListItem { Text = itemStatus.Name, Value = itemStatus.Id.ToString() });
                    }
                    ViewBag.AgendaStatus = aStatusSItem;


                    //优先级
                    List<SelectListItem> aPrioritySItem = new List<SelectListItem>();
                    List<Dictionary> agendaPriority = new List<Dictionary>();
                    Dictionary dictPriorityModel = new Dictionary();

                    dictPriorityModel = dictList.Where(d => d.Name == "待办优先级").FirstOrDefault();
                    agendaPriority = dictList.Where(l => l.ParentId == dictPriorityModel.Id).ToList();

                    foreach (var itemPriority in agendaPriority)
                    {
                        aPrioritySItem.Add(new SelectListItem { Text = itemPriority.Name, Value = itemPriority.Id.ToString() });
                    }
                    ViewBag.AgendaPriority = aPrioritySItem;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        public JsonResult Finish(int id)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                if (id > 0)
                {
                    My_Agenda model = new My_Agenda();
                    model = My_AgendaBLL.GetModel(id);
                    model.AFinishTime = DateTime.Now;
                    model.AFinishUserId = UserId;
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;

                    List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典
                    List<Dictionary> agendaStatus = new List<Dictionary>();
                    Dictionary dictModel = new Dictionary();
                    dictModel = dictList.Where(d => d.Name == "待办状态").FirstOrDefault();
                    agendaStatus = dictList.Where(l => l.ParentId == dictModel.Id).ToList();
                    Dictionary agendaModel = agendaStatus.Where(l => l.Name == "已完成").FirstOrDefault();

                    model.AStatus = agendaModel.Id.ToInt();

                    My_AgendaBLL.UpdateModel(model);

                    //追加待办日志
                    My_Agenda_Log logModel = new My_Agenda_Log();
                    logModel.AStatus = "已完成";
                    logModel.ADoFun = "完成";
                    logModel.CreateAccount = UserAccount;
                    logModel.CreateTime = DateTime.Now;
                    logModel.CreateUserId = UserId;
                    logModel.FK_AgendaId = model.Id;
                    logModel.Remark = "手动完成";
                    My_Agenda_LogBLL.AddModel(logModel);
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError("待办完成异常", ex);
            }
            return Json(result);
        }

        /// <summary>
        /// 指派跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Point(int id)
        {
            My_Agenda agenda = new My_Agenda();
            try
            {
                if (id > 0)
                {
                    agenda = My_AgendaBLL.GetModel(id);

                    //员工列表
                    List<Sys_User> uList = new List<Sys_User>();
                    List<SelectListItem> teamUser = new List<SelectListItem>(); //指派列表
                    uList = Sys_UserBLL.GetList();
                    foreach (var item in uList)
                    {
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                    }
                    ViewBag.ListTeam = teamUser;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("待办指派跳转异常", ex);
            }
            return View(agenda);
        }

        public JsonResult PointSave()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                My_Agenda model = FormHelper.GetRequestForm<My_Agenda>();
                model.UpdateAccount = UserAccount;
                model.UpdateTime = DateTime.Now;
                model.UpdateUserId = UserId;
                My_AgendaBLL.UpdateModel(model);

                //追加待办日志
                My_Agenda_Log logModel = new My_Agenda_Log();
                model = My_AgendaBLL.GetModel(model.Id);
                Dictionary dictModel = DictionaryBLL.GetModel(model.AStatus.ToInt());

                logModel.AStatus = dictModel.Name;
                logModel.ADoFun = "指派";
                logModel.CreateAccount = UserAccount;
                logModel.CreateTime = DateTime.Now;
                logModel.CreateUserId = UserId;
                logModel.FK_AgendaId = model.Id;
                logModel.Remark = "手动指派";
                My_Agenda_LogBLL.AddModel(logModel);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError("待办指派异常", ex);
            }
            return Json(result);
        }

        public JsonResult RunAgain(int id)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                if (id > 0)
                {
                    My_Agenda model = new My_Agenda();
                    model = My_AgendaBLL.GetModel(id);
                    model.AFinishTime = new DateTime(1990, 1, 1, 0, 0, 0);
                    model.AFinishUserId = -999;
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;

                    List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典
                    List<Dictionary> agendaStatus = new List<Dictionary>();
                    Dictionary dictModel = new Dictionary();
                    dictModel = dictList.Where(d => d.Name == "待办状态").FirstOrDefault();
                    agendaStatus = dictList.Where(l => l.ParentId == dictModel.Id).ToList();
                    Dictionary agendaModel = agendaStatus.Where(l => l.Name == "未开始").FirstOrDefault();
                    model.AStatus = agendaModel.Id.ToInt();

                    My_AgendaBLL.UpdateModel(model);

                    //追加待办日志
                    My_Agenda_Log logModel = new My_Agenda_Log();
                    logModel.AStatus = "待办激活";
                    logModel.ADoFun = "激活";
                    logModel.CreateAccount = UserAccount;
                    logModel.CreateTime = DateTime.Now;
                    logModel.CreateUserId = UserId;
                    logModel.FK_AgendaId = model.Id;
                    logModel.Remark = "手动激活";
                    My_Agenda_LogBLL.AddModel(logModel);
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError("待办完成异常", ex);
            }
            return Json(result);
        }





        /// <summary>
        /// 关闭跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Close(int id)
        {
            My_Agenda agenda = new My_Agenda();
            try
            {
                if (id > 0)
                {
                    agenda = My_AgendaBLL.GetModel(id);

                    //员工列表
                    List<Sys_User> uList = new List<Sys_User>();
                    List<SelectListItem> teamUser = new List<SelectListItem>(); //指派列表
                    uList = Sys_UserBLL.GetList();
                    foreach (var item in uList)
                    {
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                    }
                    ViewBag.ListTeam = teamUser;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("待办关闭跳转异常", ex);
            }
            return View(agenda);
        }


        public JsonResult CloseSave()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                My_Agenda model = FormHelper.GetRequestForm<My_Agenda>();
                model.UpdateAccount = UserAccount;
                model.UpdateTime = DateTime.Now;
                model.UpdateUserId = UserId;
                model.ACloseTime = DateTime.Now;


                List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典
                List<Dictionary> agendaStatus = new List<Dictionary>();
                Dictionary dictModel = new Dictionary();
                dictModel = dictList.Where(d => d.Name == "待办状态").FirstOrDefault();
                agendaStatus = dictList.Where(l => l.ParentId == dictModel.Id).ToList();
                Dictionary agendaModel = agendaStatus.Where(l => l.Name == "已关闭").FirstOrDefault();

                model.AStatus = agendaModel.Id.ToInt();
                My_AgendaBLL.UpdateModel(model);

                //追加待办日志
                My_Agenda_Log logModel = new My_Agenda_Log();

                logModel.AStatus = "已关闭";
                logModel.ADoFun = "关闭";
                logModel.CreateAccount = UserAccount;
                logModel.CreateTime = DateTime.Now;
                logModel.CreateUserId = UserId;
                logModel.FK_AgendaId = model.Id;
                logModel.Remark = "手动关闭";
                My_Agenda_LogBLL.AddModel(logModel);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError("待办关闭异常", ex);
            }
            return Json(result);
        }




        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult My_AgendaEdit(int? id)
        {
            My_Agenda model = new My_Agenda();
            try
            {
                if (id > 0)
                {
                    model = My_AgendaBLL.GetModel(id.ToInt());

                    List<Dictionary> dictList = DictionaryBLL.GetList();//获取字典

                    //待办状态
                    List<SelectListItem> aStatusSItem = new List<SelectListItem>();
                    List<Dictionary> agendaStatus = new List<Dictionary>();
                    Dictionary dictModel = new Dictionary();

                    dictModel = dictList.Where(d => d.Name == "待办状态").FirstOrDefault();
                    agendaStatus = dictList.Where(l => l.ParentId == dictModel.Id).ToList();

                    foreach (var itemStatus in agendaStatus)
                    {
                        aStatusSItem.Add(new SelectListItem { Text = itemStatus.Name, Value = itemStatus.Id.ToString() });
                    }
                    ViewBag.AgendaStatus = aStatusSItem;


                    //优先级
                    List<SelectListItem> aPrioritySItem = new List<SelectListItem>();
                    List<Dictionary> agendaPriority = new List<Dictionary>();
                    Dictionary dictPriorityModel = new Dictionary();

                    dictPriorityModel = dictList.Where(d => d.Name == "待办优先级").FirstOrDefault();
                    agendaPriority = dictList.Where(l => l.ParentId == dictPriorityModel.Id).ToList();

                    foreach (var itemPriority in agendaPriority)
                    {
                        aPrioritySItem.Add(new SelectListItem { Text = itemPriority.Name, Value = itemPriority.Id.ToString() });
                    }
                    ViewBag.AgendaPriority = aPrioritySItem;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            My_Agenda model = FormHelper.GetRequestForm<My_Agenda>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    row = My_AgendaBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.Id = My_AgendaBLL.AddModel(model);
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
                int row = My_AgendaBLL.DelModelById(id);
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
