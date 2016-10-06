using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Modular.Modules.ModuleB.Module;

namespace Modular.Modules.ModuleB.Migrations
{
    [DbContext(typeof(ModuleBContext))]
    partial class ModuleBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Modular.Modules.ModuleB.Module.DemoModel", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTime?>("CreatorTime");

                    b.Property<string>("CreatorUserId");

                    b.Property<bool?>("DeleteMark");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<string>("DeleteUserId");

                    b.Property<string>("DemoStr");

                    b.Property<string>("Description");

                    b.Property<bool?>("EnabledMark");

                    b.Property<DateTime?>("LastModifyTime");

                    b.Property<string>("LastModifyUserId");

                    b.Property<int?>("SortCode");

                    b.HasKey("Id");

                    b.ToTable("DemoModels");
                });
        }
    }
}
