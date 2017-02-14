/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data.Entity;
using Evolution.Domain;
using System;

namespace Evolution.Data.Entity.SystemManage
{
    public class OrganizeEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string ParentId { get; set; }
        public int? Layers { get; set; }
        public string EnCode { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string CategoryId { get; set; }
        public string ManagerId { get; set; }
        public string TelePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WeChat { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string AreaId { get; set; }
        public string Address { get; set; }
        public bool? AllowEdit { get; set; }
        public bool? AllowDelete { get; set; }
        public string TenantId { get; set; }
    }
}
