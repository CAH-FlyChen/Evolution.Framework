/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Evolution.Domain.IRepository.SystemManage;
using System.Threading.Tasks;

namespace Evolution.Application.SystemManage
{
    public class AreaApp
    {
        private IAreaRepository areaRepo = null;

        public AreaApp(IAreaRepository repo)
        {
            this.areaRepo = repo;
        }

        public Task<List<AreaEntity>> GetList()
        {
            return areaRepo.GetAllAsync();
        }
        public Task<AreaEntity> GetForm(string keyValue)
        {
            return areaRepo.FindEntityASync(t=>t.Id==keyValue);
        }
        public Task<int> Delete(string keyValue)
        {
            if (areaRepo.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return areaRepo.DeleteAsync(t => t.Id == keyValue);
            }
        }
        public Task<int> Save(AreaEntity areaEntity, string keyValue,HttpContext httpContext)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.AttachModifyInfo(keyValue, httpContext);
                return areaRepo.UpdateAsync(areaEntity);
            }
            else
            {
                areaEntity.AttachCreateInfo(httpContext);
                return areaRepo.InsertAsync(areaEntity);
            }
        }
    }
}
