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
    public class My_AgendaBLL : BaseBLL<My_Agenda, My_AgendaDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<My_AgendaModel> GetMy_AgendaList(string key, int userId, int status, int pageIndex, int pageSize)
        {
            return new My_AgendaDAL().GetMy_AgendaList(key, userId, status, pageIndex, pageSize);
        }
        #endregion

        /// <summary>
        /// 分页获取未完成数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataList<My_AgendaModel> GetMy_AgendaUnFinishList(string key, int userId, int pageIndex, int pageSize)
        {
            return new My_AgendaDAL().GetMy_AgendaUnFinishList(key, userId, pageIndex, pageSize);
        }


        /// <summary>
        /// 分页获取待定数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataList<My_AgendaModel> GetMy_AgendaAIsUndeterminedList(string key, int userId, int pageIndex, int pageSize)
        {
            return new My_AgendaDAL().GetMy_AgendaAIsUndeterminedList(key, userId, pageIndex, pageSize);
        }


        /// <summary>
        /// 分页获取指派给我的待定数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataList<My_AgendaModel> GetMy_AgendaAssignedMyList(string key, int userId, int pageIndex, int pageSize)
        {
            return new My_AgendaDAL().GetMy_AgendaAssignedMyList(key, userId, pageIndex, pageSize);
        }


        /// <summary>
        /// 分页获取我指派给他人的待定数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataList<My_AgendaModel> GetMy_AgendaAssignedOtherList(string key, int userId, int pageIndex, int pageSize)
        {
            return new My_AgendaDAL().GetMy_AgendaAssignedOtherList(key, userId, pageIndex, pageSize);
        }
    }
}
