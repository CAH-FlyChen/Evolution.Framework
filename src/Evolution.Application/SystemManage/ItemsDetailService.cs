/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using Evolution.Domain.IRepository.SystemManage;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Application.SystemManage
{
    public class ItemsDetailService : IItemsDetailService
    {
        private IItemsDetailRepository service = null;

        public ItemsDetailService(IItemsDetailRepository service)
        {
            this.service = service;
        }

        public Task<List<ItemsDetailEntity>> GetList(string itemId, string keyword, string tenantId)
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.ItemName.Contains(keyword));
                expression = expression.Or(t => t.ItemCode.Contains(keyword));
            }
            expression = expression.And(x => x.TenantId == tenantId);

            return service.IQueryable(expression).OrderBy(t => t.SortCode).ToListAsync();
        }
        public Task<List<ItemsDetailEntity>> GetItemList(string enCode, string tenantId)
        {
            return service.GetItemList(enCode,tenantId);
        }
        public Task<ItemsDetailEntity> GetForm(string keyValue, string tenantId)
        {
            return service.FindEntityASync(x=>x.Id==keyValue && x.TenantId==tenantId);
        }
        public Task<int> Delete(string keyValue, string tenantId)
        {
            return service.DeleteAsync(t => t.Id == keyValue && t.TenantId==tenantId);
        }
        public Task<int> Save(ItemsDetailEntity itemsDetailEntity, string keyValue, string userId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsDetailEntity.AttachModifyInfo(keyValue, userId);
                return service.UpdateAsync(itemsDetailEntity);
            }
            else
            {
                itemsDetailEntity.AttachCreateInfo(userId);
                return service.InsertAsync(itemsDetailEntity);
            }
        }
    }
}
