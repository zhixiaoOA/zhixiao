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
    public class AssetsDAL : DBBase<Assets>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<AssetsModel> GetAssetsList(string code, string name, int mid, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetAssetsList";
            Pmts.ClearPmts();
            Pmts.Add("code", code);
            Pmts.Add("name", name);
            Pmts.Add("mid", mid);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<AssetsModel> list = Db.ExecuteProcToList<AssetsModel>(sql, Pmts.ToArray());
            DataList<AssetsModel> pageList = new DataList<AssetsModel>(list, Pmts.ListPmts[5].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 修改库存
        /// <summary>
        /// 修改库存
        /// </summary>
        /// <param name="id">明细id</param>
        /// <param name="depts">资产资质</param>
        /// <returns></returns>
        public int UpdateAssetsNum(long? id, string depts)
        {
            string sql = @"DECLARE @aid BIGINT,@count INT
            SELECT @aid=FK_AssetsId,@count=DCount FROM AssetsUseDetail WHERE Id=@id
            UPDATE AssetsUseDetail SET NatureOfAssets=@NatureOfAssets,AuthTime=GETDATE() WHERE Id=@id2
            UPDATE dbo.Assets SET ANum=ANum-@count WHERE Id=@aid";
            Pmts.ClearPmts();
            Pmts.Add("id", id);
            Pmts.Add("id2", id);
            Pmts.Add("NatureOfAssets", depts);
            return Db.ExecuteSql(sql, Pmts.ToArray());
        }
        #endregion

        #region 根据时间获取资产统计
        /// <summary>
        /// 根据时间获取资产统计
        /// </summary>
        /// <returns></returns>
        public InStorageReport GetInStorageReport(string beginTime, string endTime)
        {
            string sql = @"SELECT InCount=SUM(ANum),TotalMoney=SUM(APrice) FROM dbo.Assets
            WHERE CONVERT(VARCHAR(10),CreateTime,20)>=@beginTime AND CONVERT(VARCHAR(10),CreateTime,20)<=@endTime";
            Pmts.ClearPmts();
            Pmts.Add("beginTime", beginTime);
            Pmts.Add("endTime", endTime);
            return Db.ExecuteToSingle<InStorageReport>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
