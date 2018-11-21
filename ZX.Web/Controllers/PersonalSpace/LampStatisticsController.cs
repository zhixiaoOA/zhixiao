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
    public class LampStatisticsController : BaseController
    {

        #region 管理员统计 分页获取数据
        // GET: LampStatistics
        public ActionResult Index()
        {
            return View();
        }

        //项目任务
        public ActionResult Project_TaskLamp()
        {
            return View();
        }

        //临时任务
        public ActionResult Temporary_TaskLamp()
        {
            return View();
        }

        /// <summary>
        /// 分页获取用户临时任务亮灯统计
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTemporaryTaskLampList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["name"] ?? "";

                int yearSelect = Request["yearSelect"].ToInt(DateTime.Now.Year);
                int quarterSelect = Request["quarterSelect"].ToInt();
                int monthSelect = Request["monthSelect"].ToInt();


                //时间段
                DateTime startTime = new DateTime();  //查询时间段起始点
                DateTime endTime = new DateTime();  //查询时间段结束点
                ///如果季度和月未选取,那么则为整年时间段
                if (quarterSelect <= 0 && monthSelect <= 0)
                {
                    startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                    endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                }
                else
                {
                    if (quarterSelect > 0)
                    {
                        switch (quarterSelect)
                        {
                            case 1:
                                startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 3, 31, 23, 59, 59);
                                break;
                            case 2:
                                startTime = new DateTime(yearSelect, 4, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 6, 30, 23, 59, 59);
                                break;
                            case 3:
                                startTime = new DateTime(yearSelect, 7, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 9, 30, 23, 59, 59);
                                break;
                            case 4:
                                startTime = new DateTime(yearSelect, 10, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        startTime = new DateTime(yearSelect, monthSelect, 1, 0, 0, 0);
                        DateTime dtCurrent = Convert.ToDateTime(yearSelect + "-" + monthSelect + "-01");
                        endTime = dtCurrent.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
                }


                DataList<Sys_UserModel> list = Sys_UserBLL.GetSys_UserList(key, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1;

                //根据人员表循环查询
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td title='" + item.RealName + "'>" + item.RealName + "</td>");

                    int userId = item.Id.ToInt();
                    #region 统计用户红灯的任务数
                    //先获取该用户所有任务
                    List<Temporary_Task> userTaskList = Temporary_TaskBLL.GetList(d => d.Where(t => (t.Assigned == userId + "" || t.CreateUserId == userId || t.TSuccess == userId + "" || t.TClosed == userId + "" || t.TCancel == userId + "") && t.AsTime >= startTime && t.AsTime <= endTime));


                    //统计用户绿灯的任务数
                    int taskTotal = 0;//个人任务总数
                    decimal greenLamp = 0; //个人绿灯数
                    decimal greenPercent = 0;//绿灯占百分比


                    decimal orangeLamp = 0; //个人黄灯数
                    decimal orangePercent = 0;//黄灯占百分比

                    decimal redLamp = 0; //个人红数
                    decimal redPercent = 0;//红灯占百分比

                    decimal doneHours = 0;//已耗时
                    decimal predictHours = 0;//预估剩余工时
                    decimal totalTExpected = 0; //获取项目总共最初预计，用于亮灯控制判断
                    #region 按照工时统计
                    //if (userTaskList.Count > 0)
                    //{
                    //    //循环计算各参数

                    //    //获取主任务
                    //    List<Temporary_Task> userMainTaskList = userTaskList.Where(l => l.ParentId.ToInt() == 0).ToList();
                    //    //循环主任务
                    //    foreach (var task in userMainTaskList)
                    //    {
                    //        //获取子任务
                    //        List<Temporary_Task> userChildTaskList = userTaskList.Where(l => l.ParentId == task.Id).ToList();

                    //        //判断是否有子任务,并进行变量累加处理
                    //        if (userChildTaskList.Count <= 0)
                    //        {
                    //            taskTotal++;//任务总数追加
                    //            Temporary_Task_Team team = new Temporary_Task_Team();
                    //            try
                    //            {
                    //                team = Temporary_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TemporaryTaskId == task.Id)).ToList().FirstOrDefault();
                    //                doneHours = team.ConsumTime.ToInt();
                    //                predictHours = team.TheTime.ToInt();
                    //            }
                    //            catch { }

                    //            decimal percent = 0; //任务完成百分比
                    //            try
                    //            {
                    //                percent = doneHours / (doneHours + predictHours) * 100;
                    //                percent = System.Decimal.Round(percent, 2);
                    //            }
                    //            catch { }

                    //            totalTExpected = task.TExpected.ToInt();

                    //            //进度到了90% 绿灯累加
                    //            if (percent >= 90 && percent < 100 && task.TState != 4)
                    //            {
                    //                greenLamp++;
                    //            }
                    //            //进度已经所消耗时间超出前期预计总耗时 红灯累加
                    //            if (doneHours > totalTExpected)
                    //            {
                    //                redLamp++;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            foreach (var childItem in userChildTaskList)
                    //            {
                    //                taskTotal++;
                    //                Temporary_Task_Team team = new Temporary_Task_Team();
                    //                try
                    //                {
                    //                    team = Temporary_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TemporaryTaskId == childItem.Id)).ToList().FirstOrDefault();
                    //                    doneHours = team.ConsumTime.ToInt();
                    //                    predictHours = team.TheTime.ToInt();
                    //                }
                    //                catch { }

                    //                decimal percent = 0; //任务完成百分比
                    //                try
                    //                {
                    //                    percent = doneHours / (doneHours + predictHours) * 100;
                    //                    percent = System.Decimal.Round(percent, 2);
                    //                }
                    //                catch { }

                    //                totalTExpected = task.TExpected.ToInt();

                    //                //进度到了90% 绿灯累加
                    //                if (percent >= 90 && percent < 100 && childItem.TState != 4)
                    //                {
                    //                    greenLamp++;
                    //                }
                    //                //进度已经所消耗时间超出前期预计总耗时 红灯累加
                    //                if (doneHours > totalTExpected)
                    //                {
                    //                    redLamp++;
                    //                }
                    //            }
                    //        }

                    //    }
                    //}

                    //try
                    //{
                    //    greenPercent = greenLamp / taskTotal * 100;
                    //    greenPercent = System.Decimal.Round(greenPercent, 2);
                    //}
                    //catch { }

                    //try
                    //{
                    //    redPercent = redLamp / taskTotal * 100;
                    //    redPercent = System.Decimal.Round(redPercent, 2);
                    //}
                    //catch { }
                    #endregion


                    #region 按截止日期统计
                    if (userTaskList.Count > 0)
                    {
                        //循环计算各参数

                        //获取主任务
                        List<Temporary_Task> userMainTaskList = userTaskList.Where(l => l.ParentId.ToInt() == 0).ToList();
                        //循环主任务
                        foreach (var task in userMainTaskList)
                        {
                            //获取子任务
                            List<Temporary_Task> userChildTaskList = userTaskList.Where(l => l.ParentId == task.Id).ToList();

                            //判断是否有子任务,并进行变量累加处理
                            if (userChildTaskList.Count <= 0)
                            {
                                taskTotal++;//任务总数追加

                                //统计黄灯或绿灯数 机制:
                                //不含取已消、已删除的任务
                                //完成时间>截止时间 红灯
                                //完成时间为空并且截止日期小于当前系统日期,红灯
                                //完成日期=截止日期-1天,黄灯
                                //完成时间为空,并且截止日期与当前系统日期相差1 黄灯


                                if (task.TSucTime.IsNullOrEmpty())
                                {
                                    TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                                else
                                {
                                    TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(task.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                            }
                            else
                            {
                                foreach (var childItem in userChildTaskList)
                                {
                                    taskTotal++;
                                    if (childItem.TSucTime.IsNullOrEmpty())
                                    {
                                        TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                        if (ts.Days == 1)
                                        {
                                            orangeLamp++;
                                        }
                                        if (ts.Days < 0)
                                        {
                                            redLamp++;
                                        }
                                    }
                                    else
                                    {
                                        TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(childItem.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                        if (ts.Days == 1)
                                        {
                                            orangeLamp++;
                                        }
                                        if (ts.Days < 0)
                                        {
                                            redLamp++;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    try
                    {
                        orangePercent = orangeLamp / taskTotal * 100;
                        orangePercent = System.Decimal.Round(orangePercent, 2);
                    }
                    catch { }

                    try
                    {
                        redPercent = redLamp / taskTotal * 100;
                        redPercent = System.Decimal.Round(redPercent, 2);
                    }
                    catch { }

                    #endregion

                    #endregion
                    string orangeDesc = "";
                    string redDesc = "";
                    if (orangeLamp > 0)
                    {
                        orangeDesc = "个人总任务:" + taskTotal + "个,黄灯任务" + orangeLamp + "个,占比:" + orangePercent + "%";
                    }
                    if (redLamp > 0)
                    {
                        redDesc = "个人总任务:" + taskTotal + "个,红灯任务" + redLamp + "个,占比:" + redPercent + "%";
                    }

                    builder.Append("<td class='text-left' title='" + orangeDesc + "'>" + orangeDesc + "</td>");
                    builder.Append("<td class='text-left' title='" + redDesc + "'>" + redDesc + "</td>");

                    //builder.Append("<td class='actions'>");

                    //builder.Append(string.Format(CurrentBtnList28, item.Id));
                    //builder.Append("</td>");
                    builder.Append("</tr>");
                }
                builder.Append("</tbody>");
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
        /// 分页获取用户项目任务亮灯统计
        /// </summary>
        /// <returns></returns>
        public JsonResult GetProjectLampList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["name"] ?? "";

                int yearSelect = Request["yearSelect"].ToInt(DateTime.Now.Year);
                int quarterSelect = Request["quarterSelect"].ToInt();
                int monthSelect = Request["monthSelect"].ToInt();

                //时间段
                DateTime startTime = new DateTime();  //查询时间段起始点
                DateTime endTime = new DateTime();  //查询时间段结束点
                ///如果季度和月未选取,那么则为整年时间段
                if (quarterSelect <= 0 && monthSelect <= 0)
                {
                    startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                    endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                }
                else
                {
                    if (quarterSelect > 0)
                    {
                        switch (quarterSelect)
                        {
                            case 1:
                                startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 3, 31, 23, 59, 59);
                                break;
                            case 2:
                                startTime = new DateTime(yearSelect, 4, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 6, 30, 23, 59, 59);
                                break;
                            case 3:
                                startTime = new DateTime(yearSelect, 7, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 9, 30, 23, 59, 59);
                                break;
                            case 4:
                                startTime = new DateTime(yearSelect, 10, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        startTime = new DateTime(yearSelect, monthSelect, 1, 0, 0, 0);
                        DateTime dtCurrent = Convert.ToDateTime(yearSelect + "-" + monthSelect + "-01");
                        endTime = dtCurrent.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
                }

                DataList<Sys_UserModel> list = Sys_UserBLL.GetSys_UserList(key, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.RealName + "'>" + item.RealName + "</td>");

                    int userId = item.Id.ToInt();
                    #region 统计用户红灯的任务数
                    //先获取该用户所有任务
                    List<Project_Task> userTaskList = Project_TaskBLL.GetList(d => d.Where(t => (t.Assigned == userId + "" || t.CreateUserId == userId || t.TSuccess == userId + "" || t.TClosed == userId + "" || t.TCancel == userId + "") && t.AsTime >= startTime && t.AsTime <= endTime));

                    //统计用户绿灯的任务数
                    int taskTotal = 0;//个人任务总数
                    decimal greenLamp = 0; //个人绿灯数
                    decimal greenPercent = 0;//绿灯占百分比

                    decimal orangeLamp = 0; //个人黄灯数
                    decimal orangePercent = 0;//黄灯占百分比

                    decimal redLamp = 0; //个人红灯数
                    decimal redPercent = 0;//红灯占百分比

                    decimal doneHours = 0;//已耗时
                    decimal predictHours = 0;//预估剩余工时
                    decimal totalTExpected = 0; //获取项目总共最初预计，用于亮灯控制判断

                    #region 按工时统计
                    //if (userTaskList.Count > 0)
                    //{
                    //    //循环计算各参数

                    //    //获取主任务
                    //    List<Project_Task> userMainTaskList = userTaskList.Where(l => l.ParentId.ToInt() == 0).ToList();
                    //    //循环主任务
                    //    foreach (var task in userMainTaskList)
                    //    {
                    //        //获取子任务
                    //        List<Project_Task> userChildTaskList = userTaskList.Where(l => l.ParentId == task.Id).ToList();

                    //        //判断是否有子任务,并进行变量累加处理
                    //        if (userChildTaskList.Count <= 0)
                    //        {
                    //            taskTotal++;//任务总数追加
                    //            Project_Task_Team team = new Project_Task_Team();
                    //            try
                    //            {
                    //                team = Project_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TaskId == task.Id)).ToList().FirstOrDefault();
                    //                doneHours = team.ConsumTime.ToInt();
                    //                predictHours = team.TheTime.ToInt();
                    //            }
                    //            catch { }

                    //            decimal percent = 0; //任务完成百分比
                    //            try
                    //            {
                    //                percent = doneHours / (doneHours + predictHours) * 100;
                    //                percent = System.Decimal.Round(percent, 2);
                    //            }
                    //            catch { }

                    //            totalTExpected = task.TExpected.ToInt();

                    //            //进度到了90% 绿灯累加
                    //            if (percent >= 90 && percent < 100 && task.TState != 4)
                    //            {
                    //                greenLamp++;
                    //            }
                    //            //进度已经所消耗时间超出前期预计总耗时 红灯累加
                    //            if (doneHours > totalTExpected)
                    //            {
                    //                redLamp++;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            foreach (var childItem in userChildTaskList)
                    //            {
                    //                taskTotal++;
                    //                Project_Task_Team team = new Project_Task_Team();
                    //                try
                    //                {
                    //                    team = Project_Task_TeamBLL.GetList(d => d.Where(t => t.FK_TaskId == childItem.Id)).ToList().FirstOrDefault();
                    //                    doneHours = team.ConsumTime.ToInt();
                    //                    predictHours = team.TheTime.ToInt();
                    //                }
                    //                catch { }

                    //                decimal percent = 0; //任务完成百分比
                    //                try
                    //                {
                    //                    percent = doneHours / (doneHours + predictHours) * 100;
                    //                    percent = System.Decimal.Round(percent, 2);
                    //                }
                    //                catch { }

                    //                totalTExpected = task.TExpected.ToInt();

                    //                //进度到了90% 绿灯累加
                    //                if (percent >= 90 && percent < 100 && childItem.TState != 4)
                    //                {
                    //                    greenLamp++;
                    //                }
                    //                //进度已经所消耗时间超出前期预计总耗时 红灯累加
                    //                if (doneHours > totalTExpected)
                    //                {
                    //                    redLamp++;
                    //                }
                    //            }
                    //        }

                    //    }
                    //}
                    //try
                    //{
                    //    greenPercent = greenLamp / taskTotal * 100;
                    //    greenPercent = System.Decimal.Round(greenPercent, 2);
                    //}
                    //catch { }

                    //try
                    //{
                    //    redPercent = redLamp / taskTotal * 100;
                    //    redPercent = System.Decimal.Round(redPercent, 2);
                    //}
                    //catch { }
                    #endregion

                    #region 按截止日期统计
                    if (userTaskList.Count > 0)
                    {
                        //循环计算各参数

                        //获取主任务
                        List<Project_Task> userMainTaskList = userTaskList.Where(l => l.ParentId.ToInt() == 0).ToList();
                        //循环主任务
                        foreach (var task in userMainTaskList)
                        {
                            //获取子任务
                            List<Project_Task> userChildTaskList = userTaskList.Where(l => l.ParentId == task.Id).ToList();

                            //判断是否有子任务,并进行变量累加处理
                            if (userChildTaskList.Count <= 0)
                            {
                                taskTotal++;//任务总数追加

                                //统计黄灯或绿灯数 机制:
                                //不含取已消、已删除的任务
                                //完成时间>截止时间 红灯
                                //完成时间为空并且截止日期小于当前系统日期,红灯
                                //完成日期=截止日期-1天,黄灯
                                //完成时间为空,并且截止日期与当前系统日期相差1 黄灯


                                if (task.TSucTime.IsNullOrEmpty())
                                {
                                    TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                                else
                                {
                                    TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(task.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                            }
                            else
                            {
                                foreach (var childItem in userChildTaskList)
                                {
                                    taskTotal++;
                                    if (childItem.TSucTime.IsNullOrEmpty())
                                    {
                                        TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                        if (ts.Days == 1)
                                        {
                                            orangeLamp++;
                                        }
                                        if (ts.Days < 0)
                                        {
                                            redLamp++;
                                        }
                                    }
                                    else
                                    {
                                        TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(childItem.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                        if (ts.Days == 1)
                                        {
                                            orangeLamp++;
                                        }
                                        if (ts.Days < 0)
                                        {
                                            redLamp++;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    try
                    {
                        orangePercent = orangeLamp / taskTotal * 100;
                        orangePercent = System.Decimal.Round(orangePercent, 2);
                    }
                    catch { }

                    try
                    {
                        redPercent = redLamp / taskTotal * 100;
                        redPercent = System.Decimal.Round(redPercent, 2);
                    }
                    catch { }

                    #endregion

                    #endregion
                    string orangeDesc = "";
                    string redDesc = "";
                    if (orangeLamp > 0)
                    {
                        orangeDesc = "个人总任务:" + taskTotal + "个,黄灯任务" + orangeLamp + "个,占比:" + orangePercent + "%";
                    }
                    if (redLamp > 0)
                    {
                        redDesc = "个人总任务:" + taskTotal + "个,红灯任务" + redLamp + "个,占比:" + redPercent + "%";
                    }

                    builder.Append("<td class='text-left' title='" + orangeDesc + "'>" + orangeDesc + "</td>");
                    builder.Append("<td class='text-left' title='" + redDesc + "'>" + redDesc + "</td>");

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

        #region 个人统计 
        public ActionResult Owner()
        {
            return View();
        }

        /// <summary>
        /// 分页获取用户临时任务亮灯统计
        /// </summary>
        /// <returns></returns>
        public JsonResult GetOwnerTemporaryTaskLampList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["name"] ?? "";

                int yearSelect = Request["yearSelect"].ToInt(DateTime.Now.Year);
                int quarterSelect = Request["quarterSelect"].ToInt();
                int monthSelect = Request["monthSelect"].ToInt();


                //时间段
                DateTime startTime = new DateTime();  //查询时间段起始点
                DateTime endTime = new DateTime();  //查询时间段结束点
                ///如果季度和月未选取,那么则为整年时间段
                if (quarterSelect <= 0 && monthSelect <= 0)
                {
                    startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                    endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                }
                else
                {
                    if (quarterSelect > 0)
                    {
                        switch (quarterSelect)
                        {
                            case 1:
                                startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 3, 31, 23, 59, 59);
                                break;
                            case 2:
                                startTime = new DateTime(yearSelect, 4, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 6, 30, 23, 59, 59);
                                break;
                            case 3:
                                startTime = new DateTime(yearSelect, 7, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 9, 30, 23, 59, 59);
                                break;
                            case 4:
                                startTime = new DateTime(yearSelect, 10, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        startTime = new DateTime(yearSelect, monthSelect, 1, 0, 0, 0);
                        DateTime dtCurrent = Convert.ToDateTime(yearSelect + "-" + monthSelect + "-01");
                        endTime = dtCurrent.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
                }


                Sys_User model = Sys_UserBLL.GetModel(UserId.ToInt());
                StringBuilder builder = new StringBuilder();
                int index = 1;

                //根据人员表循环查询

                builder.Append("<tr class='text-center'>");
                builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                builder.Append("<td title='" + model.RealName + "'>" + model.RealName + "</td>");

                int userId = model.Id.ToInt();
                #region 统计用户红灯的任务数
                //先获取该用户所有任务
                List<Temporary_Task> userTaskList = Temporary_TaskBLL.GetList(d => d.Where(t => (t.Assigned == userId + "" || t.CreateUserId == userId || t.TSuccess == userId + "" || t.TClosed == userId + "" || t.TCancel == userId + "") && t.AsTime >= startTime && t.AsTime <= endTime));


                //统计用户绿灯的任务数
                int taskTotal = 0;//个人任务总数


                decimal orangeLamp = 0; //个人黄灯数
                decimal orangePercent = 0;//黄灯占百分比

                decimal redLamp = 0; //个人红数
                decimal redPercent = 0;//红灯占百分比

                #region 按截止日期统计
                if (userTaskList.Count > 0)
                {
                    //循环计算各参数

                    //获取主任务
                    List<Temporary_Task> userMainTaskList = userTaskList.Where(l => l.ParentId.ToInt() == 0).ToList();
                    //循环主任务
                    foreach (var task in userMainTaskList)
                    {
                        //获取子任务
                        List<Temporary_Task> userChildTaskList = userTaskList.Where(l => l.ParentId == task.Id).ToList();

                        //判断是否有子任务,并进行变量累加处理
                        if (userChildTaskList.Count <= 0)
                        {
                            taskTotal++;//任务总数追加

                            //统计黄灯或绿灯数 机制:
                            //不含取已消、已删除的任务
                            //完成时间>截止时间 红灯
                            //完成时间为空并且截止日期小于当前系统日期,红灯
                            //完成日期=截止日期-1天,黄灯
                            //完成时间为空,并且截止日期与当前系统日期相差1 黄灯


                            if (task.TSucTime.IsNullOrEmpty())
                            {
                                TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                if (ts.Days == 1)
                                {
                                    orangeLamp++;
                                }
                                if (ts.Days < 0)
                                {
                                    redLamp++;
                                }
                            }
                            else
                            {
                                TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(task.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                if (ts.Days == 1)
                                {
                                    orangeLamp++;
                                }
                                if (ts.Days < 0)
                                {
                                    redLamp++;
                                }
                            }
                        }
                        else
                        {
                            foreach (var childItem in userChildTaskList)
                            {
                                taskTotal++;
                                if (childItem.TSucTime.IsNullOrEmpty())
                                {
                                    TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                                else
                                {
                                    TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(childItem.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                            }
                        }

                    }
                }

                try
                {
                    orangePercent = orangeLamp / taskTotal * 100;
                    orangePercent = System.Decimal.Round(orangePercent, 2);
                }
                catch { }

                try
                {
                    redPercent = redLamp / taskTotal * 100;
                    redPercent = System.Decimal.Round(redPercent, 2);
                }
                catch { }

                #endregion

                #endregion
                string orangeDesc = "";
                string redDesc = "";
                if (orangeLamp > 0)
                {
                    orangeDesc = "个人总任务:" + taskTotal + "个,黄灯任务" + orangeLamp + "个,占比:" + orangePercent + "%";
                }
                if (redLamp > 0)
                {
                    redDesc = "个人总任务:" + taskTotal + "个,红灯任务" + redLamp + "个,占比:" + redPercent + "%";
                }

                builder.Append("<td class='text-left' title='" + orangeDesc + "'>" + orangeDesc + "</td>");
                builder.Append("<td class='text-left' title='" + redDesc + "'>" + redDesc + "</td>");

                //builder.Append("<td class='actions'>");

                //builder.Append(string.Format(CurrentBtnList28, item.Id));
                //builder.Append("</td>");
                builder.Append("</tr>");
                builder.Append("</tbody>");
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = 1;
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
        /// 分页获取用户项目任务亮灯统计
        /// </summary>
        /// <returns></returns>
        public JsonResult GetOwnerProjectLampList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["name"] ?? "";

                int yearSelect = Request["yearSelect"].ToInt(DateTime.Now.Year);
                int quarterSelect = Request["quarterSelect"].ToInt();
                int monthSelect = Request["monthSelect"].ToInt();

                //时间段
                DateTime startTime = new DateTime();  //查询时间段起始点
                DateTime endTime = new DateTime();  //查询时间段结束点
                ///如果季度和月未选取,那么则为整年时间段
                if (quarterSelect <= 0 && monthSelect <= 0)
                {
                    startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                    endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                }
                else
                {
                    if (quarterSelect > 0)
                    {
                        switch (quarterSelect)
                        {
                            case 1:
                                startTime = new DateTime(yearSelect, 1, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 3, 31, 23, 59, 59);
                                break;
                            case 2:
                                startTime = new DateTime(yearSelect, 4, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 6, 30, 23, 59, 59);
                                break;
                            case 3:
                                startTime = new DateTime(yearSelect, 7, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 9, 30, 23, 59, 59);
                                break;
                            case 4:
                                startTime = new DateTime(yearSelect, 10, 1, 0, 0, 0);
                                endTime = new DateTime(yearSelect, 12, 31, 23, 59, 59);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        startTime = new DateTime(yearSelect, monthSelect, 1, 0, 0, 0);
                        DateTime dtCurrent = Convert.ToDateTime(yearSelect + "-" + monthSelect + "-01");
                        endTime = dtCurrent.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
                }

                Sys_User model = Sys_UserBLL.GetModel(UserId.ToInt());
                StringBuilder builder = new StringBuilder();
                int index = 1;
                builder.Append("<tr class='text-center'>");
                builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                builder.Append("<td class='text-left' title='" + model.RealName + "'>" + model.RealName + "</td>");

                int userId = model.Id.ToInt();
                #region 统计用户红灯的任务数
                //先获取该用户所有任务
                List<Project_Task> userTaskList = Project_TaskBLL.GetList(d => d.Where(t => (t.Assigned == userId + "" || t.CreateUserId == userId || t.TSuccess == userId + "" || t.TClosed == userId + "" || t.TCancel == userId + "") && t.AsTime >= startTime && t.AsTime <= endTime));

                //统计用户绿灯的任务数
                int taskTotal = 0;//个人任务总数
                decimal greenLamp = 0; //个人绿灯数
                decimal greenPercent = 0;//绿灯占百分比

                decimal orangeLamp = 0; //个人黄灯数
                decimal orangePercent = 0;//黄灯占百分比

                decimal redLamp = 0; //个人红灯数
                decimal redPercent = 0;//红灯占百分比

                #region 按截止日期统计
                if (userTaskList.Count > 0)
                {
                    //循环计算各参数

                    //获取主任务
                    List<Project_Task> userMainTaskList = userTaskList.Where(l => l.ParentId.ToInt() == 0).ToList();
                    //循环主任务
                    foreach (var task in userMainTaskList)
                    {
                        //获取子任务
                        List<Project_Task> userChildTaskList = userTaskList.Where(l => l.ParentId == task.Id).ToList();

                        //判断是否有子任务,并进行变量累加处理
                        if (userChildTaskList.Count <= 0)
                        {
                            taskTotal++;//任务总数追加

                            //统计黄灯或绿灯数 机制:
                            //不含取已消、已删除的任务
                            //完成时间>截止时间 红灯
                            //完成时间为空并且截止日期小于当前系统日期,红灯
                            //完成日期=截止日期-1天,黄灯
                            //完成时间为空,并且截止日期与当前系统日期相差1 黄灯


                            if (task.TSucTime.IsNullOrEmpty())
                            {
                                TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                if (ts.Days == 1)
                                {
                                    orangeLamp++;
                                }
                                if (ts.Days < 0)
                                {
                                    redLamp++;
                                }
                            }
                            else
                            {
                                TimeSpan ts = Convert.ToDateTime(task.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(task.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                if (ts.Days == 1)
                                {
                                    orangeLamp++;
                                }
                                if (ts.Days < 0)
                                {
                                    redLamp++;
                                }
                            }
                        }
                        else
                        {
                            foreach (var childItem in userChildTaskList)
                            {
                                taskTotal++;
                                if (childItem.TSucTime.IsNullOrEmpty())
                                {
                                    TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(DateTime.Now.Date);
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                                else
                                {
                                    TimeSpan ts = Convert.ToDateTime(childItem.AsTime.ToDateFormat("yyyy-MM-dd")).Subtract(Convert.ToDateTime(childItem.TSucTime.ToDateFormat("yyyy-MM-dd")));
                                    if (ts.Days == 1)
                                    {
                                        orangeLamp++;
                                    }
                                    if (ts.Days < 0)
                                    {
                                        redLamp++;
                                    }
                                }
                            }
                        }

                    }
                }

                try
                {
                    orangePercent = orangeLamp / taskTotal * 100;
                    orangePercent = System.Decimal.Round(orangePercent, 2);
                }
                catch { }

                try
                {
                    redPercent = redLamp / taskTotal * 100;
                    redPercent = System.Decimal.Round(redPercent, 2);
                }
                catch { }

                #endregion

                #endregion
                string orangeDesc = "";
                string redDesc = "";
                if (orangeLamp > 0)
                {
                    orangeDesc = "个人总任务:" + taskTotal + "个,黄灯任务" + orangeLamp + "个,占比:" + orangePercent + "%";
                }
                if (redLamp > 0)
                {
                    redDesc = "个人总任务:" + taskTotal + "个,红灯任务" + redLamp + "个,占比:" + redPercent + "%";
                }

                builder.Append("<td class='text-left' title='" + orangeDesc + "'>" + orangeDesc + "</td>");
                builder.Append("<td class='text-left' title='" + redDesc + "'>" + redDesc + "</td>");

                builder.Append("</tr>");
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.TotalCount = 1;
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