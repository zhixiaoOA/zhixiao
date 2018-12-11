using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using ZX.BLL;
using ZX.DAL;
using ZX.Model;
using ZX.Tools;

namespace ZX.Web.Controllers.init
{
    public class CheckController : Controller
    {
        public ActionResult Index()
        {
            string version = Environment.Version.ToString();
            Console.WriteLine(".net version:" + version);
            
            //获取<configuration>下节点的信息
            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection m_SqlConnection = new SqlConnection(connStr);
            string serverVersion = "";
            try
            {
                m_SqlConnection.Open();
                if (m_SqlConnection.State == ConnectionState.Open)
                {
                    serverVersion = m_SqlConnection.ServerVersion;
                }
                else
                {
                    Console.WriteLine("连接失败");
                }
                m_SqlConnection.Close();
            }catch(Exception e)
            {

            }
            ViewBag.NETversion = version;
            ViewBag.ServerVersion = serverVersion;
            return View("envirDetection");
        }


        public ActionResult SetSysUser()
        {
            return View("initAdminUser");
        }

        /// <summary>
        /// 初始化数据库页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DataIndex()
        {
            return View("GenerateDataBase");
        }

        public JsonResult GenerateDataBase()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                //获取<configuration>下节点的信息
                string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection m_SqlConnection = new SqlConnection(connStr);
                string[] dbcon = connStr.Split(';')[1].Split('=');
                //读取sql文件内的所有内容
                ArrayList sqlList = Getarraylist("zhixiaoOA.sql", dbcon[1]);
                ExcuteSql(m_SqlConnection, sqlList);
                sqlList.Clear();
                //获取所有的表名
                ArrayList tableNameList = generSql(m_SqlConnection);
                //执行插入数据
                sqlList = Getarraylist("zhixiaoOA-data.sql", dbcon[1]);
                ExcuteSql(m_SqlConnection, sqlList, tableNameList);
                sqlList.Clear();
                //将旧的外键值替换为新的id值
                sqlList = updateFKID(tableNameList);
                ExcuteSql(m_SqlConnection, sqlList);

            }
            catch(Exception e)
            {
                Log4Helper.WriteError(e.Message, e);
                result.Message = e.Message;
                result.Code = ResultCode.Failure;
            }
            return Json(result);
        }

        /// <summary>
        /// 利用数组获取sql脚本文件中的sql语句
        /// </summary>
        /// <param name="targetdir">路劲</param>
        /// <param name="dbname">数据库名</param>
        /// <returns></returns>
        private ArrayList Getarraylist(string fileName, string dbname)
        {
            ArrayList sqllist = new ArrayList();
            ArrayList sqls = new ArrayList();
            try
            {
                string filePath = Server.MapPath("~/Data/");
                string path = Path.Combine(filePath,  fileName);
                string commandText = "";
                string varLine = "";
                StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
                while (sr.Peek() > -1)
                {
                    varLine = sr.ReadLine();
                    varLine = varLine.Replace("[ShanDongOA]", "[" + dbname + "]");
                    if (varLine == "")
                    {
                        continue;
                    }
                    if (varLine != "GO" && varLine != "go")
                    {
                        if (varLine.IndexOf("*/") > -1 || varLine.IndexOf("INSERT") > -1) {
                            commandText += varLine;
                            commandText += "\r\n";
                            sqllist.Add(commandText);
                            commandText = "";
                        }
                        else
                        {
                            commandText += varLine;
                            commandText += "\r\n";
                        }
                    }
                    else
                    {
                        sqllist.Add(commandText);
                        commandText = "";
                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                
            }

            return sqllist;
        }

        private void ExcuteSql(SqlConnection connectstring, ArrayList sql)
        {
            Cache cache = HttpRuntime.Cache;
            connectstring.Open();
            SqlTransaction varTrans = connectstring.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connectstring;
            cmd.Transaction = varTrans;
            foreach (string sqlstring in sql)
            {
                try
                {
                    if (sqlstring.IndexOf("/*") > -1 || sqlstring.IsNullOrEmpty())
                    {
                        continue;
                    }
                    if (sqlstring != null)
                    {
                        cmd.CommandText = sqlstring;
                        int i = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                }
            }
            varTrans.Commit();
            connectstring.Close();
        }

        private void ExcuteSql(SqlConnection connectstring, ArrayList sql, ArrayList tableNameList)
        {
            Cache cache = HttpRuntime.Cache;
            connectstring.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connectstring;
            foreach (string sqlstring in sql)
            {
                try
                {
                    if (sqlstring != null)
                    {
                        string sqldata = sqlstring;
                        string tableName = "";
                        string oldFkId = "";
                        sqldata = sqldata.Replace("#id,", "#id");
                        string[] sqls = Regex.Split(sqldata, "#id");
                        //遍历剔除ID的值
                        foreach (string name in tableNameList)
                        {
                            if (sqldata.IndexOf(name) > -1)
                            {
                                if (sqldata.IndexOf("/" + name) > -1)
                                {
                                    continue;
                                }
                                tableName = name;
                                oldFkId = sqls[1];
                                break;
                            }
                            else
                            {
                                oldFkId = "";
                            }
                        }
                        sqldata = sqls[0] + sqls[2] + "SELECT  @@IDENTITY  as  'id'";
                        cmd.CommandText = sqldata;
                        int id = cmd.ExecuteScalar().ToInt();
                        if (oldFkId.IsNotNullOrEmpty()) 
                        {
                            Dictionary<string, string> dic = null;
                            if (cache[tableName] == null)
                            {
                                dic = new Dictionary<string, string>();
                            }
                            else
                            {
                                dic = (Dictionary<string, string>)cache[tableName];
                            }
                            dic.Add(oldFkId, id.ToString());
                            //保存新的Id
                            cache.Insert(tableName, dic, null, DateTime.Now.AddSeconds(600), Cache.NoSlidingExpiration);
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            connectstring.Close();
        }

        //更新旧的外键值
        private ArrayList updateFKID(ArrayList tableNameList)
        {
            ArrayList sqlList = new ArrayList();
            Cache cache = HttpRuntime.Cache;
            foreach (string name in tableNameList)
            {
                string updateSql = "";
                string conlum = "";
                 Dictionary<string, string> dict1 = null;
                switch (name)
                {
                    case "Sys_Role_Menu":
                        dict1 = (Dictionary<string, string>)cache.Get("Sys_Menu");
                        foreach (var item in dict1)
                        {
                            conlum = "FK_MenuButtonId";
                            updateSql = "UPDATE " + name + " SET " + conlum  + " = "+ item.Value + " WHERE " + conlum + " = " + item.Key;
                            sqlList.Add(updateSql);
                        }
                        break;
                    case "Sys_Menu":
                        dict1 = (Dictionary<string, string>)cache.Get("Sys_Menu");
                        foreach (var item in dict1)
                        {
                            conlum = "MParentId";
                            updateSql = "UPDATE " + name + " SET " + conlum + " = " + item.Value + " WHERE " + conlum + " = " + item.Key;
                            sqlList.Add(updateSql);
                        }
                        break;
                    case "Sys_Button":
                        dict1 = (Dictionary<string, string>)cache.Get("Sys_Menu");
                        foreach (var item in dict1)
                        {
                            conlum = "FK_MenuId";
                            updateSql = "UPDATE " + name + " SET " + conlum + " = " + item.Value + " WHERE " + conlum + " = " + item.Key;
                            sqlList.Add(updateSql);
                        }
                        break;
                    case "Menu_Block":
                        dict1 = (Dictionary<string, string>)cache.Get("Sys_Menu");
                        foreach (var item in dict1)
                        {
                            conlum = "FK_MenuId";
                            updateSql = "UPDATE " + name + " SET " + conlum + " = " + item.Value + " WHERE " + conlum + " = " + item.Key;
                            sqlList.Add(updateSql);
                        }
                        break;
                    case "Dictionary":
                        dict1 = (Dictionary<string, string>)cache.Get("Dictionary");
                        foreach (var item in dict1)
                        {
                            conlum = "ParentId";
                            updateSql = "UPDATE " + name + " SET " + conlum + " = " + item.Value + " WHERE " + conlum + " = " + item.Key;
                            sqlList.Add(updateSql);
                        }
                        break;
                    default:
                        break;
                }
            }
            return sqlList;
        }

        //获取数据库所有表名
        private ArrayList generSql(SqlConnection connectstring)
        {
            ArrayList sqlList = new ArrayList();
            string sql = "SELECT NAME FROM SYSOBJECTS WHERE TYPE='U'";
            using (SqlCommand cmd = new SqlCommand(sql, connectstring))
            {
                try
                {
                    connectstring.Open();
                    SqlDataReader reader = cmd.ExecuteReader();//返回只能读取,不能写入的SqlDataReader对象
                    while (reader.Read())
                    {
                        sqlList.Add(reader["name"]);
                    }
                    return sqlList;
                }
                catch (Exception e)
                {
                    connectstring.Close();
                    throw e;
                }finally
                {
                    if (connectstring.State == ConnectionState.Open)
                    {
                        connectstring.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <returns></returns>
        public JsonResult saveUser()
        {
            AjaxResult result = new AjaxResult();
            try
            {
                Sys_User adminUser = Sys_UserBLL.GetModel(t => t.Where(a => a.Fk_RoleId == 1));
                if (adminUser != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Data/IsNew.txt")))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Data/IsNew.txt"));
                    }
                    throw new Exception("管理员用户已存在,请直接登录");
                }
                Sys_User model = new Sys_User();
                Stream reqStream = Request.InputStream;
                StreamReader streamReader = new StreamReader(reqStream);
                string json = streamReader.ReadToEnd();
                model = (Sys_User)JsonConvert.DeserializeObject(json, model.GetType());
                int row = 0;
                if (model.Id > 0)
                {
                    model.Pwd = EnDecrypt.SHA1Hash(model.Pwd);
                    model.UpdateTime = DateTime.Now;
                    row = Sys_UserBLL.UpdateModel(model);
                }
                else
                {
                    //唯一性验证 区分大小写
                    bool bl = Sys_UserBLL.CheckUserName(model.UserName);
                    if (bl)
                    {
                        throw new Exception("用户名已存在");
                    }
                    else
                    {
                        if (model.Pwd.IsNullOrEmpty())
                        {
                            model.Pwd = EnDecrypt.SHA1Hash("88888888");
                        }
                        else
                        {
                            model.Pwd = EnDecrypt.SHA1Hash(model.Pwd);
                        }
                        model.IsDel = 1;
                        model.Fk_RoleId = 1;
                        model.CreateTime = DateTime.Now;
                        model.Id = Sys_UserBLL.AddModel(model);
                        if (System.IO.File.Exists(Server.MapPath("~/Data/IsNew.txt")))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Data/IsNew.txt"));
                        }
                    }
                }
                result.Message = "保存成功";
            }
            catch (Exception e)
            {
                result.Code = ResultCode.Failure;
                result.Message = e.Message;
                Log4Helper.WriteError(e.Message, e);
            }
            return Json(result);
        }

    }
}