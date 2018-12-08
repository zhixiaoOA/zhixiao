using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using ZX.Tools;
using System.Collections;
using ZX.Model;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;

namespace ZX.Web.Controllers.init
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View("initAdmin");
        }

        public ActionResult Agreement()
        {
            return View("agreement");
        }

        public ActionResult State()
        {
            ViewBag.version = ZXConfig.VERSION;
            return View("state");
        }

        #region 动态校验用户输入的数据库连接信息是否正确
        /// <summary>
        /// 数据连接校验
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult valiConn()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                Stream reqStream = Request.InputStream;
                StreamReader streamReader = new StreamReader(reqStream);
                string json = streamReader.ReadToEnd();
                DataBase dataBase = new DataBase();
                dataBase = (DataBase)JsonConvert.DeserializeObject(json, dataBase.GetType());

                string conStr = "data source=" + dataBase.Server + "; initial catalog=" + dataBase.DataBaseName + "; user id=" + dataBase.UserName + "; password=" + dataBase.Password;
                SqlConnection m_SqlConnection = new SqlConnection(conStr);
                m_SqlConnection.Open();
                if (m_SqlConnection.State == ConnectionState.Open)
                {
                    result.Message = "连接成功";
                    result.Code = ResultCode.Succeed;
                    ReadWriteConfig rwc = new ReadWriteConfig();
                    rwc.SetValue("connectionString", conStr);
                }
                else
                {
                    result.Message = "数据连接信息不正确";
                    result.Code = ResultCode.Failure;
                }
                m_SqlConnection.Close();
            }
            catch (SqlException e)
            {
                Log4Helper.WriteError("数据连接串配置有误：" + e.Message, e);
                result.Message = e.Message;
                result.Code = ResultCode.Failure;
            }
            return Json(result);
        }
        #endregion

        #region 检查数据库是否能够连接
        /// <summary>
        /// 检查数据库是否配置成功
        /// </summary>
        /// <returns></returns>
        public JsonResult checkDataBase()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                //获取<configuration>下节点的信息
                string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection m_SqlConnection = new SqlConnection(connStr);
                m_SqlConnection.Open();
                if (m_SqlConnection.State == ConnectionState.Open)
                {
                    result.Message = "连接成功";
                    result.Code = ResultCode.Succeed;
                }
                else
                {
                    result.Message = "连接失败";
                    result.Code = ResultCode.Failure;
                }
                m_SqlConnection.Close();
            }
            catch (Exception e)
            {
                Log4Helper.WriteError("数据库未连接成功:" + e.Message, e);
                result.Message = e.Message;
                result.Code = ResultCode.Failure;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}