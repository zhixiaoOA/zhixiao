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
    public class Supplier_TypeDAL : DBBase<Supplier_Type>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<Supplier_TypeModel> GetSupplier_TypeList(string key, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetSupplier_TypeList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
			Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<Supplier_TypeModel> list = Db.ExecuteProcToList<Supplier_TypeModel>(sql, Pmts.ToArray());
			DataList<Supplier_TypeModel> pageList = new DataList<Supplier_TypeModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
		#endregion
    }
}
