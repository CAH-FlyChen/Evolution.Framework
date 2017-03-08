namespace Evolution.Plugins.WeiXin.Entities
{
    using Data;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CustomizeMenuNewsEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string NewsId { get; set; }
        public string TenantId { get; set; }
    }
}
