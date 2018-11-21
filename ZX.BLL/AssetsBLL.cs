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
    public class AssetsBLL : BaseBLL<Assets, AssetsDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<AssetsModel> GetAssetsList(string code, string name, int mid, int pageIndex, int pageSize)
        {
            return new AssetsDAL().GetAssetsList(code, name, mid, pageIndex, pageSize);
        }
        #endregion

        #region 修改库存
        /// <summary>
        /// 修改库存
        /// </summary>
        /// <param name="id">明细id</param>
        /// <param name="depts">资产资质</param>
        /// <returns></returns>
        public static int UpdateAssetsNum(long? id, string depts)
        {
            return new AssetsDAL().UpdateAssetsNum(id, depts);
        }
        #endregion

        #region 根据时间获取资产统计
        /// <summary>
        /// 根据时间获取资产统计
        /// </summary>
        /// <returns></returns>
        public static InStorageReport GetInStorageReport(string beginTime, string endTime)
        {
            return new AssetsDAL().GetInStorageReport(beginTime, endTime);
        }
        #endregion
    }
}
