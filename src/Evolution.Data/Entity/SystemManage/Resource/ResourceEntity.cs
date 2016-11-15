/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Framework;
using System;
using System.Collections.Generic;

namespace Evolution.Domain.Entity.SystemManage
{
    public class ResourceEntity : EntityBase, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public string Name { get; set; }
        public string FullNamespace { get; set; }
        public string Url { get; set; }
        public string ParentClientId { get; set; }
        public string ActionType { get; set; }
        public string ClientID {
            get
            {
                return Md5.md5(Url, 16);
            }
        }
    }
}
