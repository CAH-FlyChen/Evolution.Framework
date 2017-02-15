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
using Evolution.Data.Entity.SystemManage;

namespace Evolution.Application.SystemManage
{
    public class OrganizeService : IOrganizeService
    {
        private IOrganizeRepository repo = null;
        public OrganizeService(IOrganizeRepository repo)
        {
            this.repo = repo;
        }
        public Task<List<OrganizeEntity>> GetList(string tenantId)
        {
            return repo.IQueryable().Where(t=>t.TenantId==tenantId).OrderBy(t => t.CreateTime).ToListAsync();
        }
        public Task<OrganizeEntity> GetForm(string keyValue,string tenantId)
        {
            return repo.FindEntityASync(t=>t.Id==keyValue && t.TenantId==tenantId);
        }
        public Task<int> Delete(string keyValue,string tenantId)
        {
            if (repo.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return repo.DeleteAsync(t => t.Id == keyValue && t.TenantId==tenantId);
            }
        }
        public Task<int> Save(OrganizeEntity organizeEntity, string keyValue,string userId)
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
