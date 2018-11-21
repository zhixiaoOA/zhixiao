using System;
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
    /// 报销
    /// </summary>
    public class My_BaoXiaoController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("My_BaoXiaoList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMy_BaoXiaoList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["name"] ?? "";
                string kemu = Request["kemu"] ?? "";
                string kemuType = Request["kemuType"] ?? "";
                int status = Request["status"].ToInt();
                int index = 1;
                DataList<My_BaoXiaoModel> list = My_BaoXiaoBLL.GetMy_BaoXiaoList(key, UserId, -1, kemuType, kemu, status, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left'>" + item.BName + "</td>");
                    builder.Append("<td class='text-left'>" + item.SubjectType + "</td>");
                    builder.Append("<td class='text-left'>" + item.Subject + "</td>");
                    builder.Append("<td class='text-left'>" + item.Currency + "</td>");
                    builder.Append("<td class='text-left'>" + item.EndHour + "</td>");
                    builder.Append("<td class='text-left'><a href='" + item.Attachment + "' target='_blank'>下载附件</a></td>");
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
            My_BaoXiao model = new My_BaoXiao();
            try
            {
                List<Dictionary> listType = DictionaryBLL.GetList(t => t.Where(a => a.ParentId == 7 || a.ParentId == 10 || a.ParentId == 14).OrderBy(a => a.Sort));
                ViewBag.ListType = listType;
                List<Sys_User> listUser = Sys_UserBLL.GetList();
                ViewBag.ListUser = listUser;
                if (id > 0)
                {
                    model = My_BaoXiaoBLL.GetModel(id.ToInt());
                    List<My_BaoXiao_Detail> listDetail = My_BaoXiao_DetailBLL.GetList(t => t.Where(a => a.FK_BaoXiaoId == id));
                    ViewBag.ListDetail = listDetail;
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

            My_BaoXiao model = FormHelper.GetRequestForm<My_BaoXiao>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateAccount = UserName;
                    row = My_BaoXiaoBLL.UpdateModel(model);
                    List<My_BaoXiao_Detail> listDetail = Request["details"].ToJsonDeserialize<List<My_BaoXiao_Detail>>();
                    foreach (var item in listDetail)
                    {
                        item.FK_BaoXiaoId = model.Id;
                    }
                    My_BaoXiao_DetailBLL.MergeModel(listDetail, "A.DDate=B.DDate AND A.EndHour=B.EndHour AND A.BaoXiaoUserId=B.BaoXiaoUserId", "A.FK_BaoXiaoId=" + model.Id);
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
                    //List<Approval_User> AppUserList = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.报销申请.ToInt());
                    //if (AppUserList.Count > 0)
                    //{
                    //    model.FK_ApprovalUserId = AppUserList[0].Id;
                    //    model.Id = My_BaoXiaoBLL.AddModel(model);
                    //    List<My_BaoXiao_Detail> listDetail = Request["details"].ToJsonDeserialize<List<My_BaoXiao_Detail>>();
                    //    foreach (var item in listDetail)
                    //    {
                    //        item.FK_BaoXiaoId = model.Id;
                    //    }
                    //    My_BaoXiao_DetailBLL.AddModel(listDetail);
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
                int row = My_BaoXiaoBLL.DelModelById(id);
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
