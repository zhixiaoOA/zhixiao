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
    public class Document_LibraryBLL : BaseBLL<Document_Library, Document_LibraryDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Document_LibraryModel> GetDocument_LibraryList(string key, int dType, int pageIndex, int pageSize)
        {
            return new Document_LibraryDAL().GetDocument_LibraryList(key, dType, pageIndex, pageSize);
        }
        #endregion
    }
}
