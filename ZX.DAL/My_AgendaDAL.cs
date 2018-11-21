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
    public class My_AgendaDAL : DBBase<My_Agenda>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<My_AgendaModel> GetMy_AgendaList(string key,int userId,int status,int pageIndex, int pageSize)
		{
			string sql = "Proc_GetMy_AgendaList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("status", status);
            Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<My_AgendaModel> list = Db.ExecuteProcToList<My_AgendaModel>(sql, Pmts.ToArray());
			DataList<My_AgendaModel> pageList = new DataList<My_AgendaModel>(list, Pmts.ListPmts[5].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
        #endregion


        /// <summary>
        /// 分页获取未完成数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataList<My_AgendaModel> GetMy_AgendaUnFinishList(string key, int userId, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMy_AgendaUnFinishList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_AgendaModel> list = Db.ExecuteProcToList<My_AgendaModel>(sql, Pmts.ToArray());
            DataList<My_AgendaModel> pageList = new DataList<My_AgendaModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }

        /// <summary>
        /// 分页获取待定数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataList<My_AgendaModel> GetMy_AgendaAIsUndeterminedList(string key, int userId, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMy_AgendaAIsUndeterminedList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_AgendaModel> list = Db.ExecuteProcToList<My_AgendaModel>(sql, Pmts.ToArray());
            DataList<My_AgendaModel> pageList = new DataList<My_AgendaModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }


        

        /// <summary>
        /// 分页获取指派给我的待定数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataList<My_AgendaModel> GetMy_AgendaAssignedMyList(string key, int userId, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMy_AgendaAssignedMyList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_AgendaModel> list = Db.ExecuteProcToList<My_AgendaModel>(sql, Pmts.ToArray());
            DataList<My_AgendaModel> pageList = new DataList<My_AgendaModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }



        

        /// <summary>
        /// 分页获取我指派给他人的待定数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataList<My_AgendaModel> GetMy_AgendaAssignedOtherList(string key, int userId, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMy_AgendaAssignedOtherList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_AgendaModel> list = Db.ExecuteProcToList<My_AgendaModel>(sql, Pmts.ToArray());
            DataList<My_AgendaModel> pageList = new DataList<My_AgendaModel>(list, Pmts.ListPmts[4].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
    }
}
