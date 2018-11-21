using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using ZX.Web.Common;

namespace ZX.Web.Controllers
{
    public class ProjectController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获取当前用户项目区块
            ViewBag.UserBlockList = Project_BlockBLL.GetList(t => t.Where(d => d.CreateUserId == UserId));
            //亮灯判断所需要的系统时间
            ViewBag.CurrentTime = DateTime.Now;
            ViewBag.RealName = RealName;

            return View("ProjectMain");
        }
        #endregion


        #region 项目列表页
        public ActionResult GetProjectList()
        {
            int pageIndex = Request.Form["pageIndex"].ToInt(1);
            int pageSize = Request.Form["pageSize"].ToInt(10);
            int status = Request.Form["status"].ToInt(-1);

            ViewBag.RoleButtonList = RoleButtonList;
            // 0：有效 10：挂起 20：完成 30：删除
            //ViewBag.ProjectList = ProjectBLL.GetList(d => d.Where(t => t.PStatus != "30" && t.PStatus != "10"));\

            DataList<ProjectModel> list = new DataList<ProjectModel>();
            if (UserId == 1 || UserId == -1111)
            {
                list = ProjectBLL.GetProjectList("", -1, status, pageIndex, pageSize);
            }
            else
            {
                list = ProjectBLL.GetProjectList("", UserId, status, pageIndex, pageSize);
            }
            List<Sys_User> uList = Sys_UserBLL.GetList();

            //亮灯判断所需要的系统时间
            ViewBag.CurrentTime = DateTime.Now;

            ViewBag.UList = uList;
            ViewBag.Count = list.TotalCount;

            return View("ProjectList", list);
        }
        #endregion

        #region 查看
        public ActionResult Readonly(int? id)
        {
            Project model = new Project();
            try
            {
                List<SelectListItem> userList = new List<SelectListItem>();
                List<SelectListItem> teamUser = new List<SelectListItem>();
                List<Sys_User> uList = Sys_UserBLL.GetList(d => d.Where(l => l.Id != 1));
                if (uList.Count() > 0)
                {
                    foreach (var item in uList)
                    {
                        userList.Add(new SelectListItem { Text = item.RealName, Value = item.RealName });
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                    }
                }
                //负责人列表
                ViewBag.HeadUserList = teamUser;
                //团队
                ViewBag.TeamUserList = teamUser;

                //获取参观者列表
                ViewBag.RoleListList = Sys_RoleBLL.GetList(d => d.Where(l => l.Id != 1));

                if (id > 0)
                {
                    model = ProjectBLL.GetModel(id.ToInt());
                    // 获取团队列表,目前作用(1)反填充下拉列表
                    List<Project_Team> teamList = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == model.Id));
                    ViewBag.TeamList = teamList;
                }
                //else {
                //    model.PStatus = "0";
                //}
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        #endregion

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id, int mid)
        {
            Project model = new Project();
            try
            {
                //保存当前菜单
                ViewBag.CurrentMid = mid;

                List<SelectListItem> userList = new List<SelectListItem>();
                List<SelectListItem> teamUser = new List<SelectListItem>();
                List<Sys_User> uList = Sys_UserBLL.GetList(d => d.Where(l => l.Id != 1));
                if (uList.Count() > 0)
                {
                    foreach (var item in uList)
                    {
                        userList.Add(new SelectListItem { Text = item.RealName, Value = item.RealName });
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                    }
                }
                //负责人列表
                ViewBag.HeadUserList = teamUser;
                //团队
                ViewBag.TeamUserList = teamUser;

                //获取参观者列表
                ViewBag.RoleListList = Sys_RoleBLL.GetList(d => d.Where(l => l.Id != 1));

                if (id > 0)
                {
                    model = ProjectBLL.GetModel(id.ToInt());
                    // 获取团队列表,目前作用(1)反填充下拉列表
                    List<Project_Team> teamList = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == model.Id));
                    ViewBag.TeamList = teamList;
                }
                else
                {
                    model.CreateTime = DateTime.Now;
                }
                //else {
                //    model.PStatus = "0";
                //}
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        #endregion

        #region 完成
        public ActionResult Finish(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            Project model = FormHelper.GetRequestForm<Project>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateAccount = UserAccount;
                    row = ProjectBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.CreateAccount = UserAccount;
                    // 0：有效 10：挂起 20：完成 30：删除
                    model.PStatus = "0";//有效

                    model.Id = ProjectBLL.AddModel(model);
                }

                string str = Request.Form["TeamUser"].ToString();
                string[] userId = str.Split(',');
                if (userId.Length > 0)
                {
                    List<Project_Team> list = new List<Project_Team>();
                    foreach (var item in userId)
                    {
                        Project_Team team = new Project_Team()
                        {
                            CreateTime = DateTime.Now,
                            CreateUserId = UserId,
                            FK_ProjectId = model.Id.ToInt(),
                            FK_UserId = item.ToInt(),
                            CreateAccount = UserAccount
                        };
                        list.Add(team);
                    }
                    Project_TeamBLL.MergeModel(list, "A.FK_ProjectId=B.FK_ProjectId and A.FK_UserId=B.FK_UserId", "A.FK_ProjectId=" + model.Id);
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

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="logText">描述</param>
        /// <returns></returns>
        public JsonResult FinishSave(int projectId, string logText)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                //// 删除任务
                //Project_TaskBLL.DelModel(" FK_ProjectId =" + id);
                //// 删除团队
                //Project_TeamBLL.DelModel(" FK_ProjectId =" + id);
                //// 删除文件
                // 删除项目
                Project model = ProjectBLL.GetModel(projectId.ToInt());
                if (model != null)
                {
                    model.PStatus = "20";
                    ProjectBLL.UpdateModel(model);
                }
                Project_Log logModel = new Project_Log()
                {
                    FK_ProjectId = projectId,
                    StatusId = 20,
                    StatusName = "完成项目",
                    Remark = logText,
                    CreateTime = DateTime.Now,
                    CreateAccount = UserAccount,
                    CreateUserId = UserId
                };
                Project_LogBLL.AddModel(logModel);
                rest.Message = "操作成功";
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
        /// <param name="id">项目Id</param>
        /// <returns></returns>
        public JsonResult DeleteData(string id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                //// 删除任务
                //Project_TaskBLL.DelModel(" FK_ProjectId =" + id);
                //// 删除团队
                //Project_TeamBLL.DelModel(" FK_ProjectId =" + id);
                //// 删除文件
                // 删除项目
                Project model = ProjectBLL.GetModel(id.ToInt());
                if (model != null)
                {
                    model.PStatus = "30";
                    int row = ProjectBLL.UpdateModel(model);
                    if (row > 0)
                    {
                        Project_TaskBLL.UpdateModel(new Project_Task() { TState = 7 }, d => d.Where(t => t.FK_ProjectId == model.Id));
                    }
                }
                rest.Message = "删除成功";
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
        /// <summary>
        /// 挂起
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <returns></returns>
        public JsonResult HangUp(string id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                Project model = ProjectBLL.GetModel(id.ToInt());
                if (model != null)
                {
                    model.PStatus = "10";
                    ProjectBLL.UpdateModel(model);
                }
                rest.Message = "挂起成功";
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
        #region 激活
        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="id">项目Id</param>
        /// <returns></returns>
        public JsonResult RunAgain(int id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                Project model = ProjectBLL.GetModel(id);
                if (model != null)
                {
                    model.PStatus = "0";
                    ProjectBLL.UpdateModel(model);
                }
                Project_Log logModel = new Project_Log()
                {
                    FK_ProjectId = id,
                    StatusId = 20,
                    StatusName = "激活项目",
                    Remark = "激活项目",
                    CreateTime = DateTime.Now,
                    CreateAccount = UserAccount,
                    CreateUserId = UserId
                };
                Project_LogBLL.AddModel(logModel);
                rest.Message = "操作成功";
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

        #region 分页获取附件
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetAttachList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";
                DataList<Project_TaskModel> list = new DataList<Project_TaskModel>();
                //用户可见程度
                if (UserId == 1 || UserId == -1111)
                {
                    list = Project_TaskBLL.GetProject_TaskList(name, pageIndex, pageSize, -1, -1, -1, -1, -1, -1);
                }
                else
                {
                    list = Project_TaskBLL.GetProject_TaskList(name, pageIndex, pageSize, -1, UserId, UserId, UserId, UserId, UserId);
                }

                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.TName + "'>" + item.TName + "</td>");
                    builder.Append("<td class='text-left' title='" + item.Attach + "'><a href='" + item.Attach + "'>" + item.Attach + "</a></td>");

                    builder.Append("<td class='text-left' title='" + item.CreateTime + "'>" + item.CreateTime + "</td>");

                    builder.Append("<td class='text-left'><a href='javascript:downLoad(" + item.Attach + ")'>下载</a>&nbsp;&nbsp;<a href='javascript:del(" + item.Id + ")'>删除</a></td>");
                    builder.Append("</tr>");
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = list.TotalCount;
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = "获取数据失败";
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion


        #region 相关文件首页
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectDocMainList()
        {
            //获取当前用户项目区块
            return View();
        }
        #endregion

        public JsonResult GetProjectDocMainList()
        {
            AjaxResult result = new AjaxResult();
            StringBuilder builder = new StringBuilder();
            try
            {
                List<Project> listProject = ProjectBLL.GetList();
                List<Project> userProject = listProject.Where(t => t.CreateUserId == UserId || t.HeadUser == (UserId + "") || t.Visitors.Contains(UserId + "")).ToList();
                foreach (var item in userProject)
                {
                    builder.Append("<div class='col-md-3'>");
                    builder.Append("<div class='libs-group-heading libs-project-heading' onclick='jumpProjectLib(" + @item.Id + ")' style='height: 30px; '>");
                    builder.Append("<a href='javascript:;' title='" + @item.PName + "' onclick='jumpProjectLib(" + @item.Id + ")'><span class='label label-success'>项目文件包</span> " + @item.PName + "</a>");
                    builder.Append("</div>");
                    builder.Append("<div class='libs-group clearfix'>");

                    builder.Append("<a class='lib w-lib-p50' title='' href='javascript:;' onclick='jumpProjectLib(" + @item.Id + ")'>");
                    builder.Append("<i class='icon icon-2x icon-folder-open-alt'></i>");
                    builder.Append("<div class='lib-name' title='文件包'>文件包</div>");
                    builder.Append("</a>");

                    builder.Append("</div>");
                    builder.Append("</div>");
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = "获取数据异常.";
                Log4Helper.WriteError("获取数据异常", ex);
            }

            result.Code = ResultCode.Succeed;
            result.Data = builder.ToString();
            return Json(result);
        }


        #region 相关文件二级页
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectDocList(int? projectId)
        {
            projectId.ToInt();
            Project pro = ProjectBLL.GetModel(projectId);

            return View(pro);
        }
        #endregion

        #region 进度统计

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult ProjectStatistics()
        {
            return View("Statistics");
        }
        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <returns></returns>
        public JsonResult ProjectStatisticsList()
        {
            int pageIndex = Request.Form["pageIndex"].ToInt(1);
            int pageSize = Request.Form["pageSize"].ToInt(20);
            int status = Request.Form["status"].ToInt(-1);

            int userId = Request["userId"].ToInt(-1);
            string roleId = Request["roleId"];

            // 0：有效 10：挂起 20：完成 30：删除
            AjaxResult result = new AjaxResult();
            try
            {

                DataList<ProjectModel> list = new DataList<ProjectModel>();
                if (UserId == 1 || UserId == -1111)
                {
                    list = ProjectBLL.GetProjectList("", -1, status, pageIndex, pageSize);
                }
                else
                {
                    list = ProjectBLL.GetProjectList("", UserId, status, pageIndex, pageSize);
                }
                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.PName + "'>" + item.PName + "</td>");
                    string pStatusName = "未知";
                    pStatusName = EnumGetDesc.GetEnumDescription((ProjectStatus)Enum.Parse(typeof(ProjectStatus), item.PStatus));

                    builder.Append("<td>" + pStatusName + "</td>");
                    int taskCounts = 0; //任务数
                    int finishCounts = 0; //完成数
                    int taskClassACounts = 0; //主任务完成数
                    int childTaskCounts = 0;//子任务完成数
                    int doingCounts = 0; //进行中
                    int notStart = 0;
                    decimal percent = 0; //百分比

                    List<Project_Task> pAllTask = Project_TaskBLL.GetList(d => d.Where(t => t.FK_ProjectId == item.Id));
                    List<Project_Task> pMainTask = pAllTask.Where(l => l.ParentId.IsNullOrEmpty()).ToList();
                    foreach (var taskClassA in pMainTask)
                    {
                        List<Project_Task> pChildTask = pAllTask.Where(t => t.ParentId == taskClassA.Id).ToList();
                        if (pChildTask.Count < 0)
                        {
                            taskCounts++;
                            if (taskClassA.TState == 4)
                            {
                                taskClassACounts++;
                            }
                            if (taskClassA.TState == 3 || taskClassA.TState == 1)
                            {
                                doingCounts++;
                            }
                            if (taskClassA.TState == 2)
                            {
                                notStart++;
                            }
                            continue;
                        }

                        //计算子任务完成情况
                        foreach (var childTask in pChildTask)
                        {
                            taskCounts++;
                            if (childTask.TState == 4)
                            {
                                childTaskCounts++;
                                continue;
                            }
                            if (childTask.TState == 3 || childTask.TState == 1)
                            {
                                doingCounts++;
                            }
                            if (taskClassA.TState == 2)
                            {
                                notStart++;
                            }
                        }


                        finishCounts = taskClassACounts + childTaskCounts;
                    }
                    builder.Append("<td>" + taskCounts + "</td>");

                    builder.Append("<td>" + finishCounts + "</td>");

                    builder.Append("<td>" + doingCounts + "</td>");

                    //计算进度值,第一步:统计当前项目下的任务总耗(任务和已完成子任务总耗时总和)工时T1.第二步,获取预计剩余工时 T2,  第三步: T1/(T1+T2)*100%
                    decimal doHours = 0;//已耗时
                    decimal predictHours = 0;//预估剩余工时

                    foreach (var task in pAllTask)
                    {
                        if (task.ParentId.ToInt() == 0) //主任务
                        {
                            List<Project_Task_Team> teamList = Project_Task_TeamBLL.GetList(t => t.Where(d => d.FK_TaskId == task.Id));
                            doHours += teamList.Sum(d => d.ConsumTime).ToDecimal();
                            predictHours += teamList.Sum(d => d.TheTime).ToDecimal();
                        }
                    }
                    try
                    {
                        percent = doHours / (doHours + predictHours) * 100;

                        percent = System.Decimal.Round(percent, 2);
                    }
                    catch { }
                    builder.Append("<td>" + notStart + "</td>");

                    builder.Append("<td title='" + percent + "%'><div role='progressbar' aria-valuenow='" + percent + "' aria-valuemin='0' aria-valuemax='100'style='width: " + percent + "%'title='" + percent + "%'><span class='sr-only'>(" + percent + ") Complete(success)</span>" + percent + "%" + "</td>");

                    builder.Append("<td>" + item.CreateTime.ToShortDate() + "</td>");

                    builder.Append("<td class='actions'>");

                    builder.Append(string.Format(CurrentBtnList28, item.Id));

                    builder.Append("</td>");
                    builder.Append("</tr>");

                }//循环项目结束
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
    }
}