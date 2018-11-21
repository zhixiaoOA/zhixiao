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
    public class Assets_LogDAL : DBBase<Assets_Log>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<Assets_LogModel> GetAssets_LogList(string key,string createAccount, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetAssets_LogList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
            Pmts.Add("createAccount", createAccount);
            Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<Assets_LogModel> list = Db.ExecuteProcToList<Assets_LogModel>(sql, Pmts.ToArray());
			DataList<Assets_LogModel> pageList = new DataList<Assets_LogModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
        #endregion

        #region 获取数据列表无分页
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">资产名</param>
        ///<param name="createAccount">借用人</param>
        /// <returns></returns>
        public List<Assets_LogModel> GetAssets_LogListNotPage(string key, string createAccount)
        {
            string sql = "Proc_GetAssets_LogListNotPage";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("createAccount", createAccount);
            List<Assets_LogModel> list = Db.ExecuteProcToList<Assets_LogModel>(sql, Pmts.ToArray());  
            return list;
        }
        #endregion
    }
}
