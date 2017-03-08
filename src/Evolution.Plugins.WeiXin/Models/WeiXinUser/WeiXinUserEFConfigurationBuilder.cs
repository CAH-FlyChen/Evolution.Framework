using Evolution.EFConfiguration;
using Evolution.Plugins.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Area.Modules
{
    public class WeiXinUserConfigurationBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new WeiXinUserEFConfiguration());
        }
    }
}
