/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using Evolution.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Evolution.Plugins.WeiXin.IServices;
using Evolution.Plugins.WeiXin.Entities;

namespace Evolution.Plugins.WeiXin.Services
{
    public class WeiXinConfigService : IWeiXinConfigService
    {
        private IWeiXinConfigRepository repo = null;
        public WeiXinConfigService(IWeiXinConfigRepository repo)
        {
            this.repo = repo;
        }
        public Task<List<WeiXinConfigEntity>> GetList()
        {
            return repo.IQueryable().OrderBy(t => t.CreateTime).ToListAsync();
        }
        public Task<WeiXinConfigEntity> GetForm(string keyValue)
        {
            return repo.FindEntityAsync(keyValue);
        }
        public Task<int> Delete(string keyValue)
        {
              return repo.DeleteAsync(t => t.Id == keyValue);
        }
        public Task<int> Save(WeiXinConfigEntity organizeEntity, string keyValue,string userId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.AttachModifyInfo(keyValue, userId);
                return repo.UpdateAsync(organizeEntity);
            }
            else
            {
                organizeEntity.AttachCreateInfo(userId);
                return repo.InsertAsync(organizeEntity);
            }
        }
    }
}
