using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ZX.Web.Controllers
{
    /// <summary>
    /// 审批统计
    /// </summary>
    public class ApprovalStatisticalController : BaseController
    {
        // GET: ApprovalStatistical
        public ActionResult Index()
        {
            return View("ApprovalStatisticalList");
        }

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult ApprovalStatisticalList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                DateTime DateTimme = DateTime.Now;

                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["key"] ?? "";

                string timeType = Request["timeType"].ToString();
                string realname = Request["realname"].ToString();

                string startTime = "";// 自
                string endTime = "";//至

                switch (timeType)
                {
                    case "Day":
                        {//本日
                            DateTime nowDate = DateTime.Now;
                            startTime = nowDate.ToString("yyyy-MM-dd");
                            endTime = nowDate.ToString("yyyy-MM-dd 23:59:59");
                            break;
                        }
                    case "Week":
                        { //本周   
                            DateTime nowDate = DateTime.Now;

                            int weekIndex = (int)nowDate.DayOfWeek;
                            if (weekIndex == 0)
                            {
                                startTime = nowDate.ToString("yyyy-MM-dd");
                                endTime = nowDate.ToString("yyyy-MM-dd 23:59:59");
                            }
                            else
                            {
                                startTime = nowDate.AddDays(-(int)nowDate.DayOfWeek + 1).ToShortDateString();
                                endTime = nowDate.AddDays(7 - (int)nowDate.DayOfWeek).ToString();
                            }
                            break;
                        }
                    case "Month":
                        { //本月
                            DateTime nowDate = DateTime.Now;

                            startTime = nowDate.ToString("yyyy-MM-01");
                            endTime = nowDate.ToString();
                            break;
                        }
                    case "byWhere":
                        { //根据传入时间段查询
                            startTime = Request["startTime"].ToString();
                            endTime = Request["endTime"].ToString();
                            break;
                        }
                }

                DataList<ApprovalStatistical> list = ApprovalStatisticalBLL.GetApprovalStatisticalList(startTime, endTime, realname, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();



                int rowNumber = 1;

                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + rowNumber++) + "</td>");
                    builder.Append("<td>" + item.Realname + "</td>");

                    if (item.My_Workcount.ToInt() > 0) {
                        builder.Append("<td onclick=\"lookMy_WorkDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"加班及调休明细\">" + item.My_Workcount.ToInt() + "</td>");
                    }
                    else { 
                        builder.Append("<td title=\"暂无明细\">" + item.My_Workcount.ToInt() + "</td>");
                    }


                    if (item.My_BusinessTripcount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMy_BusinessTripDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"出差明细\">" + item.My_BusinessTripcount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.My_BusinessTripcount.ToInt() + "</td>");
                    }


                    if (item.My_Applycount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMy_ApplyDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"出差明细\">" + item.My_Applycount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.My_Applycount.ToInt() + "</td>");
                    }


                    if (item.MyGooodsUsecount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMyGooodsUsecountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"物品领用明细\">" + item.MyGooodsUsecount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MyGooodsUsecount.ToInt() + "</td>");
                    }

                    if (item.MyGiftBuycount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMyGiftBuycountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"物资采购\">" + item.MyGiftBuycount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MyGiftBuycount.ToInt() + "</td>");
                    }

                    if (item.MyAgreementcount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMyAgreementcountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"合同协议明细\">" + item.MyAgreementcount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MyAgreementcount.ToInt() + "</td>");
                    }


                    if (item.MyCartPubliccount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMyCartPubliccountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"私车公用明细\">" + item.MyCartPubliccount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MyCartPubliccount.ToInt() + "</td>");
                    }


                    if (item.MyClockcount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMyClockcountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"未打卡证明明细\">" + item.MyClockcount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MyClockcount.ToInt() + "</td>");
                    }


                    if (item.MySealUsecount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMySealUsecountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"印章使用明细\">" + item.MySealUsecount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MySealUsecount.ToInt() + "</td>");
                    }


                    if (item.MySealOutcount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMySealOutcountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"印章外带明细\">" + item.MySealOutcount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MySealOutcount.ToInt() + "</td>");
                    }


                    if (item.My_Askcount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMy_AskcountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"请假明细\">" + item.My_Askcount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.My_Askcount.ToInt() + "</td>");
                    }


                    if (item.MyEntertaincount.ToInt() > 0)
                    {
                        builder.Append("<td onclick=\"lookMyEntertaincountDetailed(" + item.Id + ",'" + startTime + "','" + endTime + "')\" title=\"招待明细\">" + item.MyEntertaincount.ToInt() + "</td>");
                    }
                    else
                    {
                        builder.Append("<td title=\"暂无明细\">" + item.MyEntertaincount.ToInt() + "</td>");
                    }

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

        #region 加班调休明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMy_WorkDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMy_WorkDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<My_WorkModel> list = My_WorkBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    string appLen = "0天";
                    int totalHh = (Convert.ToDateTime(item.EndTime) - Convert.ToDateTime(item.StartTime)).TotalHours.ToInt();
                    int dd = totalHh / 24;
                    int hh = totalHh % 24;
                    if (hh != 0)
                    {
                        if (dd > 0)
                        {
                            appLen = "共" + dd + "天" + hh + "小时";
                        }
                        else
                        {
                            appLen = "共" + hh + "小时";
                        }
                    }
                    else
                    {
                        appLen = "共" + dd + "天";
                    }
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.StartTime.ToDateFormat("yyyy-MM-dd HH:mm") + "</td>");
                    builder.Append("<td class='text-left'>" + item.EndTime.ToDateFormat("yyyy-MM-dd HH:mm") + "</td>");
                    builder.Append("<td class='text-left'>" + appLen + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.CreateTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");

                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("加班调休明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        #region 出差明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMy_BusinessTripDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMy_BusinessTripDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                int rowIndex = 1;
                List<My_BusinessTripModel> list = My_BusinessTripBLL.GetModelListByWhere(userId,startTime,endTime);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.StartTime.ToDateFormat("yyyy-MM-dd") + "</td>");
                    builder.Append("<td class='text-left'>" + item.EndTime.ToDateFormat("yyyy-MM-dd") + "</td>");
                    builder.Append("<td class='text-left'>" + item.BLength + "天</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.CreateTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("出差申请明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        #region 申请单明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMy_ApplyDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMy_ApplyDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                int rowIndex = 1;
                List<My_ApplyModel> list = My_ApplyBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left' title=\""+ item.ADesc + "\">" + (item.ADesc.Length > 30 ? item.ADesc.Substring(0, 30) + "..." : item.ADesc) + "</td>");
                    builder.Append("<td class='text-left'>" + item.TotalMoney + "元</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("申请单明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        #region 物品领用明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMyGooodsUsecountDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMyGooodsUseDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<MyGooodsUseModel> list = MyGooodsUseBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + item.RealName + "</td>");
                    builder.Append("<td class='text-left'>" + item.DeptName + "</td>");
                    builder.Append("<td class='text-left'>" + item.YanName + "</td>");
                    builder.Append("<td class='text-left'>" + item.YanCount + item.YanUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.JiuName + "</td>");
                    builder.Append("<td class='text-left'>" + item.JiuCount + item.JiuUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.OtherName + "</td>");
                    builder.Append("<td class='text-left'>" + item.OtherCount + item.OrtherUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("物品领用明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        #region 物资采购明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMyGiftBuyDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMyGiftBuyDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<MyGiftBuyModel> list = MyGiftBuyBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + item.RealName + "</td>");
                    builder.Append("<td class='text-left'>" + item.DeptName + "</td>");
                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.TotalMoney + "元</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("物品领用明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        

        #region 合同明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMyAgreementcountDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMyAgreementcountDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<MyAgreementModel> list = MyAgreementBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + item.HtNo + "</td>");
                    builder.Append("<td class='text-left'>" + item.HtName + "</td>");
                    builder.Append("<td class='text-left'>" + item.JiaName + "</td>");
                    builder.Append("<td class='text-left'>" + item.YiName + "</td>");
                    builder.Append("<td class='text-left'>" + item.TotalMoney + "元</td>");
                    builder.Append("<td class='text-left'>" + item.YinzName + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("合同明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion

        #region 未打卡证明明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMyClockcountDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLookMyClockcountDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<MyClockModel> list = MyClockBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + (item.NoClockCount > 3 ? "超过3次" : item.NoClockCount + "次") + "</td>");
                    builder.Append("<td class='text-left'>" + item.OneDate.ToDateFormat("yyyy-MM-dd") + "人</td>");
                    //builder.Append("<td class='text-left'>" + item.OneDesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.TwoDate.ToDateFormat("yyyy-MM-dd") + "</td>");
                    //builder.Append("<td class='text-left'>" + item.TwoDesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.ThreeDate.ToDateFormat("yyyy-MM-dd") + "</td>");
                    //builder.Append("<td class='text-left'>" + item.ThreeDesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.OutThreeDate.ToDateFormat("yyyy-MM-dd") + "</td>");
                    //builder.Append("<td class='text-left'>" + item.OutThreeDesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("未打卡证明明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        #region 公章使用明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMySealUsecountDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMySealUsecountDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<MySealUseModel> list = MySealUseBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + item.YinzName + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("公章使用明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        #region 公章外带明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMySealOutcountDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMySealOutcountDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<MySealOutModel> list = MySealOutBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");
                    builder.Append("<td class='text-left'>" + item.YinzName + "</td>");
                    builder.Append("<td class='text-left'>" + item.BgName + "</td>");
                    builder.Append("<td class='text-left'>" + item.ReturnTime.ToDateFormat("yyyy-MM-dd") + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("</tr>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("公章外带明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion

        #region 请假明细查看
        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMy_AskcountDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLookMy_AskcountDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<My_AskModel> list = My_AskBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    string appLen = "0天";
                    int totalHh = (Convert.ToDateTime(item.EndTime) - Convert.ToDateTime(item.StartTime)).TotalHours.ToInt();
                    int dd = totalHh / 24;
                    int hh = totalHh % 24;
                    if (hh != 0)
                    {
                        if (dd > 0)
                        {
                            appLen = "共请假" + dd + "天" + hh + "小时";
                        }
                        else
                        {
                            appLen = "共请假" + hh + "小时";
                        }
                    }
                    else
                    {
                        appLen = "共请假" + dd + "天";
                    }
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");

                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.StartTime.ToDateFormat("yyyy-MM-dd HH:mm") + "</td>");
                    builder.Append("<td class='text-left'>" + item.EndTime.ToDateFormat("yyyy-MM-dd HH:mm") + "</td>");
                    builder.Append("<td class='text-left'>" + appLen + "</td>");
                    builder.Append("<td class='text-left'>" + item.TypeName + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.CreateTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("请假明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion


        #region 招待明细查看
        /// <summary>
        /// 12、招待
        /// </summary>
        /// <returns></returns>
        public ActionResult LookMyEntertainDetailed()
        {
            ViewBag.UserId = Request["userId"];
            ViewBag.StartTime = Request["startTime"];
            ViewBag.EndTime = Request["endTime"];
            return View();
        }

        /// <summary>
        /// 12、招待
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLookMyEntertainDetailed()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int userId = Request["userId"].ToInt();
                string startTime = Request["startTime"];
                string endTime = Request["endTime"];
                List<MyEntertainModel> list = MyEntertainBLL.GetModelListByWhere(userId, startTime, endTime);
                StringBuilder builder = new StringBuilder();
                int rowIndex = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + rowIndex + "</td>");

                    builder.Append("<td class='text-left'>" + item.KeCompanyName + "</td>");
                    builder.Append("<td class='text-left'>" + item.KeCount + "人</td>");
                    builder.Append("<td class='text-left'>" + item.ESxiang + "</td>");
                    builder.Append("<td class='text-left'>" + item.ELevel + "</td>");
                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.TotalMoney + "元</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>"); 
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    rowIndex++;
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("招待明细查看异常", ex);
            }
            return Json(result);
        }
        #endregion

    }
}