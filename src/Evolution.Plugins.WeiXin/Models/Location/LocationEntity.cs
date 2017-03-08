using Evolution.Data;
using Evolution.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evolution.Plugins.WeiXin.Entities
{    
    public class LocationEntity: EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// Î¢ÐÅºÅ
        /// </summary>
        public string WeiXinUserID { get; set; }
        /// <summary>
        /// ¹«ÖÚºÅID
        /// </summary>
        public string WeiXinAppId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Precision { get; set; }
        public string BDLongitude { get; set; }
        public string BDLatitude { get; set; }
        public string Label { get; set; }
        public string Business { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }

        public string TenantId { get; set; }
    }
}
