using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Tools;
using ZX.DAL;
using ZX.Model;

namespace ZX.DAL
{
    public class Document_ManagerDAL : DBBase<Document_Manager>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<Document_ManagerModel> GetDocument_ManagerList(string key,int userId, string roleId, int fk_LibraryId, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetDocument_ManagerList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("roleId", roleId);
            Pmts.Add("fk_LibraryId", fk_LibraryId);
            Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<Document_ManagerModel> list = Db.ExecuteProcToList<Document_ManagerModel>(sql, Pmts.ToArray());
			DataList<Document_ManagerModel> pageList = new DataList<Document_ManagerModel>(list, Pmts.ListPmts[6].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
		#endregion
    }
}
