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
        static string connStr = "Server=localhost;Integrated security=SSPI;database=master";
        static string dataBaseName = "ZXOA";
        public ActionResult Index()
        {
            return View("initAdmin");
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
                SqlConnection m_SqlConnection = new SqlConnection(connStr);
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

        #region 还原数据库
        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="backfile"></param>
        /// <returns></returns>
        public bool RestoreDatabase(string backfile)
        {
            //标记数据库是否还原成功
            bool flag = false;
            ///杀死原来所有的数据库连接进程
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = "SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" +
                          dataBaseName + "'";
            SqlCommand cmd1 = new SqlCommand(sql, conn);
            SqlDataReader dr;
            ArrayList list = new ArrayList();
            try
            {
                dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr.GetInt16(0));
                }
                dr.Close();
            }
            catch (SqlException ee)
            {
                Log4Helper.WriteError(ee.Message, ee);
            }
            finally
            {
                conn.Close();
            }
            //MessageBox.Show(list.Count.ToString());
            for (int i = 0; i < list.Count; i++)
            {
                conn.Open();
                cmd1 = new SqlCommand(string.Format("KILL {0}", list[i].ToString()), conn);
                cmd1.ExecuteNonQuery();
                conn.Close();
                Log4Helper.WriteDebug("系统已经清除的数据库线程： " + list[i].ToString() + "\r\n正在还原数据库！");
            }
            //这里一定要是master数据库，而不能是要还原的数据库，因为这样便变成了有其它进程
            //占用了数据库。
            string database = dataBaseName;
            string path = backfile;
            string BACKUP = String.Format("RESTORE DATABASE {0} FROM DISK = '{1}'", database, path);
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(BACKUP, con);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (SqlException ee)
            {
                Log4Helper.WriteError(ee.Message, ee);
            }
            finally
            {
                con.Close();
            }
            return flag;
        }
        #endregion
    }
}