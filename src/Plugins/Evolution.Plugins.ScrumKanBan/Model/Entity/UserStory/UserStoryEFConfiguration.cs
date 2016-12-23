/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evolution.Data.Entity;
using Evolution.Plugins.ScrumKanBan.Models;

namespace Evolution.EFConfiguration
{
    public class UserStoryEFConfiguration : EFConfigurationBase<UserStoryEntity>
    {
        public override void Configure(EntityTypeBuilder<UserStoryEntity> entity)
        {
            entity.ToTable("P_KanBan_UserStory");
            base.Configure(entity);
        }
    }
}
