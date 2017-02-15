/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Application.SystemManage
{
    public class DutyService : IDutyService
    {
        private IRoleRepository service = null;
        public DutyService(IRoleRepository service)
        {
            this.service = service;
        }

        public Task<List<RoleEntity>> GetList(string keyword,string tenantId)
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FullName.Contains(keyword));
                expression = expression.Or(t => t.EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.Category == 2).And(x=>x.TenantId==tenantId);
            return service.IQueryable(expression).OrderBy(t => t.SortCode).ToListAsync();
        }
        public Task<RoleEntity> GetForm(string keyValue,string tenantId)
        {
            return service.FindEntityASync(t=>t.Id==keyValue && t.TenantId==tenantId);
        }
        public Task<int> Delete(string keyValue,string tenantId)
        {
            return service.DeleteAsync(t => t.Id == keyValue && t.TenantId==tenantId);
        }
        public Task<int> Save(RoleEntity roleEntity, string keyValue,string tenantId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.TenantId = tenantId;
                //roleEntity.Modify(keyValue, context);
                return service.UpdateAsync(roleEntity);
            }
            else
            {
                //roleEntity.Create(context);
                roleEntity.Category = 2;
                roleEntity.TenantId = tenantId;
                return service.InsertAsync(roleEntity);
            }
        }
    }
}
