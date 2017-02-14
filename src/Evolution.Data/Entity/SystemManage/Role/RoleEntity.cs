/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Data.Entity;
using System;

namespace Evolution.Domain.Entity.SystemManage
{
    public class RoleEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited //IEntity<RoleEntity>,
    {
        public string OrganizeId { get; set; }
        public int? Category { get; set; }
        public string EnCode { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
        public bool? AllowEdit { get; set; }
        public bool? AllowDelete { get; set; }
        public string TenantId { get; set; }
    }
}
