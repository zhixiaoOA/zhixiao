using ZX.Tools;
using ZX.Model;
using ZX.DAL;

namespace ZX.BLL
{
    public class Sys_ButtonBLL : BaseBLL<Sys_Button, Sys_ButtonDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Sys_ButtonModel> GetSys_ButtonList(string key, int pageIndex, int pageSize)
        {
            return new Sys_ButtonDAL().GetSys_ButtonList(key, pageIndex, pageSize);
        }
        #endregion
    }
}
