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
    public class MyCostBLL : BaseBLL<MyCost, MyCostDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">合同名称</param>
        /// <param name="userId">申请人id</param>
        /// <param name="appUserId">审核人id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<MyCostModel> GetMyCostList(string key, long userId, long appUserId, string beginTime, string endTime, string status, int pageIndex, int pageSize)
		{
			return new MyCostDAL().GetMyCostList(key, userId, appUserId, beginTime, endTime, status, pageIndex, pageSize);
		}
        #endregion

        #region 根据id获取数据
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MyAgreementModel GetModelById(long id)
        {
            return new MyAgreementDAL().GetModelById(id);
        }
        #endregion

        #region 根据条件获取数据
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="startTime">时间段-自</param>
        /// <param name="endTime">时间段-至</param>
        /// <returns>My_WorkModel</returns>
        public static List<MyAgreementModel> GetModelListByWhere(int userId, string startTime, string endTime)
        {
            return new MyAgreementDAL().GetModelListByWhere(userId, startTime, endTime);
        }
        #endregion
    }
}
