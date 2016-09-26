///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：
//*********************************************************************************/
//using Microsoft.AspNetCore.Http;
//using Evolution.Domain.Entity.SystemManage;
//using Evolution.Domain.IRepository.SystemManage;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Evolution.Data;

//namespace Evolution.Application.SystemManage
//{
//    public class ModuleApp
//    {
//        private IModuleRepository service = null;
//        public ModuleApp(IModuleRepository service)
//        {
//            this.service = service;
//        }

//        public List<ModuleEntity> GetList()
//        {
//            return service.IQueryable().OrderBy(t => t.SortCode).ToList();
//        }
//        public ModuleEntity GetForm(string keyValue)
//        {
//            return service.FindEntity(keyValue);
//        }
//        public void Delete(string keyValue)
//        {
//            if (service.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
//            {
//                throw new Exception("删除失败！操作的对象包含了下级数据。");
//            }
//            else
//            {
//                service.Delete(t => t.Id == keyValue);
//            }
//        }
//        public void Save(ModuleEntity moduleEntity, string keyValue,HttpContext context)
//        {
//            if (!string.IsNullOrEmpty(keyValue))
//            {
//                moduleEntity.AttachModifyInfo(keyValue, context);
//                service.Update(moduleEntity);
//            }
//            else
//            {
//                moduleEntity.AttachCreateInfo(context);
//                service.Insert(moduleEntity);
//            }
//        }
//    }
//}
