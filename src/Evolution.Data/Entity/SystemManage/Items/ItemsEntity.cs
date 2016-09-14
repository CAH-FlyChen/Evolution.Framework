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
    public class ItemsEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string ParentId { get; set; }
        public string EnCode { get; set; }
        public string FullName { get; set; }
        public bool? IsTree { get; set; }
        public int? Layers { get; set; }

    }
}
