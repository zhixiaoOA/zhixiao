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
    public class My_HolidayBLL : BaseBLL<My_Holiday, My_HolidayDAL>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public static DataList<My_HolidayModel> GetMy_HolidayList(string key, int pageIndex, int pageSize)
		{
			return new My_HolidayDAL().GetMy_HolidayList(key, pageIndex, pageSize);
		}
		#endregion
    }
}
