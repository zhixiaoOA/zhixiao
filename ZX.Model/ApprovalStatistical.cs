using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX.Model
{
    /// <summary>
    /// 用户申请统计虚类
    /// </summary>
    public class ApprovalStatistical : BaseModel
    {
        public string Realname
        {
            get;
            set;
        }
        public int? My_Workcount
        {
            get;
            set;
        }
        public int? My_BusinessTripcount
        {
            get;
            set;
        }
        public int? My_Applycount
        {
            get;
            set;
        }
        public int? MyGooodsUsecount
        {
            get;
            set;
        }
        public int? MyGiftBuycount
        {
            get;
            set;
        }
        public int? MyAgreementcount
        {
            get;
            set;
        }
        public int? MyCartPubliccount
        {
            get;
            set;
        }
        public int? MyClockcount
        {
            get;
            set;
        }
        public int? MySealUsecount
        {
            get;
            set;
        }
        public int? MySealOutcount
        {
            get;
            set;
        }

        public int? My_Askcount
        {
            get;
            set;
        }

        public int? MyEntertaincount
        {
            get;
            set;
        }
    }
}
