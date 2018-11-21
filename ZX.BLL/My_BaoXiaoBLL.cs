using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using ZX.Model;
using ZX.DAL;

namespace ZX.BLL
{
    public class My_BaoXiaoBLL : BaseBLL<My_BaoXiao, My_BaoXiaoDAL>
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
        public static DataList<My_BaoXiaoModel> GetMy_BaoXiaoList(string key, long userId, long appUserId, string kemuType, string kemu, int status, int pageIndex, int pageSize)
        {
            return new My_BaoXiaoDAL().GetMy_BaoXiaoList(key, userId, appUserId, kemuType, kemu, status, pageIndex, pageSize);
        }
        #endregion
    }
}
