using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ZX.Tools;
using ZX.Model;
using ZX.BLL;
using System.ComponentModel;
using System.Reflection;

namespace ZX.Web.Controllers
{
    public class SupplierController : BaseController
    {
        #region 初始跳转
        /// <summary>
        /// 初始跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("SupplierList");
        }
        #endregion

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
                DataList<SupplierModel> list = SupplierBLL.GetSupplierList(name, pageIndex, pageSize);

                if (list != null)
                {
                }
                StringBuilder builder = new StringBuilder();
                int index = 1;

                foreach (var item in list)
                {
                    builder.Append("<tr class='text-left'>");
                    builder.Append("<td>" + ((pageIndex - 1) * pageSize + index++) + "</td>");
                    builder.Append("<td title='" + item.SCode + "'>" + item.SCode + "</td>");
                    builder.Append("<td title='" + item.SName + "'>" + item.SName + "</td>");
                    builder.Append("<td title='" + item.Qualified + "%'>" + item.Qualified + "%</td>");
                    builder.Append("<td title='" + item.SAddress + "'>" + item.SAddress + "</td>");
                    builder.Append("<td title='" + item.SLinkName + "'>" + item.SLinkName + "</td>");
                    builder.Append("<td title='" + item.SPhone + "'>" + item.SPhone + "</td>");
                    builder.Append("<td title='" + item.STel + "'>" + item.STel + "</td>");
                    builder.Append("<td title='" + item.SSize + "'>" + GetEnumDescription((SupplierSize)Enum.Parse(typeof(SupplierSize), item.SSize)) + "</td>");
                    builder.Append("<td title='" + item.SIndustry + "'>" + GetEnumDescription((SupplierIndustry)Enum.Parse(typeof(SupplierSize), item.SIndustry)) + "</td>");
                    builder.Append("<td class='text-left'>");
                    builder.Append(string.Format(CurrentBtnList28, item.Id));
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

        public string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
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
            Supplier model = new Supplier();
            try
            {
                if (id > 0)
                {
                    model = SupplierBLL.GetModel(id.ToInt());
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
        /// 查看跳转
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public ActionResult Readonly(int? id)
        {
            Supplier model = new Supplier();
            try
            {
                if (id > 0)
                {
                    model = SupplierBLL.GetModel(id.ToInt());
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
            Supplier model = FormHelper.GetRequestForm<Supplier>();
            AjaxResult rest = new AjaxResult();
            try
            {
                int row = 0;
                if (model.Id > 0)
                {
                    model.UpdateAccount = UserAccount;
                    model.UpdateTime = DateTime.Now;
                    model.UpdateUserId = UserId;
                    row = SupplierBLL.UpdateModel(model);
                }
                else
                {
                    model.CreateAccount = UserAccount;
                    model.CreateTime = DateTime.Now;
                    model.CreateUserId = UserId;
                    model.Id = SupplierBLL.AddModel(model);
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
                int row = SupplierBLL.DelModelById(id);
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
