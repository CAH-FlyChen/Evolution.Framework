using Evolution.Domain.IRepository.SystemManage;
using Evolution.Plugins.WeiXin.DTO.CustomizeMenu;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Plugins.WeiXin.Services
{
    public class CustomizedMenuService : ICustomizedMenuService
    {
        private ICustomizedMenuRepository repo = null;

        public CustomizedMenuService(ICustomizedMenuRepository repo)
        {
            this.repo = repo;
        }

        public void AddMenu(CustomizeMenuInput customizeMenuInput)
        {
            repo.AddMenu(customizeMenuInput);
        }


        public void DeleteMenu(string menuId, bool deleteRef)
        {
            repo.DeleteMenu(menuId, deleteRef);
        }

        public List<CustomizeMenuEntity> GetMenuList()
        {
            return repo.GetMenuList();
        }

        public void UpdateMenu(CustomizeMenuInput customizeMenuInput)
        {
            repo.UpdateMenu(customizeMenuInput);
        }
    }
}