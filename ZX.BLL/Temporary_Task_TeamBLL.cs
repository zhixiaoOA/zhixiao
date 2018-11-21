using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.DAL;
using ZX.Model;
using ZX.Tools;

namespace ZX.BLL
{
    public class Temporary_Task_TeamBLL : BaseBLL<Temporary_Task_Team, Temporary_Task_TeamDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Temporary_Task_TeamModel> GetTemporary_Task_TempList(string key, int pageIndex, int pageSize)
        {
            return new Temporary_Task_TeamDAL().GetTemporary_Task_TempList(key, pageIndex, pageSize);
        }
        #endregion
    }
}
