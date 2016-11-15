using Evolution.EFConfiguration;
using Evolution.EFConfiguration.SystemManage;
using Evolution.Plugins.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.ScrumKanBan.Modules
{
    public class KanBanModleBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            //base
            modelBuilder.AddConfiguration(new MenuEFConfiguration());
            //this
            modelBuilder.AddConfiguration(new ProjectEFConfiguration());
            modelBuilder.AddConfiguration(new StoryStatusEFConfiguration());
            modelBuilder.AddConfiguration(new UserStoryEFConfiguration());
        }
    }
}
