///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：http://www.nfine.cn
//*********************************************************************************/
//using Evolution.Framework;
//using Evolution.Data;
//using Evolution.Domain.Entity.SystemManage;
//using Evolution.Domain.IRepository.SystemManage;
//using System;

//namespace Evolution.Repository.SystemManage
//{
//    public class PermissionRepository : RepositoryBase<PermissionEntity>, IPermissionRepository
//    {
//        public PermissionRepository(EvolutionDbContext ctx) : base(ctx)
//        {

//        }

//        void IPermissionRepository.DeleteForm(string permissionId)
//        {
//            using (var db = new RepositoryBase(dbcontext).BeginTrans())
//            {
//                db.Delete<PermissionEntity>(t => t.F_Id == permissionId);
//                db.Commit();
//            }
//        }
//    }
//}
