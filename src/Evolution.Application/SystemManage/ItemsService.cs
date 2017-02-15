﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data;
using Evolution.Domain.IRepository.SystemManage;
using System.Threading.Tasks;
using Evolution.Framework;

namespace Evolution.Application.SystemManage
{
    public class ItemsService : IItemsService
    {
        private IItemsRepository service = null;
        public ItemsService(IItemsRepository service)
        {
            this.service = service;
        }

        public Task<List<ItemsEntity>> GetList(string tenantId)
        {
            Pagination p = new Pagination();
            p.page = 1;
            p.rows = Int32.MaxValue;
            return service.FindListAsync(t=>t.TenantId== tenantId,p);
        }
        public Task<ItemsEntity> GetForm(string keyValue, string tenantId)
        {
            return service.FindEntityASync(t=>t.Id==keyValue && t.TenantId==tenantId);
        }
        public Task<int> Delete(string keyValue, string tenantId)
        {
            if (service.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return service.DeleteAsync(t => t.Id == keyValue && t.TenantId==tenantId);
            }
        }
        public Task<int> Save(ItemsEntity itemsEntity, string keyValue,string userId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.AttachModifyInfo(keyValue, userId);
                return service.UpdateAsync(itemsEntity);
            }
            else
            {
                itemsEntity.AttachCreateInfo(userId);
                return service.InsertAsync(itemsEntity);
            }
        }
    }
}
