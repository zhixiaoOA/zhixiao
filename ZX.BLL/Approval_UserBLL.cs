using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using ZX.Model;
using ZX.DAL;

namespace ZX.BLL
{
    public class Approval_UserBLL : BaseBLL<Approval_User, Approval_UserDAL>
    {
        //#region 根据流程id获取审批用户
        ///// <summary>
        ///// 根据流程id获取审批用户
        ///// </summary>
        ///// <param name="aid"></param>
        ///// <returns></returns>
        //public static List<Approval_UserModel> GetApprovalUserByAId(long aid)
        //{
        //    return new Approval_UserDAL().GetApprovalUserByAId(aid);
        //}
        //#endregion

        #region 获取审批流程
        /// <summary>
        /// 获取审批流程
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="did">部门id</param>
        /// <returns></returns>
        public static List<Approval_User> GetApprovalUserByTypeId(long typeId, long did)
        {
            return new Approval_UserDAL().GetApprovalUserByTypeId(typeId, did);
        }
        #endregion
    }
}
