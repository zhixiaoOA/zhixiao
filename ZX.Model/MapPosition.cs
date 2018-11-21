using System;

namespace ZX.Model
{
    [DataFieldAttribute("MapPosition")]
    public class MapPosition : BaseModel
    {
        [DataFieldAttribute("PositionId")]
        public string PositionId
        {
            get;
            set;
        }
        [DataFieldAttribute("Title")]
        public string Title
        {
            get;
            set;
        }
        [DataFieldAttribute("Address")]
        public string Address
        {
            get;
            set;
        }
        [DataFieldAttribute("Cause")]
        public string Cause
        {
            get;
            set;
        }
        [DataFieldAttribute("Lat")]
        public string Lat
        {
            get;
            set;
        }
        [DataFieldAttribute("Lng")]
        public string Lng
        {
            get;
            set;
        }
        [DataFieldAttribute("AdCode")]
        public string AdCode
        {
            get;
            set;
        }
        [DataFieldAttribute("City")]
        public string City
        {
            get;
            set;
        }
        [DataFieldAttribute("District")]
        public string District
        {
            get;
            set;
        }
        [DataFieldAttribute("Province")]
        public string Province
        {
            get;
            set;
        }
        [DataFieldAttribute("CreateTime")]
        public DateTime? CreateTime
        {
            get;
            set;
        }
        [DataFieldAttribute("CreateUserId")]
        public Int64 CreateUserId
        {
            get;
            set;
        }
    }
}
