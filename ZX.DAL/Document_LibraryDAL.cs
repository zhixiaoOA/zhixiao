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
    public class Document_LibraryDAL : DBBase<Document_Library>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Document_LibraryModel> GetDocument_LibraryList(string key, int dType, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetDocument_LibraryList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("dType", dType);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Document_LibraryModel> list = Db.ExecuteProcToList<Document_LibraryModel>(sql, Pmts.ToArray());
            DataList<Document_LibraryModel> pageList = new DataList<Document_LibraryModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
