using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;

namespace ZX.Web.Controllers
{
    public class Temporary_TaskController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获取临时任务列表
            List<Temporary_Task_Block> userBlockList = Temporary_Task_BlockBLL.GetList(d => d.Where(t => t.CreateUserId == UserId));
            ViewBag.userBlockList = userBlockList;

            //获取当前首页对应菜单Id,供按钮权限控制
            Sys_Menu sys_Menu = new Sys_Menu();
            List<Sys_Menu> menuList = Sys_MenuBLL.GetList(d => d.Where(t => t.MUrl.Contains("/Temporary_Task/Index") && t.MParentId != 0));
            if (menuList.Count > 0)
            {
                sys_Menu = menuList.FirstOrDefault();
            }
            ViewBag.SecondMenuId = sys_Menu.Id;
            GetButtonListByMenuId(sys_Menu.Id.ToInt());
            ViewBag.RealName = RealName;
            return View();
        }
        #endregion


        #region 任务管理跳转
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Temporary_TaskList()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTemporary_TaskList()
        {
            AjaxResult result = new AjaxResult();
            DataList<Temporary_TaskModel> list = new DataList<Temporary_TaskModel>();
            try
            {
                List<Sys_User> userList = Sys_UserBLL.GetList(); //用户真实名显示所需

                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";
                int userId = -1;
                int tsuccess = -1;
                int assigned = -1;
                int isParent = -1;
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
                isParent = 1;
                //获取主任务 作为分页用途 isParent
                list = Temporary_TaskBLL.GetTemporary_TaskList(name, pageIndex, pageSize, userId, tsuccess, assigned, isParent);

                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    int tableRowNumber = ((pageIndex - 1) * pageSize + index++);

                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td class='text-left'><label class='checkbox-inline'>" + tableRowNumber + " </label></td>");
                    builder.Append("<td><span class='active pri pri-" + item.Priority + "'>" + item.Priority + "</span></td>");
                    decimal doneHours = 0;//已耗时
                    decimal predictHours = 0;//预估剩余工时
                    decimal totalTExpected = 0; //获取项目总共最初预计，用于亮灯控制判断
                    decimal percent = 0;//百分比

                    Temporary_Task_Team team = new Temporary_Task_Team();
                    try
                    {
                        team = Temporary_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TemporaryTaskId == item.Id)).ToList().FirstOrDefault();
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
                    //进度到了90% 亮灯控制
                    if (percent >= 90 && percent < 100 && item.TState != 4)
                    {
                        builder.Append("<div class=\"circular green\" title=\"进度已达90 % \"></div>");
                    }
                    //进度已经所消耗时间超出前期预计总耗时 亮灯控制
                    if (doneHours > totalTExpected)
                    {
                        builder.Append("<div class=\"circular red\" title=\"总耗时已超出最初预计" + (doneHours - totalTExpected) + "工时。\"></div>");
                    }

                    builder.Append(item.TName);
                    //获取子任务
                    List<Temporary_TaskModel> childList = Temporary_TaskBLL.GetTemporaryTaskListByParentId(item.Id.ToInt());

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
                    List<Temporary_Task_Team> taskTeamList = new List<Temporary_Task_Team>();

                    string pCreateTime = "0000-00-00";
                    if (item.CreateTime.IsNotNullOrEmpty())
                    {
                        pCreateTime = item.CreateTime.ToShortDate();
                    }
                    builder.Append("<td>" + pCreateTime + "</td>");
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
                        builder.Append("<td colspan='11' style='padding:0;'>");
                        builder.Append("<table class='layui-table' lay-size='sm'>");
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

                            builder.Append("<td class='w-40px' style='font-size: 12.8;'><span class='active pri pri-" + childItem.Priority + "'>" + childItem.Priority + "</span></td>");
                            builder.Append("<td class='text-left' title='" + childItem.TName + "' style='font-size: 12.8;'>");
                            //进度到了90% 亮灯控制
                            if (childPercent >= 90 && childPercent < 100 && childItem.TState != 4)
                            {
                                builder.Append("<div class=\"circular green\" title=\"进度已达90 % \"></div>");
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
                            builder.Append("<td class='w-100px' style='font-size: 12.8;'>" + childAssignedRealName + "</td>");

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
                            builder.Append("<td class='w-80px' style='font-size: 12.8;'>" + childDoneHours + "</td>");
                            builder.Append("<td class='w-100px' style='font-size: 12.8;'>" + childPredictHours + "</td>");
                            builder.Append("<td class='w-100px' style='font-size: 12.8;'>");
                            builder.Append("<div class=\"progress progress-striped\" style=\"margin: 0\">");
                            builder.Append("<div class=\"progress-bar\" role=\"progressbar\" aria-valuenow=\"" + childPercent + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + childPercent + "%\")\" title=\"" + childPercent + "%\")>");
                            builder.Append("<span class=\"sr-only\">" + childPercent + "% Complete(success)</span>");
                            builder.Append("</div>");
                            builder.Append("</div>");
                            builder.Append("</td>");

                            builder.Append("<td class='w-290px'>");


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

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            Temporary_Task model = new Temporary_Task();
            try
            {
                if (id > 0)
                {
                    model = Temporary_TaskBLL.GetModel(id.ToInt());
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

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

        #region 创建任务
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult Add()
        {
            Temporary_Task model = new Temporary_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                //优先级
                ViewBag.ListPriority = GetListPriority();

                List<Sys_User> uList = Sys_UserBLL.GetList();

                //获取任务团队
                if (uList.Count() > 0)
                {
                    foreach (var item in uList)
                    {
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                    }
                }

                //指派给
                ViewBag.ListTeam = teamUser;
                //抄送给
                ViewBag.ListUser = uList;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 新建保存
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult AddSave()
        {
            Temporary_Task model = FormHelper.GetRequestForm<Temporary_Task>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;

                model.CreateAccount = UserAccount;
                model.CreateTime = DateTime.Now;
                model.CreateUserId = UserId;
                model.TState = 2;
                row = Temporary_TaskBLL.AddModel(model);
                UpdateTeamTime(row, model.TExpected.ToInt(), model.TExpected.ToInt(), model.ParentId.ToInt(), null);

                //最后追加项目日志
                Temporary_Task_Log logModel = new Temporary_Task_Log()
                {
                    FK_TemporaryTaskId = row,
                    StatusId = model.TState,
                    StatusName = "创建任务",
                    Remark = "创建临时任务",
                    CreateTime = DateTime.Now,
                    CreateAccount = UserAccount,
                    CreateUserId = UserId
                };
                Temporary_Task_LogBLL.AddModel(logModel);
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
        #region 查看


        public ActionResult Readonly()
        {
            Temporary_Task model = new Temporary_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                int id = Request["Id"].ToInt();
                if (id > 0)
                {
                    model = Temporary_TaskBLL.GetModel(id);


                    //优先级
                    ViewBag.ListPriority = GetListPriority();

                    List<Sys_User> uList = Sys_UserBLL.GetList();

                    //获取任务团队
                    if (uList.Count() > 0)
                    {
                        foreach (var item in uList)
                        {
                            teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
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
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑跳转
        /// </summary>
        /// <param name="id">任务Id</param>
        /// <param name="projectId">任务所属项目编号</param>
        /// <returns></returns>
        public ActionResult Edit()
        {
            Temporary_Task model = new Temporary_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                int id = Request["Id"].ToInt();
                if (id > 0)
                {
                    model = Temporary_TaskBLL.GetModel(id);


                    //优先级
                    ViewBag.ListPriority = GetListPriority();

                    List<Sys_User> uList = Sys_UserBLL.GetList();

                    //获取任务团队
                    if (uList.Count() > 0)
                    {
                        foreach (var item in uList)
                        {
                            teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
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
        public JsonResult EditSave(string jsonDataTask, string jsonDataTaskTeam)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                //任务
                Temporary_Task taskInfo = FormHelper.GetRequestForm<Temporary_Task>();

                //任务团队
                Temporary_Task_Team taskTeamInfo = Request.Form["tempDetail"].ToJsonDeserialize<Temporary_Task_Team>();


                int row = 0;

                //修改任务表基本属性
                taskInfo.UpdateAccount = UserAccount;
                taskInfo.UpdateTime = DateTime.Now;
                taskInfo.UpdateUserId = UserId;
                taskInfo.EditCount++;
                if (taskTeamInfo.TheTime <= 0)
                {
                    taskInfo.TState = 4;
                    taskInfo.TSuccess = UserId + "";
                    taskInfo.TSucTime = DateTime.Now;
                }
                row = Temporary_TaskBLL.UpdateModel(taskInfo);


                //修改任务团队表基本属性
                UpdateTeamTime(taskInfo.Id.ToInt(), taskInfo.TExpected.ToInt(), taskTeamInfo.TheTime.ToInt(), taskInfo.ParentId.ToInt(), taskTeamInfo.ConsumTime.ToString());

                //Temporary_Task_Team task_Team = Temporary_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TemporaryTaskId == taskInfo.Id)).FirstOrDefault();
                //if (task_Team != null)
                //{
                //    task_Team.ExpectHours = taskTeamInfo.ExpectHours;
                //    task_Team.TheTime = taskTeamInfo.TheTime;
                //    task_Team.ConsumTime = taskTeamInfo.ConsumTime;
                //    task_Team.UpdateAccount = UserAccount;
                //    task_Team.UpdateTime = DateTime.Now;
                //    task_Team.UpdateUserId = UserId;
                //    row = Temporary_Task_TeamBLL.UpdateModel(task_Team);
                //}
                //else
                //{
                //    task_Team = new Temporary_Task_Team();
                //    task_Team.FK_UserId = UserId;
                //    task_Team.FK_TemporaryTaskId = taskInfo.Id.ToInt();
                //    task_Team.ExpectHours = taskTeamInfo.ExpectHours;
                //    task_Team.TheTime = taskTeamInfo.TheTime;
                //    task_Team.ConsumTime = taskTeamInfo.ConsumTime;
                //    task_Team.CreateAccount = UserAccount;
                //    task_Team.CreateTime = DateTime.Now;
                //    task_Team.CreateUserId = UserId;
                //    row = Temporary_Task_TeamBLL.AddModel(task_Team);
                //}

                //最后追加项目日志
                Temporary_Task_Log logModel = new Temporary_Task_Log()
                {
                    FK_TemporaryTaskId = taskInfo.Id.ToInt(),
                    StatusId = taskInfo.TState,
                    StatusName = "编辑任务",
                    Remark = "编辑",
                    CreateTime = DateTime.Now,
                    CreateAccount = UserAccount,
                    CreateUserId = UserId
                };
                Temporary_Task_LogBLL.AddModel(logModel);

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
            Temporary_Task model = FormHelper.GetRequestForm<Temporary_Task>();
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
                    row = Temporary_TaskBLL.UpdateModel(model);
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), model.TExpected.ToInt(), model.ParentId.ToInt(), null);

                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.TState = 2;
                    row = Temporary_TaskBLL.AddModel(model);
                    UpdateTeamTime(row, model.TExpected.ToInt(), model.TExpected.ToInt(), model.ParentId.ToInt(), null);
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
                Temporary_Task_Team team = Temporary_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TemporaryTaskId == taskId)).FirstOrDefault();

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
                    Temporary_Task_TeamBLL.UpdateModel(team);
                }
                else
                {
                    team = new Temporary_Task_Team();
                    team.FK_UserId = UserId;
                    team.FK_TemporaryTaskId = taskId;
                    team.ExpectHours = expectHours.ToInt();
                    team.TheTime = theTime.ToInt();
                    team.ConsumTime = consumTime.ToInt();
                    team.CreateAccount = UserAccount;
                    team.CreateTime = DateTime.Now;
                    team.CreateUserId = UserId;
                    Temporary_Task_TeamBLL.AddModel(team);
                }
                //判断是否有父任务,有就更新父任务的消耗时间和预计剩余时间.
                if (pid > 0)
                {
                    //获取当前主任务下的子任务,并求和,然后更新
                    int totalConsumTime = 0;
                    int totalTheTime = 0;

                    List<Temporary_Task> childTaskList = Temporary_TaskBLL.GetList(d => d.Where(t => t.ParentId == pid));
                    foreach (var childItem in childTaskList)
                    {
                        //获取团队中所存耗时
                        int fk_TaskId = childItem.Id.ToInt();

                        Temporary_Task_Team childTeam = Temporary_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TemporaryTaskId == fk_TaskId)).FirstOrDefault();
                        if (childTeam != null)
                        {
                            totalConsumTime += childTeam.ConsumTime.ToInt();
                            totalTheTime += childTeam.TheTime.ToInt();
                        }
                    }
                    //变更
                    Temporary_Task_Team mainTaskTeam = Temporary_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TemporaryTaskId == pid)).FirstOrDefault();
                    mainTaskTeam.ConsumTime = totalConsumTime;
                    mainTaskTeam.TheTime = totalTheTime;
                    mainTaskTeam.UpdateAccount = UserAccount;
                    mainTaskTeam.UpdateTime = DateTime.Now;
                    mainTaskTeam.UpdateUserId = UserId;
                    Temporary_Task_TeamBLL.UpdateModel(mainTaskTeam);

                    Temporary_Task task = Temporary_TaskBLL.GetModel(pid);
                    //判断主任务是否已完成,如已完成,修改任务状态,加入日志
                    if (mainTaskTeam.TheTime <= 0)
                    {

                        task.TState = 4;
                        task.TSuccess = UserId + "";
                        task.TSucTime = DateTime.Now;
                        task.UpdateUserId = UserId;
                        task.UpdateAccount = UserAccount;
                        task.UpdateTime = DateTime.Now;
                        Temporary_TaskBLL.UpdateModel(task);

                        //最后追加项目日志
                        Temporary_Task_Log logModel = new Temporary_Task_Log()
                        {
                            FK_TemporaryTaskId = task.Id.ToInt(),
                            StatusId = task.TState,
                            StatusName = "任务操作",
                            Remark = "子任务预计剩余工时总计为0,系统默认完成任务",
                            CreateTime = DateTime.Now,
                            CreateAccount = UserAccount,
                            CreateUserId = UserId
                        };
                        Temporary_Task_LogBLL.AddModel(logModel);
                    }
                    else
                    {
                        if (task.TState == 4)
                        {
                            task.TState = 3;
                            Temporary_TaskBLL.UpdateModel(task);
                            //最后追加项目日志
                            Temporary_Task_Log logModel = new Temporary_Task_Log()
                            {
                                FK_TemporaryTaskId = task.Id.ToInt(),
                                StatusId = task.TState,
                                StatusName = "任务操作",
                                Remark = "预计剩余非0,系统默认修改已完成任务状态为进行中",
                                CreateTime = DateTime.Now,
                                CreateAccount = UserAccount,
                                CreateUserId = UserId
                            };
                            Temporary_Task_LogBLL.AddModel(logModel);
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
                int row = Project_TaskBLL.DelModel("Id IN(" + id + ")");
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
            Temporary_Task model = new Temporary_Task();
            try
            {
                //int fk_TaskId, int fk_ProjectId, decimal expectHours, string viewName
                model.Id = Request["fk_TaskId"].ToInt();
                if (model.Id > 0)
                {
                    model = Temporary_TaskBLL.GetModel(model.Id);
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
            Temporary_Task model = FormHelper.GetRequestForm<Temporary_Task>();
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

                    row = Temporary_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());


                    //最后追加项目日志
                    Temporary_Task_Log logModel = new Temporary_Task_Log()
                    {
                        FK_TemporaryTaskId = model.Id.ToInt(),
                        StatusId = model.TState,
                        StatusName = "任务工时",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Temporary_Task_LogBLL.AddModel(logModel);

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
            Temporary_Task model = new Temporary_Task();
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
                    model = Temporary_TaskBLL.GetModel(model.Id);
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
            Temporary_Task model = FormHelper.GetRequestForm<Temporary_Task>();
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

                    row = Temporary_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());

                    //最后追加项目日志
                    Temporary_Task_Log logModel = new Temporary_Task_Log()
                    {
                        FK_TemporaryTaskId = model.Id.ToInt(),
                        ActualStart = actualStart,
                        StatusId = model.TState,
                        StatusName = "开始任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Temporary_Task_LogBLL.AddModel(logModel);

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
            Temporary_Task model = new Temporary_Task();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                //int fk_TaskId, int fk_ProjectId, decimal expectHours, string viewName
                model.Id = Request["taskId"].ToInt();
                if (model.Id > 0)
                {
                    model = Temporary_TaskBLL.GetModel(model.Id);
                }
                decimal theTime = Request["theTime"].ToDecimal();
                decimal consumTime = Request["consumTime"].ToDecimal();
                ViewBag.ConsumTime = consumTime;
                ViewBag.TheTime = theTime;

                List<Sys_User> uList = Sys_UserBLL.GetList();

                //获取人员列表
                if (uList.Count() > 0)
                {
                    foreach (var item in uList)
                    {
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
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
            Temporary_Task model = FormHelper.GetRequestForm<Temporary_Task>();
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

                    row = Temporary_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());

                    //最后追加日志
                    Temporary_Task_Log logModel = new Temporary_Task_Log()
                    {
                        FK_TemporaryTaskId = model.Id.ToInt(),

                        StatusId = model.TState,
                        StatusName = "完成任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Temporary_Task_LogBLL.AddModel(logModel);

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
            Temporary_Task model = new Temporary_Task();
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
                    model = Temporary_TaskBLL.GetModel(model.Id);
                }
                List<Sys_User> uList = Sys_UserBLL.GetList();

                //获取任务团队
                if (uList.Count() > 0)
                {
                    foreach (var item in uList)
                    {
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
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
            Temporary_Task model = FormHelper.GetRequestForm<Temporary_Task>();
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
                    row = Temporary_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), theTime.ToInt(), model.ParentId.ToInt(), consumTime.ToString());


                    //最后追加项目日志
                    Temporary_Task_Log logModel = new Temporary_Task_Log()
                    {
                        FK_TemporaryTaskId = model.Id.ToInt(),

                        StatusId = model.TState,
                        StatusName = "指派任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Temporary_Task_LogBLL.AddModel(logModel);

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
            Temporary_Task model = new Temporary_Task();
            try
            {
                int id = Request["taskId"].ToInt();
                model = Temporary_TaskBLL.GetModel(id);

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
            Temporary_Task model = FormHelper.GetRequestForm<Temporary_Task>();
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

                    row = Temporary_TaskBLL.UpdateModel(model);


                    //修改任务团队表基本属性
                    UpdateTeamTime(model.Id.ToInt(), model.TExpected.ToInt(), 0, model.ParentId.ToInt(), "0");

                    //最后追加日志
                    Temporary_Task_Log logModel = new Temporary_Task_Log()
                    {
                        FK_TemporaryTaskId = model.Id.ToInt(),

                        StatusId = model.TState,
                        StatusName = "关闭任务",
                        Remark = logText,
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Temporary_Task_LogBLL.AddModel(logModel);

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
            Temporary_Task parentModel = new Temporary_Task();
            List<Temporary_Task> taskList = new List<Temporary_Task>();
            try
            {
                int id = Request["taskId"].ToInt();
                if (id > 0)
                {
                    parentModel = Temporary_TaskBLL.GetModel(id);
                    ViewBag.ParentId = parentModel.Id;
                    //优先级
                    ViewBag.ListPriority = GetListPriority();
                    //获取任务团队
                    List<SelectListItem> teamUser = new List<SelectListItem>();
                    List<Sys_User> uList = Sys_UserBLL.GetList();
                    if (uList.Count() > 0)
                    {
                        foreach (var item in uList)
                        {
                            teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                        }
                    }

                    //指派给
                    ViewBag.ListTeam = teamUser;
                    taskList = Temporary_TaskBLL.GetList(d => d.Where(t => t.ParentId == parentModel.Id));
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
                List<Temporary_Task> list = jsonData.ToJsonDeserialize<List<Temporary_Task>>();
                List<Temporary_Task> standardList = new List<Temporary_Task>();
                List<Temporary_Task_Team> teamList = new List<Temporary_Task_Team>();
                Temporary_Task_Team teamModel = new Temporary_Task_Team();
                foreach (var item in list)
                {
                    item.CreateUserId = UserId;
                    item.CreateTime = DateTime.Now;
                    item.CreateAccount = UserAccount;
                    item.TState = 2;

                    int id = Temporary_TaskBLL.AddModel(item);

                    //修改任务团队表基本属性
                    UpdateTeamTime(id, item.TExpected.ToInt(), item.TExpected.ToInt(), item.ParentId.ToInt(), "0");

                    //最后追加日志
                    Temporary_Task_Log logModel = new Temporary_Task_Log()
                    {
                        FK_TemporaryTaskId = pid,
                        StatusName = "创建",
                        Remark = "创建",
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Temporary_Task_LogBLL.AddModel(logModel);
                }
                //Temporary_TaskBLL.MergeNotDelModel(standardList, "A.Assigned=B.Assigned and A.ParentId=B.ParentId and A.TName=B.TName");


                //Temporary_Task_TeamBLL.MergeNotDelModel(teamList, "A.FK_TemporaryTaskId=B.FK_TemporaryTaskId ");

                if (list.Count > 0)
                {
                    //
                    //最后追加主任务日志
                    Temporary_Task_Log logModel = new Temporary_Task_Log()
                    {
                        FK_TemporaryTaskId = pid,
                        StatusName = "新增子任务",
                        Remark = "新增子任务",
                        CreateTime = DateTime.Now,
                        CreateAccount = UserAccount,
                        CreateUserId = UserId
                    };
                    Temporary_Task_LogBLL.AddModel(logModel);
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