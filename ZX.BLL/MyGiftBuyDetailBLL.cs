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
    public class MyGiftBuyDetailBLL : BaseBLL<MyGiftBuyDetail, MyGiftBuyDetailDAL>
    {
		#region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
		public static DataList<MyGiftBuyDetailModel> GetMyGiftBuyDetailList(string key, int pageIndex, int pageSize)
		{
			return new MyGiftBuyDetailDAL().GetMyGiftBuyDetailList(key, pageIndex, pageSize);
		}
		#endregion
    }
}
