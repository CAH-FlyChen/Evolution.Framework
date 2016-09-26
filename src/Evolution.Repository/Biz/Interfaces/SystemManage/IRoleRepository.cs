/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.IRepository;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;

namespace Evolution.Domain.IRepository.SystemManage
{
    public interface IRoleRepository : IRepositoryBase<RoleEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(RoleEntity roleEntity, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue);
    }
}
