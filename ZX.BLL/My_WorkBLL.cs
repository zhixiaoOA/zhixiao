﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Tools;
using ZX.BLL;
using ZX.Model;
using ZX.DAL;

namespace ZX.BLL
{
    public class My_WorkBLL : BaseBLL<My_Work, My_WorkDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">事由</param>
        /// <param name="userId">申请人id</param>
        /// <param name="appUserId">审核人id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<My_WorkModel> GetMy_WorkList(string key, long userId, long appUserId, string beginTime, string endTime, string status, int pageIndex, int pageSize)
        {
            return new My_WorkDAL().GetMy_WorkList(key, userId, appUserId, beginTime, endTime, status, pageIndex, pageSize);
        }
        #endregion

        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="userId">审核人id</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<My_WorkModel> GetMy_WorkList(string key, long userId, string beginTime, string endTime, int pageIndex, int pageSize)
        {
            return new My_WorkDAL().GetMy_WorkList(key, userId, beginTime, endTime, pageIndex, pageSize);
        }
        #endregion

        #region 根据id获取数据
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static My_WorkModel GetModelById(long id)
        {
            return new My_WorkDAL().GetModelById(id);
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
        public static List<My_WorkModel> GetModelListByWhere(int userId, string startTime, string endTime)
        {
            return new My_WorkDAL().GetModelListByWhere(userId, startTime, endTime);
        }
        #endregion
    }

}
