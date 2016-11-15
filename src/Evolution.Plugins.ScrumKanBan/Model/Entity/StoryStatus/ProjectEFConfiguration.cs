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
    public class StoryStatusEFConfiguration : EFConfigurationBase<StoryStatusEntity>
    {
        public override void Configure(EntityTypeBuilder<StoryStatusEntity> entity)
        {
            entity.ToTable("P_KanBan_StoryStatus");
            base.Configure(entity);
        }
    }
}
