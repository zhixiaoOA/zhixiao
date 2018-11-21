using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ZX.Web.Controllers.DailyOffice
{
    public class DailyOfficeController : Controller
    {
        // GET: DailyOffice
        public ActionResult Index()
        {
            ViewBag.Inmark = 1;
            return View("DailyOfficeList");
        }


        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetDailyOfficeList(int mark)
        {
            AjaxResult result = new AjaxResult();
            try
            {

                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["key"] ?? "";
                int index = 1;
                StringBuilder builder = new StringBuilder();

                switch (mark)
                {
                    case 1:
                        DataList<My_AttendanceModel> matlist = new DataList<My_AttendanceModel>(); //My_AttendanceBLL.GetMy_AttendanceList(key, pageIndex, pageSize);
                        foreach (var item in matlist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Status + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = matlist.TotalPages;
                        break;
                    case 2:
                        DataList<My_AskModel> maslist = My_AskBLL.GetMy_AskList(key, -1, -1, "", "", "", pageIndex, pageSize);

                        //foreach (var item in maslist)
                        //{
                        //    builder.Append("<tr class='text-center'>");
                        //    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                        //    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.AType + "</td>");
                        //    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                        //    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartHour + "</td>");
                        //    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                        //    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndHour + "</td>");
                        //    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                        //    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ATotalLength + "</td>");

                        //    builder.Append("<td>");
                        //    builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                        //    builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                        //    builder.Append("</li>");
                        //    builder.Append("</td>");

                        //    builder.Append("</tr>");
                        //}
                        //result.Data = builder.ToString();
                        //result.PageIndex = pageIndex;
                        //result.PageTotal = maslist.TotalPages;
                        break;
                    case 3:
                        DataList<My_GoOutModel> mgolist = My_GoOutBLL.GetMy_GoOutList(key, 1, -1, "", "", -1, pageIndex, pageSize);

                        foreach (var item in mgolist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BName + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BKGName + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndCity + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = mgolist.TotalPages;
                        break;
                    case 4:
                        DataList<My_WorkModel> mwolist = My_WorkBLL.GetMy_WorkList(key, 1, -1, "", "", "", pageIndex, pageSize);
                        foreach (var item in mwolist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.AType + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ATotalLength + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = mwolist.TotalPages;
                        break;
                    case 5:
                        DataList<My_PaidLeaveModel> mpalist = My_PaidLeaveBLL.GetMy_PaidLeaveList(key, 1, -1, "", "", -1, pageIndex, pageSize);

                        foreach (var item in mpalist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.FK_WorkId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ATotalLength + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = mpalist.TotalPages;
                        break;
                    case 6:
                        DataList<My_BusinessTripModel> mbulist = My_BusinessTripBLL.GetMy_BusinessTripList(key, 1, -1, "", "", "", pageIndex, pageSize);

                        foreach (var item in mbulist)
                        {
                            //builder.Append("<tr class='text-center'>");
                            //builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BName + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BKGName + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartHour + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndHour + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartCity + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndCity + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            //builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");


                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = mbulist.TotalPages;
                        break;
                    case 7:
                        DataList<My_BaoXiaoModel> mbxlist = My_BaoXiaoBLL.GetMy_BaoXiaoList(key, 1, -1, "", "", -1, pageIndex, pageSize);

                        foreach (var item in mbxlist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BName + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.FK_DeptId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Subject + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.SubjectType + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Currency + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Attachment + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = mbxlist.TotalPages;
                        break;
                    case 8:
                        DataList<My_BaoXiao_DetailModel> mbdlist = My_BaoXiao_DetailBLL.GetMy_BaoXiao_DetailList(key, pageIndex, pageSize);

                        foreach (var item in mbdlist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.DDate + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.FK_BaoXiaoId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndHour + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BaoXiaoUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = mbdlist.TotalPages;
                        break;
                    case 9:
                        DataList<My_HolidayModel> mholist = My_HolidayBLL.GetMy_HolidayList(key, pageIndex, pageSize);

                        foreach (var item in mholist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.HTypeId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.HName + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.HDesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");
                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");

                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = mholist.TotalPages;
                        break;
                    default:
                        matlist = new DataList<My_AttendanceModel>(); // My_AttendanceBLL.GetMy_AttendanceList(key, pageIndex, pageSize);
                        foreach (var item in matlist)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Status + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.StartTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.EndTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.ADesc + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
                            builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                            builder.Append("<td>");
                            builder.Append("<a href='Javascript: void(0)' onclick='sysMenuDel(" + item.Id + ",0)' class='a_btn_menu'>删除</a>");
                            builder.Append("<a href='/Assets/addedit?id=" + item.Id + "&mid=" + item.Id + "' class='a_btn_menu'>编辑</a>");
                            builder.Append("</li>");
                            builder.Append("</td>");
                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                        result.PageIndex = pageIndex;
                        result.PageTotal = matlist.TotalPages;

                        break;
                }
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

        public ActionResult Transit(int mark)
        {
            string actionName = "Index";
            string controllerName = string.Empty;
            switch (mark)
            {
                case 1:
                    controllerName = "My_Attendance";
                    break;
                case 2:
                    controllerName = "My_Ask";
                    break;
                case 3:
                    controllerName = "My_GoOut";
                    break;
                case 4:
                    controllerName = "My_Work";
                    break;
                case 5:
                    controllerName = "My_PaidLeave";
                    break;
                case 6:
                    controllerName = "My_BusinessTrip";
                    break;
                case 7:
                    controllerName = "My_BaoXiao";
                    break;
                case 8:
                    controllerName = "My_BaoXiao_Detail";
                    break;
                case 9:
                    controllerName = "My_Holiday";
                    break;
                default:
                    controllerName = "My_Attendance";
                    break;
            }

            return RedirectToAction(actionName, controllerName);
        }

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            //
            int Inmark = Request["Inmark"].ToInt();

            News model = new News();
            try
            {
                if (id > 0)
                {
                    model = NewsBLL.GetModel(id.ToInt());
                }

            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

    }
}