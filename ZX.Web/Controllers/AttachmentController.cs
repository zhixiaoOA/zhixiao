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
    public class AttachmentController : BaseController
    {
        // GET: Attachment
        public ActionResult Index()
        {
            return View("AttachmentList");
        }


        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["name"] ?? "";
                int userId = 0;
                if (UserId == 1 || UserId == -1111)
                {
                    userId = -1;
                }
                else {
                    userId = UserId;
                }
                DataList<Project_TaskModel> list = Project_TaskBLL.GetProject_TaskList(name, pageIndex, pageSize,-1, userId,userId,userId,userId,userId);

                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.TName + "'>" + item.TName + "</td>");
                    string typeName = "任务附件";
                    //switch (item.DType)
                    //{
                    //    case 0:
                    //        {
                    //            typeName = "任务附件";
                    //            break;
                    //        }
                    //    case 10:
                    //        {
                    //            typeName = "审批附件";
                    //            break;
                    //        }
                    //    default:
                    //        break;
                    //}
                    builder.Append("<td class='text-left' title='" + typeName + "'>" + typeName + "</td>");
                    if (item.Attach.IsNotNullOrEmpty())
                    {
                        builder.Append("<td class='text-left'><a href='" + item.Attach + "' target='_blank'>下载附件</a></td>");
                    }
                    else {
                        builder.Append("<td></td>");
                    }
                    builder.Append("<td class='text-left'>");

                    //权限
                    if (RoleButtonList.Where(l=>l.BType==1||l.BType==1).Count()>0) {
                        builder.Append("<a href='javascript:edit(" + item.Id + "," + item.FK_ProjectId + ")'>编辑</a>");
                    }
                    if (RoleButtonList.Where(l => l.BType == 3).Count() > 0)
                    {
                        builder.Append("&nbsp;&nbsp;<a href='javascript:;' onclick='del(" + item.Id + "," + 1 + ")'>删除</a>");
                    }
                    if (RoleButtonList.Where(l => l.BType == 4).Count() > 0)
                    {
                        builder.Append("&nbsp;&nbsp;<a href='javascript:edit(" + item.Id + "," + item.FK_ProjectId + ")'>查看</a>");
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
                result.Message = "获取数据失败";
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">附件记录Id</param>
        /// <param name="delType">附件记录所属类型 1-任务附件,2-审批附件</param>
        /// <returns></returns>
        public JsonResult DelFileByDire(int id, int delType)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                if (delType == 1)
                {
                    Project_Task proTask = Project_TaskBLL.GetModel(id);
                    if (proTask == null || proTask.Attach.IsNullOrEmpty())
                    {
                        result.Message = "无附件可删除.";
                        result.Code = ResultCode.Failure;
                        return Json(result);
                    }

                    System.IO.File.Delete(Server.MapPath("~" + proTask.Attach));

                    proTask.UpdateAccount = UserAccount;
                    proTask.UpdateTime = DateTime.Now;
                    proTask.UpdateUserId = UserId;
                    proTask.Attach = "";
                    Project_TaskBLL.UpdateModel(proTask);

                    result.Code = ResultCode.Succeed;
                    result.Message = "删除成功";
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError("删除异常", ex);
            }

            return Json(result);
        }
    }
}