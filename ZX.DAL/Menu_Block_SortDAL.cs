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
    public class Menu_Block_SortDAL : DBBase<Menu_Block_Sort>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<Menu_Block_SortModel> GetMenu_Block_SortList(string key, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetMenu_Block_SortList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
			Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<Menu_Block_SortModel> list = Db.ExecuteProcToList<Menu_Block_SortModel>(sql, Pmts.ToArray());
			DataList<Menu_Block_SortModel> pageList = new DataList<Menu_Block_SortModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
		#endregion
    }
}
