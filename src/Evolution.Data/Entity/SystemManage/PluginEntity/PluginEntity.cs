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
    public class PluginEntity: EntityBase, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public string Name { get; set; }
        public string AssessmblyName { get; set; }
        public string Path { get; set; }
        public bool Activated { get; set; }
    }
}
