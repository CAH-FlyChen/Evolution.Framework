using System;
using Microsoft.Extensions.DependencyInjection;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Evolution.Domain.Entity.SystemManage;
using System.Threading.Tasks;
using System.IO;
using Evolution.Domain.Entity.SystemSecurity;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using Evolution.Data.Entity.SystemManage;
using Evolution.Framework.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Http;
using static Evolution.Framework.Jwt.SimpleTokenProvider;
using JWT.Common.Middlewares.TokenProvider;
using Microsoft.Extensions.Options;

namespace Evolution.Web.API.Extentions
{
    public static class ApplicationBuilderExtention
    {
        public static void InitFreameworkDbData(this IApplicationBuilder app,IServiceProvider applicationServices, string webRootPath, EvolutionDBContext dbContext)
        {
            var sqlServerDatabase = dbContext.Database;
            //try
            //{
            //    int r = sqlServerDatabase.ExecuteSqlCommand("select count(*) from Sys_User");
            //    return;
            //}
            //catch(Exception ex)
            //{
            //    //这个ex是可预期的。
                    
            //}
            try
            {
                sqlServerDatabase.EnsureDeleted();
            }
            catch { }
                
                if (sqlServerDatabase.EnsureCreated())
                {
                //sqlServerDatabase.Migrate();
                DataInitTool.OpenExcel("InitData.xlsx", webRootPath);

                DataInitTool.ProcessSheet("Sys_DbBackup", colums => {
                        DbBackupEntity entity = new DbBackupEntity();
                        entity.Id = colums[0];
                        entity.BackupType = colums[1];
                        entity.DbName = colums[2];
                        entity.FileName = colums[3];
                        entity.FileSize = colums[4];
                        entity.FilePath = colums[5];
                        entity.BackupTime = DateTime.Parse(colums[6]);
                        entity.EnabledMark = bool.Parse(colums[9]);
                        entity.CreateTime = DateTime.MinValue;
                        dbContext.DbBackups.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_FilterIP", colums => {
                        FilterIPEntity entity = new FilterIPEntity();
                        entity.Id = colums[0];
                        entity.Type = bool.Parse(colums[1]);
                        entity.StartIP = colums[2];
                        entity.EndIP = colums[3];
                        entity.EnabledMark = bool.Parse(colums[6]);
                        entity.Description = colums[7];
                        entity.CreateTime = DateTime.Parse(colums[8]);
                        entity.TenantId = colums[14];
                        dbContext.FilterIPs.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_Items", colums => {
                        ItemsEntity entity = new ItemsEntity();
                        entity.Id = colums[0];
                        entity.ParentId = colums[1];
                        entity.EnCode = colums[2];
                        entity.FullName = colums[3];
                        entity.IsTree = GetDefaultBool(colums[4], false);
                        entity.Layers = int.Parse(colums[5]);
                        entity.SortCode = int.Parse(colums[6]);
                        entity.DeleteMark = GetDefaultBool(colums[7], false);
                        entity.EnabledMark = GetDefaultBool(colums[8], true);
                        entity.CreateTime = DateTime.MinValue;
                        entity.TenantId = colums[16];
                        dbContext.Items.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_ItemsDetail", colums => {
                        ItemsDetailEntity entity = new ItemsDetailEntity();
                        entity.Id = colums[0];
                        entity.ItemId = colums[1];
                        entity.ParentId = colums[2];
                        entity.ItemCode = colums[3];
                        entity.ItemName = colums[4];
                        entity.IsDefault = GetDefaultBool(colums[6], false);
                        entity.SortCode = int.Parse(colums[8]);
                        entity.DeleteMark = GetDefaultBool(colums[9], false);
                        entity.EnabledMark = GetDefaultBool(colums[10], true);
                    entity.TenantId = colums[18];
                        dbContext.ItemsDetails.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_Log", colums => {
                        LogEntity entity = new LogEntity();
                        entity.Id = colums[0];
                        entity.Date = DateTime.Parse(colums[1]);
                        entity.Account = colums[2];
                        entity.NickName = colums[3];
                        entity.Type = colums[4];
                        entity.IPAddress = colums[5];
                        entity.IPAddressName = colums[6];
                        entity.ModuleName = colums[8];
                        entity.Result = bool.Parse(colums[9]);
                        entity.Description = colums[10];
                        entity.CreateTime = DateTime.MinValue;
                        entity.CreatorUserId = colums[12];
                    entity.TenantId = colums[13];
                        dbContext.Logs.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_Menu", colums => {
                        MenuEntity entity = new MenuEntity();
                        entity.Id = colums[0];
                        entity.ParentId = colums[1];
                        entity.Layers = int.Parse(colums[2]);
                        entity.FullName = colums[4];
                        entity.Icon = colums[5];
                        entity.UrlAddress = colums[6];
                        entity.Target = colums[7];
                        entity.IsMenu = bool.Parse(colums[8]);
                        entity.IsExpand = bool.Parse(colums[9]);
                        entity.IsPublic = bool.Parse(colums[10]);
                        entity.AllowEdit = bool.Parse(colums[11]);
                        entity.AllowDelete = bool.Parse(colums[12]);
                        entity.SortCode = int.Parse(colums[13]);
                        entity.DeleteMark = bool.Parse(colums[14]);
                        entity.EnabledMark = bool.Parse(colums[15]);
                        entity.Description = colums[16];
                        entity.CreateTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[20];
                        entity.TenantId = colums[23];
                        dbContext.Menus.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_MenuButton", colums => {
                        MenuButtonEntity entity = new MenuButtonEntity();
                        entity.Id = colums[0];
                        entity.MenuId = colums[1];
                        entity.ParentId = colums[2];
                        if (!string.IsNullOrEmpty(colums[3]))
                            entity.Layers = int.Parse(colums[3]);
                        entity.EnCode = colums[4];
                        entity.FullName = colums[5];
                        if (!string.IsNullOrEmpty(colums[7]))
                            entity.Location = int.Parse(colums[7]);
                        entity.JsEvent = colums[8];
                        entity.UrlAddress = colums[9];
                        if (!string.IsNullOrEmpty(colums[10]))
                            entity.Split = bool.Parse(colums[10]);
                        if (!string.IsNullOrEmpty(colums[11]))
                            entity.IsPublic = bool.Parse(colums[11]);
                        if (!string.IsNullOrEmpty(colums[12]))
                            entity.AllowEdit = bool.Parse(colums[12]);
                        if (!string.IsNullOrEmpty(colums[13]))
                            entity.AllowDelete = bool.Parse(colums[13]);
                        if (!string.IsNullOrEmpty(colums[14]))
                            entity.SortCode = int.Parse(colums[14]);
                        if (!string.IsNullOrEmpty(colums[15]))
                            entity.DeleteMark = bool.Parse(colums[15]);
                        if (!string.IsNullOrEmpty(colums[16]))
                            entity.EnabledMark = bool.Parse(colums[16]);
                        entity.CreateTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[20];
                    entity.TenantId = colums[24];
                        dbContext.ModuleButtons.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_Organize", colums =>
                    {
                        OrganizeEntity entity = new OrganizeEntity();
                        entity.Id = colums[0];
                        entity.ParentId = colums[1];
                        entity.Layers = int.Parse(colums[2]);
                        entity.EnCode = colums[3];
                        entity.FullName = colums[4];
                        entity.ShortName = colums[5];
                        entity.CategoryId = colums[6];
                        entity.ManagerId = colums[7];
                        entity.Address = colums[14];
                        entity.SortCode = int.Parse(colums[17]);
                        entity.DeleteMark = bool.Parse(colums[18]);
                        entity.EnabledMark = bool.Parse(colums[19]);
                        entity.CreateTime = DateTime.MinValue;
                        entity.TenantId = colums[27];
                        dbContext.Organizes.Add(entity);

                    });
                DataInitTool.ProcessSheet("Sys_Role", colums => {
                        RoleEntity entity = new RoleEntity();
                        entity.Id = colums[0];
                        entity.OrganizeId = colums[1];
                        entity.Category = int.Parse(colums[2]);
                        entity.EnCode = colums[3];
                        entity.FullName = colums[4];
                        entity.Type = colums[5];
                        entity.AllowEdit = bool.Parse(colums[6]);
                        entity.AllowDelete = bool.Parse(colums[7]);
                        entity.SortCode = int.Parse(colums[8]);
                        entity.DeleteMark = bool.Parse(colums[9]);
                        entity.EnabledMark = bool.Parse(colums[10]);

                        entity.CreateTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[15];
                        entity.TenantId = colums[18];
                        dbContext.Roles.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_RoleAuthorize", colums => {
                        RoleAuthorizeEntity entity = new RoleAuthorizeEntity();
                        entity.Id = colums[0];
                        entity.ItemType = int.Parse(colums[1]);
                        entity.ItemId = colums[2];
                        entity.ObjectType = int.Parse(colums[3]);
                        entity.ObjectId = colums[4];
                        //entity.SortCode = int.Parse(colums[5]);
                        entity.CreateTime = DateTime.MinValue;
                        entity.CreatorUserId = colums[7];
                    entity.TenantId = colums[8];
                        dbContext.RoleAuthorize.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_User", colums => {
                        UserEntity entity = new UserEntity();
                        entity.Id = colums[0];
                        entity.Account = colums[1];
                        entity.RealName = colums[2];
                        entity.NickName = colums[3];
                        entity.Gender = bool.Parse(colums[5]);
                        entity.MobilePhone = colums[7];
                        entity.OrganizeId = colums[13];
                        entity.DepartmentId = colums[14];
                        entity.RoleId = colums[15];
                        entity.DutyId = colums[16];
                        entity.IsAdministrator = bool.Parse(colums[17]);
                        entity.DeleteMark = bool.Parse(colums[19]);
                        entity.EnabledMark = bool.Parse(colums[20]);
                        entity.Description = colums[21];
                        entity.CreateTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[25];
                        entity.TenantId = colums[28];
                        dbContext.Users.Add(entity);
                    });
                DataInitTool.ProcessSheet("Sys_UserLogOn", colums => {
                        UserLogOnEntity entityt = new UserLogOnEntity();
                        entityt.Id = colums[0];
                        entityt.UserId = colums[1];
                        entityt.UserPassword = colums[2];
                        entityt.UserSecretkey = colums[3];
                        if (!string.IsNullOrEmpty(colums[9]))
                            entityt.PreviousVisitTime = DateTime.Parse(colums[9]);
                        if (!string.IsNullOrEmpty(colums[10]))
                            entityt.LastVisitTime = DateTime.Parse(colums[10]);
                        if (!string.IsNullOrEmpty(colums[13]))
                            entityt.LogOnCount = int.Parse(colums[13]);
                        entityt.TenantId = colums[20];
                        dbContext.UserLogOn.Add(entityt);
                    });
                DataInitTool.ProcessSheet("Sys_Tenant", colums => {
                    TenantEntity entityt = new TenantEntity();
                    entityt.Id = colums[0];
                    entityt.EnCode = colums[1];
                    entityt.FullName = colums[2];
                    entityt.CreateTime = DateTime.MinValue;
                    entityt.LastModifyTime = DateTime.MinValue;
                    dbContext.Tenants.Add(entityt);
                });
                dbContext.SaveChanges();
                }
        }

        private static bool GetDefaultBool(string s, bool defaultV)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultV;
            }
            else
            {
                return bool.Parse(s);
            }
        }
        /// <summary>
        /// 检测Token
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void ConfigureJwtAuth(this IApplicationBuilder app,IConfigurationRoot configuration)
        {
            var audience = configuration["Jwt:Audience:Name"];
            var issuer = configuration["Jwt:Issuer"];
            var symmetricKeyAsBase64 = configuration["Jwt:Audience:Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience,

                // Validate the token expiry
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters,
            });
        }
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void GenJWTEndpoint(this IApplicationBuilder app, IConfigurationRoot configuration)
        {
            var audience = configuration["Jwt:Audience:Name"];
            var symmetricKeyAsBase64 = configuration["Jwt:Audience:Secret"];
            var issuer = configuration["Jwt:Issuer"];
            var expDuration = double.Parse(configuration["Jwt:ExpDuration"]);
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var options = new TokenProviderOptions
            {
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromMinutes(expDuration),
                config = configuration
            };
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
        }

    }
}
