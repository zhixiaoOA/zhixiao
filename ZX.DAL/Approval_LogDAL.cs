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
    public class Approval_LogDAL : DBBase<Approval_Log>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public DataList<Approval_LogModel> GetApproval_LogList(string key, int pageIndex, int pageSize)
		{
			string sql = "Proc_GetApproval_LogList";
			Pmts.ClearPmts();
			Pmts.Add("key", key);
			Pmts.Add("pageIndex", pageIndex);
			Pmts.Add("pageSize", pageSize);
			Pmts.Add("count", -1, ParameterDirection.Output);
			List<Approval_LogModel> list = Db.ExecuteProcToList<Approval_LogModel>(sql, Pmts.ToArray());
			DataList<Approval_LogModel> pageList = new DataList<Approval_LogModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
			return pageList;
		}
        #endregion

        /// <summary>
        /// 通过审批日志获取审批用户
        /// </summary>
        /// <param name="counts">获取数例如:5; 值为-1获取全部</param>
        /// <param name="fk_TypeId">审批类型</param>
        /// <param name="fk_ApplyFlowId">审批单Id</param>
        /// <returns></returns>
        public List<Sys_User> GetUserListByApprovalLog(int counts, int fk_TypeId, int fk_ApplyFlowId)
        {
            string sql = @"DECLARE @count INT
            SET @count=@num
            SELECT Sys_User.Id,dbo.Sys_User.USgin,tmp.CreateTime FROM 
            (SELECT  TOP (@count) Id, CreateUserId,CreateTime FROM dbo.Approval_Log AS A WHERE  FK_TypeId=@FK_TypeId AND FK_ApplyFlowId=@FK_ApplyFlowId ORDER BY Id DESC ) tmp  
            LEFT JOIN dbo.Sys_User ON tmp.CreateUserId = dbo.Sys_User.Id ";
            Pmts.ClearPmts();
            Pmts.Add("num", counts);
            Pmts.Add("FK_TypeId", fk_TypeId);
            Pmts.Add("FK_ApplyFlowId", fk_ApplyFlowId);
            List<Sys_User> list = Db.ExecuteToList<Sys_User>(sql, Pmts.ToArray());

            return list;
        }

        #region 获取审批日志
        /// <summary>
        /// 获取审批日志
        /// </summary>
        /// <param name="applyFlowId">申请单id</param>
        /// <param name="typeId">申请单类型id</param>
        /// <returns></returns>
        public List<Approval_LogModel> GetApproval_LogList(long? applyFlowId, int typeId)
        {
            string sql = @"select a.*,b.RealName from Approval_Log as a
            left join Sys_User as b on a.CreateUserId=b.Id
            where a.FK_ApplyFlowId=@FK_ApplyFlowId and a.FK_TypeId=@FK_TypeId";
            Pmts.ClearPmts();
            Pmts.Add("FK_ApplyFlowId", applyFlowId);
            Pmts.Add("FK_TypeId", typeId);
            return Db.ExecuteToList<Approval_LogModel>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
