using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Tools;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace ZX.DAL
{
    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) Maticsoft
    /// </summary>
    public class DbHelper
    {
        //数据库连接字符串(web.config来配置)，多数据库可使用DbHelperSQLP来实现.
        public string connectionString = ConfigurationManager.ConnectionStrings["conn"].ToString();

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DbHelper() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbName">数据库名</param>
        public DbHelper(string dbName)
        {
            string connBegin = connectionString.Substring(0, connectionString.IndexOf("catalog=") + 8);
            string connEnd = connectionString.Substring(connectionString.IndexOf(";user"));
            connectionString = connBegin + dbName + connEnd;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customerId">参数无效,传0</param>
        /// <param name="dbServer">数据库服务地址</param>
        /// <param name="dbName">数据库名</param>
        public DbHelper(int customerId, string dbServer, string dbName)
        {
            string connBegin = connectionString.Substring(0, connectionString.IndexOf("catalog=") + 8);
            string connEnd = connectionString.Substring(connectionString.IndexOf(";user"));
            connectionString = connBegin + dbName + connEnd;
            connBegin = connectionString.Substring(0, connectionString.IndexOf("source=") + 7);
            connEnd = connectionString.Substring(connectionString.IndexOf(";initial"));
            connectionString = connBegin + dbServer + connEnd;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customerId">参数无效,传0</param>
        /// <param name="dbServer">数据库服务地址</param>
        public DbHelper(int customerId, string dbServer)
        {
            string connBegin = connectionString.Substring(0, connectionString.IndexOf("source=") + 7);
            string connEnd = connectionString.Substring(connectionString.IndexOf(";initial"));
            connectionString = connBegin + dbServer + connEnd;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strConnon">连接字符串</param>
        /// <param name="dbName">数据库名</param>
        public DbHelper(string strConnon, string dbName)
        {
            string connBegin = strConnon.Substring(0, strConnon.IndexOf("catalog=") + 8);
            string connEnd = strConnon.Substring(strConnon.IndexOf(";user"));
            connectionString = connBegin + dbName + connEnd;
        }
        #endregion

        #region 添加多条记录
        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entities">数据集合</param>
        /// <param name="batchSize">一次性最大插入条数</param>
        public void Insert<T>(IEnumerable<T> entities, int batchSize = 1000)
        {
            using (var dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        Insert(entities, transaction, SqlBulkCopyOptions.Default, batchSize);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entities">数据集合</param>
        /// <param name="tbName">表名</param>
        /// <param name="batchSize">一次性最大插入条数</param>
        public void Insert<T>(IEnumerable<T> entities, string tbName, int batchSize = 1000)
        {
            using (var dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        Insert(entities, transaction, SqlBulkCopyOptions.Default, batchSize, tbName);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="batchSize">一次性最大插入条数</param>
        public void Insert(DataTable dt, int batchSize = 1000)
        {
            using (var dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        Insert(dt, transaction, SqlBulkCopyOptions.Default, batchSize);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="tran">事务</param>
        /// <param name="options">按位标记</param>
        /// <param name="batchSize">一次性最大插入条数</param>
        void Insert(System.Data.DataTable dt, SqlTransaction tran, SqlBulkCopyOptions options, int batchSize)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(tran.Connection, options, tran))
            {
                sqlBulkCopy.BatchSize = batchSize;
                sqlBulkCopy.DestinationTableName = dt.TableName;
                sqlBulkCopy.WriteToServer(dt);
            }
        }
        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entities">数据集</param>
        /// <param name="transaction">事务</param>
        /// <param name="options">按位标记</param>
        /// <param name="batchSize">一次性最大插入条数</param>
        void Insert<T>(IEnumerable<T> entities, SqlTransaction transaction, SqlBulkCopyOptions options, int batchSize)
        {
            //TableMapping tableMapping = DbMapper.GetDbMapping(_context)[typeof(T).Name];
            using (DataTable dataTable = ListToDataTable(entities.ToList()))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, options, transaction))
                {
                    sqlBulkCopy.BatchSize = batchSize;
                    sqlBulkCopy.DestinationTableName = dataTable.TableName;
                    foreach (DataColumn item in dataTable.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                    }
                    sqlBulkCopy.WriteToServer(dataTable);
                }
            }
        }
        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entities">数据集</param>
        /// <param name="transaction">事务</param>
        /// <param name="options">按位标记</param>
        /// <param name="batchSize">一次性最大插入条数</param>
        /// <param name="tbName">表名</param>
        void Insert<T>(IEnumerable<T> entities, SqlTransaction transaction, SqlBulkCopyOptions options, int batchSize, string tbName)
        {
            //TableMapping tableMapping = DbMapper.GetDbMapping(_context)[typeof(T).Name];
            using (DataTable dataTable = ListToDataTable(entities.ToList()))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, options, transaction))
                {
                    sqlBulkCopy.BatchSize = batchSize;
                    sqlBulkCopy.DestinationTableName = tbName;
                    foreach (DataColumn item in dataTable.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                    }
                    sqlBulkCopy.WriteToServer(dataTable);
                }
            }
        }
        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable(typeof(T).Name);
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="tableMapping">表对象</param>
        /// <param name="entities">转换的数据集</param>
        /// <returns></returns>
        DataTable CreateDataTable<T>(TableMapping tableMapping, IEnumerable<T> entities)
        {
            var dataTable = BuildDataTable<T>(tableMapping);

            foreach (var entity in entities)
            {
                DataRow row = dataTable.NewRow();

                foreach (var columnMapping in tableMapping.Columns)
                {
                    var @value = entity.GetPropertyValue(columnMapping.PropertyName);

                    if (columnMapping.IsIdentity) continue;

                    if (@value == null)
                    {
                        row[columnMapping.ColumnName] = DBNull.Value;
                    }
                    else
                    {
                        row[columnMapping.ColumnName] = @value;
                    }
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        /// <summary>
        /// 构建表结构
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="tableMapping">表对象</param>
        /// <returns></returns>
        DataTable BuildDataTable<T>(TableMapping tableMapping)
        {
            var entityType = typeof(T);
            string tableName = string.Join(@".", tableMapping.SchemaName, tableMapping.TableName);

            var dataTable = new DataTable(tableName);
            var primaryKeys = new List<DataColumn>();

            foreach (var columnMapping in tableMapping.Columns)
            {
                var propertyInfo = entityType.GetProperty(columnMapping.PropertyName, '.');
                columnMapping.Type = propertyInfo.PropertyType;

                var dataColumn = new DataColumn(columnMapping.ColumnName);

                Type dataType;
                if (propertyInfo.PropertyType.IsNullable(out dataType))
                {
                    dataColumn.DataType = dataType;
                    dataColumn.AllowDBNull = true;
                }
                else
                {
                    dataColumn.DataType = propertyInfo.PropertyType;
                    dataColumn.AllowDBNull = columnMapping.Nullable;
                }

                if (columnMapping.IsIdentity)
                {
                    dataColumn.Unique = true;
                    if (propertyInfo.PropertyType == typeof(int)
                      || propertyInfo.PropertyType == typeof(long))
                    {
                        dataColumn.AutoIncrement = true;
                    }
                    else continue;
                }
                else
                {
                    dataColumn.DefaultValue = columnMapping.DefaultValue;
                }

                if (propertyInfo.PropertyType == typeof(string))
                {
                    dataColumn.MaxLength = columnMapping.MaxLength;
                }

                if (columnMapping.IsPk)
                {
                    primaryKeys.Add(dataColumn);
                }

                dataTable.Columns.Add(dataColumn);
            }

            dataTable.PrimaryKey = primaryKeys.ToArray();

            return dataTable;
        }
        #endregion

        #region 批量更新数据(每批次5000)
        /// <summary>
        /// 批量更新数据(每批次5000)
        /// </summary>
        /// <param name="connString">数据库链接字符串</param>
        /// <param name="table"></param>
        public void Update(DataTable table)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    try
                    {
                        //设置批量更新的每次处理条数
                        adapter.UpdateBatchSize = 5000;
                        //开始事务
                        adapter.SelectCommand.Transaction = connection.BeginTransaction();
                        if (table.ExtendedProperties["SQL"] != null)
                        {
                            adapter.SelectCommand.CommandText = table.ExtendedProperties["SQL"].ToString();
                        }
                        SqlCommandBuilder commandBulider = new SqlCommandBuilder(adapter);
                        commandBulider.ConflictOption = ConflictOption.OverwriteChanges;
                        adapter.UpdateCommand = cmd;
                        adapter.Update(table);
                        //提交事务
                        adapter.SelectCommand.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (adapter.SelectCommand != null && adapter.SelectCommand.Transaction != null)
                        {
                            //回滚事务
                            adapter.SelectCommand.Transaction.Rollback();
                        }
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region 批量更新数据(每批次5000)
        /// <summary>
        /// 批量更新数据(每批次5000)
        /// </summary>
        /// <param name="connString">数据库链接字符串</param>
        /// <param name="table"></param>
        public void Update<T>(IEnumerable<T> list)
        {
            DataTable table = ListToDataTable<T>(list.ToList());
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    try
                    {
                        //设置批量更新的每次处理条数
                        adapter.UpdateBatchSize = 5000;
                        //开始事务
                        adapter.SelectCommand.Transaction = connection.BeginTransaction();
                        if (table.ExtendedProperties["SQL"] != null)
                        {
                            adapter.SelectCommand.CommandText = table.ExtendedProperties["SQL"].ToString();
                        }
                        SqlCommandBuilder commandBulider = new SqlCommandBuilder(adapter);
                        commandBulider.ConflictOption = ConflictOption.OverwriteChanges;
                        adapter.Update(table);
                        //提交事务
                        adapter.SelectCommand.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (adapter.SelectCommand != null && adapter.SelectCommand.Transaction != null)
                        {
                            //回滚事务
                            adapter.SelectCommand.Transaction.Rollback();
                        }
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region 查询返回集合
        /// <summary>
        /// 查询返回集合
        /// </summary>
        /// <typeparam name="T">集合对象类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public List<T> ExecuteToList<T>(string sql, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                List<T> list = new List<T>();
                PrepareCommand(cmd, connection, null, sql, cmdParms);
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
                return list.Count > 0 ? list : new List<T>();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        #endregion

        #region 查询单个对象
        /// <summary>
        /// 查询单个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public T ExecuteToSingle<T>(string sql, params SqlParameter[] cmdParms)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        T obj = default(T);
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
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        #endregion

        #region 执行存储过程，返回影响的行数
        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int ExecuteProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                return result;
            }
        }
        #endregion

        #region 执行存储过程返回SqlDataReader
        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;

        }
        #endregion

        #region 执行存储过程返回DataSet
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="times">等待超时时间(单位:秒)</param>
        /// <returns></returns>
        public DataSet ExecuteProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        #endregion

        #region 执行存储过程,返回集合
        /// <summary>
        /// 执行存储过程,返回集合
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>list</returns>
        public List<T> ExecuteProcToList<T>(string procName, IDataParameter[] parameters)
        {
            try
            {
                List<T> list = new List<T>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = BuildQueryCommand(connection, procName, parameters))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行存储过程,返回单个对象
        /// <summary>
        /// 执行存储过程,返回集合
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>list</returns>
        public T ExecuteProcToSingle<T>(string procName, IDataParameter[] parameters)
        {
            try
            {
                List<T> list = new List<T>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = BuildQueryCommand(connection, procName, parameters))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader read = command.ExecuteReader(CommandBehavior.CloseConnection);
                        T obj = default(T);
                        while (read.Read())
                        {
                            obj = Activator.CreateInstance<T>();
                            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
                            foreach (var item in entityProperties)
                            {
                                PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                                if (read[item.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, read[item.Name], null);
                                }
                            }
                            break;
                        }
                        read.Close();
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行存储过程,返回第一行第一列值
        /// <summary>
        /// 执行存储过程,返回第一行第一列值
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public object ExecuteProcScalar(string procName, IDataParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = BuildQueryCommand(connection, procName, parameters))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        object obj = command.ExecuteScalar();
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行存储过程,返回第一行第一列值
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public object ExecuteProcScalar(string procName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = BuildQueryCommand(connection, procName, null))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        object obj = command.ExecuteScalar();
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行SQL语句，返回影响的记录数
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        //cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="times">等待超时时间(单位:秒)</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务
        /// </summary>
        /// <param name="sqlList">多条SQL语句集合</param>		
        public int ExecuteSqlTran(List<String> sqlList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    SqlTransaction tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    try
                    {
                        int count = 0;
                        for (int n = 0; n < sqlList.Count; n++)
                        {
                            string strsql = sqlList[n];
                            if (strsql.Trim().Length > 1)
                            {
                                cmd.CommandText = strsql;
                                count += cmd.ExecuteNonQuery();
                            }
                        }
                        tx.Commit();
                        return 1;
                    }
                    catch
                    {
                        tx.Rollback();
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTran(Hashtable sqlList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in sqlList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTranWithIndentity(Hashtable sqlList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in sqlList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region 执行查询语句
        /// <summary>
        /// 执行语句,返回第一行第一列值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行语句,返回第一行第一列值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="times">等待超时时间(单位:秒)</param>
        /// <returns></returns>
        public object GetSingle(string sql, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return read;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string sql, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            try
            {
                PrepareCommand(cmd, connection, null, sql, cmdParms);
                SqlDataReader read = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return read;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            //    }
            //}
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sql, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw ex;
                }
                return ds;
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sql, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        //cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw ex;
                    }
                    return ds;
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="times">等待超时时间(单位:秒)</param>
        /// <returns></returns>
        public DataSet Query(string sql, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sql, connection);
                    command.SelectCommand.CommandTimeout = times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw ex;
                }
                return ds;
            }
        }
        #endregion

        #region 构建SqlCommand
        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        /// <summary>
        /// 构建SqlCommand
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="conn">SqlConnection</param>
        /// <param name="trans">SqlConnection</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">参数</param>
        void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null) cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion
    }
}
