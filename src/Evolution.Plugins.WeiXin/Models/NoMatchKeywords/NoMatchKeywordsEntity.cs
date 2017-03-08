namespace Evolution.Plugins.WeiXin.Entities
{
    using Data;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class NoMatchKeywordsEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string AppId { get; set; }
        public string ResType { get; set; }
        public string NewsId { get; set; }
        public string Content { get; set; }
        public string TenantId { get; set; }
    }
}
