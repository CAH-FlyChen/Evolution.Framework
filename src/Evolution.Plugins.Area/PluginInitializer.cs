using Evolution.Domain.IRepository.SystemManage;
using Evolution.Framework.Data;
using Evolution.Plugins.Abstract;
using Evolution.Plugins.Area.Entities;
using Evolution.Plugins.Area.IServices;
using Evolution.Plugins.Area.Modules;
using Evolution.Plugins.Area.Services;
using Evolution.Repository.SystemManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySQL.Data.Entity.Extensions;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using System;

namespace Evolution.Plugins.Area
{
    public class ModuleInitializer : EvolutionPluginBase<AreaDbContext>
    {
        public override void InitPluginData(DbContext baseDbContext, IServiceProvider servicep,string webRootPath)
        {
            //base.InitPluginData(baseDbContext, servicep);
            var dbContext = servicep.GetService<AreaDbContext>();
            if(dbContext.Areas.CountAsync().Result!=0)
            {
                return;
            }
            DataInitTool.ProcessFile("Sys_Area.csv", webRootPath, colums => {
                AreaEntity entity = new AreaEntity();
                entity.Id = colums[0];
                entity.ParentId = colums[1];
                entity.Layers = int.Parse(colums[2]);
                entity.EnCode = colums[3];
                entity.FullName = colums[4];
                entity.SimpleSpelling = colums[5];
                entity.SortCode = int.Parse(colums[6]);
                entity.EnabledMark = bool.Parse(colums[8]);
                entity.CreateTime = DateTime.MinValue;
                dbContext.Areas.Add(entity);
            });
            dbContext.SaveChanges();
        }
        public override void InitDependenceInjection()
        {
            //TODO:此处可以添加以来注入，跨项目使用，或者接口和实现不再同一个类中时使用
            Services.AddTransient<IAreaRepository, AreaRepository>();
            Services.AddTransient<IAreaService, AreaService>();
        }
    }
}
