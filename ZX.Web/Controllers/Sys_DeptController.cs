using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZX.Model;
using ZX.BLL;
using System.Text;
using ZX.Tools;

namespace ZX.Web.Controllers
{
    public class Sys_DeptController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Sys_Dept> deptList = Sys_DeptBLL.GetList(d => d.Where(t => 1 == 1).OrderBy(t => t.DDesc));
            //获取部门树形,用于部门编辑input
            var items = deptList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.DParentId.ToInt(), open = t.DParentId - 1 });
            ViewBag.TreeJson = items.ToJsonSerialize();
            ViewBag.RealName = RealName;
            return View("AddEdit");
        }
        #endregion

        #region 获取部门树形数据
        public JsonResult GetList()
        {
            Tools.AjaxResult result = new Tools.AjaxResult();
            try
            {
                StringBuilder builder = new StringBuilder();
                List<Sys_Dept> deptList = Sys_DeptBLL.GetList(t => t.OrderBy(a => a.DSort));
                foreach (var item in deptList.Where(t => t.DParentId == 0))
                {
                    builder.Append("<li>" + item.DName + "</a>");
                    //按钮权限可见性控制
                    builder.Append(string.Format(CurrentBtnList30, item.Id));

                    if (deptList.Where(t => t.DParentId == item.Id).Count() > 0)
                    {
                        builder.Append("<ul>");
                        Recursion(deptList, item.Id, ref builder);
                        builder.Append("</ul>");
                    }
                    builder.Append("</li>");
                }
                result.Data = builder.ToString();
            }
            catch (Exception e)
            {
                Log4Helper.WriteError(e.Message, e);
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
        public void Recursion(List<Sys_Dept> nLists, long parentID, ref StringBuilder builder)
        {
            foreach (var item in nLists.Where(t => t.DParentId == parentID))
            {
                builder.Append("<li>" + item.DName);

                //按钮权限可见性控制
                builder.Append(string.Format(CurrentBtnList30, item.Id));

                if (nLists.Where(t => t.DParentId == item.Id).Count() > 0)
                {
                    builder.Append("<ul>");
                    Recursion(nLists, item.Id, ref builder);
                    builder.Append("</ul>");
                }
                builder.Append("</li>");
            }
        }
        #endregion

        #region 保存部门修改信息
        public JsonResult SaveEditData(string jsonData)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                Sys_Dept dept = jsonData.ToJsonDeserialize<Sys_Dept>();

                dept.UpdateTime = DateTime.Now;
                dept.UpdateUserId = UserId;
                int rows = Sys_DeptBLL.UpdateModel(dept);
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
        #region 添加子部门
        /// <summary>
        /// 添加子部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="pid">父部门ID</param>
        /// <returns>html 拼接串</returns>
        public JsonResult GetDeptList(int? id, int? pid)
        {
            AjaxResult result = new AjaxResult();
            StringBuilder builder = new StringBuilder();
            try
            {
                ViewBag.Pid = pid;
                List<Sys_Dept> list = Sys_DeptBLL.GetList(d => d.Where(t => 1 == 1).OrderBy(t => t.DSort));

                // 拼接HTML代码
                builder.Append("<input type='hidden' id='pid' value='" + pid + "' />");
                builder.Append("<input type='hidden' id='currentOption' value='editChileDept'/>");

                builder.Append("<div class='panel-heading'>");

                #region 拼凑导航
                //判断非顶级部门
                if (pid.ToInt() >= 0 && id > 0)
                {
                    Sys_Dept dept = list.Where(t => t.Id == id).ToList()[0];
                    if (dept.DParentId > 0)
                    {
                        RecursionPanelHeading(list, dept.DParentId, ref builder);
                        builder.Append("<a href='javascript:;'>" + dept.DName + "<i class='icon-angle-right text-muted'></i>");
                    }
                    else
                    {
                        builder.Append("<strong>子部门 <i class='icon-double-angle-right'></i> </strong>");
                        builder.Append("<a href='javascript:;'>" + dept.DName + "<i class='icon-angle-right text-muted'></i>");
                    }

                }
                else
                {
                    builder.Append("<strong>部门结构 <i class='icon-double-angle-right'></i> </strong>");
                }
                builder.Append("</div>");
                #endregion

                // 循环输出编辑框文本
                builder.Append("<div class='panel-body' id='childList'>");
                foreach (var item in list.Where(t => t.DParentId == id))
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
                    builder.Append("<div class='form-group category' data-order='" + list.Count() + i + "'>");
                    builder.Append("<div class='col-xs-6 col-md-4 col-md-offset-2'>");
                    builder.Append("<input type='text' name='dept' id='' value='' class='form-control' placeholder='部门结构'>");
                    builder.Append("</div>");
                    builder.Append("<div class='col-xs-6 col-md-2'><i class='icon-move sort-handle'></i></div>");
                    builder.Append("</div>");
                }
                builder.Append("<div class='form-group'>");
                builder.Append("<div class='col-xs-8 col-md-offset-2'>");
                //角色按钮权限
                //builder.Append("<button type='button' onclick='saveDate()' id='submit' class='btn btn-primary' data-loading='稍候...'>保存</button>");
                builder.Append(CurrentBtnList29);

                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("</div>");
            }
            catch (Exception e)
            {
                Log4Helper.WriteError("获取部门编辑信息异常", e);
            }
            result.Data = builder.ToString();
            return Json(result);
        }
        #endregion

        #region 递归生成子导航
        /// <summary>
        /// 递归查询子集
        /// </summary>
        /// <param name="nLists">子集</param>
        /// <param name="parentID">父id</param>
        /// <param name="builder">返回字符</param>
        public void RecursionPanelHeading(List<Sys_Dept> dList, int? DparentId, ref StringBuilder builder)
        {
            Sys_Dept dept = dList.Where(t => t.Id == DparentId).ToList()[0];
            if (dept.DParentId == 0)
            {
                builder.Append("<Strong>子部门 <i class='icon-double-angle-right'></i></Strong>");
                builder.Append("<a href='javascript:;'>" + dept.DName + "<i class='icon-angle-right text-muted'></i>");
            }
            else
            {
                builder.Append("<a href='javascript:;'>" + dept.DName + "<i class='icon-angle-right text-muted'></i>");
                RecursionPanelHeading(dList, dept.DParentId, ref builder);
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData(string jsonData, int? pid)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                List<Sys_Dept> list = jsonData.ToJsonDeserialize<List<Sys_Dept>>();
                List<Sys_Dept> standardList = new List<Sys_Dept>();
                foreach (var item in list)
                {
                    item.UpdateTime = DateTime.Now;
                    item.UpdateUserId = UserId;
                    standardList.Add(item);
                }
                Sys_DeptBLL.MergeModel(standardList, "A.DParentId=B.DParentId and A.DName=B.DName", "A.DParentId=" + pid);
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

        #region 级联删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public JsonResult DeleteDataContainChild(int? id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                if (id.ToInt() < 0)
                {
                    rest.Data = "";
                    return Json(rest);
                }
                //获取子级Id
                StringBuilder builder = new StringBuilder();
                List<Sys_Dept> allList = Sys_DeptBLL.GetList();
                builder.Append(id);
                foreach (var item in allList.Where(t => t.DParentId == id))
                {
                    builder.Append(",");
                    builder.Append(item.Id);
                    if (allList.Where(t => t.DParentId == item.Id).Count() > 0)
                    {
                        RecursionDelId(allList, item.Id.ToInt(), ref builder);
                    }
                }

                //根据当前所选部门id和其子级Id删除数据
                int row = Sys_DeptBLL.DelModel(" Id In( " + builder.ToString() + ")");
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


        /// <summary>
        /// 递归获取子集ID
        /// </summary>
        public void RecursionDelId(List<Sys_Dept> pLists, int DParentId, ref StringBuilder builder)
        {
            foreach (var item in pLists.Where(t => t.DParentId == DParentId))
            {
                builder.Append(",");
                builder.Append(item.Id);
                if (pLists.Where(t => t.DParentId == item.Id).Count() > 0)
                {
                    Recursion(pLists, item.Id, ref builder);
                }
            }
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

                if (Sys_DeptBLL.GetList(d => d.Where(t => t.DParentId == id)).Count() > 0)
                {
                    rest.Code = ResultCode.Failure;
                    rest.Message = "请先删除所选部门的子部门";
                    return Json(rest);
                }

                //根据当前所选部门id删除数据
                int row = Sys_DeptBLL.DelModel(id);
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

        #region 编辑部门
        /// <summary>
        /// 添加子部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="pid">父部门ID</param>
        /// <returns>html 拼接串</returns>
        public JsonResult Edit(int? id)
        {

            AjaxResult result = new AjaxResult();
            StringBuilder builder = new StringBuilder();
            try
            {
                //获取当前对象
                Sys_Dept model = Sys_DeptBLL.GetModel(id);

                //获取上级部门名称
                string deptPName = "顶级";
                if (model.DParentId > 0)
                {
                    Sys_Dept dept = Sys_DeptBLL.GetModel(model.DParentId);
                    deptPName = dept.DName;
                }

                //保存当前编辑对象id,页面隐藏
                builder.Append("<input type='hidden' id='did' value='" + id + "'/>");
                builder.Append("<input type='hidden' id='pid' value='" + model.DParentId + "'/>");
                builder.Append("<input type='hidden' id='currentOption' value='editDept'/>");//用于判断当前操作，为保存时分别调用不同的保存fun 例如保存子部门或编辑当前部门保存

                #region 拼凑导航

                builder.Append("<div class='panel-heading'>");
                builder.Append("<strong>编辑 <i class='icon-double-angle-right'></i> </strong>");
                builder.Append("</div>");

                #endregion
                //循环拼凑编辑页面
                builder.Append("<div class='panel-body' id='childList'>");
                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>上级部门</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' placeholder='上级部门' datatype='*' id='txtSysDept' value='" + deptPName + "' class='form-control' readonly='readonly' onclick='showSysDeptTree()' />");
                builder.Append("</div>");
                builder.Append("</div>");
                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>部门名称</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' name='DName' id='DName' value='" + model.DName + "' class='form-control'/>");
                builder.Append("</div>");
                builder.Append("</div>");
                builder.Append("<div class='form-group'>");
                builder.Append("<label class='col-md-2 control-label'>部门经理</label>");
                builder.Append("<div class='col-md-4'>");
                List<Sys_User> userList = new List<Sys_User>();
                userList = Sys_UserBLL.GetList();
                builder.Append("<select id='FK_UserId'>");
                builder.Append("<option value=''>请选择</option>");
                foreach (var item in userList)
                {
                    if (item.Id == model.FK_UserId)
                    {
                        builder.Append("<option value='" + item.Id + "' selected='selected'>" + item.RealName + "</option>");
                        continue;
                    }
                    builder.Append("<option value='" + item.Id + "'>" + item.RealName + "</option>");
                }
                builder.Append("</select>");
                builder.Append("</div>");
                builder.Append("</div>");
                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>排序</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' name='DSort' id='DSort' value='" + model.DSort + "' class='form-control'/>");
                builder.Append("</div>");
                builder.Append("</div>");
                builder.Append("<div class='form-group'>");
                builder.Append("<label class='col-md-2 control-label'>描述</label>");
                builder.Append("<div class='col-md-9'>");
                builder.Append("<textarea name='DDesc' id='DDesc' class='form-control' rows='5'>" + model.DDesc + "</textarea>");
                builder.Append("</div>");
                builder.Append("</div>");
                builder.Append("</div>");
                builder.Append("<div class='form-group'>");
                builder.Append("<div class='col-xs-8 col-md-offset-2'>");
                builder.Append(CurrentBtnList29);
                builder.Append("</div>");
                builder.Append("</div>");

            }
            catch (Exception ex)
            {

                Log4Helper.WriteError("编辑异常", ex);
            }
            result.Data = builder.ToString();
            return Json(result);
        }
        #endregion

        #region 查看部门
        /// <summary>
        /// 查看部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult LookAt(int? id)
        {
            AjaxResult result = new AjaxResult();
            StringBuilder builder = new StringBuilder();
            try
            {
                //获取当前对象
                Sys_Dept model = Sys_DeptBLL.GetModel(id);

                //获取上级部门名称
                string deptPName = "顶级";
                if (model.DParentId > 0)
                {
                    Sys_Dept dept = Sys_DeptBLL.GetModel(model.DParentId);
                    deptPName = dept.DName;
                }

                //保存当前编辑对象id,页面隐藏
                builder.Append("<input type='hidden' id='did' value='" + id + "'/>");
                builder.Append("<input type='hidden' id='pid' value='" + model.DParentId + "'/>");

                #region 拼凑导航

                builder.Append("<div class='panel-heading'>");
                builder.Append("<strong>查看 <i class='icon-double-angle-right'></i> </strong>");
                builder.Append("</div>");

                #endregion
                //循环拼凑编辑页面
                builder.Append("<div class='panel-body' id='childList'>");
                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>上级部门</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' placeholder='上级部门' datatype='*' id='txtSysDept' value='" + deptPName + "' class='form-control' readonly='readonly' onclick='showSysDeptTree()' />");
                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>部门名称</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' name='DName' id='DName' value='" + model.DName + "' class='form-control'/>");
                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("<div class='form-group'>");
                builder.Append("<label class='col-md-2 control-label'>部门经理</label>");
                builder.Append("<div class='col-md-4'>");

                if (model.FK_UserId.ToInt() <= 0)
                {
                    builder.Append("<input type='text' name='FK_UserId' id='FK_UserId' value='' class='form-control'/>");
                }
                else
                {
                    List<Sys_User> userList = new List<Sys_User>();
                    userList = Sys_UserBLL.GetList();
                    builder.Append("<input type='text' name='FK_UserId' id='FK_UserId' value='" + userList.Where(u => u.Id == model.FK_UserId).FirstOrDefault().RealName + "' class='form-control'/>");
                }

                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("<div class='form-group'> ");
                builder.Append("<label class='col-md-2 control-label'>排序</label>");
                builder.Append("<div class='col-md-4'>");
                builder.Append("<input type='text' name='DSort' id='DSort' value='" + model.DSort + "' class='form-control'/>");
                builder.Append("</div>");
                builder.Append("</div>");

                builder.Append("<div class='form-group'>");
                builder.Append("<label class='col-md-2 control-label'>描述</label>");
                builder.Append("<div class='col-md-9'>");
                builder.Append("<textarea name='DDesc' id='DDesc' class='form-control' rows='5'>" + model.DDesc + "</textarea>");
                builder.Append("</div>");
                builder.Append("</div>");


                builder.Append("</div>");
            }
            catch (Exception ex)
            {

                Log4Helper.WriteError("查看异常", ex);
            }
            result.Data = builder.ToString();
            return Json(result);
        }
        #endregion
    }
}