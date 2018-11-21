using System;
using System.Collections.Generic;
using ZX.DAL;
using ZX.Tools;
using ZX.Model;

namespace ZX.BLL
{
    public class Sys_UserBLL : BaseBLL<Sys_User, Sys_UserDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Sys_UserModel> GetSys_UserList(string key, int pageIndex, int pageSize)
        {
            return new Sys_UserDAL().GetSys_UserList(key, pageIndex, pageSize);
        }
        #endregion

        #region 获取用户
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="did">部门id</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public static List<Sys_User> GetUserList(long did, string name)
        {
            return new Sys_UserDAL().GetUserList(did, name);
        }
        #endregion

        #region 根据账号密码获取用户
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="userPwd">密码</param>
        /// <returns></returns>
        public static Sys_User AccountGetUserList(string userName, string userPwds)
        {
            return new Sys_UserDAL().AccountGetUserList(userName, userPwds);
        }
        #endregion

        #region 获取用户登录信息
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public static Sys_User GetUserLogin(string userName, string userPwd)
        {
            return new Sys_UserDAL().GetUserLogin(userName, userPwd);
        }
        #endregion

        #region 获取用户登录信息(手机号登录)
        /// <summary>
        /// 获取用户登录信息(手机号登录)
        /// </summary>
        /// <param name="userName">用户名或手机号</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public static Sys_User GetUserLoginByPhone(string userName, string userPwd)
        {
            return new Sys_UserDAL().GetUserLoginByPhone(userName, userPwd);
        }
        #endregion

        #region 验证用户名唯一性
        /// <summary>
        /// 验证用户名唯一性
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static bool CheckUserName(string userName)
        {
            return new Sys_UserDAL().CheckUserName(userName);
        }
        #endregion

        #region 根据id获取用户信息
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <returns></returns>
        public static Sys_UserModel GetUserById(long? userId)
        {
            return new Sys_UserDAL().GetUserById(userId);
        }
        #endregion
    }
}
