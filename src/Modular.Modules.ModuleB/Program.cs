using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Modular.Modules.ModuleB.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleB
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
    public class TemporaryDbContextFactory : IDbContextFactory<ModuleBContext>
    {
        public ModuleBContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ModuleBContext>();
            builder.UseSqlServer("Server=192.168.10.12,1433;Initial Catalog=EvolutionBase;User ID=sa;Password=Pass@word1");
            return new ModuleBContext(builder.Options);
        }
    }


}
