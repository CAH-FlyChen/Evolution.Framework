/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Data.Entity;
using System;

namespace Evolution.Domain.Entity.SystemManage
{
    public class ItemsDetailEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string ItemId { get; set; }
        public string ParentId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string SimpleSpelling { get; set; }
        public bool? IsDefault { get; set; }
        public int? Layers { get; set; }
    }
}
