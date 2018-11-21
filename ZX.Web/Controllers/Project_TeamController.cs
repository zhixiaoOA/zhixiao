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
    public class Project_TeamController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("Project_TeamList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetProject_TeamList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                string key = Request["key"] ?? "";
                DataList<Project_TeamModel> list = Project_TeamBLL.GetProject_TeamList(key, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td><input type=\"checkbox\" class=\"cbox\" name=\"cbox\" value=\"" + item.Id + "\" /></td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Id + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.FK_ProjectId + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.FK_UserId + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Permissions + "</td>");
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
            Project_Team model = new Project_Team();
            try
            {
                if (id > 0)
                {
                    model = Project_TeamBLL.GetModel(id.ToInt());
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 项目团队批量添加
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult ProjectTeamEdit(int? projectId)
        {
            ViewBag.ProjectId = projectId;
            List<Project_Team> list = new List<Project_Team>();
            List<SelectListItem> teamUser = new List<SelectListItem>();
            try
            {
                List<Sys_User> uList = Sys_UserBLL.GetList();
                if (uList.Count() > 0)
                {
                    foreach (var item in uList)
                    {
                        teamUser.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                    }
                }
                //所有团队列表
                ViewBag.TeamUserList = teamUser;

                //当前项目团队
                if (projectId > 0)
                {
                    // 获取团队列表,目前作用(1)反填充下拉列表
                    List<Project_Team> teamList = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == projectId));
                    ViewBag.TeamList = teamList;
                }

                if (projectId > 0)
                {
                    list = Project_TeamBLL.GetList(d => d.Where(t => t.FK_ProjectId == projectId));
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(list);
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData(string jsonData, int pid)
        {
            AjaxResult rest = new AjaxResult();

            try
            {
                List<Project_Team> list = jsonData.ToJsonDeserialize<List<Project_Team>>();
                List<Project_Team> standardList = new List<Project_Team>();
                foreach (var item in list)
                {
                    item.UpdateUserId = UserId;
                    item.UpdateTime = DateTime.Now;
                    item.UpdateAccount = UserAccount;
                    standardList.Add(item);
                }
                Project_TeamBLL.MergeModel(standardList, "A.FK_UserId=B.FK_UserId and A.FK_ProjectId=B.FK_ProjectId and A.Permissions=B.Permissions", "A.FK_ProjectId=" + pid);
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
                int row = Project_TeamBLL.DelModel("Id IN(" + id + ")");
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