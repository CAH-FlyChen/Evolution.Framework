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
using Evolution.Plugins.Area.Entities;
using Evolution.Plugins.Area.IServices;

namespace Evolution.Plugins.Area.Services
{
    public class AreaService : IAreaService
    {
        private IAreaRepository repo = null;
        public AreaService(IAreaRepository repo)
        {
            this.repo = repo;
        }
        public Task<List<AreaEntity>> GetList()
        {
            return repo.IQueryable().OrderBy(t => t.CreateTime).ToListAsync();
        }
        public Task<AreaEntity> GetForm(string keyValue)
        {
            return repo.FindEntityAsync(keyValue);
        }
        public Task<int> Delete(string keyValue)
        {
            if (repo.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return repo.DeleteAsync(t => t.Id == keyValue);
            }
        }
        public Task<int> Save(AreaEntity organizeEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.AttachModifyInfo(keyValue, context);
                return repo.UpdateAsync(organizeEntity);
            }
            else
            {
                organizeEntity.AttachCreateInfo(context);
                return repo.InsertAsync(organizeEntity);
            }
        }
    }
}
