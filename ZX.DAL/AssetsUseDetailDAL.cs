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
    public class AssetsUseDetailDAL : DBBase<AssetsUseDetail>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<AssetsUseDetailModel> GetAssetsUseDetailList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetAssetsUseDetailList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<AssetsUseDetailModel> list = Db.ExecuteProcToList<AssetsUseDetailModel>(sql, Pmts.ToArray());
            DataList<AssetsUseDetailModel> pageList = new DataList<AssetsUseDetailModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 根据申请id获取明细
        /// <summary>
        /// 根据申请id获取明细
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public List<AssetsUseDetailModel> GetAssetsUseDetailByAssetsUseId(long? aid)
        {
            string sql = @"SELECT A.*,B.AName,DeptName=E.DName,D.RealName
            FROM [dbo].[AssetsUseDetail] AS A
            LEFT JOIN dbo.Assets AS B ON A.FK_AssetsId = B.Id 
            LEFT JOIN AssetsUse AS C ON A.FK_AssetsUseId=C.Id
            LEFT JOIN dbo.Sys_User AS D ON C.FK_UserId=D.Id
            LEFT JOIN dbo.Sys_Dept AS E ON D.Fk_DeptId=E.Id 
            WHERE A.FK_AssetsUseId=@id";
            Pmts.ClearPmts();
            Pmts.Add("id", aid);
            return Db.ExecuteToList<AssetsUseDetailModel>(sql, Pmts.ToArray());
        }
        #endregion

    }
}
