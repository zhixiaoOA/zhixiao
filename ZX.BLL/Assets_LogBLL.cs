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
    public class Assets_LogBLL : BaseBLL<Assets_Log, Assets_LogDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Assets_LogModel> GetAssets_LogList(string key, string createAccount, int pageIndex, int pageSize)
        {
            return new Assets_LogDAL().GetAssets_LogList(key, createAccount, pageIndex, pageSize);
        }
        #endregion
        #region 根据资产id添加日志记录
        /// <summary>
        /// 根据资产id添加日志记录
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static int AddLogBasedOnId(Assets_Log model)
        {
            return Assets_LogBLL.AddModel(model);
        }
        #endregion




        #region 获取数据列表无分页
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">资产名</param>
        ///<param name="createAccount">借用人</param>
        /// <returns></returns>
        public static List<Assets_LogModel> GetAssets_LogListNotPage(string key, string createAccount)
        {
            return new Assets_LogDAL().GetAssets_LogListNotPage(key, createAccount);
        }
        #endregion
    }
}
