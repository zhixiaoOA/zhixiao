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
    public class ApprovalDAL : DBBase<Approval>
    {
        #region 分页获取数据列表
        public DataList<ApprovalModel> GetApprovalList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetApprovalList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<ApprovalModel> list = Db.ExecuteProcToList<ApprovalModel>(sql, Pmts.ToArray());
            DataList<ApprovalModel> pageList = new DataList<ApprovalModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<ApprovalModel> GetApprovalList(string key, int pageIndex, int pageSize, int conditions)
        {
            //string sql = @"SELECT A.*,UserName=B.RealName FROM dbo.Approval_User AS A
            //LEFT JOIN dbo.Sys_User AS B ON A.FK_UserId=B.Id
            //WHERE A.FK_ApprovalId=@aid";
            string sql = "Proc_GetApprovalList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<ApprovalModel> list = Db.ExecuteToList<ApprovalModel>(sql, Pmts.ToArray());
            DataList<ApprovalModel> pageList = new DataList<ApprovalModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 请假
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_AskModel> GetMy_AskList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[AType]
                          ,[StartTime]
                          ,[StartHour]
                          ,[EndTime]
                          ,[EndHour]
                          ,[ADesc]
                          ,[ATotalLength]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_Ask]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_AskModel> list = Db.ExecuteToList<My_AskModel>(sql, Pmts.ToArray());
            DataList<My_AskModel> pageList = new DataList<My_AskModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 考勤
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_AttendanceModel> GetMy_AttendanceList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[StartTime]
                          ,[EndTime]
                          ,[ADesc]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM[ShanDongOA].[dbo].[My_Attendance]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_AttendanceModel> list = Db.ExecuteToList<My_AttendanceModel>(sql, Pmts.ToArray());
            DataList<My_AttendanceModel> pageList = new DataList<My_AttendanceModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 外出
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_GoOutModel> GetMy_GoOutList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[BName]
                          ,[BKGName]
                          ,[StartTime]
                          ,[StartHour]
                          ,[EndTime]
                          ,[EndHour]
                          ,[ADesc]
                          ,[EndCity]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_GoOut]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_GoOutModel> list = Db.ExecuteToList<My_GoOutModel>(sql, Pmts.ToArray());
            DataList<My_GoOutModel> pageList = new DataList<My_GoOutModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 加班
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_WorkModel> GetMy_WorkList(string key, int pageIndex, int pageSize, int conditions)
        {
            //string sql = @"SELECT A.*,UserName=B.RealName FROM dbo.Approval_User AS A
            //LEFT JOIN dbo.Sys_User AS B ON A.FK_UserId=B.Id
            //WHERE A.FK_ApprovalId=@aid";
            string sql = @"SELECT [Id]
                          ,[AType]
                          ,[StartTime]
                          ,[StartHour]
                          ,[EndTime]
                          ,[EndHour]
                          ,[ADesc]
                          ,[ATotalLength]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_Work]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_WorkModel> list = Db.ExecuteToList<My_WorkModel>(sql, Pmts.ToArray());
            DataList<My_WorkModel> pageList = new DataList<My_WorkModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 调休
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_PaidLeaveModel> GetMy_PaidLeaveList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[FK_WorkId]
                          ,[StartTime]
                          ,[StartHour]
                          ,[EndTime]
                          ,[EndHour]
                          ,[ADesc]
                          ,[ATotalLength]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_PaidLeave]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_PaidLeaveModel> list = Db.ExecuteToList<My_PaidLeaveModel>(sql, Pmts.ToArray());
            DataList<My_PaidLeaveModel> pageList = new DataList<My_PaidLeaveModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 出差
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_BusinessTripModel> GetMy_BusinessTripList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[BName]
                          ,[BKGName]
                          ,[StartTime]
                          ,[StartHour]
                          ,[EndTime]
                          ,[EndHour]
                          ,[ADesc]
                          ,[StartCity]
                          ,[EndCity]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_BusinessTrip]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_BusinessTripModel> list = Db.ExecuteToList<My_BusinessTripModel>(sql, Pmts.ToArray());
            DataList<My_BusinessTripModel> pageList = new DataList<My_BusinessTripModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 报销
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_BaoXiaoModel> GetMy_BaoXiaoList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[BName]
                          ,[FK_DeptId]
                          ,[Subject]
                          ,[SubjectType]
                          ,[Currency]
                          ,[EndHour]
                          ,[ADesc]
                          ,[Attachment]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_BaoXiao]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_BaoXiaoModel> list = Db.ExecuteToList<My_BaoXiaoModel>(sql, Pmts.ToArray());
            DataList<My_BaoXiaoModel> pageList = new DataList<My_BaoXiaoModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 报销明细
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_BaoXiao_DetailModel> GetMy_BaoXiao_DetailList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[DDate]
                          ,[FK_BaoXiaoId]
                          ,[EndHour]
                          ,[ADesc]
                          ,[BaoXiaoUserId]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_BaoXiao_Detail]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_BaoXiao_DetailModel> list = Db.ExecuteToList<My_BaoXiao_DetailModel>(sql, Pmts.ToArray());
            DataList<My_BaoXiao_DetailModel> pageList = new DataList<My_BaoXiao_DetailModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        /// <summary>
        /// 节假日
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataList<My_HolidayModel> GetMy_HolidayList(string key, int pageIndex, int pageSize, int conditions)
        {
            string sql = @"SELECT [Id]
                          ,[HTypeId]
                          ,[HName]
                          ,[StartTime]
                          ,[EndTime]
                          ,[HDesc]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                          ,[Status]
                          ,[FK_ApprovalUserId]
                      FROM [ShanDongOA].[dbo].[My_Holiday]
                      WHERE [FK_ApprovalUserId]=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("UserId", conditions);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<My_HolidayModel> list = Db.ExecuteToList<My_HolidayModel>(sql, Pmts.ToArray());
            DataList<My_HolidayModel> pageList = new DataList<My_HolidayModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion

        #region 获取单个审批节点
        /// <summary>
        /// 根据节点获取类型
        /// </summary>
        /// <param name="FK_ApprovalId"></param>
        /// <returns></returns>
        public Approval_TypeModel GetApprovalType(int FK_ApprovalId)
        {
            string sql = @" SELECT [Id]
                          ,[FK_ApprovalId]
                          ,[AName]
                          ,[TRemark]
                          ,[TSort]
                          ,[CreateUserId]
                          ,[CreateTime]
                          ,[UpdateUserId]
                          ,[UpdateTime]
                          ,[CreateAccount]
                          ,[UpdateAccount]
                      FROM [ShanDongOA].[dbo].[Approval_Type]
                      where FK_ApprovalId=@FK_ApprovalId";
            Pmts.ClearPmts();
            Pmts.Add("FK_ApprovalId", FK_ApprovalId);
            return Db.ExecuteToSingle<Approval_TypeModel>(sql, Pmts.ToArray());
        }
        /// <summary>
        /// 根据节点获取审批用户
        /// </summary>
        /// <param name="FK_ApprovalId"></param>
        /// <returns></returns>
        public Approval_UserModel GetApprovalUser(int FK_UserId)
        {
            string sql = @"SELECT [Id]
                          ,[FK_ApprovalId]
                          ,[FK_UserId]
                          ,[FlowName]
                      FROM [ShanDongOA].[dbo].[Approval_User]
                      where FK_UserId=@FK_UserId";
            Pmts.ClearPmts();
            Pmts.Add("FK_UserId", FK_UserId);
            return Db.ExecuteToSingle<Approval_UserModel>(sql, Pmts.ToArray());
        }
        /// <summary>
        /// 获取当前审批类型的所有审批用户
        /// </summary>
        /// <param name="FK_ApprovalId">节点类型</param>
        /// <returns></returns>
        public List<Approval_UserModel> GetCurrentApprovalUser(int FK_ApprovalId)
        {
            string sql = @"SELECT [Id]
                          ,[FK_ApprovalId]
                          ,[FK_UserId]
                          ,[FlowName]
                      FROM [ShanDongOA].[dbo].[Approval_User]
                      WHERE FK_ApprovalId=(SELECT FK_ApprovalId FROM dbo.Approval_Type WHERE Id=@FK_ApprovalId)";
            Pmts.ClearPmts();
            Pmts.Add("FK_ApprovalId", FK_ApprovalId);
            List<Approval_UserModel> list = Db.ExecuteToList<Approval_UserModel>(sql, Pmts.ToArray());
            return list;
        }
        /// <summary>
        /// 获取关于当前用户的审批流
        /// </summary>
        /// <param name="FK_ApprovalId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Approval_UserModel> GetTheCurrentApprovalUser(int FK_ApprovalId, int UserId)
        {
            string sql = @"SELECT [Id]
                          ,[FK_ApprovalId]
                          ,[FK_UserId]
                          ,[FlowName]
                      FROM [ShanDongOA].[dbo].[Approval_User]
                      WHERE FK_ApprovalId=(SELECT FK_ApprovalId FROM dbo.Approval_Type WHERE Id=@FK_ApprovalId)
                      AND FK_UserId=@UserId";
            Pmts.ClearPmts();
            Pmts.Add("FK_ApprovalId", FK_ApprovalId);
            Pmts.Add("UserId", UserId);
            List<Approval_UserModel> list = Db.ExecuteToList<Approval_UserModel>(sql, Pmts.ToArray());
            return list;
        }
        #endregion

        #region 根据类型id和部门id获取流程
        /// <summary>
        /// 根据类型id和部门id获取流程
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="deptId">部门id</param>
        /// <returns></returns>
        public List<Approval> GetListByTypeId(int typeId, long deptId)
        {
            string sql = "SELECT * FROM dbo.Approval WHERE (FK_DeptId=@deptId OR FK_DeptId=0) AND FK_TypeId=@typeId";
            Pmts.ClearPmts();
            Pmts.Add("deptId", deptId);
            Pmts.Add("typeId", typeId);
            return Db.ExecuteToList<Approval>(sql, Pmts.ToArray());
        }
        #endregion
    }
}
