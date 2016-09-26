/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.IRepository;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;

namespace Evolution.Domain.IRepository.SystemManage
{
    public interface IMenuButtonRepository : IRepositoryBase<MenuButtonEntity>
    {
        void SubmitCloneButton(List<MenuButtonEntity> entitys);
    }
}
