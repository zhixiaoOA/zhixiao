﻿using System;
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
    public class Temporary_Task_BlockController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Temporary_Task_BlockList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetTemporary_Task_BlockList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                string key = Request["key"] ?? "";
                DataList<Temporary_Task_BlockModel> list = Temporary_Task_BlockBLL.GetTemporary_Task_BlockList(key, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td><input type=\"checkbox\" class=\"cbox\" name=\"cbox\" value=\"" + item.Id + "\" /></td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Id + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BName + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BWidth + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BColor + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.TType + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.RowCounts + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BOrderBy + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.TState + "</td>");
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
            Temporary_Task_Block model = new Temporary_Task_Block();
            try
            {
                if (id > 0)
                {
                    model = Temporary_Task_BlockBLL.GetModel(id.ToInt());
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
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            Temporary_Task_Block model = FormHelper.GetRequestForm<Temporary_Task_Block>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateAccount = UserAccount;
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    row = Temporary_Task_BlockBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateUserId = UserId;
                    model.CreateTime = DateTime.Now;
                    model.Id = Temporary_Task_BlockBLL.AddModel(model);
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
                int row = Temporary_Task_BlockBLL.DelModel("Id IN(" + id + ")");
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