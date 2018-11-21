using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;

namespace ZX.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = ZXConfig.SYSNAME;
            ViewBag.RealName = RealName;
            StringBuilder builder = new StringBuilder();

            List<Sys_User> userList = Sys_UserBLL.GetList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in userList)
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.RealName });
            }
            ViewBag.UserList = list;
            ViewBag.CurrentTime = DateTime.Now;
            return Redirect("/PersonalSpace/Index");
        }
        /// <summary>
        /// 定时刷新页面各列表数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetNotickHtml()
        {
            AjaxResult result = new AjaxResult();
            StringBuilder builder = new StringBuilder();
            try
            {
                //获取相关项目任务

                List<Project_TaskModel> taskList = new List<Project_TaskModel>();

                List<Sys_User> userList = Sys_UserBLL.GetList();

                string orderBy = "AsTime";
                taskList = Project_TaskBLL.GetProjectTaskNotFinish(-1, UserId);
                int j = -1;// 计量右下信息数,作为距离底部bottom 而用
                int i = 0;
                if (taskList.Count > 0)
                {
                    j++;
                }
                foreach (var taskItem in taskList)
                {
                    string userRealName = "";
                    if (taskItem.Assigned.ToInt() > 0)
                    {
                        userRealName = userList.Where(l => l.Id == taskItem.Assigned.ToInt()).FirstOrDefault().RealName;
                    }

                    builder.Append("<div id=\"noticeorder" + i + "\" data-id=\"order" + i + "\" class=\"alert alert-success with-icon alert-dismissable\" style=\"width:390px; height: 109px; position:fixed; bottom:" + (25 + 109 * j) + "px; right:15px; z-index:" + (9999 - i) + ";\"><i class=\"icon icon-envelope-alt\"></i><div class=\"content\"><p><span class=\"label label-danger\">" + (taskList.Count - i) + "</span><strong>项目任务&nbsp;&nbsp;");

                    string tName = "";
                    if (taskItem.TName.Length > 10)
                    {
                        tName = taskItem.TName.Substring(0, 10) + "...";
                    }
                    else
                    {
                        tName = taskItem.TName;
                    }

                    builder.Append("<a href=\"/Project_Task/Index?mid=3&secondMenuId=15\" title=\"" + taskItem.TName + "\">" + tName + " </a>");
                    builder.Append("</strong></p>");
                    builder.Append("<a href =\"/Project_Task/Index?mid=3&secondMenuId=15\" title=\"" + taskItem.TName + "\"><p>指派给:" + userRealName + "&nbsp;&nbsp;(" + taskItem.AsTime.ToShortDate() + ")</p>" + " </a>");
                    builder.Append("</div><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" data-read=\"\">×</button></div>");
                    i++;
                }

                //获取相关临时任务

                List<Temporary_TaskModel> temporaryTaskList = new List<Temporary_TaskModel>();
                temporaryTaskList = Temporary_TaskBLL.GetTemporary_TaskBlockList(-1, " AsTime>='" + DateTime.Now.ToShortDateString() + "' AND TState IN (1,2,3)", orderBy);

                i = 0;

                if (temporaryTaskList.Count > 0)
                {
                    j++;
                }
                foreach (var temporaryTaskItem in temporaryTaskList)
                {
                    string userRealName = "";
                    if (temporaryTaskItem.Assigned.ToInt() > 0)
                    {
                        userRealName = userList.Where(l => l.Id == temporaryTaskItem.Assigned.ToInt()).FirstOrDefault().RealName;
                    }

                    builder.Append("<div id=\"noticeorder" + i + "\" data-id=\"order" + i + "\" class=\"alert alert-success with-icon alert-dismissable\" style=\"width:390px; height: 109px; position:fixed; bottom:" + (25 + 119 * j) + "px; right:15px; z-index:" + (9999 - i) + ";\"><i class=\"icon icon-envelope-alt\"></i><div class=\"content\"><p><span class=\"label label-danger\">" + (temporaryTaskList.Count - i) + "</span><strong>临时任务");

                    string tName = "";
                    if (temporaryTaskItem.TName.Length > 10)
                    {
                        tName = temporaryTaskItem.TName.Substring(0, 10) + "...";
                    }
                    else
                    {
                        tName = temporaryTaskItem.TName;
                    }
                    builder.Append("<a href=\"/Temporary_Task/Temporary_TaskList?mid=4&secondMenuId=39\" title=\"" + temporaryTaskItem.TName + "\">" + tName + "</a>");
                    builder.Append("</strong></p>");
                    builder.Append("<a href=\"/Temporary_Task/Temporary_TaskList?mid=4&secondMenuId=39\"<p>指派给:" + userRealName + "&nbsp;&nbsp;(" + temporaryTaskItem.AsTime.ToShortDate() + ")</p></a>");
                    builder.Append("</div><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" data-read=\"\">×</button></div>");

                    i++;
                }

                //获取相关审批
                i = 0;
                List<AllApplyNotice> allApplyNoticeList = new List<AllApplyNotice>();
                int fk_CompanyPositionId = 0;

                if (UserId == 1)
                {
                    fk_CompanyPositionId = -1;
                }
                if (UserId != 1 && PositionId.ToInt() != -1)
                {
                    fk_CompanyPositionId = PositionId.ToInt();
                }

                if (fk_CompanyPositionId != 0)
                {
                    allApplyNoticeList = AllApplyNoticeBLL.GetAllApplyNoticeList(fk_CompanyPositionId);

                    if (allApplyNoticeList.Count > 0)
                    {
                        j++;
                    }
                    foreach (var applyNotice in allApplyNoticeList)
                    {
                        builder.Append("<div id=\"noticeorder" + i + "\" data-id=\"order" + i + "\" class=\"alert alert-success with-icon alert-dismissable\" style=\"width:390px; height: 109px; position:fixed; bottom:" + (25 + 119 * j) + "px; right:15px; z-index:" + (9999 - i) + ";\"><i class=\"icon icon-envelope-alt\"></i><div class=\"content\"><p><span class=\"label label-danger\">" + (allApplyNoticeList.Count - i) + "</span><strong>审批");

                        string ADesc = "";
                        if (applyNotice.ADesc.Length > 10)
                        {
                            ADesc = applyNotice.ADesc.Substring(0, 10) + "...";
                        }
                        else
                        {
                            ADesc = applyNotice.ADesc;
                        }

                        builder.Append("<a href=\"" + applyNotice.ApplyAction + "\" data-appid=\"crm\" title=\"" + applyNotice.Title + "\">" + applyNotice.Title + "</a>");
                        builder.Append("</strong></p>");
                        builder.Append("<p title=\"" + applyNotice.ADesc + "\" ><a href=\"" + applyNotice.ApplyAction + "\">描述:" + ADesc + "&nbsp;&nbsp;(" + applyNotice.FlowName + ")</a></p>");
                        builder.Append("</div><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" data-read=\"\">×</button></div>");
                        i++;
                    }
                }

                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("定时获取通知信息异常", ex);
            }
            return Json(result);
        }
    }
}