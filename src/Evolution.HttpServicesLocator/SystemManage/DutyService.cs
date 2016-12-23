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
using Evolution.IInfrastructure;
using Newtonsoft.Json;

namespace Evolution.Application.SystemManage
{
    public class DutyService : IDutyService
    {
        public DutyService()
        {
        }

        public Task<List<RoleEntity>> GetList(string keyword = "")
        {
            string r = HttpHelper.Get("/SystemManage/Duty/GetList");
            return Task.FromResult(r.ToObject<List<RoleEntity>>());
        }
        public Task<RoleEntity> GetForm(string keyValue)
        {
            string r = HttpHelper.Get("/SystemManage/Duty/GetForm");
            return Task.FromResult(r.ToObject<RoleEntity>());
        }
        public Task<int> Delete(string keyValue)
        {
            return service.DeleteAsync(t => t.Id == keyValue);
        }
        public Task<int> Save(RoleEntity roleEntity, string keyValue,HttpContext context)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //roleEntity.Modify(keyValue, context);
                return service.UpdateAsync(roleEntity);
            }
            else
            {
                //roleEntity.Create(context);
                roleEntity.Category = 2;
                return service.InsertAsync(roleEntity);
            }
        }
    }
}
