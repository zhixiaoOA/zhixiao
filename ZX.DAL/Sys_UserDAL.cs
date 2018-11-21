using System;
using System.Collections.Generic;
using System.Data;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class Sys_UserDAL : DBBase<Sys_User>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Sys_UserModel> GetSys_UserList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetSys_UserList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Sys_UserModel> list = Db.ExecuteProcToList<Sys_UserModel>(sql, Pmts.ToArray());
            DataList<Sys_UserModel> pageList = new DataList<Sys_UserModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 获取用户
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="did">部门id</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public List<Sys_User> GetUserList(long did, string name)
        {
            string sql = @"SELECT * FROM dbo.Sys_User WHERE (@name1='' OR RealName LIKE @name) and (@did<=0 or Fk_DeptId=@did1)";
            Pmts.ClearPmts();
            Pmts.Add("name1", name);
            Pmts.Add("name", name.ToSqlLike());
            Pmts.Add("did", did);
            Pmts.Add("did1", did);
            return Db.ExecuteToList<Sys_User>(sql, Pmts.ToArray());
        }
        #endregion

        #region 根据账号密码获取用户
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public Sys_User AccountGetUserList(string userName, string userPwds)
        {
            string sql = @"SELECT * FROM dbo.Sys_User WHERE ([UserName]=@userName AND [Pwd]=@userPwd)";
            Pmts.ClearPmts();
            Pmts.Add("userName", userName);
            Pmts.Add("userPwd", userPwds);
            return Db.ExecuteToSingle<Sys_User>(sql, Pmts.ToArray());
        }
        #endregion

        #region 根据时间及创建用户统计申请数
        /// <summary>
        /// 根据时间及创建用户统计申请数
        /// </summary>
        /// <param name="startTime">自</param>
        /// <param name="endTime">至</param>
        /// <param name="realname">姓名,模糊查询</param>
        /// <param name="pageIndex">分页页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public DataList<ApprovalStatistical> GetApprovalStatisticalList(string startTime, string endTime, string realname, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetApprovalStatisticalList";

            Pmts.ClearPmts();
            Pmts.Add("startTime", startTime);
            Pmts.Add("endTime", endTime);
            Pmts.Add("realname", realname);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<ApprovalStatistical> list = Db.ExecuteProcToList<ApprovalStatistical>(sql, Pmts.ToArray());
            DataList<ApprovalStatistical> pageList = new DataList<ApprovalStatistical>(list, Pmts.ListPmts[5].Value.ToInt());
            return pageList;
        }
        #endregion

        #region 获取用户登录信息
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public Sys_User GetUserLogin(string userName, string userPwd)
        {
            string sql = "select * from Sys_User where UserName COLLATE Chinese_PRC_CS_AS=@UserName and Pwd COLLATE Chinese_PRC_CS_AS=@UserPassword AND IsDel=1";
            Pmts.ClearPmts();
            Pmts.Add("UserName", userName);
            Pmts.Add("UserPassword", userPwd);
            return Db.ExecuteToSingle<Sys_User>(sql, Pmts.ToArray());
        }
        #endregion

        #region 获取用户登录信息(手机号登录)
        /// <summary>
        /// 获取用户登录信息(手机号登录)
        /// </summary>
        /// <param name="userName">用户名或手机号</param>
        /// <param name="userPwd">用户密码</param>
        /// <returns></returns>
        public Sys_User GetUserLoginByPhone(string userName, string userPwd)
        {
            string sql = "select * from Sys_User where (UserName COLLATE Chinese_PRC_CS_AS=@UserName or UPhone=@userPhone) and Pwd COLLATE Chinese_PRC_CS_AS=@UserPassword";
            Pmts.ClearPmts();
            Pmts.Add("UserName", userName);
            Pmts.Add("userPhone", userName);
            Pmts.Add("UserPassword", userPwd);
            return Db.ExecuteToSingle<Sys_User>(sql, Pmts.ToArray());
        }
        #endregion

        #region 验证用户名唯一性
        /// <summary>
        /// 验证用户名唯一性
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            string sql = "select * from Sys_User where UserName COLLATE Chinese_PRC_CS_AS=@UserName";
            Pmts.ClearPmts();
            Pmts.Add("UserName", userName);
            Sys_User model = Db.ExecuteToSingle<Sys_User>(sql, Pmts.ToArray());
            bool bl = false;
            if (model != null)
            {
                bl = true;
            }
            return bl;
        }
        #endregion

        #region 根据id获取用户信息
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <returns></returns>
        public Sys_UserModel GetUserById(long? userId)
        {
            string sql = @"SELECT *,DeptName=B.DName,PositionName=ISNULL(C.Name,'普通员工')
            FROM dbo.Sys_User AS A
            LEFT JOIN dbo.Sys_Dept AS B ON A.Fk_DeptId=B.Id
            LEFT JOIN dbo.CompanyPosition AS C ON A.FK_CompanyPositionId=C.Id
            WHERE A.Id=@id";
            Pmts.ClearPmts();
            Pmts.Add("id", userId);
            return Db.ExecuteToSingle<Sys_UserModel>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
