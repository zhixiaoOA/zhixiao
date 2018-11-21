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
    public class Approval_UserDAL : DBBase<Approval_User>
    {
        //#region 根据流程id获取审批用户
        ///// <summary>
        ///// 根据流程id获取审批用户
        ///// </summary>
        ///// <param name="aid"></param>
        ///// <returns></returns>
        //public List<Approval_UserModel> GetApprovalUserByAId(long aid)
        //{
        //    string sql = @"SELECT A.*,UserName=B.RealName FROM dbo.Approval_User AS A
        //    LEFT JOIN dbo.Sys_User AS B ON A.FK_UserId=B.Id
        //    WHERE A.FK_ApprovalId=@aid";
        //    Pmts.ClearPmts();
        //    Pmts.Add("aid", aid);
        //    return Db.ExecuteToList<Approval_UserModel>(sql, Pmts.ToArray());
        //}
        //#endregion

        #region 获取审批流程
        /// <summary>
        /// 获取审批流程
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="did">部门id</param>
        /// <returns></returns>
        public List<Approval_User> GetApprovalUserByTypeId(long typeId,long did)
        {
            string sql = @"SELECT * FROM dbo.Approval_User WHERE FK_ApprovalId=(SELECT Id FROM dbo.Approval WHERE ((FK_TypeId=@typeId AND FK_DeptId=@did) OR (FK_TypeId=@typeId1 AND FK_DeptId=0)))";
            Pmts.ClearPmts();
            Pmts.Add("typeId", typeId);
            Pmts.Add("typeId1", typeId);
            Pmts.Add("did", did);
            return Db.ExecuteToList<Approval_User>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
