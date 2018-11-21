using ZX.DAL;
using ZX.Model;
using ZX.Tools;

namespace ZX.BLL
{
    public class CompanyPositionBLL : BaseBLL<CompanyPosition, CompanyPositionDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<CompanyPositionModel> GetCompanyPositionList(string key, int pageIndex, int pageSize)
        {
            return new CompanyPositionDAL().GetCompanyPositionList(key, pageIndex, pageSize);
        }
        #endregion
    }
}
