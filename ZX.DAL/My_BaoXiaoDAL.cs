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
    public class My_BaoXiaoDAL : DBBase<My_BaoXiao>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">事由</param>
        /// <param name="userId">申请人id</param>
        /// <param name="appUserId">审核人id</param>
        /// <param name="kemuType">科目类型</param>
        /// <param name="kemu">科目</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<My_BaoXiaoModel> GetMy_BaoXiaoList(string key, long userId, long appUserId, string kemuType, string kemu, int status, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetMy_BaoXiaoList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("appUserId", appUserId);
            Pmts.Add("kemuType", kemuType);
            Pmts.Add("kemu", kemu);
            Pmts.Add("status", status);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_BaoXiaoModel> list = Db.ExecuteProcToList<My_BaoXiaoModel>(sql, Pmts.ToArray());
            DataList<My_BaoXiaoModel> pageList = new DataList<My_BaoXiaoModel>(list, Pmts.ListPmts[8].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
