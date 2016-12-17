/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Domain.IRepository.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Application.SystemSecurity
{
    public class DbBackupService
    {
        private IDbBackupRepository service = null;
        public DbBackupService(IDbBackupRepository service)
        {
            this.service = service;
        }
        public Task<List<DbBackupEntity>> GetList(string queryJson)
        {
            var expression = ExtLinq.True<DbBackupEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "DbName":  
                        expression = expression.And(t => t.DbName.Contains(keyword));
                        break;
                    case "FileName":
                        expression = expression.And(t => t.FileName.Contains(keyword));
                        break;
                }
            }
            return service.IQueryable(expression).OrderByDescending(t => t.BackupTime).ToListAsync();
        }
        public Task<DbBackupEntity> GetForm(string keyValue)

        {
            return service.FindEntityAsync(keyValue);
        }
        public Task<int> Delete(string keyValue)
        {
            return service.Delete(keyValue);
        }
        public Task<int> Save(DbBackupEntity dbBackupEntity)
        {
            dbBackupEntity.Id = Common.GuId();
            dbBackupEntity.EnabledMark = true;
            dbBackupEntity.BackupTime = DateTime.Now;
            return service.ExecuteDbBackup(dbBackupEntity);
        }
    }
}
