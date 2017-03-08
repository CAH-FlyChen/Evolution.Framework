using Evolution.Plugins.WeiXin.DTO.CustomizeMenu;
using Evolution.Plugins.WeiXin.Entities;
using System;
using System.Collections.Generic;
using System.Data;
namespace Evolution.Plugins.WeiXin.IServices
{
    public interface ICustomizedMenuService
    {
        void AddMenu(CustomizeMenuInput customizeMenuInput);
        void DeleteMenu(string menuId, bool deleteRef);
        void UpdateMenu(CustomizeMenuInput customizeMenuInput);
        List<CustomizeMenuEntity> GetMenuList();
    }
}
