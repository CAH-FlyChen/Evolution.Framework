/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Evolution.Data;
using Evolution.IInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Loader;
using System.IO;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;

namespace Evolution.Application.SystemManage
{
    public class PluginService
    {
        #region 私有变量

        private HttpContext currentContext = null;
        private IPluginRepository repo = null;
        #endregion

        #region 构造函数
        public PluginService(IHttpContextAccessor accessor,IPluginRepository repo)
        {
            this.currentContext = accessor.HttpContext;
            this.repo = repo;
        }
        #endregion

        public Task<List<PluginEntity>> GetList(Pagination pagination, string keyword)
        {
            return repo.GetAllAsync();
        }

        public bool Activate(string pluginId)
        {

            return true;
        }
    }
}
