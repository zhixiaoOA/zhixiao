using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Model;
using ZX.BLL;
using ZX.Tools;
using System.Text;

namespace ZX.Web.Areas.api.Controllers
{
    public class ProjectTaskController : BaseController
    {
        #region 获取项目任务列表
        /// <summary>
        /// 获取项目任务列表
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="userId">用户id</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetList(string appId, string timestamp, string sign, long userId, string status, int pageIndex)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("userId", userId + "");
            pmts.Add("status", status + "");
            pmts.Add("pageIndex", pageIndex + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    DataList<Project_TaskModel> list = Project_TaskBLL.GetProjectTaskList(pageIndex, PageSize, status, userId);
                    result.Data = list;
                    result.PageTotal = list.TotalPages;
                }
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

        #region 根据id获取任务
        /// <summary>
        /// 根据id获取任务
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetModel(string appId, string timestamp, string sign, long id)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("id", id + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    Project_Task model = Project_TaskBLL.GetModel(id);
                    List<Project_TaskModel> listDetail = Project_TaskBLL.GetProjectTaskListByParentId(model.Id.ToInt());
                    result.Data = new { model, listDetail };
                }
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

        #region 修改任务状态-完成
        /// <summary>
        /// 修改任务状态-完成
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateWcStatus(string appId, string timestamp, string sign, long id)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("id", id + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    //完成主任务
                    Project_TaskBLL.UpdateModel(new Project_Task() { Id = id, TState = TaskStatus.已完成.ToInt() });
                    //完成子任务
                    Project_TaskBLL.UpdateModel(new Project_Task() { TState = TaskStatus.已完成.ToInt() }, t => t.Where(a => a.ParentId == id));
                }
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

        #region 修改任务状态-进行中
        /// <summary>
        /// 修改任务状态-进行中
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateKsStatus(string appId, string timestamp, string sign, long id)
        {
            ApiPmts pmts = new ApiPmts(System.Web.HttpContext.Current);
            pmts.Add("appId", appId);
            pmts.Add("timestamp", timestamp);
            pmts.Add("sign", sign);
            pmts.Add("id", id + "");
            AjaxResult result = CheckApiSign(pmts);
            try
            {
                if (result.Code == ResultCode.Succeed)
                {
                    //完成主任务
                    Project_TaskBLL.UpdateModel(new Project_Task() { Id = id, TState = TaskStatus.进行中.ToInt() });
                }
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
    }
}
