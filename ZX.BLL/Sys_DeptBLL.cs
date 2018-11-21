using ZX.DAL;
using ZX.Model;
using ZX.Tools;

namespace ZX.BLL
{
    public class Sys_DeptBLL : BaseBLL<Sys_Dept, Sys_DeptDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Sys_DeptModel> GetSys_DeptList(string key, int pageIndex, int pageSize)
        {
            return new Sys_DeptDAL().GetSys_DeptList(key, pageIndex, pageSize);
        }
        #endregion
    }
}
