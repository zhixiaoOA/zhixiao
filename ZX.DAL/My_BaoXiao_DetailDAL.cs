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
    public class My_BaoXiao_DetailDAL : DBBase<My_BaoXiao_Detail>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<My_BaoXiao_DetailModel> GetMy_BaoXiao_DetailList(string key, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetMy_BaoXiao_DetailList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
			Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<My_BaoXiao_DetailModel> list = Db.ExecuteProcToList<My_BaoXiao_DetailModel>(sql, Pmts.ToArray());
			DataList<My_BaoXiao_DetailModel> pageList = new DataList<My_BaoXiao_DetailModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
		#endregion
    }
}
