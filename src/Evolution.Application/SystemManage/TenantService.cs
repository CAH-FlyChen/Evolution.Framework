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
    public class TenantService : ITenantService
    {
        private ITenantRepository service = null;
        public TenantService(ITenantRepository service)
        {
            this.service = service;
        }

        public Task<List<TenantEntity>> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<TenantEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FullName.Contains(keyword));
                expression = expression.Or(t => t.EnCode.Contains(keyword));
            }
            //expression = expression.And(t => t.Category == 2);
            return service.IQueryable(expression).OrderBy(t => t.SortCode).ToListAsync();
        }
        public Task<TenantEntity> GetForm(string keyValue)
        {
            return service.FindEntityAsync(keyValue);
        }
        public Task<int> Delete(string keyValue)
        {
            return service.DeleteAsync(t => t.Id == keyValue);
        }
        public Task<int> Save(TenantEntity roleEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //roleEntity.Modify(keyValue, context);
                return service.UpdateAsync(roleEntity);
            }
            else
            {
                //roleEntity.Create(context);
                return service.InsertAsync(roleEntity);
            }
        }
    }
}
