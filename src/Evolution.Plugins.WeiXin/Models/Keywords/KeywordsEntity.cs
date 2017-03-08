namespace Evolution.Plugins.WeiXin.Entities
{
    using Data;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class KeywordsEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string AppId { get; set; }
        public string KeywordsText { get; set; }
        public string MatchType { get; set; }
        public string ResType { get; set; }
        public string NewsId { get; set; }
        public string Content { get; set; }
        public string TenantId { get; set; }
    }
}
