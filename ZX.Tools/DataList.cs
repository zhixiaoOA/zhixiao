using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Tools
{
    public class DataList<T> : List<T>
    {
        #region 公开属性

        /// <summary>
        /// 当前页面的除了页数之外的url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 当前所请求的页面
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录条数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        #endregion

        #region 构造函数
        public DataList() { }
        /// <summary>
        /// 新建一个通用列表
        /// </summary>
        /// <param name="modelles">所要传递的集合</param>
        /// <param name="totalCount">数据库中该记录的总数</param>
        /// <param name="pageIndex">当前请求的页面</param>
        /// <param name="pageSize">页面大小</param>
        public DataList(IQueryable<T> modelles, long totalCount, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            if (totalCount == 0)
            {
                this.TotalPages = 0;
            }
            else if (totalCount < pageSize)
            {
                this.TotalPages = 1;
            }
            else
            {
                this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            }

            if (this.PageIndex > this.TotalPages)
            {
                this.PageIndex = 1;
            }
            if (modelles != null)
                this.AddRange(modelles);
        }

        /// <summary>
        /// 新建一个通用列表
        /// </summary>
        /// <param name="modelles">所要传递的集合</param>
        /// <param name="totalCount">数据库中该记录的总数</param>
        public DataList(IQueryable<T> modelles, long totalCount)
        {
            this.PageIndex = 1;
            this.PageSize = int.Parse(totalCount.ToString());
            this.TotalCount = totalCount;
            this.TotalPages = 1;
            this.PageIndex = 1;

            if (modelles != null)
                this.AddRange(modelles);
        }

        /// <summary>
        /// 新建一个通用列表
        /// </summary>
        /// <param name="modelles">所要传递的集合</param>
        /// <param name="totalCount">数据库中该记录的总数</param>
        /// <param name="pageIndex">当前请求的页面</param>
        /// <param name="pageSize">页面大小</param>
        public DataList(IEnumerable<T> modelles, long totalCount, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            if (totalCount == 0)
            {
                this.TotalPages = 0;
            }
            else if (totalCount < pageSize)
            {
                this.TotalPages = 1;
            }
            else
            {
                this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            }

            if (this.PageIndex > this.TotalPages)
            {
                this.PageIndex = 1;
            }
            this.AddRange(modelles);
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="key"></typeparam>
        /// <param name="OrderByFieId"></param>
        /// <returns></returns>
        public DataList<T> MyOrderByDescending<key>(Func<T, key> OrderByFieId)
        {
            var souce = this.OrderByDescending(OrderByFieId);
            DataList<T> result = new DataList<T>(souce, this.TotalCount, this.PageIndex, this.PageSize);
            result.Url = this.Url;
            result.TotalPages = this.TotalPages;
            return result;
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="key"></typeparam>
        /// <param name="OrderByFieId"></param>
        /// <returns></returns>
        public DataList<T> MyOrderBy<key>(Func<T, key> OrderByFieId)
        {
            var souce = this.OrderBy(OrderByFieId);
            DataList<T> result = new DataList<T>(souce, this.TotalCount, this.PageIndex, this.PageSize);
            result.Url = this.Url;
            result.TotalPages = this.TotalPages;
            return result;
        }

        /// <summary>
        /// 新建一个通用列表
        /// </summary>
        /// <param name="list">所要传递的集合</param>
        /// <param name="totalCount">数据库中该记录的总数</param>
        public DataList(List<T> list, long totalCount)
        {
            this.PageIndex = 1;
            this.PageSize = int.Parse(totalCount.ToString());
            this.TotalCount = totalCount;
            this.TotalPages = 1;
            this.PageIndex = 1;

            if (list != null)
                this.AddRange(list);
        }

        /// <summary>
        /// 新建一个通用列表
        /// </summary>
        /// <param name="list">所要传递的集合</param>
        /// <param name="totalCount">数据库中该记录的总数</param>
        /// <param name="pageIndex">当前请求的页面</param>
        /// <param name="pageSize">页面大小</param>
        public DataList(List<T> list, long totalCount, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            if (totalCount == 0)
            {
                this.TotalPages = 0;
            }
            else if (totalCount < pageSize)
            {
                this.TotalPages = 1;
            }
            else
            {
                this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            }

            if (this.PageIndex > this.TotalPages)
            {
                this.PageIndex = 1;
            }
            this.AddRange(list);
        }
        /// <summary>
        /// 新建一个通用列表
        /// </summary>
        /// <param name="list">所要传递的集合</param>
        /// <param name="pageIndex">当前请求的页面</param>
        /// <param name="pageSize">页面大小</param>
        public DataList(List<T> list, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = list.Count;
            if (list.Count == 0)
            {
                this.TotalPages = 0;
            }
            else if (list.Count < pageSize)
            {
                this.TotalPages = 1;
            }
            else
            {
                this.TotalPages = (int)Math.Ceiling(list.Count / (double)pageSize);
            }
            if (this.PageIndex > this.TotalPages)
            {
                this.PageIndex = 1;
            }
            this.AddRange(list.Skip(pageIndex).Take(pageSize));
        }

        /// <summary>
        /// 新建一个通用列表
        /// </summary>
        /// <param name="list">所要传递的集合</param>
        /// <param name="pageIndex">当前请求的页面</param>
        /// <param name="pageSize">页面大小</param>
        public DataList(IEnumerable<T> list, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = list.Count();
            if (this.TotalCount == 0)
            {
                this.TotalPages = 0;
            }
            else if (this.TotalCount < pageSize)
            {
                this.TotalPages = 1;
            }
            else
            {
                this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)pageSize);
            }
            if (this.PageIndex > this.TotalPages)
            {
                this.PageIndex = 1;
            }
            this.AddRange(list.Skip(pageIndex).Take(pageSize));
        }
        #endregion
    }
}
