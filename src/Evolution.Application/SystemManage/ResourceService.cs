/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Domain.Entity.SystemManage;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Evolution.Application.SystemManage
{
    public class ResourceService : IResourceService
    {
        #region 私有变量
        IHostingEnvironment env;
        #endregion
        #region 构造函数
        public ResourceService(IHostingEnvironment env)
        {
            this.env = env;
        }
        #endregion
        /// <summary>
        /// 获取系统资源列表
        /// </summary>
        /// <returns>系统资源列表</returns>
        public Task<List<ResourceEntity>> GetList()
        {
            var loadableAssemblies = new List<ResourceEntity>();
            var deps = DependencyContext.Default;
            foreach (var compilationLibrary in deps.CompileLibraries)
            {
                if (compilationLibrary.Name != "Evolution.Web") continue;
                var assembly = Assembly.Load(new AssemblyName(compilationLibrary.Name));
                foreach(var typ in assembly.GetTypes())
                {
                    if (!typ.Name.EndsWith("Controller")) continue;
                    ResourceEntity entity = new ResourceEntity();
                    entity.ParentClientId = "0";
                    entity.Name = typ.Name;
                    entity.FullNamespace = typ.FullName;
                    var controllerAreaAttr = typ.GetTypeInfo().GetCustomAttribute<AreaAttribute>();
                    string controllerName = typ.Name.Replace("Controller","");
                    if (controllerAreaAttr != null)
                        entity.Url = string.Format($"/{controllerAreaAttr.RouteValue}/{controllerName}");
                    else
                        entity.Url = string.Format($"/{controllerName}");
                            
                    loadableAssemblies.Add(entity);

                    foreach (var act in typ.GetTypeInfo().GetMembers())
                    {
                        var attr1 = act.GetCustomAttribute<HttpGetAttribute>();
                        var attr2 = act.GetCustomAttribute<HttpPostAttribute>();
                        if (attr1 == null && attr2 == null) continue;
                        
                        ResourceEntity ae = new ResourceEntity();
                        ae.Name = act.Name;
                        ae.FullNamespace = typ.FullName + "." + act.Name;
                        ae.ActionType = "";
                        ae.ParentClientId = entity.ClientID;
                        if (controllerAreaAttr != null)
                            ae.Url = string.Format($"/{controllerAreaAttr.RouteValue}/{controllerName}/{ae.Name}");
                        else
                            ae.Url = string.Format($"/{controllerName}/{ae.Name}");
                        if (attr1 != null) ae.ActionType += "| HttpGet";
                        if (attr2 != null) ae.ActionType += "| HttpPost";
                        ae.ActionType = ae.ActionType.Length > 0 ? ae.ActionType.Substring(1).Trim() : "";
                        loadableAssemblies.Add(ae);
                    }
                }
            }
            loadableAssemblies.Sort((x,y)=> {
                return x.Name.CompareTo(y.Name);
            });
            return Task.FromResult(loadableAssemblies);
        }
    }
}
