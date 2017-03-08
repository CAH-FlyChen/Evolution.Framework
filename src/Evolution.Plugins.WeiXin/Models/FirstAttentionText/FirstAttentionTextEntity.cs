namespace Evolution.Plugins.WeiXin.Entities
{
    using Data;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FirstAttentionTextEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string AppId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string TenantId { get; set; }
    }
}
