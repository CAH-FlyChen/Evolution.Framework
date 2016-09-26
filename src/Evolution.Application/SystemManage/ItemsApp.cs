/*******************************************************************************
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

namespace Evolution.Application.SystemManage
{
    public class ItemsApp
    {
        private IItemsRepository service = null;

        public ItemsApp(IItemsRepository service)
        {
            this.service = service;
        }

        public List<ItemsEntity> GetList()
        {
            return service.GetAll();
        }
        public ItemsEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void Delete(string keyValue)
        {
            if (service.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.Id == keyValue);
            }
        }
        public void Save(ItemsEntity itemsEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.AttachModifyInfo(keyValue, context);
                service.Update(itemsEntity);
            }
            else
            {
                itemsEntity.AttachCreateInfo(context);
                service.Insert(itemsEntity);
            }
        }
    }
}
