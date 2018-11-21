using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;

namespace ZX.Tools
{
    public static class DataConvert
    {
        static string abc = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        #region 转换为string类型
        /// <summary>
        /// 将object转换为string类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>string</returns>
        public static string ConvToString(this object oldObject, string defaultVal = "")
        {
            try
            {
                return oldObject + "";
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将double转换为string类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>string</returns>
        public static string ConvToString(this double oldObject, string defaultVal = "")
        {
            try
            {
                return oldObject + "";
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将Int32转换为string类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>string</returns>
        public static string ConvToString(this Int32 oldObject, string defaultVal = "")
        {
            try
            {
                return oldObject + "";
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将Int64转换为string类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>string</returns>
        public static string ConvToString(this Int64 oldObject, string defaultVal = "")
        {
            try
            {
                return oldObject + "";
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将decimal转换为string类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>string</returns>
        public static string ConvToString(this decimal oldObject, string defaultVal = "")
        {
            try
            {
                return oldObject + "";
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        #endregion

        #region 转换为int类型
        /// <summary>
        /// 将string转换为int类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static int ToInt(this string oldObject, int defaultVal = 0)
        {
            try
            {
                return Int32.Parse(oldObject);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将object转换为int类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static int ToInt(this object oldObject, int defaultVal = 0)
        {
            try
            {
                return Convert.ToInt32(oldObject);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将double转换为int类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static int ToInt(this double oldObject, int defaultVal = 0)
        {
            try
            {
                oldObject = Math.Round(oldObject);
                return (Int32)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将Int64转换为int类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static int ToInt(this Int64 oldObject, int defaultVal = 0)
        {
            try
            {
                return (Int32)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将decimal转换为int类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static int ToInt(this decimal oldObject, int defaultVal = 0)
        {
            try
            {
                return (Int32)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        #endregion

        #region 格式化时间
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static DateTime ToDate(this string oldObject)
        {
            try
            {
                return DateTime.Parse(oldObject.ToString());
            }
            catch (Exception)
            {
                return DateTime.Parse("1949-01-01");
            }
        }
        #endregion

        #region 格式化时间
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static string ToDateFormat(this DateTime? oldObject, string format = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                return DateTime.Parse(oldObject.ToString()).ToString(format);
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 格式化短日期格式
        /// <summary>
        /// 格式化短日期格式
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>int</returns>
        public static string ToShortDate(this DateTime? oldObject)
        {
            try
            {
                return Convert.ToDateTime(oldObject).ToShortDateString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 转换为long类型
        /// <summary>
        /// 将string转换为long类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>long</returns>
        public static long ToLong(this string oldObject, long defaultVal = 0)
        {
            try
            {
                return long.Parse(oldObject);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将object转换为long类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>long</returns>
        public static long ToLong(this object oldObject, long defaultVal = 0)
        {
            try
            {
                return long.Parse(oldObject != DBNull.Value ? oldObject + "" : "0");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将double转换为long类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>long</returns>
        public static long ToLong(this double oldObject, long defaultVal = 0)
        {
            try
            {
                return long.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将long转换为int64类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>long</returns>
        public static long ToLong(this int oldObject, int defaultVal = 0)
        {
            try
            {
                return long.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将Int16转换为long类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>long</returns>
        public static long ToLong(this Int16 oldObject, long defaultVal = 0)
        {
            try
            {
                return long.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将int64转换为long类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>long</returns>
        public static long ToLong(this Int64 oldObject, long defaultVal = 0)
        {
            try
            {
                return long.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将decimal转换为long类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>long</returns>
        public static long ToLong(this decimal oldObject, long defaultVal = 0)
        {
            try
            {
                return long.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        #endregion

        #region 转换为decimal类型
        /// <summary>
        /// 将string转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this string oldObject, decimal defaultVal = 0)
        {
            try
            {
                return decimal.Parse(oldObject);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将object转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this object oldObject, decimal defaultVal = 0)
        {
            try
            {
                decimal.TryParse(oldObject + "", out defaultVal);
                //return decimal.Parse(oldObject != DBNull.Value ? oldObject + "" : "0");
                return defaultVal;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将object转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="num">保留小数位数</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this object oldObject, int num, decimal defaultVal = 0)
        {
            try
            {
                decimal.TryParse(oldObject + "", out defaultVal);
                defaultVal = Math.Round(defaultVal, num, MidpointRounding.AwayFromZero);
                return defaultVal;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将decimal?转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="num">保留小数位数</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this decimal? oldObject, int num, decimal defaultVal = 0)
        {
            try
            {
                decimal.TryParse(oldObject + "", out defaultVal);
                defaultVal = Math.Round(defaultVal, num, MidpointRounding.AwayFromZero);
                return defaultVal;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将double转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this double oldObject, decimal defaultVal = 0)
        {
            try
            {
                return (decimal)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将double转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="num">保留小数位数</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this double oldObject, int num, decimal defaultVal = 0)
        {
            try
            {
                return (decimal)Math.Round(oldObject, num, MidpointRounding.AwayFromZero);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将double转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="num">保留小数位数</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this decimal oldObject, int num, decimal defaultVal = 0)
        {
            try
            {
                return Math.Round(oldObject, num, MidpointRounding.AwayFromZero);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将int转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this int oldObject, int defaultVal = 0)
        {
            try
            {
                return (decimal)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将Int16转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this Int16 oldObject, decimal defaultVal = 0)
        {
            try
            {
                return (decimal)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将int64转换为decimal类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this Int64 oldObject, decimal defaultVal = 0)
        {
            try
            {
                return (decimal)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        #endregion

        #region 转换为double类型
        /// <summary>
        /// 将string转换为double类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>double</returns>
        public static double ToDouble(this string oldObject, double defaultVal = 0)
        {
            try
            {
                return double.Parse(oldObject);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将object转换为double类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>double</returns>
        public static double ToDouble(this object oldObject, double defaultVal = 0)
        {
            try
            {
                return double.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将decimal转换为double类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>double</returns>
        public static double ToDecimal(this decimal oldObject, double defaultVal = 0)
        {
            try
            {
                return (double)oldObject;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将int转换为double类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>double</returns>
        public static double ToDouble(this int oldObject, int defaultVal = 0)
        {
            try
            {
                return double.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将Int16转换为double类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>double</returns>
        public static double ToDouble(this Int16 oldObject, double defaultVal = 0)
        {
            try
            {
                return double.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将int64转换为double类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>double</returns>
        public static double ToDouble(this Int64 oldObject, double defaultVal = 0)
        {
            try
            {
                return double.Parse(oldObject + "");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        #endregion

        #region 转换为bool类型
        /// <summary>
        /// 将string转换为bool类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>bool</returns>
        public static bool ToBoolean(this string oldObject, bool defaultVal = false)
        {
            try
            {
                return bool.Parse(oldObject);
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 将object转换为bool类型
        /// </summary>
        /// <param name="oldObject">需要转换的对象</param>
        /// <param name="defaultVal">默认返回值</param>
        /// <returns>double</returns>
        public static bool ToBoolean(this object oldObject, bool defaultVal = false)
        {
            try
            {
                return bool.Parse(oldObject != DBNull.Value ? oldObject + "" : "false");
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        #endregion

        #region 转换为SqlParameter的like参数
        /// <summary>
        /// 转换为SqlParameter的like参数
        /// </summary>
        /// <param name="oldObject"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string ToSqlLike(this string oldObject, string defaultVal = "%%")
        {
            try
            {
                return "%" + oldObject + "%";
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }
        #endregion

        #region Json序列化和反序列化
        /// <summary>
        /// json序列化
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="oldObject">序列化对象</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns>返回json格式字符串</returns>
        public static string ToJsonSerialize<T>(this T oldObject)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                return jss.Serialize(oldObject);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// json反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="oldObject">反序列化对象</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns>返回对象</returns>
        public static T ToJsonDeserialize<T>(this string oldObject)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                return jss.Deserialize<T>(oldObject);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public static string ToJsonByDataTable(this DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue;
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ConvToString());
                }
                arrayList.Add(dictionary);
            }
            return javaScriptSerializer.Serialize(arrayList);
        }
        #endregion

        #region List转换为DataTable
        /// <summary>
        /// List转换为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> entitys, string dtName = null)
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
            dtName = dtName ?? typeof(T).Name;
            DataTable dt = new DataTable(dtName);
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
        #endregion

        #region DataTable转换为List
        /// <summary>
        /// 将DataTable转换为List
        /// </summary>
        /// <typeparam name="T">List对象类型</typeparam>
        /// <param name="table">转换的DataTable</param>
        /// <returns>List集合</returns>
        public static List<T> ToList<T>(this DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return ConvertTo<T>(rows);
        }
        static List<T> ConvertTo<T>(IList<DataRow> rows)
        {
            List<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }
        static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        if (row[column.ColumnName] != DBNull.Value)
                        {
                            object value = row[column.ColumnName];
                            prop.SetValue(obj, value, null);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 将datarow转换为实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">当前行</param>
        /// <returns>当前实体</returns>
        public static T ToModel<T>(this DataRow row)
        {
            T obj = CreateItem<T>(row);
            return obj;
        }
        #endregion

        #region DataTable转换为List(报表专用)
        /// <summary>
        /// 将DataTable转换为List
        /// </summary>
        /// <typeparam name="T">List对象类型</typeparam>
        /// <param name="table">转换的DataTable</param>
        /// <param name="storeId">分店id</param>
        /// <param name="storeName">分店名称</param>
        /// <returns>List集合</returns>
        public static List<T> ToListByReport<T>(this DataTable table, int storeId, string storeName)
        {
            if (table == null)
            {
                return null;
            }
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return ConvertTo<T>(rows, storeId, storeName);
        }
        static List<T> ConvertTo<T>(IList<DataRow> rows, int storeId, string storeName)
        {
            List<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row, storeId, storeName);
                    list.Add(item);
                }
            }

            return list;
        }
        static T CreateItem<T>(DataRow row, int storeId, string storeName)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        switch (column.ColumnName)
                        {
                            case "StoreId":
                                prop.SetValue(obj, storeId, null);
                                break;
                            case "StoreName":
                                prop.SetValue(obj, storeName, null);
                                break;
                            default:
                                if (row[column.ColumnName] != DBNull.Value)
                                {
                                    object value = row[column.ColumnName];
                                    prop.SetValue(obj, value, null);
                                }
                                break;
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return obj;
        }
        #endregion

        #region 判断字符为空
        /// <summary>
        /// 判断字符为空
        /// </summary>
        /// <param name="oldObject"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string oldObject)
        {
            if (string.IsNullOrEmpty(oldObject))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断字符为空
        /// </summary>
        /// <param name="oldObject"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object oldObject)
        {
            string str = oldObject + "";
            if (str == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 判断字符不为空
        /// <summary>
        /// 判断字符不为空
        /// </summary>
        /// <param name="oldObject"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string oldObject)
        {
            if (string.IsNullOrEmpty(oldObject))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判断字符不为空
        /// </summary>
        /// <param name="oldObject"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this object oldObject)
        {
            string str = oldObject + "";
            if (str == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region 将string类型数组转换为int类型数组
        /// <summary>
        /// 将string类型数组转换为int类型数组
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this string[] strs)
        {
            try
            {
                int[] idArray = Array.ConvertAll<string, int>(strs, delegate (string s) { return s.ToInt(); });
                return idArray;
            }
            catch (Exception ex)
            {
                return new int[] { };
            }
        }

        /// <summary>
        /// 将string字符转换为int数组
        /// </summary>
        /// <param name="strs">string字符,用逗号","连接</param>
        /// <returns></returns>
        public static int[] ToIntArray(this string strs)
        {
            try
            {
                string[] strIds = strs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int[] idArray = Array.ConvertAll<string, int>(strIds, delegate (string s) { return s.ToInt(); });
                return idArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 将string类型数组转换为long类型数组
        /// <summary>
        /// 将string类型数组转换为long类型数组
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static long[] ToLongArray(this string[] strs)
        {
            try
            {
                long[] idArray = Array.ConvertAll<string, long>(strs, delegate (string s) { return s.ToLong(); });
                return idArray;
            }
            catch (Exception ex)
            {
                return new long[] { };
            }
        }

        /// <summary>
        /// 将string字符转换为int数组
        /// </summary>
        /// <param name="strs">string字符,用逗号","连接</param>
        /// <returns></returns>
        public static long[] ToLongArray(this string strs, string split = ",")
        {
            try
            {
                string[] strIds = strs.Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries);
                long[] idArray = Array.ConvertAll<string, long>(strIds, delegate (string s) { return s.ToLong(); });
                return idArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 取汉字拼音的首字母
        /// <summary>
        /// 取汉字拼音的首字母
        /// </summary>
        /// <param name="UnName">汉字</param>
        /// <returns>首字母</returns>
        public static string GetFirstCode(this string UnName)
        {
            int i = 0;
            ushort key = 0;
            string strResult = string.Empty;
            System.Text.Encoding unicode = System.Text.Encoding.Unicode;
            System.Text.Encoding gbk = System.Text.Encoding.GetEncoding(936);
            byte[] unicodeBytes = unicode.GetBytes(UnName);
            byte[] gbkBytes = System.Text.Encoding.Convert(unicode, gbk, unicodeBytes);
            while (i < gbkBytes.Length)
            {
                if (gbkBytes[i] <= 127)
                {
                    char a = (char)gbkBytes[i];
                    if (abc.Contains(a))
                    {
                        strResult = strResult + a;
                    }
                    i++;
                }
                #region 生成汉字拼音简码,取拼音首字母
                else
                {
                    key = (ushort)(gbkBytes[i] * 256 + gbkBytes[i + 1]);
                    if (key >= '\uB0A1' && key <= '\uB0C4')
                    {
                        strResult = strResult + "A";
                    }
                    else if (key >= '\uB0C5' && key <= '\uB2C0')
                    {
                        strResult = strResult + "B";
                    }
                    else if (key >= '\uB2C1' && key <= '\uB4ED')
                    {
                        strResult = strResult + "C";
                    }
                    else if (key >= '\uB4EE' && key <= '\uB6E9')
                    {
                        strResult = strResult + "D";
                    }
                    else if (key >= '\uB6EA' && key <= '\uB7A1')
                    {
                        strResult = strResult + "E";
                    }
                    else if (key >= '\uB7A2' && key <= '\uB8C0')
                    {
                        strResult = strResult + "F";
                    }
                    else if (key >= '\uB8C1' && key <= '\uB9FD')
                    {
                        strResult = strResult + "G";
                    }
                    else if (key >= '\uB9FE' && key <= '\uBBF6')
                    {
                        strResult = strResult + "H";
                    }
                    else if (key >= '\uBBF7' && key <= '\uBFA5')
                    {
                        strResult = strResult + "J";
                    }
                    else if (key >= '\uBFA6' && key <= '\uC0AB')
                    {
                        strResult = strResult + "K";
                    }
                    else if (key >= '\uC0AC' && key <= '\uC2E7')
                    {
                        strResult = strResult + "L";
                    }
                    else if (key >= '\uC2E8' && key <= '\uC4C2')
                    {
                        strResult = strResult + "M";
                    }
                    else if (key >= '\uC4C3' && key <= '\uC5B5')
                    {
                        strResult = strResult + "N";
                    }
                    else if (key >= '\uC5B6' && key <= '\uC5BD')
                    {
                        strResult = strResult + "O";
                    }
                    else if (key >= '\uC5BE' && key <= '\uC6D9')
                    {
                        strResult = strResult + "P";
                    }
                    else if (key >= '\uC6DA' && key <= '\uC8BA')
                    {
                        strResult = strResult + "Q";
                    }
                    else if (key >= '\uC8BB' && key <= '\uC8F5')
                    {
                        strResult = strResult + "R";
                    }
                    else if (key >= '\uC8F6' && key <= '\uCBF9')
                    {
                        strResult = strResult + "S";
                    }
                    else if (key >= '\uCBFA' && key <= '\uCDD9')
                    {
                        strResult = strResult + "T";
                    }
                    else if (key >= '\uCDDA' && key <= '\uCEF3')
                    {
                        strResult = strResult + "W";
                    }
                    else if (key >= '\uCEF4' && key <= '\uD188')
                    {
                        strResult = strResult + "X";
                    }
                    else if (key >= '\uD1B9' && key <= '\uD4D0')
                    {
                        strResult = strResult + "Y";
                    }
                    else if (key >= '\uD4D1' && key <= '\uD7F9')
                    {
                        strResult = strResult + "Z";
                    }
                    else
                    {
                        strResult = strResult + "?";
                    }
                    i = i + 2;
                }
                #endregion
            }
            return strResult;
        }
        #endregion

        #region 生成随机字母
        /// <summary>
        /// 生成随机字母
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="isUpper">是否大写</param>
        /// <returns></returns>
        public static string GetRandomAbc(int length)
        {
            string[] strArray = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M",
                                "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m"};
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                str.Append(strArray[rd.Next(51)]);
            }
            return str.ToString();
        }
        #endregion

        #region 生成随机数
        /// <summary>
        /// 生成随机数(默认四位随机数)
        /// </summary>
        /// <param name="startNumber">开始数字</param>
        /// <param name="endNumber">结束数字</param>
        /// <returns></returns>
        public static string GetRandomNumber(int startNumber = 1000, int endNumber = 9999)
        {
            Random rd = new Random();
            return rd.Next(startNumber, endNumber) + "";
        }
        #endregion

        #region 过滤特殊字符
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="oldObject">原字符串</param>
        /// <param name="defaultVal">默认字符串</param>
        /// <returns></returns>
        public static string ToFilterSpecial(this string oldObject, string defaultVal = null)
        {
            try
            {
                return oldObject.Replace("/", "").Replace("\\", "")
                    .Replace("<", "").Replace(">", "")
                    .Replace(",", "").Replace("`", "")
                    .Replace("~", "").Replace("!", "")
                    .Replace("@", "").Replace("#", "")
                    .Replace("$", "").Replace("%", "")
                    .Replace("^", "").Replace("&", "")
                    .Replace("?", "").Replace("*", "")
                    .Replace(":", "").Replace("\"", "")
                    .Replace("|", "");
            }
            catch (Exception ex)
            {
                return defaultVal;
            }
        }
        #endregion

        #region 生成时间戳
        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        #endregion

        #region 时间戳转化成时间(13位)
        /// <summary>
        /// 时间戳转化成时间(13位)
        /// </summary>
        /// <returns></returns>
        public static DateTime ToTimeStamp13(this long timestamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            long lTime = (timestamp.ToString() + "0000").ToLong();
            TimeSpan nowSpan = new TimeSpan(lTime);
            DateTime dt = startTime.Add(nowSpan);
            return dt;
        }
        #endregion

        #region 时间戳转化成时间(10位)
        /// <summary>
        /// 时间戳转化成时间(10位)
        /// </summary>
        /// <returns></returns>
        public static DateTime ToTimeStamp10(this long timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(timestamp);
        }
        #endregion

        #region 时间戳转化成时间(13位)
        /// <summary>
        /// 时间戳转化成时间(13位)
        /// </summary>
        /// <returns></returns>
        public static DateTime ToTimeStamp13(this string timestamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            long lTime = (timestamp.ToString() + "0000").ToLong();
            TimeSpan nowSpan = new TimeSpan(lTime);
            DateTime dt = startTime.Add(nowSpan);
            return dt;
        }
        #endregion

        #region 时间戳转化成时间(10位)
        /// <summary>
        /// 时间戳转化成时间(10位)
        /// </summary>
        /// <returns></returns>
        public static DateTime ToTimeStamp10(this string timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(timestamp.ToLong());
        }
        #endregion

        #region 时间相减
        /// <summary>
        /// 时间相减
        /// </summary>
        /// <returns></returns>
        public static TimeSpan ToSubDate(DateTime dt, DateTime dt1)
        {
            TimeSpan span = dt.Subtract(dt1);
            return span;
        }
        #endregion

        #region 数组连接成字符
        /// <summary>
        /// 数组连接成字符
        /// </summary>
        /// <param name="oldArray">当前数组</param>
        /// <param name="separator">用于连接的字符</param>
        /// <returns></returns>
        public static string ToJoinString(this IEnumerable<object> oldArray, string separator = ",")
        {
            try
            {
                return string.Join(separator, oldArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 数组连接成字符
        /// </summary>
        /// <param name="oldArray">当前数组</param>
        /// <param name="separator">用于连接的字符</param>
        /// <returns></returns>
        public static string ToJoinString(this IEnumerable<int> oldArray, string separator = ",")
        {
            try
            {
                return string.Join(separator, oldArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 获取枚举值名称
        /// <summary>
        /// 获取枚举值名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public static string GetEnumName<T>(this Int32? enumValue)
        {
            try
            {
                return Enum.GetName(typeof(T), enumValue);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region 获取枚举值名称
        /// <summary>
        /// 获取枚举值名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public static string GetEnumName<T>(this int enumValue)
        {
            try
            {
                return Enum.GetName(typeof(T), enumValue);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        public static string Html2Text(this string htmlStr)
        {
            if (String.IsNullOrEmpty(htmlStr))
            {
                return "";
            }
            string regEx_style = "<style[^>]*?>[\\s\\S]*?<\\/style>";//定义style的正则表达式
            string regEx_script = "<script[^>]*?>[\\s\\S]*?<\\/script>";//定义script的正则表达式
            string regEx_html = "<[^>]+>";  //定义HTML标签的正则表达式   
            htmlStr = Regex.Replace(htmlStr, regEx_style, ""); //删除css
            htmlStr = Regex.Replace(htmlStr, regEx_script, ""); //删除js
            htmlStr = Regex.Replace(htmlStr, regEx_html, ""); //删除html标记
            htmlStr = Regex.Replace(htmlStr, "\\s*|\t|\r|\n", ""); //去除tab、空格、空行
            htmlStr = htmlStr.Replace(" ", "");
            htmlStr = htmlStr.Replace("\"", "");
            htmlStr = htmlStr.Replace("\"", "");
            htmlStr = htmlStr.Replace("&nbsp;", "");
            return htmlStr.Trim();
        }
    }
}
