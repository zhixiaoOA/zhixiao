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
using Ionic.Zip;
using System.Drawing.Imaging;

namespace ZX.Web.Controllers
{
    /// <summary>
    /// 资产领取申请单
    /// </summary>
    public class AssetsUseController : BaseController
    {
        #region 申请单

        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("AssetsUseList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetAssetsUseList()
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
                DataList<AssetsUseModel> list = AssetsUseBLL.GetAssetsUseList(key, UserId, -1, beginTime, endTime, status, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
                    builder.Append("<td class='text-left'>" + item.Status.GetEnumName<ApplyStatus>() + "</td>");
                    builder.Append("<td class='text-left'>" + item.FlowName + "-" + item.ApplyUserName + "</td>");
                    builder.Append("<td class='text-left'>" + item.AddTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("<td class='text-left'>");
                    if (ApplyStatus.新申请.ToInt() == item.Status || ApplyStatus.驳回.ToInt() == item.Status)
                    {
                        builder.Append("<a href='Javascript:;' onclick='edit(" + item.Id + ")' >编辑</a>&nbsp;&nbsp;");
                        //builder.Append("<a href='Javascript:;' onclick='del(" + item.Id + ")' >删除</a>&nbsp;&nbsp;");
                    }
                    else
                    {
                        builder.Append("<a href='Javascript:;' onclick='detail(" + item.Id + ")' >查看</a>&nbsp;&nbsp;");
                    }
                    if (ApplyStatus.已审核.ToInt() == item.Status)
                    {
                        builder.Append("<a href='Javascript:;' onclick='qrcode(" + item.Id + ")' >生成二维码</a>&nbsp;&nbsp;");
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
            AssetsUse model = new AssetsUse()
            {
                AddTime = DateTime.Now
            };
            try
            {
                Sys_UserModel user = Sys_UserBLL.GetUserById(UserId);
                ViewBag.User = user;
                if (id > 0)
                {
                    model = AssetsUseBLL.GetModel(id.ToInt());
                    List<AssetsUseDetail> listDetail = AssetsUseDetailBLL.GetList(t => t.Where(a => a.FK_AssetsUseId == model.Id));
                    ViewBag.ListDetail = listDetail;
                    int typeId = ApplyType.资产领取申请单.ToInt();
                    List<Approval_Log> listLog = Approval_LogBLL.GetList(t => t.Where(a => a.FK_ApplyFlowId == id && a.FK_TypeId == typeId));
                    ViewBag.ListLog = listLog;
                }
                List<Assets> listAssets = AssetsBLL.GetList(t => t.Where(a => a.ANum > 0));
                ViewBag.ListAssets = listAssets;
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

            AssetsUse model = FormHelper.GetRequestForm<AssetsUse>();
            AjaxResult rest = new AjaxResult();
            try
            {
                List<AssetsUseDetail> listDetail = Request.Form["detail"].ToJsonDeserialize<List<AssetsUseDetail>>();
                if (listDetail.GroupBy(t => t.FK_AssetsId).Select(t => new { key = t.Key, count = t.Count() }).Count(t => t.count > 1) > 1)
                {
                    rest.Message = "资产明细重复";
                    rest.Code = ResultCode.Failure;
                }
                else
                {
                    int row = 0;
                    if (model.Id > 0)
                    {
                        model.UpdateTime = DateTime.Now;
                        if (model.Status == ApplyStatus.驳回.ToInt())
                        {
                            model.Status = ApplyStatus.审核中.ToInt();
                            List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.资产领取申请单.ToInt(), DeptId);
                            if (listApplyUser.Count > 0)
                            {
                                model.FK_ApprovalUserId = listApplyUser[0].Id;
                                row = AssetsUseBLL.UpdateModel(model);
                            }
                            else
                            {
                                rest.Message = "当前申请未配置审批流程";
                                rest.Code = ResultCode.Failure;
                            }
                        }
                        else
                        {
                            row = AssetsUseBLL.UpdateModel(model);
                        }
                    }
                    else
                    {
                        model.AddTime = DateTime.Now;
                        model.FK_UserId = UserId;
                        model.UpdateTime = model.AddTime;
                        model.Status = ApplyStatus.新申请.ToInt();
                        List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.资产领取申请单.ToInt(), DeptId);
                        if (listApplyUser.Count > 0)
                        {
                            model.FK_ApprovalUserId = listApplyUser[0].Id;
                            model.Id = AssetsUseBLL.AddModel(model);
                        }
                        else
                        {
                            rest.Message = "当前申请未配置审批流程";
                            rest.Code = ResultCode.Failure;
                        }
                    }
                    foreach (var item in listDetail)
                    {
                        item.FK_AssetsUseId = model.Id;
                        item.NatureOfAssets = "资产入库";
                    }
                    AssetsUseDetailBLL.MergeModel(listDetail, "A.FK_AssetsUseId=B.FK_AssetsUseId AND A.FK_AssetsId=B.FK_AssetsId", "A.FK_AssetsUseId=" + model.Id);
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
                int row = AssetsUseBLL.DelModelById(id);
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

        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateQrCode(long id)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "filename=" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".zip");
                List<AssetsUseDetailModel> listDetail = AssetsUseDetailBLL.GetAssetsUseDetailByAssetsUseId(id);
                string savePath = Server.MapPath("~/QrCodeImage");
                if (!Directory.Exists(savePath))
                {
                    //创建文件
                    Directory.CreateDirectory(savePath);
                }
                else
                {
                    //清空文件
                    BaseHelper.DeleteFiles(Server.MapPath("~/QrCodeImage") + "\\");
                }
                string qrUrl = "", filePath = "";
                foreach (AssetsUseDetailModel item in listDetail)
                {
                    filePath = savePath + "\\" + item.DeptName + "\\" + item.AName;
                    if (!Directory.Exists(filePath))
                    {
                        //创建文件
                        Directory.CreateDirectory(filePath);
                    }
                    for (int i = 1; i <= item.DCount; i++)
                    {
                        qrUrl = "http://mhoa.gxgxun.com/AssetsQrCode/QrCode?id=" + item.FK_AssetsId + "&dept=" + item.DeptName + "&name=" + i + "号" + item.AName;
                        System.Drawing.Image image = QrCodeHelper.CreateQRCode(qrUrl);
                        image.Save(filePath + "\\" + i + "号" + item.AName + ".png");
                    }
                }
                using (ZipFile zip = new ZipFile(System.Text.Encoding.Default))//解决中文乱码问题
                {
                    zip.AddDirectory(savePath);
                    zip.Save(Response.OutputStream);
                }
                Response.End();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return null;
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
                int index = 1;
                DataList<AssetsUseModel> list = AssetsUseBLL.GetAssetsUseList(key, -1, UserId, beginTime, endTime, status, pageIndex, PageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left'>" + item.RealName + "</td>");
                    builder.Append("<td class='text-left'>" + item.PositionName + "</td>");
                    builder.Append("<td class='text-left'>" + item.DeptName + "</td>");
                    builder.Append("<td class='text-left'>" + item.ADesc + "</td>");
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
            AssetsUse model = new AssetsUse();
            try
            {
                ViewBag.UserId = UserId;
                model = AssetsUseBLL.GetModel(id.ToInt());
                Sys_UserModel user = Sys_UserBLL.GetUserById(model.FK_UserId);
                ViewBag.User = user;
                int typeId = ApplyType.资产领取申请单.ToInt();
                List<Approval_Log> listLog = Approval_LogBLL.GetList(t => t.Where(a => a.FK_ApplyFlowId == id && a.FK_TypeId == typeId));
                ViewBag.ListLog = listLog;
                List<AssetsUseDetailModel> listDetail = AssetsUseDetailBLL.GetAssetsUseDetailByAssetsUseId(model.Id);
                ViewBag.ListDetail = listDetail;
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
            AssetsUse model = new AssetsUse();
            AjaxResult rest = new AjaxResult();
            try
            {
                model.Id = Request.Form["Id"].ToLong();
                //获取所有审批流程
                List<Approval_User> listApplyUser = Approval_UserBLL.GetApprovalUserByTypeId(ApplyType.资产领取申请单.ToInt(), Request.Form["deptId"].ToLong());
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
                        FK_TypeId = ApplyType.资产领取申请单.ToInt(),
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
                        FK_TypeId = ApplyType.资产领取申请单.ToInt(),
                        LContent = approval_User.FlowName + "[同意]，" + applyMsg
                    };
                    Approval_LogBLL.AddModel(log);
                }
                AssetsUseBLL.UpdateModel(model);
                if (model.Status == ApplyStatus.已审核.ToInt())
                {
                    Sys_UserModel user = Sys_UserBLL.GetUserById(model.FK_UserId);
                    List<AssetsUseDetail> listDetail = AssetsUseDetailBLL.GetList(t => t.Where(a => a.FK_AssetsUseId == model.Id));
                    foreach (var item in listDetail)
                    {
                        AssetsBLL.UpdateAssetsNum(item.Id, user.DeptName + user.RealName + "使用资产");
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

        #endregion
    }
}
