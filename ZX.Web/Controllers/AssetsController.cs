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
using System.Net;
using System.Collections.Specialized;
using Aspose.Cells;
using System.Data;

namespace ZX.Web.Controllers
{
    //[PermissionFilter]
    public class AssetsController : BaseController
    {
        #region 首页跳转
        /// <summary>
        /// 首页跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult HomeIndex()
        {
            return View();
        }
        #endregion

        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //int pageIndex = Request["pageIndex"].ToInt(1);
            //int pageSize = Request["pageSize"].ToInt(10);

            //DataList<AssetsModel> list = AssetsBLL.GetAssetsList(key, pageIndex, PageSize);
            //ViewBag.AssetsList = list;
            return View("AssetsList");

        }
        #endregion

        #region 扫描二维码进入验证页面
        /// <summary>
        /// 扫描二维码进入验证页面
        /// </summary>
        /// <param name="id">资产编号</param>
        /// <returns></returns>
        public ActionResult QrCode(int id)
        {
            ViewBag.AssetId = id;
            return View("QrCodeReturnData");
        }
        #endregion

        #region 验证用户并插入一条日志信息
        /// <summary>
        /// 验证用户并插入一条日志信息
        /// </summary>
        /// <param name="userName">需要验证的账号</param>
        /// <param name="userPwd">需要验证的密码</param>
        /// <param name="id">资产编号</param>
        /// <returns></returns>
        public ActionResult Authentication(string userName, string userPwd, int id)
        {
            AjaxApiResult result = new AjaxApiResult();

            //加密用户输入的密码
            string md5StrPwd = EnDecrypt.MD5(userPwd);
            //根据用户输入的账号及加密后的密码查询
            Sys_User model = Sys_UserBLL.AccountGetUserList(userName, md5StrPwd);
            if (model != null)
            {
                SessionHelper.WriteLoginSession(model);
                result.Code = 200;

                //根据id资产信息
                Assets assets = AssetsBLL.GetModel(id);

                Assets_Log aslog = new Assets_Log();
                aslog.FK_AssetsId = id;
                var datetime = DateTime.Now;
                aslog.CreateTime = datetime;
                aslog.CreateUserId = (int)model.Id;
                aslog.UpdateTime = datetime;
                aslog.UpdateUserId = (int)model.Id;
                aslog.CreateAccount = model.UserName;
                aslog.UpdateAccount = model.UserName;

                //根据资产id添加使用日志信息
                var assetslog = Assets_LogBLL.AddLogBasedOnId(aslog);
            }
            else
            {
                result.Message = "用户不存在或密码错误!";
                result.Code = 300;
            }
            return Content(string.Format("<script type='text/javascript'>alert('返回状态码：{0}\r\n 返回结果：{1}')</script>", result.Code, result.Message));
            //return View("QrCodeReturnData");
        }
        #endregion

        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult AssetsManageIndex()
        {
            return View("AssetsManageList");
        }
        #endregion

        #region 获取资产管理数据列表
        /// <summary>
        /// 获取资产管理数据列表
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetAssetsList()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                int mid = Request.Form["mid"].ToInt();
                int pageIndex = Request["pageIndex"].ToInt(1);
                int pageSize = Request["pageSize"].ToInt(10);
                string name = Request["nameKey"] ?? "";
                string code = Request["codeKey"] ?? "";
                DataList<AssetsModel> list = AssetsBLL.GetAssetsList(code, name, mid, pageIndex, pageSize);
                StringBuilder builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append("<tr class='text-left'>");
                    builder.Append("<td><input name=\"cbox\" type=\"checkbox\" value=\"" + item.Id + "\">" + item.ACode + "</td>");
                    builder.Append("<td>" + item.AName + "</td>");
                    builder.Append("<td title=\"" + item.SupplierName + "\">" + item.SupplierName + "</td>");
                    builder.Append("<td>" + item.TName + "</td>");
                    builder.Append("<td>" + item.DeptName + "</td>");
                    builder.Append("<td>" + item.ANum + "</td>");
                    builder.Append("<td>" + item.UnitName + "</td>");
                    builder.Append("<td>" + item.APrice + "</td>");
                    builder.Append("<td>" + item.UsePeriod + item.UsePeriodUnit + "</td>");
                    builder.Append("<td>" + item.NatureOfAssets + "</td>");
                    builder.Append("<td>" + item.CreateTime.ToDateFormat("yyyy-MM-dd HH:mm:ss") + "</td>");
                    builder.Append("<td>");
                    string btnHtml = string.Format(CurrentBtnList28, item.Id);
                    builder.Append(btnHtml);
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

        #region 获取资产分类数据
        /// <summary>
        /// 获取资产分类数据
        /// </summary>        
        /// <param name="key">关键词</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public JsonResult GetAssetsTypeList()
        {

            int mid = Request["mid"].ToInt();
            AjaxResult result = new AjaxResult();
            try
            {
                StringBuilder builder = new StringBuilder();
                List<Assets_Type> listMenu = Assets_TypeBLL.GetList(t => t.OrderBy(a => a.Id));
                foreach (var item in listMenu.Where(t => t.ParentId == 0))
                {

                    builder.Append("<li><a href='javascript:void(0);' onclick='showAssets(" + item.Id + ")'>" + item.TName + "</a>");

                    //操作权限
                    string btnHtml = string.Format(CurrentBtnList30, item.Id);
                    builder.Append(btnHtml);

                    if (listMenu.Where(t => t.ParentId == item.Id).Count() > 0)
                    {
                        builder.Append("<ul>");
                        Recursion(listMenu, item.Id, mid, ref builder);
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

        #region 递归查询子集
        /// <summary>
        /// 递归查询子集
        /// </summary>
        /// <param name="nLists">子集</param>
        /// <param name="parentID">父id</param>
        /// <param name="builder">返回字符</param>
        public void Recursion(List<Assets_Type> nLists, long parentID, int mid, ref StringBuilder builder)
        {
            foreach (var item in nLists.Where(t => t.ParentId == parentID))
            {
                builder.Append("<li><a href='javascript:;' onclick='showAssets(" + item.Id + ")'>" + item.TName + "</a>");
                //操作权限

                string btnHtml = string.Format(CurrentBtnList30, item.Id);
                builder.Append(btnHtml);

                if (nLists.Where(t => t.ParentId == item.Id).Count() > 0)
                {
                    builder.Append("<ul>");
                    Recursion(nLists, item.Id, mid, ref builder);
                    builder.Append("</ul>");
                }
                builder.Append("</li>");
            }
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
            Assets model = new Assets()
            {
                Id = id.ToInt(),
                NatureOfAssets = "入库资产",
            };
            try
            {
                List<Supplier> listSupplier = SupplierBLL.GetList();
                ViewBag.ListSupplier = listSupplier;
                List<Assets_Type> list = Assets_TypeBLL.GetList(t => t.Where(a => a.Id != model.Id));
                var items = list.Select(t => new { id = t.Id.ToInt(), name = t.TName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                ViewBag.TreeJson = items.ToJsonSerialize();
                if (id > 0)
                {
                    model = AssetsBLL.GetModel(id.ToInt());
                    ViewBag.TypeName = list.Where(t => t.Id == model.FK_TypeId).FirstOrDefault().TName;
                }
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
            Assets model = new Assets();
            try
            {
                if (id > 0)
                {
                    model = AssetsBLL.GetModel(id.ToInt());
                    List<Assets_Type> list = Assets_TypeBLL.GetList(t => t.Where(a => a.Id != model.Id));

                    var items = list.Select(t => new { id = t.Id.ToInt(), name = t.TName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                    ViewBag.TreeJson = items.ToJsonSerialize();
                    ViewBag.TypeName = list.FirstOrDefault(t => t.Id == model.FK_TypeId).TName;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 添加/编辑跳转
        /// <summary>
        /// 添加/编辑跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult AssetsTypeAddEdit(int? id)
        {
            int mPId = Request["pid"].ToInt();
            Assets_Type model = new Assets_Type()
            {
                Id = id.ToInt(),
                ParentId = mPId
            };
            try
            {
                List<Assets_Type> list = Assets_TypeBLL.GetList(t => t.Where(a => a.Id != model.Id));
                Assets_Type menu = new Assets_Type()
                {
                    Id = 0,
                    TName = "顶级",
                    ParentId = -1,
                    TSort = -1
                };
                list.Add(menu);
                var items = list.Select(t => new { id = t.Id.ToInt(), name = t.TName, pId = t.ParentId.ToInt(), open = t.ParentId == -1 });
                ViewBag.TreeJson = items.ToJsonSerialize();
                if (id > 0)
                {
                    model = Assets_TypeBLL.GetModel(id.ToInt());
                }
                ViewBag.TypeName = list.FirstOrDefault(t => t.Id == model.ParentId).TName;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }
        #endregion

        #region 保存资产管理数据
        [ValidateInput(false)]
        /// <summary>
        /// 保存资产管理数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult SaveData()
        {
            Assets model = FormHelper.GetRequestForm<Assets>();

            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    if (model.QrCodeUrl == null || model.QrCodeUrl == "")
                    {
                        SetQrCode();
                        model.QrCodeUrl = ViewBag.ErWeiMa;
                    }
                    row = AssetsBLL.UpdateModel(model);
                }
                else
                {
                    //2018-06-27 要求可空
                    if (model.ACode.IsNullOrEmpty())
                    {
                        model.CreateTime = DateTime.Now;
                        model.CreateUserId = UserId;
                        model.UpdateTime = model.CreateTime;
                        model.UpdateUserId = UserId;

                        SetQrCode();
                        model.QrCodeUrl = ViewBag.ErWeiMa;

                        model.Id = AssetsBLL.AddModel(model);
                    }
                    else
                    {
                        List<Assets> list = AssetsBLL.GetList(x => x.Where(t => t.ACode == model.ACode));
                        if (list.Count > 0)
                        {
                            rest.Message = "资产编号已存在";
                            rest.Code = ResultCode.Failure;
                        }
                        else
                        {
                            model.CreateTime = DateTime.Now;
                            model.CreateUserId = UserId;
                            model.UpdateTime = model.CreateTime;
                            model.UpdateUserId = UserId;

                            SetQrCode();
                            model.QrCodeUrl = ViewBag.ErWeiMa;

                            model.Id = AssetsBLL.AddModel(model);
                        }
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

        public ActionResult SetQrCode()
        {
            string uploadPath = Server.MapPath("~/UploadFile/member");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            Random ran = new Random();
            int num = ran.Next(10000, 99999);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + num + ".png";//Path.GetExtension(itemFile.FileName);
            uploadPath += "\\" + fileName;
            string regUrl = System.Configuration.ConfigurationManager.AppSettings["Domain"] + "/reg?id=" + UserId;
            System.Drawing.Image image = ZX.Tools.QrCodeHelper.CreateQRCode(regUrl);
            image.Save(uploadPath, System.Drawing.Imaging.ImageFormat.Png);
            uploadPath = "/UploadFile/member/" + fileName;
            ViewBag.ErWeiMa = uploadPath;

            return View();
        }

        public ActionResult QrImgDownloadHandler(int id)
        {
            try
            {
                string name = "";
                Assets model = new Assets();
                if (id > 0)
                {
                    model = AssetsBLL.GetModel(id.ToInt());
                    name = "资产编号-" + model.ACode + "_名称-" + model.AName;

                    string uploadPath = Server.MapPath("~/UploadFile/QrCode");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    string regUrl = "https://wdoa.aioae.com:8001/AssetsQrCode/QrCode?id=" + id;
                    //string regUrl = "http://mhoa.gxgxun.com/AssetsQrCode/QrCode?id=" + id;
                    System.Drawing.Image image = ZX.Tools.QrCodeHelper.CreateQRCode(regUrl);
                    MemoryStream ms = new MemoryStream();

                    Random ran = new Random();
                    int num = ran.Next(10000, 99999);
                    string fileName = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + num + ".png";//Path.GetExtension(itemFile.FileName);
                    uploadPath += "\\" + fileName;

                    image.Save(uploadPath + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    FileInfo fileInfo = new FileInfo(uploadPath + ".png");
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.AddHeader("Content-Transfer-Encoding", "binary");
                    Response.ContentType = "application/octet-stream";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    Response.WriteFile(fileInfo.FullName);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        #endregion

        #region 保存资产分类数据
        [ValidateInput(false)]
        /// <summary>
        /// 保存资产分类数据
        /// </summary>        
        /// <returns></returns>
        public JsonResult AssetsTypeSaveData()
        {
            Assets_Type model = FormHelper.GetRequestForm<Assets_Type>();

            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateUserId = UserId;
                    model.UpdateTime = DateTime.Now;
                    row = Assets_TypeBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.UpdateTime = model.CreateTime;
                    model.UpdateUserId = UserId;

                    model.Id = Assets_TypeBLL.AddModel(model);
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

        #region 删除资产管理及资产分类数据
        /// <summary>
        /// 删除资产管理及资产分类数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public JsonResult DeleteData(string id, int IsType)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                //根据传入参数进行选择删除任务
                switch (IsType)
                {
                    case 0:
                        break;
                    case 1:
                        int Typerow = Assets_TypeBLL.DelModelById(id);
                        if (Typerow == 0)
                        {
                            rest.Message = "删除失败";
                            rest.Code = ResultCode.Failure;
                        }
                        break;
                    case 2:
                        int Asrow = AssetsBLL.DelModelById(id);
                        if (Asrow == 0)
                        {
                            rest.Message = "删除失败";
                            rest.Code = ResultCode.Failure;
                        }
                        break;
                    case 3:
                        int Logrow = Assets_LogBLL.DelModelById(id);
                        if (Logrow == 0)
                        {
                            rest.Message = "删除失败";
                            rest.Code = ResultCode.Failure;
                        }
                        break;
                    default:
                        break;
                }
                //if (IsType == 1)
                //{
                //    int Typerow = Assets_TypeBLL.DelModel("Id IN(" + id + ")");
                //    if (Typerow == 0)
                //    {
                //        rest.Message = "删除失败";
                //        rest.Code = ResultCode.Failure;
                //    }
                //}
                //else
                //{
                //    int row = AssetsBLL.DelModel("Id IN(" + id + ")");
                //    if (row == 0)
                //    {
                //        rest.Message = "删除失败";
                //        rest.Code = ResultCode.Failure;
                //    }
                //}


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

        #region 删除资产分类数据
        /// <summary>
        /// 删除资产管理及资产分类数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public JsonResult DeleteType(string id)
        {
            AjaxResult rest = new AjaxResult();
            try
            {
                int Typerow = Assets_TypeBLL.DelModelById(id);
                if (Typerow == 0)
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

        #region 领取记录
        /// <summary>
        /// 领取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UseLog(long id)
        {
            List<AssetsUseDetail> listDetail = null;
            try
            {
                listDetail = AssetsUseDetailBLL.GetList(t => t.Where(a => a.FK_AssetsId == id));
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(listDetail);
        }
        #endregion

        #region 资产统计跳转
        /// <summary>
        /// 资产统计跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult InStorageReport()
        {
            try
            {
                DateTime dt = DateTime.Now;
                ViewBag.BDate = dt.AddDays(1 - (dt.DayOfWeek.ToInt() == 0 ? 7 : dt.DayOfWeek.ToInt())).ToString("yyyy-MM-dd");
                ViewBag.EDate = dt.ToString("yyyy-MM-dd");
                //本月数据
                string beginTime = dt.AddDays(1 - dt.Day).ToString("yyyy-MM-dd");
                string endTime = dt.ToString("yyyy-MM-dd");
                InStorageReport monthModel = AssetsBLL.GetInStorageReport(beginTime, endTime);
                ViewBag.MonthData = monthModel;
                //本季度数据
                beginTime = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day).ToString("yyyy-MM-dd");
                InStorageReport jiduModel = AssetsBLL.GetInStorageReport(beginTime, endTime);
                ViewBag.JiduData = jiduModel;
                //本年度数据
                beginTime = dt.Year + "-01-01";
                InStorageReport yearModel = AssetsBLL.GetInStorageReport(beginTime, endTime);
                ViewBag.YearData = yearModel;
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View();
        }
        #endregion

        #region 获取入库统计数据
        /// <summary>
        /// 获取入库统计数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetInStorageList(string beginTime, string endTime)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                StringBuilder builder = new StringBuilder();
                InStorageReport model = AssetsBLL.GetInStorageReport(beginTime, endTime);
                builder.Append("<tr class='text-left'><td class='w-150px'>" + model.InCount + "</td><td>" + model.TotalMoney + "</td></tr>");
                result.Data = builder.ToString();
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return Json(result);
        }
        #endregion

        #region 导入资产
        /// <summary>
        /// 导入资产
        /// </summary>
        /// <returns></returns>
        public JsonResult ImportExec()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                if (hfc.Count > 0)
                {
                    string direName = "/UploadFile";
                    if (!Directory.Exists(Server.MapPath("~" + direName)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~" + direName));
                    }
                    string ftypeName = Path.GetExtension(hfc[0].FileName);
                    string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
                    string filePath = Server.MapPath("~" + direName) + fileName + ftypeName;
                    hfc[0].SaveAs(filePath);
                    //读取导入的文件
                    Workbook book = new Workbook();
                    book.Open(filePath);
                    Worksheet sheet = book.Worksheets[0];
                    Cells cells = sheet.Cells;
                    DataTable dt = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxDataColumn + 1, true);
                    List<Assets> list = new List<Assets>();
                    string typeName = "", superName = "";
                    foreach (DataRow item in dt.Rows)
                    {
                        typeName = item["资产类别"].ConvToString();
                        superName = item["供应商"].ConvToString();
                        //资产类型
                        Assets_Type assetsType = Assets_TypeBLL.GetModel(t => t.Where(a => a.TName == typeName));
                        if (assetsType == null)
                        {
                            assetsType = new Assets_Type()
                            {
                                ParentId = 0,
                                TName = typeName,
                                CreateAccount = UserName,
                                CreateTime = DateTime.Now,
                                CreateUserId = UserId,
                                UpdateAccount = UserName,
                                UpdateTime = DateTime.Now,
                                UpdateUserId = UserId
                            };
                            assetsType.Id = Assets_TypeBLL.AddModel(assetsType);
                        }
                        //供应商
                        Supplier supplier = SupplierBLL.GetModel(t => t.Where(a => a.SName == superName));
                        if (supplier == null)
                        {
                            supplier = new Supplier()
                            {
                                SName = superName,
                                CreateAccount = UserName,
                                CreateTime = DateTime.Now,
                                CreateUserId = UserId,
                                UpdateAccount = UserName,
                                UpdateTime = DateTime.Now,
                                UpdateUserId = UserId
                            };
                            supplier.Id = SupplierBLL.AddModel(supplier);
                        }
                        Assets model = new Assets()
                        {
                            CreateAccount = UserName,
                            CreateTime = DateTime.Now,
                            CreateUserId = UserId,
                            UpdateAccount = UserName,
                            UpdateTime = DateTime.Now,
                            UpdateUserId = UserId,
                            ACode = item["资产编号"].ConvToString(),
                            ANum = item["数量"].ToInt(),
                            AName = item["资产名称"].ConvToString(),
                            DeptName = item["使用部门"].ConvToString(),
                            APrice = item["单价"].ToDecimal(),
                            UnitName = item["单位"].ConvToString(),
                            FK_TypeId = assetsType.Id.ToInt(),
                            FK_SupplierId = supplier.Id,
                            PurchaseDay = Convert.ToDateTime(item["购入日期"]),
                            UsePeriod = item["使用期限"].ToInt(),
                            UsePeriodUnit = item["期限单位"].ConvToString(),
                            NatureOfAssets = "入库资产"
                        };
                        list.Add(model);
                    }
                    if (list.Count > 0) AssetsBLL.AddModel(list);
                }
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

        #region 详情

        public ActionResult Details(int id)
        {

            Assets model = new Assets()
            {
                Id = id.ToInt(),
                NatureOfAssets = "入库资产",
            };
            try
            {
                List<Supplier> listSupplier = SupplierBLL.GetList();
                ViewBag.ListSupplier = listSupplier;
                if (id > 0)
                {
                    model = AssetsBLL.GetModel(id.ToInt());
                    ViewBag.TypeName = Assets_TypeBLL.GetList(t => t.Where(l => l.Id == model.FK_TypeId)).FirstOrDefault().TName;
                }
            }
            catch (Exception ex)
            {
                Log4Helper.WriteError(ex.Message, ex);
            }
            return View(model);
        }

        #endregion
    }
}
