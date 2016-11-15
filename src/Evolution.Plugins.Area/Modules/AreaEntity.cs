﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Domain;
using System;

namespace Evolution.Plugins.Area.Entities
{
    public class AreaEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string ParentId { get; set; }
        public int? Layers { get; set; }
        public string EnCode { get; set; }
        public string FullName { get; set; }
        public string SimpleSpelling { get; set; }

    }
}
