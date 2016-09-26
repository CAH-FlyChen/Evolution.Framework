///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：
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
//using Evolution.Data.Entity.SystemManage;
//using Microsoft.AspNetCore.Mvc;

//namespace Evolution.Application.SystemManage
//{
//    public class PermissionApp
//    {

//        /// <summary>
//        /// 获取权限级别列表
//        /// </summary>
//        /// <param name="keyword"></param>
//        /// <returns></returns>
//        public List<PermissionEntity> GetList(Type controllerType, string keyword = "")
//        {
//            //assembly
//            List<PermissionEntity> list = new List<PermissionEntity>();
//            var a = controllerType.GetTypeInfo().Assembly;
//            Type[] types = a.GetTypes();
//            foreach (Type t in types)
//            {
//                PermissionEntity pe = new PermissionEntity();
//                if (t.FullName.EndsWith("Controller"))
//                {
//                    var controllerPermissionAttr = t.GetTypeInfo().GetCustomAttribute<PermissionLevelDescripAttribute>();
                    
//                    pe.Name = GetControllerName(t.FullName);
//                    pe.Type = 0;
//                    pe.MoudleName = GetModuleName(t.FullName);
                    
//                    if (controllerPermissionAttr != null)
//                    {
//                        pe.DescriptionName = controllerPermissionAttr.Name;
//                        pe.Description = controllerPermissionAttr.Description;
//                    }

//                    list.Add(pe);
//                    foreach (var m in t.GetMethods())
//                    {
//                        var attr = m.GetCustomAttribute<PermissionLevelDescripAttribute>();
//                        if (attr != null)
//                        {
//                            PermissionEntity pe0 = new PermissionEntity();
//                            pe0.FullNamespace = t.FullName + "." + m.Name;
//                            pe0.Name = GetControllerName(t.FullName) + "." + m.Name;
//                            pe0.Type = 1;
//                            pe0.DescriptionName = attr.Name;
//                            pe0.Description = attr.Description;
//                            pe0.MoudleName = GetModuleName(t.FullName);

//                            list.Add(pe0);
//                        }

//                    }
//                }
//            }
//            return list;

//        }

//        public string GetModuleName(string fullName)
//        {
//            //	Evolution.Web.Areas.SystemSecurity.Controllers.FilterIPController	
//            int areaStart = fullName.IndexOf(".Areas.") + 7;
//            int controllerStart = fullName.IndexOf(".Controllers.");
//            return fullName.Substring(areaStart, controllerStart - areaStart);

//        }

//        public string GetControllerName(string fullName)
//        {
//            int controllerStart = fullName.IndexOf(".Controllers.") + 13;
//            return fullName.Substring(controllerStart);
//        }
//    }
//}
