using Evolution.Data;
using Evolution.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evolution.Plugins.WeiXin.Entities
{    
    public class CustomizeMenuLinkEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string MenuLink { get; set; }
        public string TenantId { get; set; }
    }
}
