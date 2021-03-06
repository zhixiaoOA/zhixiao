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
    public class NewsController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("NewsList");
        }
        #endregion

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

                int userId = UserId == 1?-1:UserId;

                DataList<NewsModel> list = NewsBLL.Proc_GetNewsListByUserId(name, userId, -1, pageIndex, pageSize);
                List<Sys_User> listUser = new List<Sys_User>();
                if (list != null)
                {
                    listUser = Sys_UserBLL.GetList();
                }
                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.DName + "'>" + item.DName + "</td>");
                    string typeName = "公司公告";
                    switch (item.DType)
                    {
                        case 0:
                            {
                                typeName = "公司公告";
                                break;
                            }
                        case 10:
                            {
                                typeName = "新闻";
                                break;
                            }
                        default:
                            break;
                    }
                    builder.Append("<td class='text-left' title='" + typeName + "'>" + typeName + "</td>");
                    //builder.Append("<td class='text-left' title='" + item.DContent + "'>" + item.DContent + "</td>");
                    builder.Append("<td class='text-left' title='" + item.CreateTime + "'>" + item.CreateTime + "</td>");

                    string userName = listUser.Where(l => l.Id == item.CreateUserId).FirstOrDefault().RealName;
                    builder.Append("<td class='text-left' title='" + userName + "'>" + userName + "</td>");

                    builder.Append("<td class='text-left' title='" + item.UpdateTime + "'>" + item.UpdateTime + "</td>");
                    string updateUserName = "";
                    if (item.UpdateUserId.IsNotNullOrEmpty())
                    {
                        updateUserName = listUser.Where(l => l.Id == item.UpdateUserId).FirstOrDefault().RealName;
                    }
                    builder.Append("<td class='text-left' title='" + updateUserName + "'>" + updateUserName + "</td>");
                    builder.Append("<td class='text-left' title='" + item.DSort + "'>" + item.DSort + "</td>");
                    if (item.DImageUrl.IsNotNullOrEmpty())
                    {
                        builder.Append("<td class='text-left'><a href='" + item.DImageUrl + "'>下载</a></td>");
                    }
                    else
                    {
                        builder.Append("<td></td>");
                    }



                    //按钮权限可见性控制
                    List<Sys_Button> btnList = RoleButtonList;

                    builder.Append("<td class='text-left'>");

                    builder.Append(string.Format(CurrentBtnList28, item.Id));

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

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
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

        #region 查看跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult Readonly(int? id)
        {
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

        #region 保存数据
        [ValidateInput(false)]
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            News model = FormHelper.GetRequestForm<News>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;

                    row = NewsBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;

                    model.Id = NewsBLL.AddModel(model);
                }
                rest.Code = ResultCode.Succeed;
                rest.Message = "保存成功";
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
                int row = NewsBLL.DelModelById(id);
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
