using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace ZX.Tools
{
    /// <summary>
    /// 默认缓存
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <param name="cacheObject">缓存对象</param>
        public static void Insert(string cacheKey, object cacheObject)
        {
            HttpRuntime.Cache.Insert(cacheKey, cacheObject);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <param name="cacheObject">缓存对象</param>
        /// <param name="expiration">缓存过期时间</param>
        public static void Insert(string cacheKey, object cacheObject, TimeSpan expiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, cacheObject, null, DateTime.MaxValue, expiration, CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <param name="cacheObject">缓存对象</param>
        /// <param name="minutes">缓存分钟</param>
        public static void Insert(string cacheKey, object cacheObject, int minutes)
        {
            Insert(cacheKey, cacheObject, new TimeSpan(0, minutes, 0));
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <param name="cacheObject">缓存对象</param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public static void SetCache(string cacheKey, object cacheObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, cacheObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="cacheKey">缓存Key</param>
        /// <returns>缓存对象</returns>
        public static T Get<T>(string cacheKey)
        {
            return (T)HttpRuntime.Cache[cacheKey];
        }

        /// <summary>
        /// 获取缓存Key
        /// </summary>
        /// <returns>缓存Key</returns>
        public static List<string> GetCacheKeys()
        {
            List<string> cacheKeys = new List<string>();
            IDictionaryEnumerator ide = HttpRuntime.Cache.GetEnumerator();
            while (ide.MoveNext())
            {
                cacheKeys.Add(ide.Key.ToString());
            }
            return cacheKeys;
        }

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        public static void Delete(string cacheKey)
        {
            if (Exists(cacheKey))
                HttpRuntime.Cache.Remove(cacheKey);
        }

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <returns>true or false</returns>
        public static bool Exists(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey] != null;
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void Flush()
        {
            List<string> cacheKeys = GetCacheKeys();
            foreach (string cacheKey in cacheKeys)
            {
                Delete(cacheKey);
            }
        }
    }

    public class CacheKey
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public const string LOGINSYSUSER = "LoginSysUser_";

        /// <summary>
        /// 用户菜单信息
        /// </summary>
        public const string SYSUSERMODULE = "SysUserModule_";

        /// <summary>
        /// 用户按钮信息
        /// </summary>
        public const string SYSUSERBUTTON = "SysUserButton_";

        /// <summary>
        /// 用户菜单信息
        /// </summary>
        public const string SYSMENUMANAGER = "SysMenuManager";

        /// <summary>
        /// 用户按钮信息
        /// </summary>
        public const string SYSBUTTON = "SYSBUTTON";
    }
}
