/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.IRepository;
using Evolution.Domain.Entity.SystemSecurity;
using System.Threading.Tasks;

namespace Evolution.Domain.IRepository.SystemSecurity
{
    public interface IDbBackupRepository : IRepositoryBase<DbBackupEntity>
    {
        Task<int> Delete(string keyValue);
        Task<int> ExecuteDbBackup(DbBackupEntity dbBackupEntity);
    }
}
