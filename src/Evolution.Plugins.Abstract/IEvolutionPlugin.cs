using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Abstract
{
    public interface IEvolutionPlugin
    {
        void MeragePluginDataBase(IServiceProvider applicationServices);
        void AddPluginEFService(IServiceCollection services, IConfiguration config);
        void InitPluginData(DbContext baseDbContext, IServiceProvider servicep, string webRootPath);
        void InitDependenceInjection();
    }
}
