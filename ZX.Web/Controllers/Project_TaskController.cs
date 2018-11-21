using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using Aspose.Cells;

namespace ZX.Web.Controllers
{
    public class Project_TaskController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            ViewBag.ProjectId = 0;
            ViewBag.ProjectName = "";
            DataList<Project_TaskModel> list = new DataList<Project_TaskModel>();
            try
            {
                ViewBag.ListUser = Sys_UserBLL.GetList();
                int pageIndex = Request.Form["pageIndex"].ToInt(1);
                int pageSize = Request.Form["pageSize"].ToInt(20);
                int status = Request.Form["status"].ToInt(-1);
                int tsuccess = Request.Form["tsuccess"].ToInt(UserId);
                int tcancel = Request.Form["tcancel"].ToInt(UserId);
                int tclosed = Request.Form["tclosed"].ToInt(UserId);
                int assigned = Request.Form["assigned"].ToInt(UserId);

                ViewBag.PageIndex = pageIndex;
                ViewBag.PageSize = pageSize;

                if (UserId == -1111)
                {
                    list = Project_TaskBLL.GetProject_TaskList("", pageIndex, pageSize, status, -1, -1, -1, -1, -1);
                }
                else
                {
                    list = Project_TaskBLL.GetProject_TaskList("", pageIndex, pageSize, status, UserId, tsuccess, tcancel, tclosed, assigned);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("加载异常", ex);
            }

            ViewBag.Count = list.TotalCount;
            //亮灯判断所需要的系统时间
            ViewBag.CurrentTime = DateTime.Now;


            return View("Project_TaskManage", list);
        }
        #endregion
        public ActionResult Count()
        {
            ViewBag.ProjectId = 0;
            ViewBag.ProjectName = "";
            DataList<Project_TaskModel> list = new DataList<Project_TaskModel>();
            try
            {
                ViewBag.ListUser = Sys_UserBLL.GetList();
                int pageIndex = Request.Form["pageIndex"].ToInt(1);
                int pageSize = Request.Form["pageSize"].ToInt(20);
                int status = Request.Form["status"].ToInt(-1);
                int tsuccess = Request.Form["tsuccess"].ToInt(UserId);
                int tcancel = Request.Form["tcancel"].ToInt(UserId);
                int tclosed = Request.Form["tclosed"].ToInt(UserId);
                int assigned = Request.Form["assigned"].ToInt(UserId);

                ViewBag.PageIndex = pageIndex;
                ViewBag.PageSize = pageSize;

                if (UserId == -1111)
                {
                    list = Project_TaskBLL.GetProject_TaskList("", pageIndex, pageSize, status, -1, -1, -1, -1, -1);
                }
                else
                {
                    list = Project_TaskBLL.GetProject_TaskList("", pageIndex, pageSize, status, UserId, tsuccess, tcancel, tclosed, assigned);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("加载异常", ex);
            }

            ViewBag.Count = list.TotalCount;
            //亮灯判断所需要的系统时间
            ViewBag.CurrentTime = DateTime.Now;


            return View("Project_TaskCount", list);
        }

        /// <summary>
        /// 任务管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Project_TaskManage()
        {
            ViewBag.ProjectId = 0;
            ViewBag.ProjectName = "";

            DataList<Project_TaskModel> list = new DataList<Project_TaskModel>();
            try
            {
                ViewBag.ListUser = Sys_UserBLL.GetList();
                int pageIndex = Request.Form["pageIndex"].ToInt(1);
                int pageSize = Request.Form["pageSize"].ToInt(20);
                int status = Request.Form["status"].ToInt(-1);
                int userId = -1;
                int tsuccess = -1;
                int tcancel = -1;
                int tclosed = -1;
                int assigned = -1;

                int selectType = Request.Form["selectType"].ToInt(-1);

                switch (selectType)
                {
                    case -1://所有任务
                        {
                            assigned = UserId;
                            tsuccess = UserId;
                            tcancel = UserId;
                            tclosed = UserId;
                            userId = UserId;
                            break;
                        }
                    case 1: //指派给我
                        {
                            userId = UserId;
                            assigned = UserId;
                            break;
                        }
                    case 2://由我创建
                        {
                            userId = UserId;
                            break;
                        }
                    case 3://由我完成
                        {
                            userId = UserId;
                            tsuccess = UserId;
                            break;
                        }
                    default:
                        break;
                }


                list = Project_TaskBLL.GetProject_TaskList("", pageIndex, pageSize, status, userId, tsuccess, tcancel, tclosed, assigned);
                //亮灯判断所需要的系统时间
                ViewBag.CurrentTime = DateTime.Now;

                ViewBag.PageIndex = pageIndex;
                ViewBag.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("加载异常", ex);
            }
            ViewBag.Count = list.TotalCount;
            return View("Project_TaskManage", list);
        }

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="mark">页面传入的分类标记</param>
        /// <returns></returns>

        public ActionResult GetProject_TaskList(int mark)//int projectId
        {
            //List<Project_Task> list = new List<Project_Task>();
            List<Project_TaskModel> list = new List<Project_TaskModel>();

            //int mark = Request["mark"].ToInt(0);
            int pageIndex = Request["pageIndex"].ToInt(1);
            int pageSize = Request["pageSize"].ToInt(10);

            string key = Request["key"] ?? "";

            AjaxResult result = new AjaxResult();
            try
            {
                ViewBag.ListUser = Sys_UserBLL.GetList();
                //根据传入参数进行分类查询 <主任务>
                switch (mark)
                {
                    case 0://所有任务
                        list = Project_TaskBLL.GetProject_TaskToList(key, pageIndex, pageSize);
                        break;
                    case 1://指派给我
                        list = Project_TaskBLL.GetProject_TaskList_C(key, pageIndex, pageSize, " and ParentId is null AND A.Assigned=" + UserId);
                        break;
                    case 2://由我创建
                        list = Project_TaskBLL.GetProject_TaskList_C(key, pageIndex, pageSize, " and ParentId is null AND A.CreateUserId=" + UserId);
                        break;
                    case 3://由我完成
                        list = Project_TaskBLL.GetProject_TaskList_C(key, pageIndex, pageSize, " and ParentId is null AND A.TSuccess=" + UserId);
                        break;
                    default://默认显示<指派给我>的任务
                        list = Project_TaskBLL.GetProject_TaskList_C(key, pageIndex, pageSize, " and ParentId is null AND A.Assigned=" + UserId);
                        break;
                }

            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("任务初始跳转异常", ex);
            }

            //Response.Write(@"<script type='text/javascript'>window.onload = function () {initPage();};</script>");
            ViewBag.mark = mark;
            //亮灯判断所需要的系统时间
            ViewBag.CurrentTime = DateTime.Now;


            return View("Project_TaskList", list);
            //return Json(result);
        }
        #endregion

        /// <summary>
        /// 获取项目下的任务
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult GetProjectTaskList(int? projectId)
        {
            List<Project_TaskModel> list = new List<Project_TaskModel>();
            try
            {
                projectId = projectId.ToInt();
                ViewBag.ProjectId = projectId;
                ViewBag.ListUser = Sys_UserBLL.GetList();
                Project pm = ProjectBLL.GetModel(projectId);
                ViewBag.ProjectName = pm.PName;
                //获取项目截止日期距离今天多少天
                TimeSpan ts = Convert.ToDateTime(pm.EndTime).Subtract(DateTime.Now.Date);
                int days = ts.Days;
                ViewBag.TimeSpanDays = days;

                list = Project_TaskBLL.GetProjectTaskListByProjectId(projectId.ToInt());

            }
            catch (Exception ex)
            {

                Log4Helper.WriteError("加载异常:" + ex.Message, ex);
            }

            //亮灯判断所需要的系统时间
            ViewBag.CurrentTime = DateTime.Now;

            return View("Project_TaskList", list);
        }

        #region 优先级
        private List<SelectListItem> GetListPriority()
        {
            List<SelectListItem> listPriority = new List<SelectListItem>() {
                new SelectListItem { Text = "1", Value = "1" },
                new SelectListItem { Text = "2", Value = "2" },
                new SelectListItem { Text = "3", Value = "3" },
                new SelectListItem { Text = "4", Value = "4" }
            };
            return listPriority;
        }

        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult Add(int? id, int projectId)
        {
            Project_Task model = new Project_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                int timeSpanDays = Request["timeSpanDays"].ToInt();
                //当天距离项目截止日期天数
                ViewBag.TimeSpanDays = timeSpanDays;
                //项目编号
                ViewBag.ProjectId = projectId;
                //优先级
                ViewBag.ListPriority = GetListPriority();

                List<Sys_User> uList = Sys_UserBLL.GetList(d => d.Where(l => l.Id != 1));

                //获取任务团队
                List<Project_Team> listAssignedTask = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == projectId));
                if (uList.Count() > 0)
                {
                    foreach (var item in listAssignedTask)
                    {
                        var userModel = uList.Where(l => l.Id == item.FK_UserId).FirstOrDefault();
                        teamUser.Add(new SelectListItem { Text = userModel.RealName, Value = userModel.Id.ToString() });
                    }
                }

                //指派给
                ViewBag.ListTeam = teamUser;
                //抄送给
                ViewBag.ListUser = uList;


                if (id > 0)
                {
                    model = Project_TaskBLL.GetModel(id.ToInt());
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        public ActionResult Readonly()
        {
            Project_Task model = new Project_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                int id = Request["Id"].ToInt();
                if (id > 0)
                {
                    model = Project_TaskBLL.GetModel(id);



                    //项目编号

                    Project pm = ProjectBLL.GetModel(model.FK_ProjectId);
                    if (!pm.IsNullOrEmpty())
                    {
                        ViewBag.ProjectName = pm.PName;
                    }
                    else
                    {
                        ViewBag.ProjectName = "";
                    }


                    //优先级
                    ViewBag.ListPriority = GetListPriority();

                    List<Sys_User> uList = Sys_UserBLL.GetList();

                    //获取任务团队
                    List<Project_Team> listAssignedTask = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == model.FK_ProjectId));
                    if (uList.Count() > 0)
                    {
                        foreach (var item in listAssignedTask)
                        {
                            var userModel = uList.Where(l => l.Id == item.FK_UserId).FirstOrDefault();
                            if (userModel != null)
                            {
                                teamUser.Add(new SelectListItem { Text = userModel.RealName, Value = userModel.Id.ToString() });
                            }
                        }
                    }

                    //指派给
                    ViewBag.ListTeam = teamUser;
                    //抄送给
                    ViewBag.ListUser = uList;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        #region 编辑
        /// <summary>
        /// 编辑跳转
        /// </summary>
        /// <param name="id">任务Id</param>
        /// <param name="projectId">任务所属项目编号</param>
        /// <returns></returns>
        public ActionResult Edit()
        {
            Project_Task model = new Project_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                int id = Request["Id"].ToInt();

                if (id > 0)
                {
                    model = Project_TaskBLL.GetModel(id);

                    //项目编号

                    Project pm = ProjectBLL.GetModel(model.FK_ProjectId);
                    if (!pm.IsNullOrEmpty())
                    {
                        ViewBag.ProjectName = pm.PName;
                    }
                    else
                    {
                        ViewBag.ProjectName = "";
                    }
                    //获取项目截止日期距离今天多少天
                    TimeSpan ts = Convert.ToDateTime(pm.EndTime).Subtract(DateTime.Now.Date);
                    int days = ts.Days;
                    ViewBag.TimeSpanDays = days;
                    //优先级
                    ViewBag.ListPriority = GetListPriority();

                    List<Sys_User> uList = Sys_UserBLL.GetList(d => d.Where(l => l.Id != 1));

                    //获取任务团队
                    List<Project_Team> listAssignedTask = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == model.FK_ProjectId));
                    if (uList.Count() > 0)
                    {
                        foreach (var item in listAssignedTask)
                        {
                            var userModel = uList.Where(l => l.Id == item.FK_UserId).FirstOrDefault();
                            if (userModel != null)
                            {
                                teamUser.Add(new SelectListItem { Text = userModel.RealName, Value = userModel.Id.ToString() });
                            }
                        }
                    }

                    //指派给
                    ViewBag.ListTeam = teamUser;
                    //抄送给
                    ViewBag.ListUser = uList;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="jsonDataTask"></param>
        /// <param name="jsonDataTaskTeam"></param>
        /// <returns></returns>
        public JsonResult EditSave()
        {

            AjaxResult rest = new AjaxResult();
            try
            {
                //任务
                Project_Task taskInfo = FormHelper.GetRequestForm<Project_Task>();

                //任务团队
                Project_Task_Team taskTeamInfo = Request.Form["tempDetail"].ToJsonDeserialize<Project_Task_Team>();

                int row = 0;

                //修改任务表基本属性
                taskInfo.UpdateAccount = UserAccount;
                taskInfo.UpdateTime = DateTime.Now;
                taskInfo.UpdateUserId = UserId;
                taskInfo.EditCount++;


                //如果状态为已完成,设置预计剩余工时为0 或者 剩余时间为0时 默认已完成
                if (taskInfo.TState == 4 || taskTeamInfo.TheTime <= 0)
                {
                    taskInfo.TState = 4;
                    taskTeamInfo.TheTime = 0;
                    taskInfo.TSuccess = UserId + "";
                    taskInfo.TSucTime = DateTime.Now;
                }

                row = Project_TaskBLL.UpdateModel(taskInfo);



                //修改任务团队表基本属性
                UpdateTeamTime(taskInfo.Id.ToInt(), taskInfo.TExpected.ToInt(), taskTeamInfo.TheTime.ToInt(), taskInfo.ParentId.ToInt(), taskTeamInfo.ConsumTime.ToString());


                //最后追加项目日志
                Project_Log logModel = new Project_Log()
                {
                    FK_TaskId = taskInfo.Id.ToInt(),
                    FK_ProjectId = taskInfo.FK_ProjectId,
                    StatusId = taskInfo.TState,
                    StatusName = "编辑任务",
                    Remark = "编辑",
                    CreateTime = DateTime.Now,
                    CreateAccount = UserAccount,
                    CreateUserId = UserId
                };
                Project_LogBLL.AddModel(logModel);

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

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            Project_Task model = FormHelper.GetRequestForm<Project_Task>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    model.EditCount++;
                    row = Project_TaskBLL.UpdateModel(model);

                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.TState = 2;
                    row = Project_TaskBLL.AddModel(model);
                }
                UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), model.TExpected.ToInt(), model.ParentId.ToInt(), null);
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
        /// 操作任务团队
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <param name="expectHours">最初预计耗时</param>
        /// <param name="theTime">预计剩余耗时</param>
        /// <param name="pid">任务父编号</param>
        /// <param name="consumTime">总耗时,传入控制默认不修改</param>
        public void UpdateTeamTime(int taskId, int expectHours, int theTime, int pid, string consumTime = null)
        {
            try
            {
                Project_Task_Team team = Project_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TaskId == taskId)).FirstOrDefault();

                if (team != null)
                {
                    team.ExpectHours = expectHours;
                    team.TheTime = theTime;
                    if (consumTime.IsNotNullOrEmpty())
                    {
                        team.ConsumTime = consumTime.ToInt();
                    }
                    team.UpdateAccount = UserAccount;
                    team.UpdateTime = DateTime.Now;
                    team.UpdateUserId = UserId;
                    Project_Task_TeamBLL.UpdateModel(team);
                }
                else
                {
                    team = new Project_Task_Team
                    {
                        FK_UserId = UserId,
                        FK_TaskId = taskId,
                        ExpectHours = expectHours.ToInt(),
                        TheTime = theTime.ToInt(),
                        ConsumTime = consumTime.ToInt(),
                        CreateAccount = UserAccount,
                        CreateTime = DateTime.Now,
                        CreateUserId = UserId
                    };
                    Project_Task_TeamBLL.AddModel(team);
                }
                //判断是否有父任务,有就更新父任务的消耗时间和预计剩余时间.
                if (pid > 0)
                {
                    //获取当前主任务下的子任务,并求和,然后更新
                    int totalConsumTime = 0;
                    int totalTheTime = 0;

                    List<Project_Task> childTaskList = Project_TaskBLL.GetList(d => d.Where(t => t.ParentId == pid));
                    foreach (var childItem in childTaskList)
                    {
                        //获取团队中所存耗时
                        int fk_TaskId = childItem.Id.ToInt();

                        Project_Task_Team childTeam = Project_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TaskId == fk_TaskId)).FirstOrDefault();
                        if (childTeam != null)
                        {
                            totalConsumTime += childTeam.ConsumTime.ToInt();
                            totalTheTime += childTeam.TheTime.ToInt();
                        }
                    }
                    //变更
                    Project_Task_Team mainTaskTeam = Project_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TaskId == pid)).FirstOrDefault();
                    mainTaskTeam.ConsumTime = totalConsumTime;
                    mainTaskTeam.TheTime = totalTheTime;
                    mainTaskTeam.UpdateAccount = UserAccount;
                    mainTaskTeam.UpdateTime = DateTime.Now;
                    mainTaskTeam.UpdateUserId = UserId;
                    Project_Task_TeamBLL.UpdateModel(mainTaskTeam);

                    Project_Task task = Project_TaskBLL.GetModel(pid);
                    //判断主任务是否已完成,如已完成,修改任务状态,加入日志
                    if (mainTaskTeam.TheTime <= 0)
                    {

                        task.TState = 4;
                        task.TSuccess = UserId + "";
                        task.TSucTime = DateTime.Now;
                        task.UpdateUserId = UserId;
                        task.UpdateAccount = UserAccount;
                        task.UpdateTime = DateTime.Now;
                        Project_TaskBLL.UpdateModel(task);

                        //最后追加项目日志
                        Project_Log logModel = new Project_Log()
                        {
                            FK_TaskId = task.Id.ToInt(),
                            FK_ProjectId = task.FK_ProjectId.ToInt(),
                            StatusId = task.TState,
                            StatusName = "任务操作",
                            Remark = "子任务预计剩余工时总计为0,系统默认完成任务",
                            CreateTime = DateTime.Now,
                            CreateAccount = UserAccount,
                            CreateUserId = UserId
                        };
                        Project_LogBLL.AddModel(logModel);
                    }
                    else
                    {
                        if (task.TState == 4)
                        {
                            task.TState = 3;
                            Project_TaskBLL.UpdateModel(task);
                            //最后追加项目日志
                            Project_Log logModel = new Project_Log()
                            {
                                FK_TaskId = task.Id.ToInt(),
                                FK_ProjectId = task.FK_ProjectId.ToInt(),
                                StatusId = task.TState,
                                StatusName = "任务操作",
                                Remark = "预计剩余非0,系统默认修改已完成任务状态为进行中",
                                CreateTime = DateTime.Now,
                                CreateAccount = UserAccount,
                                CreateUserId = UserId
                            };
                            Project_LogBLL.AddModel(logModel);
                        }
                    }
                }
            }
            catch { }
        }

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
                int row = Project_TaskBLL.DelModelById(id);
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

        #region 工时
        /// <summary>
        /// 工时
        /// </summary>
        /// <param name="id">任务Id</param>
        /// <param name="viewName">视图名称</param>
        /// <returns></returns>
        public ActionResult WorkTime()
        {
            Project_Task model = new Project_Task();
            try
            {
                //int fk_TaskId, int fk_ProjectId, decimal expectHours, string viewName
                model.Id = Request["taskId"].ToInt();
                if (model.Id > 0)
                {
                    model = Project_TaskBLL.GetModel(model.Id);
                }
                decimal theTime = Request["theTime"].ToDecimal();
                decimal consumTime = Request["consumTime"].ToDecimal();
                ViewBag.ConsumTime = consumTime;
                ViewBag.TheTime = theTime;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 工时保存
        /// <summary>
        /// 工时
        /// </summary>
        /// <param name="FK_TaskId">任务Id</param>
        /// <param name="FK_ProjectId">项目Id</param>
        /// <param name="ExpectHours">最初预计</param>
        /// <param name="ConsumTime">个人消耗</param>
        /// <param name="TheTime">预计耗时</param>
        /// <param name="logText">描述</param>
        /// <returns></returns>
        public JsonResult WorkTimeSave()
        {
            Project_Task model = FormHelper.GetRequestForm<Project_Task>();
            AjaxResult rest = new AjaxResult();
            try
            {
                decimal consumTime = Request["consumTime"].ToDecimal();
                decimal theTime = Request["theTime"].ToDecimal();
                string logText = Request["LogRemark"] ?? "";

                int row = 0;
                if (model.Id > 0)
                {
                    //修改任务表基本属性
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;

                    if (theTime <= 0)
                    {
                        model.TState = 4;
                        model.TSuccess = UserId + "";
                        model.TSucTime = DateTime.Now;
                    }
                    else
                    {
                        model.TState = 3;
                        model.TSuccess = "";
                    }

                    row = Project_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());


                    //最后追加项目日志
                    Project_Log logModel = new Project_Log()
                    {
                        FK_TaskId = model.Id.ToInt(),
                        FK_ProjectId = model.FK_ProjectId.ToInt(),
                        StatusId = model.TState,
                        StatusName = "任务工时",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Project_LogBLL.AddModel(logModel);

                    rest.Message = "保存成功";
                    rest.Code = ResultCode.Succeed;
                }
                else
                {
                    rest.Message = "保存失败,任务获取失败.";
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

        #region 开始
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="id">任务Id</param>
        /// <returns></returns>
        public ActionResult Run()
        {
            Project_Task model = new Project_Task();
            try
            {
                model.Id = Request["taskId"].ToInt();
                decimal theTime = Request["theTime"].ToDecimal();
                decimal consumTime = Request["consumTime"].ToDecimal();

                ViewBag.ConsumTime = consumTime;
                ViewBag.TheTime = theTime;
                ViewBag.TExpected = model.TExpected;

                if (model.Id > 0)
                {
                    model = Project_TaskBLL.GetModel(model.Id);
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 开始保存
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public JsonResult RunSave()
        {
            Project_Task model = FormHelper.GetRequestForm<Project_Task>();
            AjaxResult rest = new AjaxResult();
            try
            {
                DateTime actualStart = Request["actualStart"].ToDate();
                decimal consumTime = Request["consumTime"].ToDecimal();
                decimal theTime = Request["theTime"].ToDecimal();
                string logText = Request["LogRemark"] ?? "";

                int row = 0;
                if (model.Id > 0)
                {
                    //修改任务表基本属性
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;

                    if (theTime <= 0)
                    {
                        model.TState = 4;
                        model.TSuccess = UserId + "";
                        model.TSucTime = DateTime.Now;
                    }
                    else
                    {
                        model.TState = 3;
                        model.TSuccess = "";
                    }

                    row = Project_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());

                    //最后追加项目日志
                    Project_Log logModel = new Project_Log()
                    {
                        FK_TaskId = model.Id.ToInt(),
                        FK_ProjectId = model.FK_ProjectId,
                        StatusId = model.TState,
                        ActualStart = actualStart,
                        StatusName = "开始任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Project_LogBLL.AddModel(logModel);

                    rest.Message = "保存成功";
                    rest.Code = ResultCode.Succeed;
                }
                else
                {
                    rest.Message = "保存失败,任务获取失败.";
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

        #region 完成跳转
        public ActionResult Finish()
        {
            Project_Task model = new Project_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                //int fk_TaskId, int fk_ProjectId, decimal expectHours, string viewName
                model.Id = Request["taskId"].ToInt();
                if (model.Id > 0)
                {
                    model = Project_TaskBLL.GetModel(model.Id);
                }
                decimal theTime = Request["theTime"].ToDecimal();
                decimal consumTime = Request["consumTime"].ToDecimal();
                ViewBag.ConsumTime = consumTime;
                ViewBag.TheTime = theTime;

                List<Sys_User> uList = Sys_UserBLL.GetList();

                //获取任务团队
                List<Project_Team> listAssignedTask = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == model.FK_ProjectId));
                if (uList.Count() > 0)
                {
                    foreach (var item in listAssignedTask)
                    {
                        var userModel = uList.Where(l => l.Id == item.FK_UserId).FirstOrDefault();
                        if (userModel != null)
                        {
                            teamUser.Add(new SelectListItem { Text = userModel.RealName, Value = userModel.Id.ToString() });
                        }
                    }
                }
                //指派给
                ViewBag.ListTeam = teamUser;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 完成保存
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="FK_TaskId">任务Id</param>
        /// <param name="FK_ProjectId">项目Id</param>
        /// <param name="ExpectHours">最初预计</param>
        /// <param name="ConsumTime">个人消耗</param>
        /// <param name="TheTime">预计耗时</param>
        /// <param name="logText">描述</param>
        /// <returns></returns>
        public JsonResult FinishSave()
        {
            Project_Task model = FormHelper.GetRequestForm<Project_Task>();
            AjaxResult rest = new AjaxResult();
            try
            {
                decimal consumTime = Request["consumTime"].ToDecimal();
                decimal theTime = Request["theTime"].ToDecimal();
                string logText = Request["LogRemark"] ?? "";

                int row = 0;
                if (model.Id > 0)
                {
                    //修改任务表基本属性
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    model.TState = 4; //完成
                    theTime = 0;// 预计剩余清零
                    model.TSuccess = UserId + "";

                    row = Project_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());

                    //最后追加项目日志
                    Project_Log logModel = new Project_Log()
                    {
                        FK_TaskId = model.Id.ToInt(),
                        FK_ProjectId = model.FK_ProjectId,
                        StatusId = model.TState,
                        StatusName = "完成任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Project_LogBLL.AddModel(logModel);

                    rest.Message = "保存成功";
                    rest.Code = ResultCode.Succeed;
                }
                else
                {
                    rest.Message = "保存失败,任务获取失败.";
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

        #region 指派跳转
        public ActionResult AppointTask()
        {
            Project_Task model = new Project_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                //int fk_TaskId, int fk_ProjectId, decimal expectHours, string viewName
                model.Id = Request["taskId"].ToInt();
                decimal theTime = Request["theTime"].ToDecimal();
                decimal consumTime = Request["consumTime"].ToDecimal();

                ViewBag.ConsumTime = consumTime;
                ViewBag.TheTime = theTime;

                if (model.Id > 0)
                {
                    model = Project_TaskBLL.GetModel(model.Id);
                }
                List<Sys_User> uList = Sys_UserBLL.GetList(d => d.Where(l => l.Id != 1));

                //获取任务团队
                List<Project_Team> listAssignedTask = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == model.FK_ProjectId));
                if (uList.Count() > 0)
                {
                    foreach (var item in listAssignedTask)
                    {
                        var userModel = uList.Where(l => l.Id == item.FK_UserId).FirstOrDefault();
                        if (userModel != null)
                        {
                            teamUser.Add(new SelectListItem { Text = userModel.RealName, Value = userModel.Id.ToString() });
                        }
                    }
                }
                //指派给
                ViewBag.ListTeam = teamUser;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 指派保存
        /// <summary>
        /// 指派
        /// </summary>
        /// <returns></returns>
        public JsonResult AppointSave()
        {
            Project_Task model = FormHelper.GetRequestForm<Project_Task>();
            AjaxResult rest = new AjaxResult();
            try
            {
                decimal consumTime = Request["consumTime"].ToDecimal();
                decimal theTime = Request["theTime"].ToDecimal();
                string logText = Request["LogRemark"] ?? "";

                int row = 0;
                if (model.Id > 0)
                {

                    //修改任务表基本属性
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;



                    if (theTime <= 0)
                    {
                        model.TState = 4;
                        model.TSuccess = UserId + "";
                        model.TSucTime = DateTime.Now;
                    }
                    else
                    {
                        model.TState = 3;
                        model.TSuccess = "";
                    }
                    row = Project_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());


                    //最后追加项目日志
                    Project_Log logModel = new Project_Log()
                    {
                        FK_TaskId = model.Id.ToInt(),
                        FK_ProjectId = model.FK_ProjectId.ToInt(),
                        StatusId = model.TState,
                        StatusName = "指派任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Project_LogBLL.AddModel(logModel);

                    rest.Message = "保存成功";
                    rest.Code = ResultCode.Succeed;
                }
                else
                {
                    rest.Message = "保存失败,任务获取失败.";
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

        #region 关闭
        public ActionResult Close()
        {
            Project_Task model = new Project_Task();
            try
            {
                int id = Request["taskId"].ToInt();
                model = Project_TaskBLL.GetModel(id);

                ViewBag.ExpectHours = model.TExpected;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        public JsonResult CloseSave()
        {
            Project_Task model = FormHelper.GetRequestForm<Project_Task>();
            AjaxResult rest = new AjaxResult();
            try
            {
                decimal consumTime = Request["consumTime"].ToDecimal();
                decimal theTime = Request["theTime"].ToDecimal();
                string logText = Request["LogRemark"];
                int row = 0;
                if (model.Id > 0)
                {
                    //修改任务表基本属性
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    model.TClosed = UserId.ToString();
                    model.TClosedTime = DateTime.Now;
                    model.TClosedWhy = logText;

                    model.TState = 6;

                    row = Project_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());

                    //最后追加项目日志
                    Project_Log logModel = new Project_Log()
                    {
                        FK_TaskId = model.Id.ToInt(),
                        FK_ProjectId = model.FK_ProjectId,
                        StatusId = model.TState,
                        StatusName = "关闭任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Project_LogBLL.AddModel(logModel);

                    rest.Message = "保存成功";
                    rest.Code = ResultCode.Succeed;
                }
                else
                {
                    rest.Message = "保存失败,任务获取失败.";
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

        #region 子任务
        public ActionResult Children()
        {
            Project_Task parentModel = new Project_Task();
            List<Project_Task> taskList = new List<Project_Task>();
            try
            {
                int id = Request["taskId"].ToInt();
                if (id > 0)
                {
                    parentModel = Project_TaskBLL.GetModel(id);
                    ViewBag.ParentId = parentModel.Id;
                    ViewBag.FK_ProjectId = parentModel.FK_ProjectId;
                    //优先级
                    ViewBag.ListPriority = GetListPriority();
                    //获取任务团队
                    List<SelectListItem> teamUser = new List<SelectListItem>();
                    List<Sys_User> uList = Sys_UserBLL.GetList(d => d.Where(l => l.Id != 1));
                    List<Project_Team> listAssignedTask = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == parentModel.FK_ProjectId));
                    if (uList.Count() > 0)
                    {
                        foreach (var item in listAssignedTask)
                        {
                            var userModel = uList.Where(l => l.Id == item.FK_UserId).FirstOrDefault();
                            if (userModel != null)
                            {
                                teamUser.Add(new SelectListItem { Text = userModel.RealName, Value = userModel.Id.ToString() });
                            }
                        }
                    }

                    //指派给
                    ViewBag.ListTeam = teamUser;
                    taskList = Project_TaskBLL.GetList(d => d.Where(t => t.ParentId == parentModel.Id));
                    ViewBag.ChildTaskList = taskList;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(taskList);
        }
        public JsonResult ChildrenSave(string jsonData, int pid)
        {
            AjaxResult rest = new AjaxResult();

            try
            {
                List<Project_Task> list = jsonData.ToJsonDeserialize<List<Project_Task>>();
                List<Project_Task> standardList = new List<Project_Task>();
                foreach (var item in list)
                {
                    item.CreateUserId = UserId;
                    item.CreateTime = DateTime.Now;
                    item.CreateAccount = UserAccount;
                    item.TState = 2;

                    int id = Project_TaskBLL.AddModel(item);

                    //修改任务团队表基本属性
                    UpdateTeamTime(id, item.TExpected.ToInt(), item.TExpected.ToInt(), item.ParentId.ToInt(), "0");

                    //最后追加项目日志
                    Project_Log logModel = new Project_Log()
                    {
                        FK_TaskId = item.Id.ToInt(),
                        FK_ProjectId = item.FK_ProjectId,
                        StatusId = item.TState,
                        StatusName = "创建",
                        Remark = "子任务创建",
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Project_LogBLL.AddModel(logModel);
                }
                if (list.Count > 0)
                {
                    //
                    //最后追加主任务日志
                    //最后追加主任务日志
                    Project_Log logModel = new Project_Log()
                    {
                        FK_TaskId = pid.ToInt(),
                        StatusName = "新增子任务",
                        Remark = "新增子任务",
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
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


        /// <summary>
        /// 个人空间-个人任务跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalTask()
        {
            ViewBag.CurrentAction = "/Project_Task/GetPersonalTask"; //  页面的便捷切换需要用到,例如:指派给我
            return View("PersonalTask");
        }

        public JsonResult GetPersonalTask()
        {
            AjaxResult result = new AjaxResult();
            DataList<Project_TaskModel> list = new DataList<Project_TaskModel>();
            try
            {
                List<Sys_User> userList = Sys_UserBLL.GetList();
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";
                int userId = -1;
                int tsuccess = -1;
                int assigned = -1;

                int selectType = Request.Form["selectType"].ToInt(-1);

                switch (selectType)
                {
                    case 1: //指派给我
                        {
                            assigned = UserId;
                            break;
                        }
                    case 2://由我创建
                        {
                            userId = UserId;
                            break;
                        }
                    case 3://由我完成
                        {
                            tsuccess = UserId;
                            break;
                        }
                    default:
                        break;
                }

                //获取主任务 作为分页用途
                list = Project_TaskBLL.GetPersonalTask(name, pageIndex, pageSize, userId, tsuccess, assigned, 1);

                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    int tableRowNumber = ((pageIndex - 1) * pageSize + index++);

                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td class='text-left'><label class='checkbox-inline'><input type='checkbox' name='taskIDList[]' value='" + item.Id + "' />" + tableRowNumber + " </label></td>");
                    string projectName = "";//所属项目名

                    if (item.ProjectName.Length > 5)
                    {
                        projectName = item.ProjectName.Substring(0, 5) + "...";
                    }
                    else
                    {
                        projectName = item.ProjectName;
                    }

                    builder.Append("<td class='text-left' title='" + item.ProjectName + "'>" + projectName + "</td>");
                    builder.Append("<td><span class='active pri pri-" + item.Priority + "'>" + item.Priority + "</span></td>");

                    decimal doneHours = 0;//已耗时
                    decimal predictHours = 0;//预估剩余工时
                    decimal totalTExpected = 0; //获取项目总共最初预计，用于亮灯控制判断
                    decimal percent = 0;//百分比

                    Project_Task_Team team = new Project_Task_Team();
                    try
                    {
                        team = Project_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TaskId == item.Id)).ToList().FirstOrDefault();
                        doneHours = team.ConsumTime.ToInt();
                        predictHours = team.TheTime.ToInt();
                    }
                    catch { }

                    try
                    {
                        percent = doneHours / (doneHours + predictHours) * 100;
                        percent = System.Decimal.Round(percent, 2);
                    }
                    catch { }

                    totalTExpected += item.TExpected.ToInt();




                    builder.Append("<td class='text-left' title='" + item.TName + "'>");

                    //任务结束前一天 亮黄灯控制
                    TimeSpan ts = Convert.ToDateTime(item.PEndTime).Subtract(Convert.ToDateTime(DateTime.Now.ToShortTimeString().ToString()));

                    if (ts.Days == 1 && item.TState != 4)
                    {
                        builder.Append("<div class=\"circular Orange\" title=\"仅剩一天时间\"></div>");
                    }
                    //超时
                    if (ts.Days < 0 && item.TState != 4)
                    {
                        builder.Append("<div class=\"circular red\" title=\"已超时" + (ts.Days * -1) + "天。\"></div>");
                    }


                    ////进度小于90% 亮灯控制
                    //if (percent < 90 && item.TState != 4)
                    //{
                    //    builder.Append("<div class=\"circular green\" title=\"进度已达"+ percent + "% \"></div>");
                    //}
                    ////90%<进度<100% 亮灯控制
                    //if (percent >= 90 && percent < 100 && item.TState != 4)
                    //{
                    //    builder.Append("<div class=\"circular Orange\" title=\"进度已达" + percent + " % \"></div>");
                    //}
                    ////进度已经所消耗时间超出前期预计总耗时 亮灯控制
                    //if (doneHours > totalTExpected)
                    //{
                    //    builder.Append("<div class=\"circular red\" title=\"总耗时已超出最初预计" + (doneHours - totalTExpected) + "工时。\"></div>");
                    //}

                    builder.Append(item.TName);

                    //获取子任务
                    List<Project_TaskModel> childList = Project_TaskBLL.GetProjectTaskListByParentId(item.Id.ToInt());

                    if (childList.Count > 0)
                    {
                        builder.Append("<span class='task-toggle'>&nbsp;&nbsp;<i class='icon icon-minus'></i>&nbsp;&nbsp;</span>");
                    }
                    builder.Append("</td>");
                    string asTimeStr = "0000-00-00";
                    string assignedRealName = "";
                    try
                    {
                        if (item.AsTime.IsNotNullOrEmpty())
                        {
                            asTimeStr = item.AsTime.ToShortDate();
                        }

                        if (item.Assigned.ToInt() > 0)
                        {
                            assignedRealName = userList.Where(l => l.Id == item.Assigned.ToInt()).FirstOrDefault().RealName;
                        }
                    }
                    catch { }
                    builder.Append("<td>" + asTimeStr + "</td>");
                    builder.Append("<td>" + assignedRealName + "</td>");

                    switch (item.TState)
                    {
                        case 1:
                            {
                                builder.Append("<td class='wait'>未完成</td>");
                                break;
                            }
                        case 2:
                            {
                                builder.Append("<td class='wait'>未开始</td>");

                                break;
                            }
                        case 3:
                            {
                                builder.Append("<td class='doing'>进行中</td>");

                                break;
                            }
                        case 4:
                            {
                                builder.Append("<td class='done'>已完成</td>");
                                break;
                            }
                        case 5:
                            {
                                builder.Append("<td class='cancel'>已取消</td>");
                                break;
                            }
                        case 6:
                            {
                                builder.Append("<td class='closed'>已关闭</td>");
                                break;
                            }

                        default:
                            builder.Append("<td>未知</td>");
                            break;
                    }
                    // 任务
                    //按钮权限可见性控制
                    List<Sys_Button> btnList = RoleButtonList;
                    List<Project_Task_Team> taskTeamList = new List<Project_Task_Team>();

                    string pCreateTime = "0000-00-00";
                    if (item.CreateTime.IsNotNullOrEmpty())
                    {
                        pCreateTime = item.CreateTime.ToShortDate();
                    }
                    builder.Append("<td>" + pCreateTime + "</td>");

                    #region 通过判断是否有子任务进行sum运算总耗时和总预计耗时 *此机制主要看任务执行中，时候每做相应操作就更新每一条任务1:1关系的task_team 表中记录。如果不更新，将采用此方法统计
                    //获取子任务
                    //List<Project_TaskModel> childList = new List<Project_TaskModel>();

                    //childList = list.Where(l => l.ParentId == item.Id).ToList();

                    //if (childList.Count <= 0)
                    //{
                    //    taskTeamList = Project_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TaskId == item.Id));
                    //    consumTime = taskTeamList.Sum(d => d.ConsumTime).ToInt();
                    //    tExpected = taskTeamList.Sum(d => d.TheTime).ToInt();
                    //}
                    //else
                    //{
                    //    StringBuilder childIds = new StringBuilder();

                    //    foreach (var childItem in childList)
                    //    {
                    //        childIds.Append(childItem.Id);
                    //        if (childItem != childList.Last())
                    //        {
                    //            childIds.Append(",");
                    //        }
                    //    }
                    //    taskTeamList = Project_Task_TeamBLL.GetList(" FK_TaskId in(" + childIds + ")");
                    //    consumTime = taskTeamList.Sum(d => d.ConsumTime).ToInt();
                    //    tExpected = taskTeamList.Sum(d => d.TheTime).ToInt();
                    //}
                    #endregion
                    builder.Append("<td>" + item.ConsumTime.ToInt() + "</td>");
                    builder.Append("<td>" + item.TheTime.ToInt() + "</td>");

                    builder.Append("<td>");
                    builder.Append("<div class=\"progress progress-striped\" style=\"margin: 0\">");
                    builder.Append("<div class=\"progress-bar\" role=\"progressbar\" aria-valuenow=\"" + percent + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + percent + "%\")\" title=\"" + percent + "%\")>");
                    builder.Append("<span class=\"sr-only\">" + percent + "% Complete(success)</span>");
                    builder.Append("</div>");
                    builder.Append("</div>");
                    builder.Append("</td>");
                    builder.Append("<td class='w-290px'>");
                    //表行按钮组
                    List<Sys_Button> currentBtnList28 = new List<Sys_Button>();

                    currentBtnList28 = btnList.Where(l => l.BGroup == 28 && l.BIsUse == 0).OrderBy(l => l.BSort).ToList();
                    if (childList.Count > 0)
                    {
                        if (currentBtnList28.Count > 0)
                        {
                            foreach (var btnItem in currentBtnList28)
                            {
                                if (btnItem.BName.Contains("工时") || btnItem.BName.Contains("指派") || btnItem.BName.Contains("开始") || btnItem.BName.Contains("完成") || btnItem.BName.Contains("关闭"))
                                {
                                    builder.Append("<a href = 'javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                    continue;
                                }
                                builder.Append("<a href = 'javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + item.Id + "," + item.ConsumTime.ToInt() + "," + item.TheTime.ToInt() + "," + "'" + item.TName + "'\" class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                            }
                        }
                    }
                    else
                    {
                        if (currentBtnList28.Count > 0)
                        {
                            foreach (var btnItem in currentBtnList28)
                            {
                                //如果是进行中,那么禁用开始 如果已完成,禁用完成 
                                if ((item.TState == 3 && btnItem.BName.Contains("开始")) || (item.TState == 4 && btnItem.BName.Contains("完成")) || (item.TState == 6 && btnItem.BName.Contains("关闭")))
                                {
                                    builder.Append("<a href = 'javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                    continue;
                                }
                                builder.Append("<a href = 'javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + item.Id + "," + item.ConsumTime.ToInt() + "," + item.TheTime.ToInt() + "," + "'" + item.TName + "'\" class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                            }
                        }
                    }
                    builder.Append("</td>");
                    builder.Append("</tr>");
                    //展示子记录
                    if (childList.Count > 0)
                    {
                        builder.Append("<tr class='tr-child' style='display:table-row;'>");
                        builder.Append("<td colspan='12'>");
                        builder.Append("<table class='table table-hover table-striped table-bordered tablesorter table-data table-fixed'>");
                        int j = 1;
                        foreach (var childItem in childList)
                        {
                            decimal childDoneHours = childItem.ConsumTime;//已耗时
                            decimal childPredictHours = childItem.TheTime;//预估剩余工时

                            decimal childPercent = 0;//百分比

                            try
                            {
                                childPercent = childDoneHours / (childDoneHours + childPredictHours) * 100;
                                childPercent = System.Decimal.Round(childPercent, 2);
                            }
                            catch { }

                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td class='w-60px' style='font-size: 12.8;'>" + tableRowNumber + "-" + j + "</td>");
                            builder.Append("<td class='text-left w-100px' title='" + item.ProjectName + "' style='font-size: 12.8;'>" + projectName + "</td>");

                            builder.Append("<td class='w-40px' style='font-size: 12.8;'><span class='active pri pri-" + childItem.Priority + "'>" + childItem.Priority + "</span></td>");

                            builder.Append("<td class='text-left' title='" + childItem.TName + "' style='font-size: 12.8;'>");

                            //进度小于90% 亮灯控制
                            if (childPercent < 90 && childItem.TState != 4)
                            {
                                builder.Append("<div class=\"circular green\" title=\"进度已达" + childPercent + "% \"></div>");
                            }
                            //90%<进度<100% 亮灯控制
                            if (childPercent >= 90 && childPercent < 100 && childItem.TState != 4)
                            {
                                builder.Append("<div class=\"circular Orange\" title=\"进度已达" + childPercent + "% \"></div>");
                            }
                            //进度已经所消耗时间超出前期预计总耗时 亮灯控制
                            if (childDoneHours > childItem.TExpected)
                            {
                                builder.Append("<div class=\"circular red\" title=\"总耗时已超出最初预计" + (childDoneHours - childItem.TExpected) + "工时。\"></div>");
                            }
                            builder.Append("<span class='label'>子</span>");
                            builder.Append(childItem.TName);
                            builder.Append("</td>");


                            string childAsTimeStr = "0000-00-00";
                            string childAssignedRealName = "";
                            try
                            {
                                if (childItem.AsTime.IsNotNullOrEmpty())
                                {
                                    childAsTimeStr = childItem.AsTime.ToShortDate();
                                }

                                if (item.Assigned.ToInt() > 0)
                                {
                                    childAssignedRealName = userList.Where(l => l.Id == childItem.Assigned.ToInt()).FirstOrDefault().RealName;
                                }
                            }
                            catch { }
                            builder.Append("<td class='w-100px' style='font-size: 12.8;'>" + childAsTimeStr + "</td>");
                            builder.Append("<td class='w-80px' style='font-size: 12.8;'>" + childAssignedRealName + "</td>");

                            switch (childItem.TState)
                            {
                                case 1:
                                    {
                                        builder.Append("<td class='w-90px wait' style='font-size: 12.8;'>未完成</td>");
                                        break;
                                    }
                                case 2:
                                    {
                                        builder.Append("<td class='w-90px wait' style='font-size: 12.8;'>未开始</td>");

                                        break;
                                    }
                                case 3:
                                    {
                                        builder.Append("<td class='w-90px doing' style='font-size: 12.8;'>进行中</td>");

                                        break;
                                    }
                                case 4:
                                    {
                                        builder.Append("<td class='w-90px done' style='font-size: 12.8;'>已完成</td>");
                                        break;
                                    }
                                case 5:
                                    {
                                        builder.Append("<td class='w-90px cancel' style='font-size: 12.8;'>已取消</td>");
                                        break;
                                    }
                                case 6:
                                    {
                                        builder.Append("<td class='w-90px closed' style='font-size: 12.8;'>已关闭</td>");
                                        break;
                                    }

                                default:
                                    builder.Append("<td class='w-90px wait' style='font-size: 12.8;'>未知</td>");
                                    break;
                            }

                            string childCreateTime = "0000-00-00";
                            if (childItem.CreateTime.IsNotNullOrEmpty())
                            {
                                childCreateTime = childItem.CreateTime.ToShortDate();
                            }
                            builder.Append("<td class='w-90px' style='font-size: 12.8;'>" + childCreateTime + "</td>");
                            builder.Append("<td class='w-80px' style='font-size: 12.8;'>" + childItem.ConsumTime + "</td>");
                            builder.Append("<td class='w-100px' style='font-size: 12.8;'>" + childItem.TheTime + "</td>");

                            builder.Append("<td class='w-100px' style='font-size: 12.8;'>");
                            builder.Append("<div class=\"progress progress-striped\" style=\"margin: 0\">");
                            builder.Append("<div class=\"progress-bar\" role=\"progressbar\" aria-valuenow=\"" + childPercent + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + childPercent + "%\")\" title=\"" + childPercent + "%\")>");
                            builder.Append("<span class=\"sr-only\">" + childPercent + "% Complete(success)</span>");
                            builder.Append("</div>");
                            builder.Append("</div>");
                            builder.Append("</td>");

                            builder.Append("<td class='w-290px'>");


                            //按钮权限可见性控制

                            //按钮权限可见性控制

                            if (currentBtnList28.Count > 0)
                            {
                                foreach (var btnItem in currentBtnList28)
                                {
                                    //如果是进行中,那么禁用开始 如果已完成,禁用完成 如果是关闭,禁用关闭
                                    if ((childItem.TState == 3 && btnItem.BName.Contains("开始")) || (childItem.TState == 4 && btnItem.BName.Contains("完成")) || (childItem.TState == 6 && btnItem.BName.Contains("关闭")))
                                    {
                                        builder.Append(" <a href='javascript:;' class=\"a_btn_menu\" disabled>" + btnItem.BName + "</a>");
                                        continue;
                                    }
                                    if (btnItem.BName.Contains("子任务"))
                                    {
                                        continue;
                                    }
                                    builder.Append("<a href='javascript:;' onclick=\"" + btnItem.BOptionFun + "\" val=\"" + childItem.Id + "," + childItem.ConsumTime + "," + childItem.TheTime + ", '" + childItem.TName + "'\"  class=\"a_btn_menu\">" + btnItem.BName + "</a>");
                                }
                            }

                            builder.Append("</td>");
                            builder.Append("</tr>");
                            j++;
                        }
                        builder.Append("</table>");
                        builder.Append("</td>");
                        builder.Append("</tr>");
                    }//是否循环子任务结束
                }//for 主任务结束
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

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public ActionResult OutExcel()
        {
            List<ProjecTask_Out> list = new List<ProjecTask_Out>();
            int fk_CompanyPositionId = PositionId.ToInt();
            string beginTime = Request["beginTime"] ?? "";
            string endTime = Request["endTime"] ?? "";
            int status = Request["status"].ToInt(-1);
            //认证
            Aspose.Cells.License lic = new Aspose.Cells.License();
            Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(License));
            lic.SetLicense(stream);
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/Temp/Task_Out.xlsx"));

            list = Project_TaskBLL.getExcelList();
            foreach (var item in list)
            {
                switch (item.TState)
                {
                    case 1:
                        {
                            item.strTState = "未完成";
                            break;
                        }
                    case 2:
                        {
                            item.strTState = "未开始";
                            break;
                        }
                    case 3:
                        {
                            item.strTState = "进行中";
                            break;
                        }
                    case 4:
                        {
                            item.strTState = "已完成";
                            break;
                        }
                    case 5:
                        {
                            item.strTState = "已取消";
                            break;
                        }
                    case 6:
                        {
                            item.strTState = "已关闭";
                            break;
                        }

                    default:
                        item.strTState = "未知";
                        break;
                }
            }
            designer.SetDataSource("model", list);
            designer.Process();
            designer.Save(System.Web.HttpUtility.UrlEncode("任务汇总" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", System.Text.Encoding.UTF8),
            SaveType.OpenInExcel,
            FileFormatType.Excel2003,
            System.Web.HttpContext.Current.Response);
            Response.Flush();
            Response.Close();
            designer = null;
            return null;
        }
        #endregion
    }
}