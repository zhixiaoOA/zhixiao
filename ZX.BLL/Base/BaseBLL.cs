using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZX.DAL;
using ZX.Tools;
using ZX.Model;

namespace ZX.BLL
{
    public class BaseBLL<T, DAL>
     where DAL : DBBase<T>, new()
     where T : BaseModel
    {
        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <returns></returns>
        public static int AddModel(T t)
        {
            return new DAL().AddModel(t);
        }
        #endregion

        #region 添加多条数据
        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="list">添加的实体集合</param>
        /// <returns></returns>
        public static int AddModel(List<T> list)
        {
            return new DAL().AddModel(list);
        }
        #endregion

        #region Merge数据
        /// <summary>
        /// Merge数据(A:目标表,B:源表)
        /// </summary>
        /// <param name="list">Merge的数据集合</param>
        /// <param name="where">条件</param>
        /// <param name="deleteWhere">删除条件</param>
        /// <returns></returns>
        public static int MergeModel(List<T> list, string where, string deleteWhere)
        {
            return new DAL().MergeModel(list, where, deleteWhere);
        }
        #endregion

        #region Merge数据(不执行删除)
        /// <summary>
        /// Merge数据不执行删除(A:目标表,B:源表)
        /// </summary>
        /// <param name="list">Merge的数据集合</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static int MergeNotDelModel(List<T> list, string where)
        {
            return new DAL().MergeNotDelModel(list, where);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static int DelModel(Expression<Func<IQueryable<T>, IQueryable<T>>> exp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().DelModel(expc);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public static int DelModel(string where)
        {
            return new DAL().DelModel(where);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">条件</param>
        /// <returns></returns>
        public static int DelModel(long? id)
        {
            Expression<Func<IQueryable<T>, IQueryable<T>>> exp = o => o.Where(t => t.Id == id);
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().DelModel(expc);
        }
        #endregion

        #region 根据id删除数据
        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="ids">ids集合</param>
        /// <returns></returns>
        public static int DelModelById(string ids)
        {
            return new DAL().DelModelById(ids);
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="t">修改的实体</param>
        /// <returns></returns>
        public static int UpdateModel(T t, Expression<Func<IQueryable<T>, IQueryable<T>>> exp = null)
        {
            if (exp == null) exp = o => o.Where(a => a.Id == t.Id);
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().UpdateModel(t, expc);
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="t">修改的实体</param>
        /// <param name="fields">修改的字段集合,如name='aaa'</param>
        /// <param name="where">条件,如id=1</param>
        /// <returns></returns>
        public static int UpdateModel(T t, string fields, string where)
        {
            return new DAL().UpdateModel(t, fields, where);
        }
        #endregion

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public static List<T> GetList()
        {
            return new DAL().GetList();
        }
        #endregion

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public static List<T> GetList(string where)
        {
            return new DAL().GetList(where);
        }
        #endregion

        #region 获取缓存所有数据
        /// <summary>
        /// 获取缓存所有数据
        /// </summary>
        /// <returns></returns>
        public static List<T> GetCacheList()
        {
            string tbName = Activator.CreateInstance<T>().GetType().Name;
            List<T> list = CacheHelper.Get<List<T>>(tbName + "List");
            if (list == null)
            {
                list = new DAL().GetList();
                CacheHelper.Insert(tbName + "List", list);
            }
            return list;
        }
        #endregion

        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static List<T> GetList(Expression<Func<IQueryable<T>, IQueryable<T>>> exp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().GetList(expc);
        }
        #endregion
        #region 获取单个数据
        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static T GetModel(long? id)
        {
            Expression<Func<IQueryable<T>, IQueryable<T>>> exp = o => o.Where(a => a.Id == id);
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().GetModel(expc);
        }
        #endregion
        #region 获取单个数据
        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static T GetModel(Expression<Func<IQueryable<T>, IQueryable<T>>> exp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().GetModel(expc);
        }
        #endregion

        #region 获取count记录
        /// <summary>
        /// 获取count记录
        /// </summary>
        /// <param name="exp">条件</param>
        /// <param name="countName">count字段</param>        
        /// <returns></returns>
        public static int GetCount(Expression<Func<IQueryable<T>, IQueryable<T>>> exp, string countName = "1")
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().GetCount(expc, countName).ToInt();
        }
        #endregion

        #region 获取sum记录
        /// <summary>
        /// 获取sum记录
        /// </summary>
        /// <param name="sumName">sum字段</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public static object GetSum(string sumName, Expression<Func<IQueryable<T>, IQueryable<T>>> exp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().GetSum(sumName, expc);
        }
        #endregion

        #region 获取max记录
        /// <summary>
        /// 获取max记录
        /// </summary>
        /// <param name="maxName">max字段</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public static object GetMax(string maxName, Expression<Func<IQueryable<T>, IQueryable<T>>> exp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().GetMax(maxName, expc);
        }
        #endregion

        #region 获取top数据
        /// <summary>
        /// 获取top数据
        /// </summary>
        /// <param name="top">top条数</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public static List<T> GetTopList(int top, Expression<Func<IQueryable<T>, IQueryable<T>>> exp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().GetTopList(top, expc);
        }
        #endregion

        #region 检测数据是否存在
        /// <summary>
        /// 检测数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static bool CheckModel(Expression<Func<IQueryable<T>, IQueryable<T>>> exp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>(exp);
            return new DAL().CheckModel(expc);
        }
        #endregion
    }
}
