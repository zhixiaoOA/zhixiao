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
	[PermissionFilter]
    public class Approval_TypeController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Approval_TypeList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetApproval_TypeList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                string key = Request["key"] ?? "";
                DataList<Approval_TypeModel> list = Approval_TypeBLL.GetApproval_TypeList(key, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td><input type=\"checkbox\" class=\"cbox\" name=\"cbox\" value=\"" + item.Id + "\" /></td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Id + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.AName + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.TRemark + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.TSort + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateUserId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateTime + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateUserId + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateTime + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.CreateAccount + "</td>");
					builder.Append("<td onclick=\"onSelectTr(this)\">" + item.UpdateAccount + "</td>");

                    builder.Append("</tr>");
                }
                result.Data = builder.ToString();
                result.PageIndex = pageIndex;
                result.PageTotal = list.TotalPages;
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
            Approval_Type model = new Approval_Type();
            try
            {
                if (id > 0)
                {
                    model = Approval_TypeBLL.GetModel(id.ToInt());
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
            Approval_Type model = FormHelper.GetRequestForm<Approval_Type>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    row = Approval_TypeBLL.UpdateModel(model);
                }
                else
                {
                    model.Id = Approval_TypeBLL.AddModel(model);
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
                int row = Approval_TypeBLL.DelModel("Id IN(" + id + ")");
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
