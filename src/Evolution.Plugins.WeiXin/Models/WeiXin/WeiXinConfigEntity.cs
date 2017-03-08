/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Domain;
using System;

namespace Evolution.Plugins.WeiXin.Entities
{
    public class WeiXinConfigEntity : EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string Token { get; set; }
        public string EncodingAESKey { get; set; }
        public string AppId { get; set; }
        public string TenantId { get; set; }
        public string AppSecret { get; internal set; }
    }
}
