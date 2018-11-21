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
    public class AssetsUseDAL : DBBase<AssetsUse>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">事由</param>
        /// <param name="userId">申请人id</param>
        /// <param name="appUserId">审核人id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<AssetsUseModel> GetAssetsUseList(string key, long userId, long appUserId, string beginTime, string endTime, string status, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetAssetsUseList";
			Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("userId", userId);
            Pmts.Add("appUserId", appUserId);
            Pmts.Add("beginTime", beginTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("status", status);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<AssetsUseModel> list = Db.ExecuteProcToList<AssetsUseModel>(sql, Pmts.ToArray());
			DataList<AssetsUseModel> pageList = new DataList<AssetsUseModel>(list, Pmts.ListPmts[8].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
		#endregion
    }
}
