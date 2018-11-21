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
    public class Project_LogBLL : BaseBLL<Project_Log, Project_LogDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<Project_LogModel> GetProject_LogList(string key, int pageIndex, int pageSize)
        {
            return new Project_LogDAL().GetProject_LogList(key, pageIndex, pageSize);
        }
        #endregion
    }
}
