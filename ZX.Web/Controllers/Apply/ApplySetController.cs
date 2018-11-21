using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;
using System.Text;

namespace ZX.Web.Controllers
{
    public class ApplySetController : BaseController
    {
        // GET: ApplySet
        public ActionResult Index()
        {
            return View("ApplySetList");
        }

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
                DataList<ApprovalModel> list = ApprovalBLL.GetApprovalList(name, pageIndex, pageSize);
                List<Sys_Menu> listMenuClassA = new List<Sys_Menu>();
                if (list.Count > 0)
                {
                    listMenuClassA = Sys_MenuBLL.GetList(d => d.Where(t => t.MParentId == 0));
                }
                StringBuilder builder = new StringBuilder();
                int index = 1;
                foreach (var item in list)
                {

                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.DName + "'>" + item.DName + "</td>");
                    builder.Append("<td class='text-left' title='" + item.DeptName + "'>" + item.DeptName + "</td>");
                    //builder.Append("<td class='text-left' title='" + (item.IsSdong == 1 ? "手动" : "自动") + "'>" + (item.IsSdong == 1 ? "手动" : "自动") + "</td>");
                    builder.Append("<td class='text-left' title='" + item.TypeName + "'>" + item.TypeName + "</td>");
                    builder.Append("<td class='text-left'>");
                    builder.Append(string.Format(CurrentBtnList28, item.Id));
                    builder.Append("<a href='javascript:'onclick = 'del(this)' val = '" + item.Id + "'>删除</a></td>");

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
            Approval model = new Approval()
            {
                FK_DeptId = 0
            };
            try
            {
                List<CompanyPosition> listPosition = CompanyPositionBLL.GetList(t => t.OrderBy(a => a.Sort));
                ViewBag.ListPosition = listPosition;
                List<Dictionary> listDic = DictionaryBLL.GetList(t => t.Where(a => a.ParentId == 35));
                ViewBag.ListDic = listDic;
                if (id > 0)
                {
                    model = ApprovalBLL.GetModel(id.ToInt());
                    List<Approval_User> listUser = Approval_UserBLL.GetList(t => t.Where(a => a.FK_ApprovalId == id));
                    ViewBag.ListUser = listUser;
                }
                List<Sys_Dept> list = Sys_DeptBLL.GetList(t => t.OrderBy(a => a.DSort));
                Sys_Dept dept = new Sys_Dept()
                {
                    Id = 0,
                    DName = "所有部门",
                    DParentId = -1,
                    DSort = -1
                };
                list.Add(dept);
                var items = list.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.DParentId.ToInt(), open = t.DParentId == -1 });
                ViewBag.TreeJson = items.ToJsonSerialize();
                ViewBag.DeptName = list.FirstOrDefault(t => t.Id == model.FK_DeptId).DName;
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
            Approval model = new Approval();
            try
            {
                if (id > 0)
                {
                    model = ApprovalBLL.GetModel(id.ToInt());
                    List<Approval_User> listUser = Approval_UserBLL.GetList(t => t.Where(a => a.FK_ApprovalId == id));
                    ViewBag.ListUser = listUser;
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
            AjaxResult rest = new AjaxResult();
            try
            {
                Approval model = FormHelper.GetRequestForm<Approval>();
                List<Approval_User> listUser = Request.Form["Users"].ToJsonDeserialize<List<Approval_User>>();
                if (model.Id > 0)
                {
                    //Approval checkModel = ApprovalBLL.GetModel(t => t.Where(a => a.FK_TypeId == model.FK_TypeId && a.FK_DeptId == model.FK_DeptId));
                    //if (checkModel==null||checkModel.Id == model.Id)
                    //{
                    //    ApprovalBLL.UpdateModel(model);
                    //}
                    //else
                    //{
                    //    rest.Message = "该审批流程已存在";
                    //    rest.Code = ResultCode.Failure;
                    //}
                    //author:jack date:20180512 desc:一个部门可以录入多条审批流程
                    ApprovalBLL.UpdateModel(model);
                }
                else
                {
                    //author:jack date:20180512 desc:一个部门可以录入多条审批流程
                    //bool bl = ApprovalBLL.CheckModel(t => t.Where(a => a.FK_DeptId == model.FK_DeptId && a.FK_TypeId == model.FK_TypeId));
                    //if (!bl)
                    //{
                    //    model.Id = ApprovalBLL.AddModel(model);
                    //}
                    //else
                    //{
                    //    rest.Message = "该审批流程已存在";
                    //    rest.Code = ResultCode.Failure;
                    //}
                    model.Id = ApprovalBLL.AddModel(model);
                }
                foreach (var item in listUser)
                {
                    item.FK_ApprovalId = model.Id;
                }
                Approval_UserBLL.DelModel(t=>t.Where(a=>a.FK_ApprovalId==model.Id));
                Approval_UserBLL.AddModel(listUser);
            }
            catch (Exception ex)
            {
                rest.Message = "保存失败";
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
                int row = ApprovalBLL.DelModelById(id);
                if (row == 0)
                {
                    rest.Message = "删除失败";
                    rest.Code = ResultCode.Failure;
                }
            }
            catch (Exception ex)
            {
                rest.Message = "删除失败";
                rest.Code = ResultCode.Failure;
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(rest);
        }
        #endregion
    }
}