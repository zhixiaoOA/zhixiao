using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Model;
using ZX.DAL;
using ZX.Tools;

namespace ZX.BLL
{
    public class Sys_MenuBLL : BaseBLL<Sys_Menu, Sys_MenuDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Sys_MenuModel> GetSys_MenuList(string key, int pageIndex, int pageSize)
        {
            return new Sys_MenuDAL().GetSys_MenuList(key, pageIndex, pageSize);
        }
        #endregion

        #region 
        public static List<Sys_Menu> GetUserMenu(int userId)
        {
            return new Sys_MenuDAL().GetUserMenu(userId);
        }
        #endregion
    }
}
