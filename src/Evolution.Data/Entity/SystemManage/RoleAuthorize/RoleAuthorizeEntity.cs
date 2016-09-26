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
    public class RoleAuthorizeEntity : EntityBase, ICreationAudited
    {
        /// <summary>
        /// 1 菜单 2按钮 3列表 4资源
        /// </summary>
        public int? ItemType { get; set; }
        public string ItemId { get; set; }
        /// <summary>
        /// 1 角色 2部门 3用户
        /// </summary>
        public int? ObjectType { get; set; }
        public string ObjectId { get; set; }
    }
}
