///*******************************************************************************
// * Copyright © 2016 NFine.Framework 版权所有
// * Author: NFine
// * Description: NFine快速开发平台
// * Website：http://www.nfine.cn
//*********************************************************************************/
//using Evolution.Framework;
//using Evolution.Domain.Entity.SystemManage;
//using Evolution.Domain.IRepository.SystemManage;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Reflection;
//using Evolution;

//namespace NFine.Application.SystemManage
//{
//    public class PermissionApp
//    {
//        private IPermissionRepository service = null;

//        public PermissionApp(IPermissionRepository service)
//        {
//            this.service = service;
//        }

//        /// <summary>
//        /// 获取权限级别列表
//        /// </summary>
//        /// <param name="keyword"></param>
//        /// <returns></returns>
//        public List<PermissionEntity> GetList(Type controllerType, string keyword = "")
//        {
//            List<PermissionEntity> list = new List<PermissionEntity>();
//            var a = controllerType.GetTypeInfo().Assembly;
//            Type[] types = a.GetTypes();
//            foreach (Type t in types)
//            {
//                PermissionEntity pe = new PermissionEntity();
//                if (t.FullName.EndsWith("Controller"))
//                {
//                    var controllerAttr = t.GetTypeInfo().GetCustomAttribute<PermissionLevelDescripAttribute>();

//                    pe.F_Name = GetControllerName(t.FullName);
//                    pe.F_Type = 0;
//                    pe.F_MoudleName = GetModuleName(t.FullName);
//                    pe.F_FullPath = t.FullName;

//                    if (controllerAttr != null)
//                    {
//                        pe.F_DescriptionName = controllerAttr.Name;
//                        pe.F_Description = controllerAttr.Description;
//                    }

//                    list.Add(pe);
//                    foreach (var m in t.GetMethods())
//                    {
//                        var attr = m.GetCustomAttribute<PermissionLevelDescripAttribute>();
//                        if (attr != null)
//                        {
//                            PermissionEntity pe0 = new PermissionEntity();
//                            pe0.F_FullPath = t.FullName + "." + m.Name;
//                            pe0.F_Name = GetControllerName(t.FullName) +"."+ m.Name;
//                            pe0.F_Type = 1;
//                            pe0.F_DescriptionName = attr.Name;
//                            pe0.F_Description = attr.Description;
//                            pe0.F_MoudleName = GetModuleName(t.FullName);

//                            list.Add(pe0);
//                        }

//                    }
//                }
//            }
//            return list;

//        }

//        public string GetModuleName(string fullName)
//        {
//            //	NFine.Web.Areas.SystemSecurity.Controllers.FilterIPController	
//            int areaStart = fullName.IndexOf(".Areas.")+7;
//            int controllerStart = fullName.IndexOf(".Controllers.");
//            return fullName.Substring(areaStart, controllerStart - areaStart);

//        }

//        public string GetControllerName(string fullName)
//        {
//            int controllerStart = fullName.IndexOf(".Controllers.")+13;
//            return fullName.Substring(controllerStart);
//        }



//        /// <summary>
//        /// 获取一个权限级别
//        /// </summary>
//        /// <param name="keyValue"></param>
//        /// <returns></returns>
//        public PermissionEntity GetForm(string permissionId)
//        {
//            return service.FindEntity(permissionId);
//        }
//        /// <summary>
//        /// 删除一个权限级别
//        /// </summary>
//        /// <param name="permissionId"></param>
//        public void DeleteForm(string permissionId)
//        {
//            service.DeleteForm(permissionId);
//        }
//        public void SubmitForm(PermissionEntity organizeEntity, string id, HttpContext context)
//        {
//            //if (!string.IsNullOrEmpty(id))
//            //{
//            //    organizeEntity.Modify(id, context);
//            //    service.Update(organizeEntity);
//            //}
//            //else
//            //{
//            //    organizeEntity.Create(context);
//            //    service.Insert(organizeEntity);
//            //}
//            return;
//        }
//    }
//}
