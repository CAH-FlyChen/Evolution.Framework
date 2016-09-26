/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Framework;
using Evolution.Data;
using Evolution.Domain.Entity.SystemSecurity;
using Evolution.Domain.IRepository.SystemSecurity;
using Evolution.Data.Extensions;

namespace Evolution.Repository.SystemSecurity
{
    public class DbBackupRepository : RepositoryBase<DbBackupEntity>, IDbBackupRepository
    {
        public DbBackupRepository(EvolutionDbContext ctx) : base(ctx)
        {

        }
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase(dbcontext).BeginTrans())
            {
                var dbBackupEntity = db.FindEntity<DbBackupEntity>(keyValue);
                if (dbBackupEntity != null)
                {
                    FileHelper.DeleteFile(dbBackupEntity.FilePath);
                }
                db.Delete<DbBackupEntity>(dbBackupEntity);
                db.Commit();
            }
        }
        public void ExecuteDbBackup(DbBackupEntity dbBackupEntity)
        {
            DbHelper.ExecuteSqlCommand(string.Format("backup database {0} to disk ='{1}'", dbBackupEntity.DbName, dbBackupEntity.FilePath));
            dbBackupEntity.FileSize = FileHelper.ToFileSize(FileHelper.GetFileSize(dbBackupEntity.FilePath));
            dbBackupEntity.FilePath = "/Resource/DbBackup/" + dbBackupEntity.FileName;
            this.Insert(dbBackupEntity);
        }
    }
}
