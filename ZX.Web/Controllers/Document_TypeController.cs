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
    public class Document_TypeController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int libraryId)
        {
            ViewBag.BackPageName = Request["pageName"];
            List<Document_Type> dList = Document_TypeBLL.GetList(d => d.Where(t =>t.FK_LibraryId==libraryId).OrderBy(t => t.DSort));
            //获取文件分类树形,用于部门编辑input
            var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
            ViewBag.TreeJson = items.ToJsonSerialize();
            ViewBag.FK_LibraryId = libraryId;
            return View("Document_TypeList");
        }
        #endregion

        #region 获取文件分类树形数据
        public JsonResult GetTree(int libraryId)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                StringBuilder builder = new StringBuilder();
                List<Document_Type> list = Document_TypeBLL.GetList(t =>t.Where(d=>d.FK_LibraryId== libraryId).OrderBy(a => a.DSort));
                foreach (var item in list.Where(t => t.ParentId == 0))
                {
                    builder.Append("<li>" + item.DName + "</a>");
                    builder.Append("<a href='javascript:;' onclick='docTypeDel(" + item.Id + ")' class='a_btn_menu'>删除</a>");
                    builder.Append("<a href='javascript:;' onclick='docTypeAddEdit(" + item.Id + "," + item.Id + ")' class='a_btn_menu'>子目录</a>");
                    builder.Append("<a href='javascript:;' onclick='docTypeEdit(" + item.Id + ")' class='a_btn_menu'>编辑</a>");

                    if (list.Where(t => t.ParentId == item.Id).Count() > 0)
                    {
                        builder.Append("<ul>");
                        Recursion(list, item.Id, ref builder);
                        builder.Append("</ul>");
                    }
                    builder.Append("</li>");
                }
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion


        #region 递归查询树形数据子集
        /// <summary>
        /// 递归查询子集
        /// </summary>
        /// <param name="nLists">子集</param>
        /// <param name="parentID">父id</param>
        /// <param name="builder">返回字符</param>
        public void Recursion(List<Document_Type> nLists, long parentID, ref StringBuilder builder)
        {
            foreach (var item in nLists.Where(t => t.ParentId == parentID))
            {
                builder.Append("<li>" + item.DName);
                builder.Append("<a href='javascript:;' onclick='docTypeDel(" + item.Id + ")' class='a_btn_menu'>删除</a>");
                builder.Append("<a href='javascript:;' onclick='docTypeAddEdit(" + item.Id + "," + item.Id + ")' class='a_btn_menu'>子目录</a>");
                builder.Append("<a href='javascript:;' onclick='docTypeEdit(" + item.Id + ")' class='a_btn_menu'>编辑</a>");
                if (nLists.Where(t => t.ParentId == item.Id).Count() > 0)
                {
                    builder.Append("<ul>");
                    Recursion(nLists, item.Id, ref builder);
                    builder.Append("</ul>");
                }
                builder.Append("</li>");
            }
        }
        #endregion

        #region 添加子分类

        /// <summary>
        /// 添加子部门
        /// </summary>
        /// <param name="id">目录分类</param>
        /// <param name="pid">父目录ID</param>
        /// <returns>html 拼接串</returns>
        public JsonResult GetDocTypeList(int? id, int? pid,int? libraryId)
        {
            AjaxResult result = new AjaxResult();
            StringBuilder builder = new StringBuilder();
            ViewBag.Pid = pid;
            try
            {
                List<Document_Type> dList = Document_TypeBLL.GetList(d => d.Where(t => t.FK_LibraryId== libraryId).OrderBy(t => t.DSort));
                builder.Append("<input type='hidden' id='pid' value='" + pid + "'/>");
                #region 拼凑导航
                builder.Append("<div class='panel-heading'>");
                if (pid.ToInt() >= 0 && id > 0)
                {
                    Document_Type dm = dList.Where(t => t.Id == id).ToList()[0];
                    //非顶级部门
                    if (dm.ParentId> 0)
                    {
                        RecursionPanelHeading(dList, dm.ParentId, ref builder);
                        builder.Append("<a href='javascript:;'>" + dm.DName + "<i class='icon-angle-right text-muted'></i>");
                    }
                    else
                    {
                        builder.Append("<strong>子目录 <i class='icon-double-angle-right'></i> </strong>");
                        builder.Append("<a href='javascript:;'>" + dm.DName + "<i class='icon-angle-right text-muted'></i>");
                    }
                }
                else
                {
                    builder.Append("<strong>类目<i class='icon-double-angle-right'></i> </strong>");
                }
                builder.Append("</div>");
                #endregion
                //循环输出编辑文本框
                builder.Append("<div class='panel-body' id='childList'>");
                foreach (var item in dList.Where(d => d.ParentId == id))
                {
                    builder.Append("<div class='form-group category' data-order='" + item.DSort + "'>");
                    builder.Append("<div class='col-xs-6 col-md-4 col-md-offset-2'>");
                    builder.Append("<input type='text' name='dept' id='" + item.Id + "' value='" + item.DName + "' class='form-control'>");
                    builder.Append("</div>");
                    builder.Append("<div class='col-xs-6 col-md-2'><i class='icon-move sort-handle'></i></div>");
                    builder.Append("</div>");
                }
                //额外追加5行空部门
                for (int i = 0; i < 5; i++)
                {
                    builder.Append("<div class='form-group category' data-order='" + dList.Count() + i + "'>");
                    builder.Append("<div class='col-xs-6 col-md-4 col-md-offset-2'>");
                    builder.Append("<input type='text' name='dept' id='' value='' class='form-control' placeholder='文档分类'>");
                    builder.Append("</div>");
                    builder.Append("<div class='col-xs-6 col-md-2'><i class='icon-move sort-handle'></i></div>");
                    builder.Append("</div>");
                }
                builder.Append("</div>");
            }
            catch (Exception)
            {

                throw;
            }

            result.Data = builder.ToString();
            return Json(result);
        }
        #region 递归生成子导航
        /// <summary>
        /// 递归查询子集
        /// </summary>
        /// <param name="nLists">子集</param>
        /// <param name="parentID">父id</param>
        /// <param name="builder">返回字符</param>
        public void RecursionPanelHeading(List<Document_Type> pLists, int? DParentId, ref StringBuilder builder)
        {
            Document_Type dm = pLists.Where(t => t.Id ==DParentId).ToList()[0];
            if (dm.ParentId == 0)
            {
                builder.Append("<strong>子分类 <i class='icon-double-angle-right'></i> </strong>");
                builder.Append("<a href='javascript:;'>" + dm.DName + "<i class='icon-angle-right text-muted'></i>");
            }
            else
            {
                builder.Append("<a href='javascript:;'>" + dm.DName + "<i class='icon-angle-right text-muted'></i>");
                RecursionPanelHeading(pLists, dm.ParentId, ref builder);
            }
        }
        #endregion

        #endregion

        #region 编辑类目
        /// <summary>
        /// 添加子部门
        /// </summary>
        /// <param name="id">类目Id</param>
        /// <param name="pid">类目父ID</param>
        /// <returns>html 拼接串</returns>
        public JsonResult Edit(int? id)
        {

            AjaxResult result = new AjaxResult();
            StringBuilder builder = new StringBuilder();
            try
            {
                //获取当前对象
                Document_Type model = Document_TypeBLL.GetModel(id);

                //获取上级部门名称
                string typePName = "顶级";
                if (model.ParentId> 0)
                {
                    Document_Type docType = Document_TypeBLL.GetModel(model.ParentId);
                    typePName = docType.DName;
                }

                //保存当前编辑对象id,页面隐藏
                builder.Append("<input type='hidden' id='tId' value='" + id + "'/>");
                builder.Append("<input type='hidden' id='pid' value='" + model.ParentId + "'/>");

                #region 拼凑导航

                builder.Append("<div class='panel-heading'>");
                builder.Append("<strong>编辑 <i class='icon-double-angle-right'></i> </strong>");
                builder.Append("</div>");

                #endregion
                //循环拼凑编辑页面
                builder.Append("<div class='panel-body' id='childList'>");
                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>上级类目</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' placeholder='上级类目' datatype='*' id='txtDTypeParent' value='" + typePName + "' class='form-control' readonly='readonly' onclick='showSysDeptTree()' />");
                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>类目名称<span style=\"color:red\">*</span></label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' name='DName' id='DName' value='" + model.DName + "' class='form-control'  datatype=\"*\" />");
                builder.Append("</div>");
                builder.Append("</div>");


                builder.Append("<div class='form-group'>");
                builder.Append("<label class='col-md-2 control-label'>描述</label>");
                builder.Append("<div class='col-md-9'>");
                builder.Append("<textarea name='DDescribe' id='DDescribe' class='form-control' rows='5'>" + model.DDescribe + "</textarea>");
                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>排序</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' name='DSort' id='DSort' value='" + model.DSort + "' class='form-control'/>");
                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("</div>");

            }
            catch (Exception)
            {

                throw;
            }
            result.Data = builder.ToString();
            return Json(result);
        }

        public JsonResult SaveEditData(string jsonData)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                Document_Type docType = jsonData.ToJsonDeserialize<Document_Type>();

                docType.UpdateTime = DateTime.Now;
                docType.UpdateUserId = UserId;
                docType.UpdateAccount = UserAccount;

                int rows = Document_TypeBLL.UpdateModel(docType);
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

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData(string jsonData, int? pid,int? libraryId)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                List<Document_Type> list = jsonData.ToJsonDeserialize<List<Document_Type>>();
                List<Document_Type> listType = new List<Document_Type>();
                foreach (var item in list)
                {
                    item.CreateUserId = UserId;
                    item.CreateTime = DateTime.Now;
                    item.CreateAccount = UserAccount;
                    listType.Add(item);
                }
                if (listType.Count() > 0)
                {
                    Document_TypeBLL.MergeModel(listType, "A.ParentId=B.ParentId and A.DName=B.DName and A.FK_LibraryId=B.FK_LibraryId", "A.FK_LibraryId=" + libraryId + " and A.ParentId="+ pid);
                    rest.Message = "保存成功";
                    rest.Code = ResultCode.Succeed;
                }
                else {
                    rest.Message = "无记录可保存";
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

        #region 删除数据不含子级
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public JsonResult DeleteDataNotChild(int? id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                if (id.ToInt() <= 0)
                {
                    rest.Code = ResultCode.Failure;
                    rest.Message = "未选中任何记录";
                    return Json(rest);
                }
                //判断是否有子级
                StringBuilder builder = new StringBuilder();

                if (Document_TypeBLL.GetList().Where(d => d.ParentId == id).Count() > 0)
                {
                    rest.Code = ResultCode.Failure;
                    rest.Message = "请先删除所选类目的子类目";
                    return Json(rest);
                }

                //根据当前所选部门id删除数据
                int row = Document_TypeBLL.DelModel(id);
                if (row == 0)
                {
                    rest.Message = "删除失败";
                    rest.Code = ResultCode.Failure;
                }
                rest.Message = "删除成功";
                rest.Code = ResultCode.Succeed;
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
