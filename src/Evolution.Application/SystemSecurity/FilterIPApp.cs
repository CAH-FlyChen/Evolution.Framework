/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Domain.IRepository.SystemSecurity;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Application.SystemSecurity
{
    public class FilterIPApp
    {
        private IFilterIPRepository service = null;
        public FilterIPApp(IFilterIPRepository service)
        {
            this.service = service;
        }
        public Task<List<FilterIPEntity>> GetList(string keyword)
        {
            var expression = ExtLinq.True<FilterIPEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.StartIP.Contains(keyword));
            }
            return service.IQueryable(expression).OrderByDescending(t => t.DeleteTime).ToListAsync();
        }
        public Task<FilterIPEntity> GetForm(string keyValue)
        {
            return service.FindEntityAsync(keyValue);
        }
        public Task<int> Delete(string keyValue)
        {
            return service.DeleteAsync(t => t.Id == keyValue);
        }
        public Task<int> Save(FilterIPEntity filterIPEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterIPEntity.AttachModifyInfo(keyValue, context);
                return service.UpdateAsync(filterIPEntity);
            }
            else
            {
                filterIPEntity.AttachCreateInfo(context);
                return service.InsertAsync(filterIPEntity);
            }
        }
    }
}
