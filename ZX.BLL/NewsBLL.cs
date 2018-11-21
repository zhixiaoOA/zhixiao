using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.DAL;
using ZX.Model;
using ZX.Tools;

namespace ZX.BLL
{
    public class NewsBLL : BaseBLL<News, NewsDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<NewsModel> GetNewsList(string key, int typeId, int pageIndex, int pageSize)
        {
            return new NewsDAL().GetNewsList(key, typeId, pageIndex, pageSize);
        }
        #endregion

        /// <summary>
        /// 获取指定行数记录
        /// </summary>
        /// <param name="counts">获取数例如:5</param>
        /// <param name="userId">用户Id,例如5,(-1查询所有用户).</param>
        /// <param name="nType">0：公司公告 10：新闻</param>
        /// <param name="orderBy">排序字段 例如: id</param>
        /// <param name="upOrDown">升序还是降序:例如 DESC</param>
        /// <returns></returns>
        public static List<NewsModel> GetNewsTopList(int counts, int userId, int nType)
        {
            return new NewsDAL().GetNewsTopList(counts, userId, nType);
        }

        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>\
        /// <param name="userId">-1获取所有,其它获取个人</param>
        /// <param name="typeId">类型,0-公告,10-新闻</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<NewsModel> Proc_GetNewsListByUserId(string key, int userId, int typeId, int pageIndex, int pageSize)
        {
            return new NewsDAL().Proc_GetNewsListByUserId(key, userId, typeId, pageIndex, pageSize);
        }
        #endregion

        #region 根据id获取数据
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NewsModel GetModelById(long id)
        {
            return new NewsDAL().GetModelById(id);
        }
        #endregion

    }
}
