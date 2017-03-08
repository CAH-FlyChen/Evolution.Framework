/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Evolution.IRepository;
using Evolution.Plugins.WeiXin.DTO.CustomizeMenu;
using Evolution.Plugins.WeiXin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolution.Domain.IRepository.SystemManage
{
    public interface ICustomizedMenuRepository : IRepositoryBase<CustomizeMenuEntity>
    {
        void AddMenu(CustomizeMenuInput customizeMenuInput);
        void DeleteMenu(string MenuId, bool deleteRef);
        void UpdateMenu(CustomizeMenuInput customizeMenuInput);
        List<CustomizeMenuEntity> GetMenuList();
    }
}
