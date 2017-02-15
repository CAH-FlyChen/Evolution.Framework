/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Data.Entity;
using System;

namespace Evolution.Domain.Entity.SystemSecurity
{
    public class FilterIPEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public bool? Type { get; set; }
        public string StartIP { get; set; }
        public string EndIP { get; set; }
        public string TenantId { get; set; }

    }
}
