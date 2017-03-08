using Evolution.Data;
using Evolution.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evolution.Plugins.WeiXin.Entities
{
    public class WeiXinUserEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string NickName { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public string HeadImgUrl { get; set; }
        public DateTime? AttentionTime { get; set; }
        public string TenantId { get; set; }
    }
}
