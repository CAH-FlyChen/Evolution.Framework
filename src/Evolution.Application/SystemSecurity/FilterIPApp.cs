/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Domain.IRepository.SystemSecurity;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;

namespace Evolution.Application.SystemSecurity
{
    public class FilterIPApp
    {
        private IFilterIPRepository service = null;
        public FilterIPApp(IFilterIPRepository service)
        {
            this.service = service;
        }
        public List<FilterIPEntity> GetList(string keyword)
        {
            var expression = ExtLinq.True<FilterIPEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.StartIP.Contains(keyword));
            }
            return service.IQueryable(expression).OrderByDescending(t => t.DeleteTime).ToList();
        }
        public FilterIPEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.Id == keyValue);
        }
        public void SubmitForm(FilterIPEntity filterIPEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterIPEntity.Modify(keyValue, context);
                service.Update(filterIPEntity);
            }
            else
            {
                filterIPEntity.Create(context);
                service.Insert(filterIPEntity);
            }
        }
    }
}
