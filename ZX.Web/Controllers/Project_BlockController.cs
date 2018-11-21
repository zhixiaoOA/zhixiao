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
    public class Project_BlockController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("Project_BlockList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetProject_BlockList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                string key = Request["key"] ?? "";
                DataList<Project_BlockModel> list = Project_BlockBLL.GetProject_BlockList(key, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr>");
                    builder.Append("<td><input type=\"checkbox\" class=\"cbox\" name=\"cbox\" value=\"" + item.Id + "\" /></td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.Id + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BType + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BName + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BWidth + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BColor + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.PTType + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.BOrderBy + "</td>");
                    builder.Append("<td onclick=\"onSelectTr(this)\">" + item.PTState + "</td>");

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
        public ActionResult AddEdit(int? id, int mid)
        {
            Project_Block model = new Project_Block();
            //保存当前菜单
            ViewBag.CurrentMid = mid;
            try
            {
                if (id > 0)
                {
                    model = Project_BlockBLL.GetModel(id.ToInt());
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
        public JsonResult SaveData(string jsonData)
        {
            Project_Block model = jsonData.ToJsonDeserialize<Project_Block>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    model.UpdateAccount = UserAccount;
                    row = Project_BlockBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateUserId = UserId;
                    model.CreateTime = DateTime.Now;
                    model.CreateAccount = UserAccount;
                    model.Id = Project_BlockBLL.AddModel(model);
                    if (model.Id > 0)
                    {
                        rest.Code = ResultCode.Succeed;
                    }
                    else
                    {
                        rest.Code = ResultCode.Failure;
                    }
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
            AjaxResult rest = new AjaxResult
            {
                Code = ResultCode.Succeed,
                Message = "删除成功"
            };
            try
            {
                int row = Project_BlockBLL.DelModelById(id);
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