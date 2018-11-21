using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class NewsDAL : DBBase<News>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<NewsModel> GetNewsList(string key, int typeId, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetNewsList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("typeId", typeId);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<NewsModel> list = Db.ExecuteProcToList<NewsModel>(sql, Pmts.ToArray());
            DataList<NewsModel> pageList = new DataList<NewsModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
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
        public List<NewsModel> GetNewsTopList(int counts, int userId, int nType)
        {
            string sql = "SELECT TOP(@counts) * FROM News WHERE 1=1 AND (CreateUserId =@userId or @userId=-1) AND DType=@nType  ORDER BY CreateTime DESC";
            Pmts.ClearPmts();
            Pmts.Add("counts", counts);
            Pmts.Add("userId", userId);
            Pmts.Add("nType", nType);

            List<NewsModel> list = Db.ExecuteToList<NewsModel>(sql, Pmts.ToArray());

            return list;
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
        public DataList<NewsModel> Proc_GetNewsListByUserId(string key, int userId, int typeId, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetNewsListByUserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("typeId", typeId);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<NewsModel> list = Db.ExecuteProcToList<NewsModel>(sql, Pmts.ToArray());
            DataList<NewsModel> pageList = new DataList<NewsModel>(list, Pmts.ListPmts[5].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 根据id获取数据
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewsModel GetModelById(long id)
        {
            string sql = @"select a.*,b.RealName from News as a
            left join Sys_User as b on a.CreateUserId=b.Id
            where a.Id=@id";
            Pmts.Add("id", id);
            return Db.ExecuteToSingle<NewsModel>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
