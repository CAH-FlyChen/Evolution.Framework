using Evolution.EFConfiguration;
using Evolution.Plugins.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.WeiXin.Modules
{
    public class KeywordsEFConfigurationBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new KeywordsEFConfiguration());
        }
    }
}
