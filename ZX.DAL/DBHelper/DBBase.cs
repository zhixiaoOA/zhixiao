using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using ZX.Tools;
using ZX.Model;
using System.Data.SqlClient;
using System.Reflection;


namespace ZX.DAL
{
    public class DBBase<T> : IBulkHelper<T> where T : BaseModel
    {
        public DbHelper Db = new DbHelper();
        public Pmts Pmts = new Pmts();
        public string strConn = ConfigurationManager.ConnectionStrings["conn"].ToString();
        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="notFields">排除的字段</param>
        /// <returns></returns>
        public virtual int AddModel(T t)
        {
            if (t == null) throw new Exception("添加的对象不能为空!");
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = CreateAddSql(t);
                SqlParameter[] pmts = CreateParameter(t);
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    try
                    {
                        int id = cmd.ExecuteScalar().ToInt();
                        return id;
                    }
                    catch (SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region 添加多条数据
        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="list">添加的实体集合</param>
        /// <returns></returns>
        public int AddModel(List<T> list)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                int rows = 1;
                conn.Open();
                T obj = Activator.CreateInstance<T>();
                string tbName = obj.GetType().Name;
                DataTable dt = list.ToDataTable<T>();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            sqlBulkCopy.BatchSize = 10000;
                            sqlBulkCopy.DestinationTableName = tbName;
                            foreach (DataColumn item in dt.Columns)
                            {
                                sqlBulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                            }
                            sqlBulkCopy.WriteToServer(dt);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw ex;
                    }
                }
                return rows;
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public int DelModel(AiExpConditions<T> exp)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            string sql = "DELETE " + tbName + " " + exp.GetWhereParameter();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public int DelModel(string where)
        {
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            string sql = "DELETE " + tbName + " WHERE " + where;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, null))
                {
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region 根据id删除数据
        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="ids">ids集合</param>
        /// <returns></returns>
        public int DelModelById(string ids)
        {
            string where = "";
            List<SqlParameter> listPmts = new List<SqlParameter>();
            string[] idsArray = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < idsArray.Length; i++)
            {
                where += "@p" + i + ",";
                listPmts.Add(new SqlParameter("@p" + i, idsArray[i].ToLong()));
            }
            if (where != "") where = where.Substring(0, where.Length - 1);
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            string sql = "DELETE " + tbName + " WHERE Id IN(" + where + ")";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, listPmts.ToArray()))
                {
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="t">修改的实体</param>
        /// <returns></returns>
        public int UpdateModel(T t, AiExpConditions<T> exp)
        {
            if (t == null) throw new Exception("修改的对象不能为空!");
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string where = exp.GetWhereParameter() + exp.OrderBy();
                List<SqlParameter> listPmts = new List<SqlParameter>();
                SqlParameter[] pmts2 = exp.GetValParameter();
                //string where = exp.Where();
                string sql = CreateUpdateSql(t, where);
                SqlParameter[] pmts = CreateUpdateParameter(t);
                listPmts.AddRange(pmts);
                if (pmts2 != null) listPmts.AddRange(pmts2);
                using (SqlCommand cmd = CreateCommand(conn, null, sql, listPmts.ToArray()))
                {
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="t">修改的实体</param>
        /// <returns></returns>
        public int UpdateModel(T t, string fields, string where)
        {
            if (t == null) throw new Exception("修改的对象不能为空!");
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = CreateUpdateSql(t, fields, where);
                SqlParameter[] pmts = CreateUpdateParameter(t);
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region Merge数据
        /// <summary>
        /// Merge数据
        /// </summary>
        /// <param name="list">Merge的数据集合</param>
        /// <param name="where">条件</param>
        /// <param name="notFields">排除的字段(用逗号","分割)</param>
        /// <returns></returns>
        public int MergeModel(List<T> list, string where, string deleteWhere)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                int rows = 0;
                conn.Open();
                T obj = Activator.CreateInstance<T>();
                string tbName = obj.GetType().Name;
                DataTable dt = list.ToDataTable<T>();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "SELECT * INTO #" + tbName + " FROM " + tbName + " WHERE 1>2";
                        using (SqlCommand cmd = CreateCommand(conn, transaction, sql, null))
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            sqlBulkCopy.BatchSize = 10000;
                            sqlBulkCopy.DestinationTableName = "#" + tbName;
                            foreach (DataColumn item in dt.Columns)
                            {
                                sqlBulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                            }
                            sqlBulkCopy.WriteToServer(dt);
                        }
                        sql = CreateMergeSql(where, deleteWhere);
                        using (SqlCommand cmd = CreateCommand(conn, transaction, sql, null))
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw ex;
                    }
                }
                return rows;
            }
        }
        #endregion

        #region Merge数据(不执行删除)
        /// <summary>
        /// Merge数据(不执行删除)
        /// </summary>
        /// <param name="list">Merge的数据集合</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public int MergeNotDelModel(List<T> list, string where)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                int rows = 0;
                conn.Open();
                T obj = Activator.CreateInstance<T>();
                string tbName = obj.GetType().Name;
                DataTable dt = list.ToDataTable<T>();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "SELECT * INTO #" + tbName + " FROM " + tbName + " WHERE 1>2";
                        using (SqlCommand cmd = CreateCommand(conn, transaction, sql, null))
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            sqlBulkCopy.BatchSize = 10000;
                            sqlBulkCopy.DestinationTableName = "#" + tbName;
                            foreach (DataColumn item in dt.Columns)
                            {
                                sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(item.ColumnName, item.ColumnName));
                            }
                            sqlBulkCopy.WriteToServer(dt);
                        }
                        sql = CreateMergeNotDelSql(where);
                        using (SqlCommand cmd = CreateCommand(conn, transaction, sql, null))
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw ex;
                    }
                }
                return rows;
            }
        }
        #endregion

        #region Merge数据(不执行删除)
        /// <summary>
        /// Merge数据(不执行删除)
        /// </summary>
        /// <param name="dt">Merge的数据集合</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public int MergeNotDelModel(DataTable dt, string where)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                int rows = 0;
                conn.Open();
                T obj = Activator.CreateInstance<T>();
                string tbName = obj.GetType().Name;
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "SELECT * INTO #" + tbName + " FROM " + tbName + " WHERE 1>2";
                        using (SqlCommand cmd = CreateCommand(conn, transaction, sql, null))
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            sqlBulkCopy.BatchSize = 10000;
                            sqlBulkCopy.DestinationTableName = "#" + tbName;
                            foreach (DataColumn item in dt.Columns)
                            {
                                sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(item.ColumnName, item.ColumnName));
                            }
                            sqlBulkCopy.WriteToServer(dt);
                        }
                        sql = CreateMergeNotDelSql(where);
                        using (SqlCommand cmd = CreateCommand(conn, transaction, sql, null))
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw ex;
                    }
                }
                return rows;
            }
        }
        #endregion

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            string sql = CreateSelectSql(null);
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, null))
                {
                    SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (read.Read())
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] entityProperties = obj.GetType().GetProperties();
                        foreach (var item in entityProperties)
                        {
                            PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                            try
                            {
                                if (read[item.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, read[item.Name], null);
                                }
                            }
                            catch (Exception ex) { }
                        }
                        list.Add(obj);
                    }
                    read.Close();
                    return list;
                }
            }
        }
        #endregion

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public List<T> GetList(string strWhere)
        {
            string sql = CreateSelectSql(strWhere.IsNotNullOrEmpty() ? " WHERE " + strWhere : "");
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, null))
                {
                    SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (read.Read())
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] entityProperties = obj.GetType().GetProperties();
                        foreach (var item in entityProperties)
                        {
                            PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                            try
                            {
                                if (read[item.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, read[item.Name], null);
                                }
                            }
                            catch (Exception ex) { }
                        }
                        list.Add(obj);
                    }
                    read.Close();
                    return list;
                }
            }
        }
        #endregion

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public List<T> GetList(AiExpConditions<T> exp)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            string strWhere = exp != null ? (" " + exp.GetWhereParameter() + exp.OrderBy()) : "";
            string sql = CreateSelectSql(strWhere);
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (read.Read())
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] entityProperties = obj.GetType().GetProperties();
                        foreach (var item in entityProperties)
                        {
                            PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                            try
                            {
                                if (read[item.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, read[item.Name], null);
                                }
                            }
                            catch (Exception ex) { }
                        }
                        list.Add(obj);
                    }
                    read.Close();
                    return list;
                }
            }
        }
        #endregion

        #region 获取单个数据
        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public T GetModel(AiExpConditions<T> exp)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            string strWhere = exp != null ? (" " + exp.GetWhereParameter() + exp.OrderBy()) : "";
            string sql = CreateSelectSql(strWhere);
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    T obj = null;
                    SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (read.Read())
                    {
                        obj = Activator.CreateInstance<T>();
                        PropertyInfo[] entityProperties = obj.GetType().GetProperties();
                        foreach (var item in entityProperties)
                        {
                            PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                            try
                            {
                                if (read[item.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, read[item.Name], null);
                                }
                            }
                            catch (Exception ex) { }
                        }
                        break;
                    }
                    read.Close();
                    return obj;
                }
            }
        }
        #endregion

        #region 获取count记录
        /// <summary>
        /// 获取count记录
        /// </summary>
        /// <param name="exp">条件</param>
        /// <param name="countName">count字段</param>        
        /// <returns></returns>
        public object GetCount(AiExpConditions<T> exp, string countName)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            string where = exp.GetWhereParameter() + exp.OrderBy();
            string sql = CreateCountSql(countName) + " " + where;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
        }
        #endregion

        #region 获取sum记录
        /// <summary>
        /// 获取sum记录
        /// </summary>
        /// <param name="sumName">sum字段</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public object GetSum(string sumName, AiExpConditions<T> exp)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            string sql = CreateSumSql(sumName) + " " + exp.GetWhereParameter() + exp.OrderBy();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
        }
        #endregion

        #region 获取max记录
        /// <summary>
        /// 获取max记录
        /// </summary>
        /// <param name="maxName">max字段</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public object GetMax(string maxName, AiExpConditions<T> exp)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            string sql = CreateMaxSql(maxName) + " " + exp.GetWhereParameter() + exp.OrderBy();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
        }
        #endregion

        #region 获取top数据
        /// <summary>
        /// 获取top数据
        /// </summary>
        /// <param name="top">top条数</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public List<T> GetTopList(int top, AiExpConditions<T> exp)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            string sql = CreateTopSql(top) + " " + exp.GetWhereParameter() + exp.OrderBy();
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (read.Read())
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] entityProperties = obj.GetType().GetProperties();
                        foreach (var item in entityProperties)
                        {
                            PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                            try
                            {
                                if (read[item.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, read[item.Name], null);
                                }
                            }
                            catch (Exception ex) { }
                        }
                        list.Add(obj);
                    }
                    read.Close();
                    return list;
                }
            }
        }
        #endregion

        #region 检测数据是否存在
        /// <summary>
        /// 检测数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool CheckModel(AiExpConditions<T> exp)
        {
            SqlParameter[] pmts = exp.GetValParameter();
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            string sql = "SELECT 1 FROM [" + tbName + "] " + exp.GetWhereParameter() + exp.OrderBy();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = CreateCommand(conn, null, sql, pmts))
                {
                    object o = cmd.ExecuteScalar();
                    return o.ToInt() > 0;
                }
            }
        }
        #endregion

        #region 创建添加sql语句
        /// <summary>
        /// 创建添加sql语句
        /// </summary>
        /// <param name="obj">添加的实体对象</param>
        /// <returns></returns>
        string CreateAddSql(T obj)
        {
            object attribute = null;
            StringBuilder sqlField = new StringBuilder(@"INSERT INTO [" + obj.GetType().Name + "](");
            StringBuilder sqlValues = new StringBuilder("VALUES(");
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            if (entityProperties == null || entityProperties.Length == 0) throw new Exception("没有可添加的字段!");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var item = entityProperties[i];
                object o = item.GetValue(obj, null);
                if (o == null)
                {
                    if (i == entityProperties.Length - 1)
                    {
                        sqlField = sqlField.Remove(sqlField.Length - 1, 1);
                        sqlValues = sqlValues.Remove(sqlValues.Length - 1, 1);
                        sqlField.Append(")");
                        sqlValues.Append(")SELECT IDENT_CURRENT('" + obj.GetType().Name + "')");
                    }
                    continue;
                }
                attribute = item.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attribute != null)
                {
                    DataFieldAttribute[] colattrib = (DataFieldAttribute[])attribute;
                    if (colattrib.Length > 0)
                    {
                        DataFieldAttribute dfa = colattrib[0];
                        if (dfa.PK == "pk")
                        {
                            if (i == entityProperties.Length - 1)
                            {
                                sqlField = sqlField.Remove(sqlField.Length - 1, 1);
                                sqlValues = sqlValues.Remove(sqlValues.Length - 1, 1);
                                sqlField.Append(")");
                                sqlValues.Append(")SELECT IDENT_CURRENT('" + obj.GetType().Name + "')");
                            }
                            continue;
                        }
                    }
                }
                if (i == entityProperties.Length - 1)
                {
                    sqlField.Append(item.Name + ")");
                    sqlValues.Append("@" + item.Name + ")SELECT IDENT_CURRENT('" + obj.GetType().Name + "')");
                }
                else
                {
                    sqlField.Append(item.Name + ",");
                    sqlValues.Append("@" + item.Name + ",");
                }
            }
            sqlField.Append(sqlValues.ToString());
            return sqlField.ToString();
        }
        #endregion
        #region 创建修改sql语句
        /// <summary>
        /// 创建修改sql语句
        /// </summary>
        /// <param name="obj">修改的实体对象</param>
        /// <param name="strWhere">排除的字段集合</param>
        /// <returns></returns>
        string CreateUpdateSql(T obj, string strWhere)
        {
            object attribute = null;
            StringBuilder sqlField = new StringBuilder("UPDATE [" + obj.GetType().Name + "] SET ");
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            if (entityProperties == null || entityProperties.Length == 0) throw new Exception("没有可修改的字段!");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var item = entityProperties[i];
                object o = item.GetValue(obj, null);
                if (o == null)
                {
                    if (i == entityProperties.Length - 1)
                    {
                        sqlField.Append(item.Name + "=@" + item.Name + " ");
                    }
                    continue;
                }
                attribute = item.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attribute != null)
                {
                    DataFieldAttribute[] colattrib = (DataFieldAttribute[])attribute;
                    if (colattrib.Length > 0)
                    {
                        DataFieldAttribute dfa = colattrib[0];
                        if (dfa.PK == "pk")
                        {
                            if (i == entityProperties.Length - 1)
                            {
                                sqlField = sqlField.Remove(sqlField.Length - 1, 1);
                            }
                            continue;
                        }
                    }
                }
                if (i == entityProperties.Length - 1)
                {
                    sqlField.Append(item.Name + "=@" + item.Name + " ");
                }
                else
                {
                    sqlField.Append(item.Name + "=@" + item.Name + ",");
                }
            }
            sqlField.Append(" " + strWhere);
            return sqlField.ToString();
        }
        #endregion
        #region 创建修改sql语句(修改某个字段)
        /// <summary>
        /// 创建修改sql语句(修改某个字段)
        /// </summary>
        /// <param name="obj">修改的实体对象</param>
        /// <param name="fields">修改的字段</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        string CreateUpdateSql(T obj, string fields, string strWhere)
        {
            StringBuilder sqlField = new StringBuilder("UPDATE [" + obj.GetType().Name + "] SET " + fields + " WHERE " + strWhere);
            return sqlField.ToString();
        }
        #endregion
        #region 创建Merge语句
        /// <summary>
        /// 创建Merge语句
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="deleteWhere">删除条件</param>
        /// <returns></returns>
        string CreateMergeSql(string strWhere, string deleteWhere)
        {
            object attribute = null;
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            StringBuilder sqlField = new StringBuilder("MERGE INTO [" + tbName + "] AS A\n");
            sqlField.Append("USING #" + tbName + " AS B ON " + strWhere + "\n");
            sqlField.Append("WHEN MATCHED THEN\n");
            StringBuilder updateSql = new StringBuilder("UPDATE SET ");
            StringBuilder insertSql = new StringBuilder("INSERT(");
            StringBuilder insertValSql = new StringBuilder("VALUES(");
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            if (entityProperties == null || entityProperties.Length == 0) throw new Exception("没有可添加的字段!");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var item = entityProperties[i];
                attribute = item.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attribute != null)
                {
                    DataFieldAttribute[] colattrib = (DataFieldAttribute[])attribute;
                    if (colattrib.Length > 0)
                    {
                        DataFieldAttribute dfa = colattrib[0];
                        if (dfa.PK == "pk")
                        {
                            if (i == entityProperties.Length - 1)
                            {
                                updateSql = updateSql.Remove(updateSql.Length - 1, 1);
                                insertSql = insertSql.Remove(insertSql.Length - 1, 1);
                                insertSql.Append(")");
                                insertValSql = insertValSql.Remove(insertValSql.Length - 1, 1);
                                insertValSql.Append(")");
                            }
                            continue;
                        }
                    }
                }
                if (i == entityProperties.Length - 1)
                {
                    updateSql.Append("A." + item.Name + "=B." + item.Name);
                    insertSql.Append(item.Name + ")");
                    insertValSql.Append("B." + item.Name + ")");
                }
                else
                {
                    updateSql.Append("A." + item.Name + "=B." + item.Name + ",");
                    insertSql.Append(item.Name + ",");
                    insertValSql.Append("B." + item.Name + ",");
                }
            }
            sqlField.Append(updateSql + "\n");
            sqlField.Append("WHEN NOT MATCHED THEN\n");
            sqlField.Append(insertSql);
            sqlField.Append(insertValSql + "\n");
            sqlField.Append("WHEN NOT MATCHED BY SOURCE " + (deleteWhere.IsNotNullOrEmpty() ? "AND " + deleteWhere : "") + " THEN DELETE;");
            return sqlField.ToString();
        }
        #endregion
        #region 创建Merge语句(不执行删除)
        /// <summary>
        /// 创建Merge语句
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        string CreateMergeNotDelSql(string strWhere)
        {
            object attribute = null;
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            StringBuilder sqlField = new StringBuilder("MERGE INTO [" + tbName + "] AS A\n");
            sqlField.Append("USING #" + tbName + " AS B ON " + strWhere + "\n");
            sqlField.Append("WHEN MATCHED THEN\n");
            StringBuilder updateSql = new StringBuilder("UPDATE SET ");
            StringBuilder insertSql = new StringBuilder("INSERT(");
            StringBuilder insertValSql = new StringBuilder("VALUES(");
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            if (entityProperties == null || entityProperties.Length == 0) throw new Exception("没有可添加的字段!");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var item = entityProperties[i];
                attribute = item.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (attribute != null)
                {
                    DataFieldAttribute[] colattrib = (DataFieldAttribute[])attribute;
                    if (colattrib.Length > 0)
                    {
                        DataFieldAttribute dfa = colattrib[0];
                        if (dfa.PK == "pk")
                        {
                            if (i == entityProperties.Length - 1)
                            {
                                updateSql = updateSql.Remove(updateSql.Length - 1, 1);
                                insertSql = insertSql.Remove(insertSql.Length - 1, 1);
                                insertSql.Append(")");
                                insertValSql = insertValSql.Remove(insertValSql.Length - 1, 1);
                                insertValSql.Append(")");
                            }
                            continue;
                        }
                    }
                }
                if (i == entityProperties.Length - 1)
                {
                    updateSql.Append("A." + item.Name + "=B." + item.Name);
                    insertSql.Append(item.Name + ")");
                    insertValSql.Append("B." + item.Name + ")");
                }
                else
                {
                    updateSql.Append("A." + item.Name + "=B." + item.Name + ",");
                    insertSql.Append(item.Name + ",");
                    insertValSql.Append("B." + item.Name + ",");
                }
            }
            sqlField.Append(updateSql + "\n");
            sqlField.Append("WHEN NOT MATCHED THEN\n");
            sqlField.Append(insertSql);
            sqlField.Append(insertValSql + ";");
            return sqlField.ToString();
        }
        #endregion
        #region 创建查询sql语句
        /// <summary>
        /// 创建查询sql语句
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        string CreateSelectSql(string strWhere)
        {
            //object attribute = null;
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            StringBuilder sqlField = new StringBuilder("SELECT ");
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            if (entityProperties == null || entityProperties.Length == 0) throw new Exception("没有可查询的字段!");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var item = entityProperties[i];
                //attribute = item.GetCustomAttributes(typeof(DataFieldAttribute), false);
                //if (attribute != null)
                //{
                //    DataFieldAttribute[] colattrib = (DataFieldAttribute[])attribute;
                //    if (colattrib.Length > 0)
                //    {
                //        DataFieldAttribute dfa = colattrib[0];
                //        if (dfa.PK == "pk")
                //        {
                //            continue;
                //        }
                //    }
                //}
                sqlField.Append("[" + item.Name + "],");
            }
            sqlField = sqlField.Remove(sqlField.Length - 1, 1);
            sqlField.Append(" FROM [" + tbName + "] " + strWhere);

            return sqlField.ToString();
        }
        #endregion
        #region 创建求数量sql语句
        /// <summary>
        /// 创建求数量sql语句
        /// </summary>
        /// <param name="countName">数量字段</param>
        /// <returns></returns>
        string CreateCountSql(string countName)
        {
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            StringBuilder sqlField = new StringBuilder("SELECT ");
            sqlField.Append("COUNT(" + countName + ") ");
            sqlField.Append("FROM [" + tbName + "]");
            return sqlField.ToString();
        }
        #endregion
        #region 创建求和sql语句
        /// <summary>
        /// 创建求和sql语句
        /// </summary>
        /// <param name="sumName">求和字段</param>
        /// <returns></returns>
        string CreateSumSql(string sumName)
        {
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            StringBuilder sqlField = new StringBuilder("SELECT ");
            sqlField.Append("SUM(" + sumName + ") ");
            sqlField.Append("FROM [" + tbName + "]");
            return sqlField.ToString();
        }
        #endregion
        #region 创建最大值sql语句
        /// <summary>
        /// 创建最大值sql语句
        /// </summary>
        /// <param name="maxName">最大值字段字段</param>
        /// <returns></returns>
        string CreateMaxSql(string maxName)
        {
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            StringBuilder sqlField = new StringBuilder("SELECT ");
            sqlField.Append("MAX(" + maxName + ") ");
            sqlField.Append("FROM [" + tbName + "]");
            return sqlField.ToString();
        }
        #endregion
        #region 创建TOP sql语句
        /// <summary>
        /// 创建TOP sql语句
        /// </summary>
        /// <param name="top">条数</param>
        /// <returns></returns>
        string CreateTopSql(int top)
        {
            T obj = Activator.CreateInstance<T>();
            string tbName = obj.GetType().Name;
            StringBuilder sqlField = new StringBuilder("SELECT ");
            sqlField.Append("TOP(" + top + ") * ");
            sqlField.Append("FROM [" + tbName + "]");
            return sqlField.ToString();
        }
        #endregion
        #region 创建SqlParameter
        /// <summary>
        /// 创建SqlParameter
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        SqlParameter[] CreateParameter(T obj)
        {
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            if (entityProperties == null || entityProperties.Length == 0) throw new Exception("没有可添加的字段!");
            List<SqlParameter> listPmts = new List<SqlParameter>();
            int index = 0;
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var item = entityProperties[i];
                object o = item.GetValue(obj, null);
                if (o == null) continue;
                try
                {
                    PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                    listPmts.Add(new SqlParameter(item.Name, prop.GetValue(obj, null) ?? ""));
                }
                catch (Exception ex)
                {
                }
                index++;
            }
            return listPmts.ToArray();
        }
        #endregion
        #region 创建修改SqlParameter
        /// <summary>
        /// 创建修改SqlParameter
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        SqlParameter[] CreateUpdateParameter(T obj)
        {
            object attribute = null;
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            if (entityProperties == null || entityProperties.Length == 0) throw new Exception("没有可添加的字段!");
            List<SqlParameter> listPmts = new List<SqlParameter>();
            int index = 0;
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var item = entityProperties[i];
                try
                {
                    object o = item.GetValue(obj, null);
                    if (o == null) continue;
                    attribute = item.GetCustomAttributes(typeof(DataFieldAttribute), false);
                    if (attribute != null)
                    {
                        DataFieldAttribute[] colattrib = (DataFieldAttribute[])attribute;
                        if (colattrib.Length > 0)
                        {
                            DataFieldAttribute dfa = colattrib[0];
                            if (dfa.PK == "pk")
                            {
                                continue;
                            }
                        }
                    }
                    SqlParameter pmt = new SqlParameter(item.Name, o ?? "");
                    listPmts.Add(pmt);
                }
                catch (Exception ex)
                {
                }
                index++;
            }
            return listPmts.ToArray();
        }
        #endregion
        #region 创建SqlCommand
        /// <summary>
        /// 创建SqlCommand
        /// </summary>
        /// <param name="conn">SqlConnection连接对象</param>
        /// <param name="trans">事务</param>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdParms">sql语句参数</param>
        /// <param name="cmdType">语句类型</param>
        SqlCommand CreateCommand(SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms, CommandType cmdType = CommandType.Text)
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State != ConnectionState.Open) conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null) cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null) cmd.Parameters.AddRange(cmdParms);
            //if (cmdParms != null)
            //{
            //    foreach (SqlParameter parameter in cmdParms)
            //    {
            //        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
            //            (parameter.Value == null))
            //        {
            //            parameter.Value = DBNull.Value;
            //        }
            //        cmd.Parameters.Add(parameter);
            //    }
            //}
            return cmd;
        }
        #endregion
    }
}
