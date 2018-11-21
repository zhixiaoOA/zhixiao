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
    public class My_Agenda_TeamDAL : DBBase<My_Agenda_Team>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<My_Agenda_TeamModel> GetMy_Agenda_TeamList(string key, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetMy_Agenda_TeamList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
			Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<My_Agenda_TeamModel> list = Db.ExecuteProcToList<My_Agenda_TeamModel>(sql, Pmts.ToArray());
			DataList<My_Agenda_TeamModel> pageList = new DataList<My_Agenda_TeamModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
		#endregion
    }
}
