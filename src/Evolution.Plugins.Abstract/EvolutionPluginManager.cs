using CoreProfiler;
using CoreProfiler.Data;
using Evolution.Data;
using Evolution.Framework;
using Evolution.Plugins.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using MySQL.Data.Entity.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Evolution.Plugins.Abstract
{
    
    /// <summary>
    /// 处理所有插件内容
    /// </summary>
    public class EvolutionPluginManager
    {
        #region 私有变量
        public List<PluginAssembly> PluginAssemblies = new List<PluginAssembly>();
        private IHostingEnvironment env = null;
        private IServiceCollection services = null;
        private static List<IEvolutionPlugin> PluginTypes = null;
        #endregion
        #region 构造函数
        public EvolutionPluginManager(IHostingEnvironment env, IServiceCollection services)
        {
            this.env = env;
            this.services = services;
        }
        #endregion

        public List<PluginAssembly> LoadPluginAssembly()
        {
            List<PluginAssembly> pa = new List<PluginAssembly>();
            var pluginsRootFolder = env.ContentRootFileProvider.GetDirectoryContents("/Plugins");
            foreach (var pluginFolder in pluginsRootFolder.Where(x => x.IsDirectory))
            {
                var binFolder = new DirectoryInfo(Path.Combine(pluginFolder.PhysicalPath, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }
                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException ex)
                    {
                        if (ex.Message == "Assembly with same name is already loaded")
                        {
                            continue;
                        }
                        throw;
                    }
                    if (assembly.FullName.Contains(pluginFolder.Name))
                    {
                        var assId = assembly.GetCustomAttributes<GuidAttribute>().First().Value;
                        pa.Add(new PluginAssembly
                        {
                            Name = pluginFolder.Name,
                            Assembly = assembly,
                            Path = pluginFolder.PhysicalPath,
                            Id = assId
                        });
                    }
                }
            }
            return pa;
        }
        /// <summary>
        /// 加载插件到内存
        /// </summary>
        public List<PluginAssembly> LoadPluginAssembly(IMvcBuilder mvcBuilder)
        {
            PluginAssemblies = this.LoadPluginAssembly();
            foreach (var p in PluginAssemblies)
            {
                var assembly = p.Assembly;
                mvcBuilder.AddApplicationPart(assembly);//.AddControllersAsServices();
            }
            return PluginAssemblies;
        }
        /// <summary>
        /// 加入插件的EF服务
        /// </summary>
        public void AddPluginEFService(IConfigurationRoot configuration)
        {
            ResolveTypes();
            foreach (var p in PluginTypes)
            {
                p.AddPluginEFService(services, configuration);
            }
        }

        public void InjectEvolutionDependency()
        {
            ResolveTypes();
            foreach (var p in PluginTypes)
            {
                p.InitDependenceInjection();
            }
        }

        public void MeragePluginDBStruct(IServiceProvider applicationServices)
        {
            ResolveTypes();
            foreach (var p in PluginTypes)
            {
                p.MeragePluginDataBase(applicationServices);
            }
        }

        public void InitPluginData(DbContext dbContextBase,IServiceProvider servicep, string webRootPath)
        {
            ResolveTypes();
            foreach (var p in PluginTypes)
            {
                p.InitPluginData(dbContextBase, servicep, webRootPath);
            }
        }

        private void ResolveTypes()
        {
            if(PluginTypes==null)
            {
                PluginTypes = new List<IEvolutionPlugin>();
                foreach (var ass in PluginAssemblies)
                {
                    var assTypes = ass.Assembly.GetTypes();
                    var pluginType = assTypes.Where(x => typeof(IEvolutionPlugin).IsAssignableFrom(x)).FirstOrDefault();
                    if (pluginType != null && pluginType != typeof(IEvolutionPlugin))
                    {
                        var plugin = (IEvolutionPlugin)Activator.CreateInstance(pluginType);
                        PluginTypes.Add(plugin);
                    }
                }
            }
        }

        /// <summary>
        /// 注册插件的EF实体
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddPluginEFModle(ModelBuilder modelBuilder)
        {
            List<PluginAssembly> assemblies = GloableConfiguration.PluginAssemblies;
            List<Type> typeToRegisters = new List<Type>();
            foreach (var ass in assemblies)
                typeToRegisters.AddRange(ass.Assembly.DefinedTypes.Select(t => t.AsType()));
            RegisterEntities(modelBuilder, typeToRegisters);
            RegisterEFCustomMappings(modelBuilder, typeToRegisters);
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="Configuration"></param>
        /// <param name="DataBase"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(IConfigurationRoot Configuration, string DataBase)
        {
            if (DataBase.ToLower() == "sqlserver")
            {
                var connStr = Configuration.GetConnectionString("MDatabase");
                SqlConnection conn = new SqlConnection(connStr);
                return new ProfiledDbConnection(conn, () => {
                    if (ProfilingSession.Current == null)
                        return null;
                    return new DbProfiler(ProfilingSession.Current.Profiler);
                });

            }
            else if (DataBase.ToLower() == "mysql")
            {
                var connStr = Configuration.GetConnectionString("MMysqlDatabase");
                MySqlConnection conn = new MySqlConnection(connStr);
                return new ProfiledDbConnection(conn, () => {
                    if (ProfilingSession.Current == null)
                        return null;
                    return new DbProfiler(ProfilingSession.Current.Profiler);
                });
            }
            else
            {
                return null;
            }
        }
        private static void RegisterEFCustomMappings(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));
            foreach (var builderType in customModelBuilderTypes)
            {
                if (builderType != null && builderType != typeof(ICustomModelBuilder))
                {
                    var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType);
                    builder.Build(modelBuilder);
                }
            }
        }
        private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var entityTypes = typeToRegisters.Where(x => x.GetTypeInfo().IsSubclassOf(typeof(EntityBase)) && !x.GetTypeInfo().IsAbstract);
            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }
    }
}
