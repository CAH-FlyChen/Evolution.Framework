using AutoMapper;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Plugins.Abstract;
using Evolution.Plugins.ScrumKanBan.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using System.Linq;
using System;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Plugins.ScrumKanBan
{
    public class ModuleInitializer : EvolutionPluginBase<KanBanDbContext>
    {
        public override void InitPluginData(DbContext baseDbContextx)
        {
            EvolutionDBContext baseDbContext = (EvolutionDBContext)baseDbContextx;
            if (baseDbContext.Menus.Count(t => t.UrlAddress == "/kanban/userstory") == 0)
            {
                MenuEntity section = new MenuEntity();
                section.Id = Guid.NewGuid().ToString();
                section.FullName = "敏捷项目管理";
                section.Layers = 1;
                section.IsExpand = true;
                section.IsMenu = false;
                section.IsPublic = false;
                section.LastModifyTime = DateTime.Now;
                section.LastModifyUserId = "System";
                section.SortCode = 0;
                section.Target = "expand";
                section.ParentId = "0";
                section.EnabledMark = true;
                section.AllowDelete = false;
                section.AllowEdit = false;
                section.DeleteMark = false;
                baseDbContext.Add(section);

                MenuEntity menuItem = new MenuEntity();
                menuItem.Id = "76824036-58D7-44DD-BF94-A1E0679FFFEE";
                menuItem.FullName = "项目管理";
                menuItem.Layers = 2;
                menuItem.IsExpand = false;
                menuItem.IsMenu = true;
                menuItem.IsPublic = false;
                menuItem.LastModifyTime = DateTime.Now;
                menuItem.LastModifyUserId = "System";
                menuItem.SortCode = 999;
                menuItem.Target = "iframe";
                menuItem.ParentId = section.Id;
                menuItem.EnabledMark = true;
                menuItem.UrlAddress = "/kanban/project";
                menuItem.AllowDelete = false;
                menuItem.AllowEdit = false;
                menuItem.DeleteMark = false;
                baseDbContext.Add(menuItem);

                ProjectEntity pe = new ProjectEntity();
                pe.Id = Guid.NewGuid().ToString();
                pe.Name = "测试项目";
                pe.LastModifyTime = DateTime.Now;
                pe.LastModifyUserId = "System";
                pe.SortCode = 0;
                pe.EnabledMark = true;
                pe.DeleteMark = false;
                baseDbContext.Add(pe);
                baseDbContext.SaveChanges();
            }
        }
        public override void InitDependenceInjection()
        {
            //TODO:此处可以添加以来注入，跨项目使用，或者接口和实现不再同一个类中时使用
            //Services.AddTransient<IAreaRepository, AreaRepository>();
            //Services.AddTransient<IAreaService, AreaService>();
            Services.AddAutoMapper();
        }
    }
}
