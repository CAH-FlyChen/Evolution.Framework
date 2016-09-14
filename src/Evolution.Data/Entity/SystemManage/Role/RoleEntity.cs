/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
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
    }
}
