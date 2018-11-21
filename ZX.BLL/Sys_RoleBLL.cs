using ZX.DAL;
using ZX.Model;
using ZX.Tools;
namespace ZX.BLL
{
    public class Sys_RoleBLL : BaseBLL<Sys_Role, Sys_RoleDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Sys_RoleModel> GetSys_RoleList(string key, int pageIndex, int pageSize)
        {
            return new Sys_RoleDAL().GetSys_RoleList(key, pageIndex, pageSize);
        }
        #endregion

        #region 获取通用角色id
        /// <summary>
        /// 获取通用角色ID
        /// </summary>
        /// <returns></returns>
        public static Sys_RoleModel getSysRoleID()
        {
            return new Sys_RoleDAL().getSysRoleID();
        }
        #endregion

    }
}
