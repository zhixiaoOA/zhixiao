using ZX.Model;

namespace ZX.Model.Model
{
    public class MapPositionModel : MapPosition
    {
        public string SignDate
        {
            get;
            set;
        }
         public string RealName
        {
            get;
            set;
        }
        public string CreateUser
        {
            get;
            set;
        }
        public string ADate { get; set; }
        public string StrStartTime { get; set; }
    }
}
