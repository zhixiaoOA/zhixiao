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
    /// 所需物品领用申请单
    /// </summary>
    public class MyGooodsUseController : BaseController
    {
        #region 申请单

        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("MyGooodsUseList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetMyGooodsUseList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["adesc"] ?? "";
                string beginTime = Request["beginTime"] ?? "";
                string endTime = Request["endTime"] ?? "";
                string status = Request["status"] + "";
                int index = 1;
                DataList<MyGooodsUseModel> list = MyGooodsUseBLL.GetMyGooodsUseList(key, UserId, -1, beginTime, endTime, status, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();


                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left'>" + item.YanName + "</td>");
                    builder.Append("<td class='text-left'>" + item.YanCount + item.YanUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.JiuName + "</td>");
                    builder.Append("<td class='text-left'>" + item.JiuCount + item.JiuUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.OtherName + "</td>");
                    builder.Append("<td class='text-left'>" + item.OtherCount + item.OrtherUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.FlowName + "-" + item.ApplyUserName + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("<td class='text-left'>");
                    if (ApplyStatus.草稿.ToInt() == item.Status || ApplyStatus.驳回.ToInt() == item.Status)
                    {
                        builder.Append("<a href='Javascript:;' onclick='edit(" + item.Id + ")' >编辑</a>&nbsp;&nbsp;");
                        //builder.Append("<a href='Javascript:;' onclick='del(" + item.Id + ")' >删除</a>&nbsp;&nbsp;");
                    }
                    else
                    {
                        builder.Append("<a href='Javascript:;' onclick='detail(" + item.Id + ")' >查看</a>&nbsp;&nbsp;");
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
            MyGooodsUse model = new MyGooodsUse()
            {
                Status = ApplyStatus.新申请.ToInt(),
                AddTime = DateTime.Now
            };
            try
            {
                int typeId = ApplyType.所需物品领用申请单.ToInt();
                Sys_UserModel user = Sys_UserBLL.GetUserById(UserId);
                ViewBag.User = user;
                if (id > 0)
                {
                    model = MyGooodsUseBLL.GetModel(id.ToInt());

                    List<Approval_Log> listLog = Approval_LogBLL.GetList(t => t.Where(a => a.FK_ApplyFlowId == id && a.FK_TypeId == typeId));
                    ViewBag.ListLog = listLog;
                }

                //获取烟/酒/其它 单位
                List<Dictionary> list = DictionaryBLL.GetList();

                //烟
                List<Dictionary> listYan = list.Where(l => l.ParentId == 67).OrderBy(l => l.Sort).ToList();
                ViewBag.ListYan = listYan;

                //酒
                List<Dictionary> listJiu = list.Where(l => l.ParentId == 71).OrderBy(l => l.Sort).ToList();
                ViewBag.ListJiu = listJiu;

                //其它
                List<Dictionary> listOrther = list.Where(l => l.ParentId == 74).OrderBy(l => l.Sort).ToList();
                ViewBag.ListOrther = listOrther;

                //获取单据流程
                List<Approval> listApproval = ApprovalBLL.GetListByTypeId(typeId, DeptId);
                ViewBag.ListApproval = listApproval;
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

            MyGooodsUse model = FormHelper.GetRequestForm<MyGooodsUse>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateTime = DateTime.Now;
                    if (model.Status == ApplyStatus.驳回.ToInt())
                    {
                        model.Status = ApplyStatus.审核中.ToInt();
                        List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.所需物品领用申请单.ToInt(), DeptId);
                        if (listApplyUser.Count > 0)
                        {
                            model.FK_ApprovalUserId = listApplyUser[0].Id;
                            //判断当前职位是否与流程职位相等,如果相等则跳转到下一个步骤,如果没有下一个步骤,则为自己审批,直接审核通过
                            if (PositionId == listApplyUser[0].FK_CompanyPositionId)
                            {
                                Approval_User appUser = listApplyUser.FirstOrDefault(t => t.Id > listApplyUser[0].Id);
                                if (appUser == null)
                                {
                                    model.Status = ApplyStatus.已审核.ToInt();
                                }
                                else
                                {
                                    model.FK_ApprovalUserId = appUser.Id;
                                }
                            }
                            row = MyGooodsUseBLL.UpdateModel(model);
                        }
                        else
                        {
                            rest.Message = "当前申请未配置审批流程";
                            rest.Code = ResultCode.Failure;
                        }
                    }
                    else
                    {
                        row = MyGooodsUseBLL.UpdateModel(model);
                    }
                }
                else
                {
                    model.AddTime = DateTime.Now;
                    model.FK_UserId = UserId;
                    model.UpdateTime = model.AddTime;

                    List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.所需物品领用申请单.ToInt(), DeptId);
                    if (listApplyUser.Count > 0)
                    {
                        if (model.FK_ApprovalUserId.ToInt() == 0)
                        {
                            model.FK_ApprovalUserId = listApplyUser[0].Id;
                            //判断当前职位是否与流程职位相等,如果相等则跳转到下一个步骤,如果没有下一个步骤,则为自己审批,直接审核通过
                            if (PositionId == listApplyUser[0].FK_CompanyPositionId)
                            {
                                Approval_User appUser = listApplyUser.FirstOrDefault(t => t.Id > listApplyUser[0].Id);
                                if (appUser == null)
                                {
                                    model.Status = ApplyStatus.已审核.ToInt();
                                }
                                else
                                {
                                    model.FK_ApprovalUserId = appUser.Id;
                                }
                            }
                        }
                        model.Id = MyGooodsUseBLL.AddModel(model);
                    }
                    else
                    {
                        rest.Message = "当前申请未配置审批流程";
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
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = MyGooodsUseBLL.DelModelById(id);
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

        #region 打印跳转
        public ActionResult My_GooodsUsePrint(int id)
        {
            MyGooodsUse model = new MyGooodsUse();
            try
            {
                int typeId = ApplyType.申请单.ToInt();
                Sys_UserModel user = Sys_UserBLL.GetUserById(UserId);
                ViewBag.User = user;
                if (id > 0)
                {
                    model = MyGooodsUseBLL.GetModel(id.ToInt());

                    //通过审批日志 获取用户签名
                    int fk_Typeid = ApplyType.所需物品领用申请单.ToInt();

                    List<Sys_User> userList = Approval_LogBLL.GetUserListByApprovalLog(1, fk_Typeid, model.Id.ToInt());

                    int userCount = userList.Count;

                    if (userCount > 0)
                    {
                        ViewBag.UserList = userList;
                    }

                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #endregion

        #region 审批单

        /// <summary>
        /// 审批跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult AuthIndex()
        {
            return View("MyApprovalList");
        }

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetApprovalList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(1);
                string key = Request["adesc"] ?? "";
                string beginTime = Request["beginTime"] ?? "";
                string endTime = Request["endTime"] ?? "";
                string status = Request["status"] + "";
                int ctype = Request["ctype"].ToInt(1);
                int index = 1;
                DataList<MyGooodsUseModel> list = new DataList<MyGooodsUseModel>();
                if (ctype == 1)
                {
                    list = MyGooodsUseBLL.GetMyGooodsUseList(key, -1, UserId, beginTime, endTime, status, pageIndex, pageSize);
                }
                else
                {
                    list = MyGooodsUseBLL.GetMyGooodsUseList(key, UserId, beginTime, endTime, pageIndex, pageSize);
                }
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left'>" + item.RealName + "</td>");
                    builder.Append("<td class='text-left'>" + item.DeptName + "</td>");
                    builder.Append("<td class='text-left'>" + item.YanName + "</td>");
                    builder.Append("<td class='text-left'>" + item.YanCount + item.YanUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.JiuName + "</td>");
                    builder.Append("<td class='text-left'>" + item.JiuCount + item.JiuUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.OtherName + "</td>");
                    builder.Append("<td class='text-left'>" + item.OtherCount + item.OrtherUnitName + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.FlowName + "-" + item.ApplyUserName + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("<td class='text-left'>");
                    if (ApplyStatus.新申请.ToInt() == item.Status || ApplyStatus.审核中.ToInt() == item.Status)
                    {
                        builder.Append("<a href='Javascript:;' onclick='edit(" + item.Id + ")' >审批</a>&nbsp;&nbsp;");
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

        #region 审批明细跳转
        /// <summary>
        /// 审批明细跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult ApprovalDetail(int? id)
        {
            MyGooodsUse model = new MyGooodsUse();
            try
            {
                ViewBag.UserId = UserId;
                model = MyGooodsUseBLL.GetModel(id.ToInt());
                Sys_UserModel user = Sys_UserBLL.GetUserById(model.FK_UserId);
                ViewBag.User = user;
                int typeId = ApplyType.所需物品领用申请单.ToInt();
                List<Approval_Log> listLog = Approval_LogBLL.GetList(t => t.Where(a => a.FK_ApplyFlowId == id && a.FK_TypeId == typeId));
                ViewBag.ListLog = listLog;

            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 保存审批
        /// <summary>
        /// 保存审批
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveApproval()
        {
            MyGooodsUse model = new MyGooodsUse();
            AjaxResult rest = new AjaxResult();
            try
            {
                model.Id = Request.Form["Id"].ToLong();
                //获取所有审批流程
                List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.所需物品领用申请单.ToInt(), Request.Form["deptId"].ToLong());
                string applyMsg = Request.Form["applyMsg"];
                int status = Request.Form["status"].ToInt();
                Approval_User approval_User = listApplyUser.FirstOrDefault(t => t.FK_CompanyPositionId == PositionId);
                if (status == ApplyStatus.驳回.ToInt())
                {
                    Approval_Log log = new Approval_Log()
                    {
                        CreateAccount = UserId + "",
                        CreateTime = DateTime.Now,
                        CreateUserId = UserId,
                        FK_ApplyFlowId = model.Id,
                        FK_TypeId = ApplyType.所需物品领用申请单.ToInt(),
                        Status = 1,
                        LContent = approval_User.FlowName + "[驳回]，" + applyMsg
                    };
                    Approval_LogBLL.AddModel(log);
                    model.Status = ApplyStatus.驳回.ToInt();
                }
                else
                {

                    Approval_User approvalUserNext = listApplyUser.FirstOrDefault(t => t.Id > approval_User.Id);
                    //判断是否有下一步流程
                    if (approvalUserNext != null)
                    {
                        model.Status = ApplyStatus.审核中.ToInt();
                        model.FK_ApprovalUserId = approvalUserNext.Id;
                    }
                    else
                    {
                        model.Status = ApplyStatus.已审核.ToInt();
                    }
                    Approval_Log log = new Approval_Log()
                    {
                        CreateAccount = UserId + "",
                        CreateTime = DateTime.Now,
                        CreateUserId = UserId,
                        FK_ApplyFlowId = model.Id,
                        FK_TypeId = ApplyType.所需物品领用申请单.ToInt(),
                        Status = 0,
                        LContent = approval_User.FlowName + "[同意]，" + applyMsg
                    };
                    Approval_LogBLL.AddModel(log);
                }
                MyGooodsUseBLL.UpdateModel(model);
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

        #endregion
    }
}
