using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using ZX.Model;
using ZX.DAL;

namespace ZX.BLL
{
    public class Document_ManagerBLL : BaseBLL<Document_Manager, Document_ManagerDAL>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public static DataList<Document_ManagerModel> GetDocument_ManagerList(string key, int userId, string roleId,int fk_LibraryId, int pageIndex, int pageSize)
		{
			return new Document_ManagerDAL().GetDocument_ManagerList(key, userId, roleId, fk_LibraryId, pageIndex, pageSize);
		}
		#endregion
    }
}
