using System;
using System.Reflection;
using ZX.Model;
using ZX.Tools;

namespace ZX.Web
{
    public class FormHelper
    {
        #region 将表单提交的数据转换为对象
        /// <summary>
        /// 将表单提交的数据转换为对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <returns></returns>
        public static T GetRequestForm<T>() where T : BaseModel, new()
        {
            T obj = Activator.CreateInstance<T>();
            PropertyInfo[] entityProperties = obj.GetType().GetProperties();
            foreach (var item in entityProperties)
            {
                PropertyInfo prop = obj.GetType().GetProperty(item.Name);
                try
                {
                    object o = System.Web.HttpContext.Current.Request.Form[item.Name];
                    if (o != null)
                    {
                        switch (prop.PropertyType.ToString())
                        {
                            case "System.Nullable`1[System.Double]":
                            case "System.Double":
                                o = o.ToDouble();
                                break;
                            case "System.Nullable`1[System.Decimal]":
                            case "System.Decimal":
                                o = o.ToDecimal();
                                break;
                            case "System.Nullable`1[System.Int32]":
                            case "System.Int32":
                                o = o.ToInt();
                                break;
                            case "System.Nullable`1[System.Int64]":
                            case "System.Int64":
                                o = o.ToLong();
                                break;
                            case "System.Nullable`1[System.Boolean]":
                            case "System.Boolean":
                                o = o.ToBoolean();
                                break;
                            case "System.Nullable`1[System.DateTime]":
                            case "System.DateTime":
                                o = Convert.ToDateTime(o);
                                break;
                        }
                        prop.SetValue(obj, o, null);
                    }
                }
                catch (Exception ex) { }
            }
            return obj;
        }
        #endregion
    }
}