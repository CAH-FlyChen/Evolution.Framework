/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Data;
using Evolution.Domain.IRepository.SystemManage;

namespace NFine.Application.SystemManage
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
        public void DeleteForm(string keyValue)
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
        public void SubmitForm(ItemsEntity itemsEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.Modify(keyValue, context);
                service.Update(itemsEntity);
            }
            else
            {
                itemsEntity.Create(context);
                service.Insert(itemsEntity);
            }
        }
    }
}
