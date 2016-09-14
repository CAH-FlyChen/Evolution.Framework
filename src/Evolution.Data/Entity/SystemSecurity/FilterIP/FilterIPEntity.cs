/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Data.Entity;
using System;

namespace Evolution.Domain.Entity.SystemSecurity
{
    public class FilterIPEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public bool? Type { get; set; }
        public string StartIP { get; set; }
        public string EndIP { get; set; }

    }
}
