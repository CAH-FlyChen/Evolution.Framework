/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Evolution.Data;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Domain.IRepository.SystemSecurity;
using Evolution.Data.Extensions;
using System.Threading.Tasks;

namespace Evolution.Repository.SystemSecurity
{
    public class DbBackupRepository : RepositoryBase<DbBackupEntity>, IDbBackupRepository
    {
        public DbBackupRepository(EvolutionDBContext ctx) : base(ctx)
        {

        }
        public async Task<int> Delete(string keyValue)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                var dbBackupEntity = await db.FindEntityAsync<DbBackupEntity>(keyValue);
                if (dbBackupEntity != null)
                {
                    FileHelper.DeleteFile(dbBackupEntity.FilePath);
                }
                await db.DeleteAsync<DbBackupEntity>(dbBackupEntity);
                return await db.CommitAsync();
            }
        }
        public Task<int> ExecuteDbBackup(DbBackupEntity dbBackupEntity)
        {
            DbHelper.ExecuteSqlCommand(string.Format("backup database {0} to disk ='{1}'", dbBackupEntity.DbName, dbBackupEntity.FilePath));
            dbBackupEntity.FileSize = FileHelper.ToFileSize(FileHelper.GetFileSize(dbBackupEntity.FilePath));
            dbBackupEntity.FilePath = "/Resource/DbBackup/" + dbBackupEntity.FileName;
            return this.InsertAsync(dbBackupEntity);
        }
    }
}
