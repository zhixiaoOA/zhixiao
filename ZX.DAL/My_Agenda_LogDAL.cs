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
    public class My_Agenda_LogDAL : DBBase<My_Agenda_Log>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<My_Agenda_LogModel> GetMy_Agenda_LogList(string key, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetMy_Agenda_LogList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
			Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<My_Agenda_LogModel> list = Db.ExecuteProcToList<My_Agenda_LogModel>(sql, Pmts.ToArray());
			DataList<My_Agenda_LogModel> pageList = new DataList<My_Agenda_LogModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
		#endregion
    }
}
