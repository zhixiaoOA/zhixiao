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
    public class DictionaryDAL : DBBase<Dictionary>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<DictionaryModel> GetDictionaryList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetDictionaryList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<DictionaryModel> list = Db.ExecuteProcToList<DictionaryModel>(sql, Pmts.ToArray());
            DataList<DictionaryModel> pageList = new DataList<DictionaryModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 获取申请单类型数据
        /// <summary>
        /// 获取申请单类型数据
        /// </summary>
        /// <returns></returns>
        public List<DictionaryModel> GetApplyTypeList()
        {
            string sql = @"select 46 AS Id,'请假申请单' AS Name
            union all
            select Id, Name from Dictionary where ParentId = 35 and Name not like '%请假%'";            
            List<DictionaryModel> list = Db.ExecuteToList<DictionaryModel>(sql);
            return list;
        }
        #endregion

        #region 根据父id获取数据
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="parentIds">父id</param>
        /// <returns></returns>
        public List<Dictionary> GetDictionaryListByPid(string parentIds)
        {
            Pmts.ClearPmts();
            string[] pids = parentIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string strPmts = "";
            for (int i = 0; i < pids.Length; i++)
            {
                strPmts += "@p" + i + ",";
                Pmts.Add("p" + i, pids[i]);
            }
            strPmts = strPmts.Substring(0, strPmts.Length - 1);
            string sql = "select * from Dictionary where ParentId in(" + strPmts + ")";
            List<Dictionary> list = Db.ExecuteToList<Dictionary>(sql, Pmts.ToArray());
            return list;
        }
        #endregion
    }

}

