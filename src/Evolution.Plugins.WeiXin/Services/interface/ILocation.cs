//using Evolution.Framework;
//using Evolution.Plugins.WeiXin.Entities;
//using OvalTech.TechCloud.Module.WeiChat.Model;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Evolution.Plugins.WeiXin.IServices
//{
//    public interface ILocation
//    {
//        //void AddCheckIn(string type, string mmid, string sourceid, string latitude, string longitude, string precision, string bdlongitude, string bdlatitude, string label, string business, string province, string city, string district, string street, string street_number);
//        void AddLocation(string weixinUserId, string sourceid, string latitude, string longitude, string precision, string bdlongitude, string bdlatitude, string label, string business, string province, string city, string district, string street, string street_number,string tenantId);
//        /// <summary>
//        /// 最后一次所在位置
//        /// </summary>
//        /// <param name="weixinUserId">微信用户Id</param>
//        /// <returns></returns>
//        LocationEntity GetLastLocation(string weixinUserId,string tenantId);
//        /// <summary>
//        /// 通过微信用户id获取所有位置信息
//        /// </summary>
//        /// <param name="wexinUserId">微信用户Id</param>
//        /// <param name="p">分页信息</param>
//        /// <returns></returns>
//        Task<List<LocationEntity>> GetLocationListByWXUserId(string wexinUserId, string tenantId,Pagination p);
//    }
//}
