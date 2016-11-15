using Evolution.Plugins.Area.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Organize
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public class TemporaryDbContextFactory : IDbContextFactory<AreaDbContext>
        {
            public AreaDbContext Create(DbContextFactoryOptions options)
            {
                //此处链接测试时使用。以插件形式发布后此处不会被调用
                var builder = new DbContextOptionsBuilder<AreaDbContext>();
                builder.UseSqlServer("Server=192.168.10.12,1433;Initial Catalog=EvolutionBase;User ID=sa;Password=Pass@word1");
                return new AreaDbContext(builder.Options);
            }
        }
    }
}
