﻿using System;
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
    /// <summary>
    /// 外出
    /// </summary>
    public class My_GoOutController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("My_GoOutList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMy_GoOutList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["adesc"] ?? "";
                string beginTime = Request["beginTime"] ?? "";
                string endTime = Request["endTime"] ?? "";
                int status = Request["status"].ToInt();
                int index = 1;
                DataList<My_GoOutModel> list = My_GoOutBLL.GetMy_GoOutList(key, UserId, -1, beginTime, endTime, status, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left'>" + item.BName + "</td>");
                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.StartTime.ToDateFormat("yyyy-MM-dd") + "</td>");
                    builder.Append("<td class='text-left'>" + item.EndTime.ToDateFormat("yyyy-MM-dd") + "</td>");
                    builder.Append("<td class='text-left'>" + item.BKGName + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.FlowName + "-" + item.ApplyUserName + "</td>");
                    builder.Append("<td class='text-left'>" + item.CreateTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("<td class='text-left'>");
                    if (ApplyStatus.新申请.ToInt() == item.Status || ApplyStatus.驳回.ToInt() == item.Status)
                    {
                        builder.Append("<a href='Javascript:;' onclick='edit(" + item.Id + ")' >编辑</a>&nbsp;&nbsp;");
                        builder.Append("<a href='Javascript:;' onclick='del(" + item.Id + ")' >删除</a>&nbsp;&nbsp;");
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
                result.Message = ex.Message;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            My_GoOut model = new My_GoOut()
            {
                StartTime = DateTime.Now
            };
            try
            {
                if (id > 0)
                {
                    model = My_GoOutBLL.GetModel(id.ToInt());
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 保存数据
        [ValidateInput(false)]
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {

            My_GoOut model = FormHelper.GetRequestForm<My_GoOut>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateAccount = UserName;
                    row = My_GoOutBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.CreateAccount = UserName;
                    model.UpdateTime = model.CreateTime;
                    model.UpdateUserId = UserId;
                    model.UpdateAccount = UserName;
                    model.Status = ApplyStatus.新申请.ToInt();
                    //List<Approval_User> AppUserList = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.外出申请.ToInt());
                    //if (AppUserList.Count > 0)
                    //{
                    //    model.FK_ApprovalUserId = AppUserList[0].Id;
                    //    model.Id = My_GoOutBLL.AddModel(model);
                    //}
                    //else
                    //{
                    //    rest.Message = "当前申请未配置审批流程";
                    //    rest.Code = ResultCode.Failure;
                    //}
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
                int row = My_GoOutBLL.DelModelById(id);
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
