using System;
using System.Collections.Generic;
using System.Linq;
using ZX.BLL;
using ZX.Model;
using ZX.Tools;
using System.IO;
using System.Data;
using System.Web.Mvc;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;


namespace ZX.Web.Controllers
{
    //[PermissionFilter]
    public class Sys_UserController : BaseController
    {
        #region 跳转用户列表页
        /// <summary>
        /// 跳转用户列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.RealName = RealName;
            return View("Sys_UserList");
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetSys_UserList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string key = Request["name"] ?? "";
                DataList<Sys_UserModel> list = Sys_UserBLL.GetSys_UserList(key, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                int index = 1;

                foreach (var item in list)
                {
                    builder.Append("<tr class='text-center'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td class='text-left' title='" + item.UserName + "'>" + item.UserName + "</td>");
                    builder.Append("<td class='text-left' title='" + item.RealName + "'>" + item.RealName + "</td>");
                    if (item.Fk_DeptId.ToInt() > 0)
                    {
                        try
                        {
                            Sys_Dept deptModel = Sys_DeptBLL.GetModel(item.Fk_DeptId);
                            builder.Append("<td class='text-center' title='" + deptModel.DDesc + "'>" + deptModel.DName + "</td>");
                        }
                        catch
                        {
                            builder.Append("<td></td>");
                        }
                    }
                    else
                    {
                        builder.Append("<td></td>");
                    }


                    switch (item.USex.ToInt())
                    {
                        case 1:
                            {
                                builder.Append("<td class='text-left' title='男'>男</td>");
                                break;
                            }
                        case 2:
                            {
                                builder.Append("<td class='text-left' title='女'>女</td>");
                                break;
                            }
                        default:
                            {
                                builder.Append("<td class='text-left' title='未知'>未知</td>");
                                break;
                            }
                    }
                    builder.Append("<td class='text-left' title='" + item.USex + "'>" + item.USex + "</td>");
                    builder.Append("<td class='text-left' title='" + item.UEmail + "'>" + item.UEmail + "</td>");
                    builder.Append("<td class='text-left' title='" + item.UQQ + "'>" + item.UQQ + "</td>");
                    builder.Append("<td class='text-left' title='" + item.UAddress + "'>" + item.UAddress + "</td>");
                    switch (item.IsDel.ToInt())
                    {
                        case 0:
                            {
                                builder.Append("<td class='text-left' title='禁用'>禁用</td>");
                                break;
                            }
                        case 1:
                            {
                                builder.Append("<td class='text-left' title='启用'>启用</td>");
                                break;
                            }
                        default:
                            {
                                builder.Append("<td class='text-left' title='未知'>未知</td>");
                                break;
                            }
                    }
                    builder.Append("<td class='actions'>");

                    builder.Append(string.Format(CurrentBtnList28, item.Id));

                    ////按钮权限可见性控制
                    //List<Sys_Button> btnList = RoleButtonList;

                    //if (btnList.Where(l => l.BType == 4).Count() > 0)
                    //{
                    //    builder.Append("<a href = 'l?id=" + item.Id + "&mid=" + Request.Form["mid"] + "&secondMenuId=" + SecondMenuId + "'> 查看 </a>");
                    //}
                    //if (btnList.Where(l => l.BType == 1).Count() > 0)
                    //{
                    //    builder.Append("<a href = 'AddEdit?id=" + item.Id + "&mid=" + Request.Form["mid"] + "&secondMenuId=" + SecondMenuId + "'> 编辑 </a>");
                    //}
                    //if (btnList.Where(l => l.BType == 3).Count() > 0)
                    //{
                    //    builder.Append("<a href='javascript:;' onclick='sysUserDel(" + item.Id + ")'>删除</a>");
                    //}
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
            Sys_User model = new Sys_User()
            {
                IsDel = 1,
                USex = "2"
            };
            try
            {
                List<Sys_Dept> dList = Sys_DeptBLL.GetList(d => d.Where(t => 1 == 1).OrderBy(t => t.DSort));
                //获取部门树形,用于部门编辑input
                var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.DParentId.ToInt(), open = t.DParentId == -1 });
                ViewBag.TreeJson = items.ToJsonSerialize();
                ViewBag.RoleList = Sys_RoleBLL.GetList(d => d.Where(t => t.Id != 1));
                ViewBag.CurrentUserId = UserId;
                List<CompanyPosition> list = CompanyPositionBLL.GetList();
                CompanyPosition dic = new CompanyPosition()
                {
                    Id = -1,
                    Name = "普通员工",
                    ParentId = 0,
                    Sort = -1
                };
                list.Add(dic);
                var zwItems = list.Select(t => new { id = t.Id.ToInt(), name = t.Name, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                ViewBag.ZwTreeJson = zwItems.ToJsonSerialize();
                if (id > 0)
                {
                    model = Sys_UserBLL.GetModel(id.ToInt());
                    ViewBag.txtSysDept = dList.FirstOrDefault(d => d.Id == model.Fk_DeptId).DName;
                }
                if (model.FK_CompanyPositionId > 0)
                {
                    ViewBag.CompanyPositionName = list.FirstOrDefault(t => t.Id == model.FK_CompanyPositionId).Name;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            ViewBag.RealName = RealName;
            return View(model);
        }
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetRoles()
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            List<Sys_Role> rList = Sys_RoleBLL.GetList(d => d.Where(t => t.Id != 1));
            if (rList.Count() > 0)
            {
                foreach (var item in rList)
                {
                    roleList.Add(new SelectListItem { Text = item.RName, Value = item.Id.ToString() });
                }
            }
            return roleList;
        }
        #endregion


        #region 查看信息
        public ActionResult LookAt(int? id)
        {
            List<Sys_Dept> dList = Sys_DeptBLL.GetList(d => d.Where(t => 1 == 1).OrderBy(t => t.DSort));
            //获取部门树形,用于部门编辑input
            var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.DParentId.ToInt(), open = t.DParentId == -1 });
            ViewBag.TreeJson = items.ToJsonSerialize();

            ViewBag.RoleList = GetRoles();

            ViewBag.CurrentUserId = UserId;

            Sys_User model = new Sys_User();
            try
            {
                if (id > 0)
                {
                    model = Sys_UserBLL.GetModel(id.ToInt());
                    ViewBag.txtSysDept = dList.FirstOrDefault(d => d.Id == model.Fk_DeptId).DName;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        /// <summary>
        /// /个人中心-我的资料
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
        {
            List<Sys_Dept> dList = Sys_DeptBLL.GetList(d => d.Where(t => 1 == 1).OrderBy(t => t.DSort));
            //获取部门树形,用于部门编辑input
            var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.DParentId.ToInt(), open = t.DParentId == -1 });
            ViewBag.TreeJson = items.ToJsonSerialize();

            ViewBag.RoleList = GetRoles();

            Sys_User model = new Sys_User();
            try
            {
                if (UserId > 0)
                {
                    model = Sys_UserBLL.GetModel(UserId.ToInt());
                    ViewBag.txtSysDept = dList.FirstOrDefault(d => d.Id == model.Fk_DeptId).DName;
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    return Redirect("/Login/Index");
                }

                if (model.FK_CompanyPositionId > 0)
                {
                    List<CompanyPosition> list = CompanyPositionBLL.GetList();
                    CompanyPosition dic = new CompanyPosition()
                    {
                        Id = -1,
                        Name = "普通员工",
                        ParentId = 0,
                        Sort = -1
                    };
                    list.Add(dic);
                    ViewBag.CompanyPositionName = list.FirstOrDefault(t => t.Id == model.FK_CompanyPositionId).Name;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        /// <summary>
        /// 首页用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo()
        {
            List<Sys_Dept> dList = Sys_DeptBLL.GetList(d => d.Where(t => 1 == 1).OrderBy(t => t.DSort));
            //获取部门树形,用于部门编辑input
            var items = dList.Select(t => new { id = t.Id.ToInt(), name = t.DName, pId = t.DParentId.ToInt(), open = t.DParentId == -1 });
            ViewBag.TreeJson = items.ToJsonSerialize();

            ViewBag.RoleList = GetRoles();

            Sys_User model = new Sys_User();
            try
            {
                if (UserId > 0)
                {
                    model = Sys_UserBLL.GetModel(UserId.ToInt());
                    ViewBag.txtSysDept = dList.FirstOrDefault(d => d.Id == model.Fk_DeptId).DName;
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    return Redirect("/Login/Index");
                }

                if (model.FK_CompanyPositionId > 0)
                {
                    List<CompanyPosition> list = CompanyPositionBLL.GetList();
                    CompanyPosition dic = new CompanyPosition()
                    {
                        Id = -1,
                        Name = "普通员工",
                        ParentId = 0,
                        Sort = -1
                    };
                    list.Add(dic);
                    ViewBag.CompanyPositionName = list.FirstOrDefault(t => t.Id == model.FK_CompanyPositionId).Name;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        #region 保存数据
        [ValidateInput(false)]
        /// <summary>
        /// 保存数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            AjaxResult rest = new AjaxResult();
            Sys_User model = FormHelper.GetRequestForm<Sys_User>();

            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    if (model.Pwd.IsNotNullOrEmpty())
                    {
                        model.Pwd = EnDecrypt.SHA1Hash(model.Pwd);
                    }
                    else
                    {
                        model.Pwd = null;
                    }

                    row = Sys_UserBLL.UpdateModel(model);
                }
                else
                {
                    //唯一性验证 区分大小写
                    bool bl = Sys_UserBLL.CheckUserName(model.UserName);
                    if (bl)
                    {
                        rest.Message = "用户名已存在";
                        rest.Code = ResultCode.Failure;
                        return Json(rest);
                    }
                    if (model.Pwd.IsNullOrEmpty())
                    {
                        model.Pwd = EnDecrypt.SHA1Hash("88888888");
                    }
                    else
                    {
                        model.Pwd = EnDecrypt.SHA1Hash(model.Pwd);
                    }

                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.Id = Sys_UserBLL.AddModel(model);
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


        #region 修改个人密码
        public ActionResult UpdatePwd()
        {
            Sys_User myModel = Sys_UserBLL.GetModel(UserId);
            return View(myModel);
        }

        public JsonResult SaveUpdatePwd()
        {
            AjaxResult rest = new AjaxResult();
            Sys_User model = FormHelper.GetRequestForm<Sys_User>();
            try
            {
                Sys_User myModel = Sys_UserBLL.GetModel(UserId);
                if (model.Id > 0)
                {
                    myModel.UpdateTime = DateTime.Now;
                    myModel.UpdateUserId = UserId;

                    myModel.Pwd = EnDecrypt.SHA1Hash(model.Pwd);

                    Sys_UserBLL.UpdateModel(myModel);
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


        #region 重置密码

        public JsonResult ResetPwd(int id)
        {
            AjaxResult rest = new AjaxResult();

            try
            {
                Sys_User model = Sys_UserBLL.GetModel(id);
                if (model.Id > 0)
                {
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;

                    model.Pwd = EnDecrypt.SHA1Hash("88888888");

                    Sys_UserBLL.UpdateModel(model);
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
                int row = Sys_UserBLL.DelModelById(id);
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

        public ActionResult ExcelImport()
        {
            return View();
        }
        /// <summary>
        /// 导入员工信息
        /// </summary>
        /// <returns></returns>
        public JsonResult ImportUserExcel()
        {
            AjaxResult result = new AjaxResult();
            IWorkbook workbook = null;
            FileStream fileStream = null;
            try
            {
                string fileUrl = Request["fileUrl"];
                //新建IWorkbook对象  
                string fileName = Server.MapPath("~" + fileUrl);
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
                {
                    workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本  
                {
                    workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
                }
                ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  

                DataTable dt = ExcelHelper.ImportDt(sheet, 1, false, 1);

                List<Sys_User> userList = new List<Sys_User>();
                List<Sys_Dept> deptList = Sys_DeptBLL.GetList();
                List<CompanyPosition> positionList = CompanyPositionBLL.GetList();

                DateTime createTime = DateTime.Now;
                foreach (DataRow item in dt.Rows)
                {
                    string strDept = item[0].ToString();
                    string userName = item[1].ToString();
                    string position = item[2].ToString();

                    Sys_User userModel = new Sys_User();

                    //按照部门名称查询部门
                    Sys_Dept deptModle = deptList.Where(l => l.DName.Equals(strDept)).FirstOrDefault();

                    if (deptModle != null)
                    {
                        userModel.Fk_DeptId = deptModle.Id.ToInt();
                    }

                    userModel.RealName = userName;

                    //按岗位名称查询职位Id
                    CompanyPosition positionModle = positionList.Where(l => l.Name.Equals(position)).FirstOrDefault();

                    if (positionModle != null)
                    {
                        userModel.FK_CompanyPositionId = positionModle.Id.ToInt();
                    }
                    else
                    {
                        result.Message += "\n系统并未查询到" + userName + "员工的" + position + "岗位,因此将默认设为普通员工导入:\n";
                        userModel.FK_CompanyPositionId = -1;//普通员工
                    }

                    userModel.Pwd = EnDecrypt.SHA1Hash("88888888");
                    userModel.CreateTime = createTime;
                    userModel.CreateUserId = UserId;
                    userList.Add(userModel);
                }

                Sys_UserBLL.MergeNotDelModel(userList, "A.RealName=B.RealName");

            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failure;
                result.Message = ex.Message;
                Log4Helper.WriteError("导入员工异常", ex);
            }
            finally
            {
                fileStream.Close();
                workbook.Close();
            }

            return Json(result);
        }


        public FileResult ExportUserList()
        {
            MemoryStream ms = new MemoryStream();
            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");
            string fileName = "员工信息" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            try
            {
                List<Sys_Dept> deptList = Sys_DeptBLL.GetList();
                List<CompanyPosition> positionList = CompanyPositionBLL.GetList();

                //获取list数据
                List<Sys_User> userList = Sys_UserBLL.GetList();

                //给sheet1添加第一行的头部标题
                IRow row1 = sheet1.CreateRow(0);
                row1.CreateCell(0).SetCellValue("id");
                row1.CreateCell(1).SetCellValue("用户名");
                row1.CreateCell(2).SetCellValue("真实姓名");
                row1.CreateCell(3).SetCellValue("部门");
                row1.CreateCell(4).SetCellValue("职位");
                row1.CreateCell(5).SetCellValue("性别");
                row1.CreateCell(6).SetCellValue("电话/手机");
                row1.CreateCell(7).SetCellValue("邮箱");
                row1.CreateCell(8).SetCellValue("QQ");
                row1.CreateCell(9).SetCellValue("通讯地址");
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < userList.Count; i++)
                {
                    IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(userList[i].Id);
                    rowtemp.CreateCell(1).SetCellValue(userList[i].UserName);
                    rowtemp.CreateCell(2).SetCellValue(userList[i].RealName);
                    string deptName = "";
                    try
                    {
                        if (userList[i].Fk_DeptId.ToInt() > 0)
                        {
                            deptName = deptList.Where(l => l.Id == userList[i].Fk_DeptId.ToInt()).FirstOrDefault().DName;
                        }
                    }
                    catch { }

                    rowtemp.CreateCell(3).SetCellValue(deptName);


                    string positionName = "";
                    try
                    {
                        if (userList[i].FK_CompanyPositionId.ToInt() > 0)
                        {
                            positionName = positionList.Where(l => l.Id == userList[i].FK_CompanyPositionId.ToInt()).FirstOrDefault().Name;
                        }
                    }
                    catch { }

                    rowtemp.CreateCell(4).SetCellValue(positionName);


                    string uSex = "";
                    switch (userList[i].USex.ToInt())
                    {
                        case 1:
                            {
                                uSex = "男";
                                break;
                            }
                        case 2:
                            {
                                uSex = "女";
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    rowtemp.CreateCell(5).SetCellValue(uSex);
                    rowtemp.CreateCell(6).SetCellValue(userList[i].UPhone);
                    rowtemp.CreateCell(7).SetCellValue(userList[i].UEmail);
                    rowtemp.CreateCell(8).SetCellValue(userList[i].UQQ);
                    rowtemp.CreateCell(9).SetCellValue(userList[i].UAddress);
                }
                // 写入到客户端 
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError("导出员工Excel异常", ex);
            }
            return File(ms, "application/vnd.ms-excel", fileName);
        }

    }
}