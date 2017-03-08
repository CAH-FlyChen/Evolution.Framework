using Evolution.Domain.IRepository.SystemManage;
using Evolution.Framework.Data;
using Evolution.Plugins.Abstract;
using Evolution.Plugins.WeiXin.Entities;
using Evolution.Plugins.WeiXin.IServices;
using Evolution.Plugins.WeiXin.Modules;
using Evolution.Plugins.WeiXin.Services;
using Evolution.Repository.SystemManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySQL.Data.Entity.Extensions;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using System;

namespace Evolution.Plugins.WeiXin
{
    public class ModuleInitializer : EvolutionPluginBase<WeiXinDbContext>
    {
        public override void InitPluginData(DbContext baseDbContext, IServiceProvider servicep,string webRootPath)
        {
            //base.InitPluginData(baseDbContext, servicep);
            var dbContext = servicep.GetService<WeiXinDbContext>();
            if(dbContext.WeiXinConfigs.CountAsync().Result!=0)
            {
                return;
            }
            if(DataInitTool.workbook==null)
                DataInitTool.OpenExcel("InitData.xlsx", webRootPath);
            DataInitTool.ProcessSheet("Plugin_WeiXin_Config", colums =>
            {
                WeiXinConfigEntity entity = new WeiXinConfigEntity();
                entity.Id = colums[0];
                entity.AppId = colums[1];
                entity.EncodingAESKey = colums[2];
                entity.Token = colums[3];
                entity.AppSecret = colums[4];
                entity.TenantId = colums[5];
                dbContext.WeiXinConfigs.Add(entity);
            });
            dbContext.SaveChanges();
        }
        public override void InitDependenceInjection()
        {
            //TODO:此处可以添加以来注入，跨项目使用，或者接口和实现不再同一个类中时使用
            Services.AddTransient<IWeiXinConfigRepository, WeiXinConfigRepository>();
            Services.AddSingleton<IWeiXinConfigService, WeiXinConfigService>();

            Services.AddTransient<IWeiXinUserRepository, WeiXinUserRepository>();
            Services.AddTransient<IWeiXinUserService, WeiXinUserService>();
        }
    }
}
