using ZX.DAL;
using ZX.Tools;
using ZX.Model;

namespace ZX.BLL
{
    public class Sys_Role_MenuBLL : BaseBLL<Sys_Role_Menu, Sys_Role_MenuDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Sys_Role_MenuModel> GetSys_Role_MenuList(string key, int pageIndex, int pageSize)
        {
            return new Sys_Role_MenuDAL().GetSys_Role_MenuList(key, pageIndex, pageSize);
        }
        #endregion
    }
}
