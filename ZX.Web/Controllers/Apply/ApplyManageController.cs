using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ZX.Web.Controllers
{
    public class ApplyManageController : BaseController
    {
        // GET: ApplyManage
        public ActionResult Index()
        {
            return View("ApplyManageList");
        }

        public JsonResult GetMyCreateApplyList()
        {
            //获取相关审批
            List<AllApplyNotice> myCreateApplyNoticeList = new List<AllApplyNotice>();
            AjaxResult result = new AjaxResult();
            try
            {
                //int pageIndex = Request["pageIndex"].ToInt(1);
                //int pageSize = Request["pageSize"].ToInt(10);
                //string key = Request["key"] ?? "";

                if (UserId != 0)
                {
                    myCreateApplyNoticeList = AllApplyNoticeBLL.GetMyCreateApplyNoticeList(UserId);

                    if (myCreateApplyNoticeList.Count > 0)
                    {
                        StringBuilder builder = new StringBuilder("");
                        int rowIndex = 1;
                        foreach (var applyNotice in myCreateApplyNoticeList)
                        {
                            builder.Append("<tr class='text-center'>");
                            builder.Append("<td>" + (rowIndex++) + "</td>");
                            builder.Append("<td class=\"text-left\" title=\"" + applyNotice.ADesc + "\"><a href=\"" + applyNotice.ApplyAction + "\">" + applyNotice.Title + "</a></td>");
                            builder.Append("<td class='text-left' title='" + applyNotice.ADesc + "'>" + applyNotice.ADesc + "</td>");
                            builder.Append("<td class='text-left' title='" + applyNotice.FlowName + "'>" + applyNotice.FlowName + "</td>");
                            //0-新申请, 1-审核中, 2-已审核, 3-驳回, 4-草稿
                            string tStatusName = "未知";
                            switch (applyNotice.CurrentState.ToInt())
                            {
                                case 0:
                                    {
                                        tStatusName = "新申请";
                                        break;
                                    }
                                case 1:
                                    {
                                        tStatusName = "审批中";

                                        break;
                                    }
                                case 2:
                                    {
                                        tStatusName = "已审核";

                                        break;
                                    }
                                case 3:
                                    {
                                        tStatusName = "驳回";

                                        break;
                                    }
                                case 4:
                                    {
                                        tStatusName = "草稿";

                                        break;
                                    }
                            }
                            builder.Append("<td>" + tStatusName + "</td>");
                            builder.Append("<td>" + applyNotice.CreateTime + "</td>");
                            builder.Append("</tr>");
                        }
                        result.Data = builder.ToString();
                    }
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
    }
}