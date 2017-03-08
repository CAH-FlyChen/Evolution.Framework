//using Evolution.Plugins.WeiXin.IServices;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Evolution.Framework;
//using Evolution.Plugins.WeiXin.Entities;
//using Evolution.Repository;
//using Evolution.Plugins.WeiXin.Repos;

//namespace Evolution.Plugins.WeiXin.Services
//{
//    public class Location : ILocation
//    {
//        private ILocationRepository repo = null;

//        public Location(ILocationRepository repo)
//        {
//            this.repo = repo;
//        }

//        public void AddLocation(string weixinUserId, string sourceid, string latitude, string longitude, string precision, string bdlongitude, string bdlatitude, string label, string business, string province, string city, string district, string street, string street_number,string tenantId)
//        {
//            //LocationEntity e = new LocationEntity();
//            //e.BDLatitude = bdlatitude;
//            //e.BDLongitude = bdlongitude;
//            //e.Business = business;
//            //e.City = city;
//            //e.CreateTime = DateTime.Now;
//            //e.Id = Guid.NewGuid().ToString("N");
//            //e.Label = label;
//            //e.Latitude = latitude;
//            //e.Longitude = longitude;
//            //e.Precision = precision;
//            //e.Province = province;
//            //e.Street = street;
//            //e.StreetNumber = street_number;
//            //e.TenantId = tenantId;
//            //e.WeiXinAppId = 
//        }

//        public LocationEntity GetLastLocation(string weixinUserId, string tenantId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<LocationEntity>> GetLocationListByWXUserId(string wexinUserId, string tenantId, Pagination p)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
