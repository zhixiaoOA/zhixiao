using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZX.Model;
using ZX.Tools;

namespace ZX.DAL
{
    public class Project_Task_TeamDAL : DBBase<Project_Task_Team>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public DataList<Project_Task_TeamModel> GetProject_Task_TeamList(string key, int pageIndex, int pageSize)
        {
            string sql = "Proc_GetProject_Task_TeamList";
            Pmts.ClearPmts();
            Pmts.Add("key", key);
            Pmts.Add("pageIndex", pageIndex);
            Pmts.Add("pageSize", pageSize);
            Pmts.Add("count", -1, ParameterDirection.Output);
            List<Project_Task_TeamModel> list = Db.ExecuteProcToList<Project_Task_TeamModel>(sql, Pmts.ToArray());
            DataList<Project_Task_TeamModel> pageList = new DataList<Project_Task_TeamModel>(list, Pmts.ListPmts[3].Value.ToInt(), pageIndex, pageSize);
            return pageList;
        }
        #endregion
    }
}
