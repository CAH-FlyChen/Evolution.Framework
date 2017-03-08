namespace Evolution.Plugins.WeiXin.Entities
{
    using Data;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class WeiXinMPUserRelationEntity
    {
        public string AppId { get; set; }
        public string WeiXinUserId { get; set; }
        public string TenantId { get; set; }
    }
}
