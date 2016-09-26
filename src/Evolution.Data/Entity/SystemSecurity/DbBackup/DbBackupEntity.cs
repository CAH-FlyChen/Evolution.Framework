/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Data.Entity;
using System;

namespace Evolution.Domain.Entity.SystemSecurity
{
    public class DbBackupEntity :EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string BackupType { get; set; }
        public string DbName { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FilePath { get; set; }
        public DateTime? BackupTime { get; set; }
    }
}
