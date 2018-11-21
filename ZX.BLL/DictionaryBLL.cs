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
    public class DictionaryBLL : BaseBLL<Dictionary, DictionaryDAL>
    {
        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static DataList<DictionaryModel> GetDictionaryList(string key, int pageIndex, int pageSize)
        {
            return new DictionaryDAL().GetDictionaryList(key, pageIndex, pageSize);
        }
        #endregion

        #region 获取申请单类型数据
        /// <summary>
        /// 获取申请单类型数据
        /// </summary>
        /// <returns></returns>
        public static List<DictionaryModel> GetApplyTypeList()
        {
            return new DictionaryDAL().GetApplyTypeList();
        }
        #endregion

        #region 根据父id获取数据
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="parentIds">父id</param>
        /// <returns></returns>
        public static List<Dictionary> GetDictionaryListByPid(string parentIds)
        {
            return new DictionaryDAL().GetDictionaryListByPid(parentIds);
        }
        #endregion
    }
}
