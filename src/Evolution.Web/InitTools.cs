using System;
using Microsoft.Extensions.DependencyInjection;
using Evolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Evolution.Domain.Entity.SystemManage;
using System.Threading.Tasks;
using System.IO;
using Evolution.Domain.Entity.SystemSecurity;

namespace Evolution.Web
{
    public class InitTools
    {
        public static string WebRootPath = null;
        public static async Task InitializeBasicDb(IServiceProvider applicationServices,string webRootPath)
        {
            WebRootPath = webRootPath;
            using (EvolutionDbContext dbContext = applicationServices.GetService<EvolutionDbContext>())
            {
                var sqlServerDatabase = dbContext.Database;
                try
                {
                    int r = sqlServerDatabase.ExecuteSqlCommand("select count(*) from Sys_Area");
                    //如果正常，则表示数据库有表，不需要创建
                    return;
                }
                catch (Exception ex)
                {
                }

                sqlServerDatabase.EnsureDeleted();
                if (sqlServerDatabase.EnsureCreated())
                {
                    //sqlServerDatabase.Migrate();
                    ProcessFile("Sys_Area.csv", colums => {
                        AreaEntity entity = new AreaEntity();
                        entity.Id = colums[0];
                        entity.ParentId = colums[1];
                        entity.Layers = int.Parse(colums[2]);
                        entity.EnCode = colums[3];
                        entity.FullName = colums[4];
                        entity.SimpleSpelling = colums[5];
                        entity.SortCode = int.Parse(colums[6]);
                        entity.EnabledMark = bool.Parse(colums[8]);
                        entity.CreatorTime = DateTime.MinValue;
                        dbContext.Areas.Add(entity);
                    });
                    ProcessFile("Sys_DbBackup.csv", colums => {
                        DbBackupEntity entity = new DbBackupEntity();
                        entity.Id = colums[0];
                        entity.BackupType = colums[1];
                        entity.DbName = colums[2];
                        entity.FileName = colums[3];
                        entity.FileSize = colums[4];
                        entity.FilePath = colums[5];
                        entity.BackupTime = DateTime.Parse(colums[6]);
                        entity.EnabledMark = bool.Parse(colums[9]);
                        entity.CreatorTime = DateTime.MinValue;
                        dbContext.DbBackups.Add(entity);
                    });
                    ProcessFile("Sys_FilterIP.csv", colums => {
                        FilterIPEntity entity = new FilterIPEntity();
                        entity.Id = colums[0];
                        entity.Type = bool.Parse(colums[1]);
                        entity.StartIP = colums[2];
                        entity.EndIP = colums[3];
                        entity.EnabledMark = bool.Parse(colums[6]);
                        entity.Description = colums[7];
                        entity.CreatorTime = DateTime.Parse(colums[8]);
                        dbContext.FilterIPs.Add(entity);
                    });
                    ProcessFile("Sys_Items.csv", colums => {
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
                        entity.CreatorTime = DateTime.MinValue;
                        dbContext.Items.Add(entity);
                    });
                    ProcessFile("Sys_ItemsDetail.csv", colums => {
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
                        dbContext.ItemsDetails.Add(entity);
                    });
                    ProcessFile("Sys_Log.csv", colums => {
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
                        entity.CreatorTime = DateTime.MinValue;
                        entity.CreatorUserId = colums[12];
                        dbContext.Logs.Add(entity);
                    });
                    ProcessFile("Sys_Menu.csv", colums => {
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
                        entity.CreatorTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[20];
                        dbContext.Menus.Add(entity);
                    });
                    ProcessFile("Sys_MenuButton.csv", colums => {
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
                        entity.CreatorTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[20];
                        dbContext.ModuleButtons.Add(entity);
                    });
                    ProcessFile("Sys_Organize.csv", colums => {
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
                        entity.CreatorTime = DateTime.MinValue;
                        dbContext.Organizes.Add(entity);

                    });
                    ProcessFile("Sys_Role.csv", colums => {
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

                        entity.CreatorTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[15];
                        dbContext.Roles.Add(entity);
                    });
                    ProcessFile("Sys_RoleAuthorize.csv", colums => {
                        RoleAuthorizeEntity entity = new RoleAuthorizeEntity();
                        entity.Id = colums[0];
                        entity.ItemType = int.Parse(colums[1]);
                        entity.ItemId = colums[2];
                        entity.ObjectType = int.Parse(colums[3]);
                        entity.ObjectId = colums[4];
                        //entity.SortCode = int.Parse(colums[5]);
                        entity.CreatorTime = DateTime.MinValue;
                        entity.CreatorUserId = colums[7];
                        dbContext.RoleAuthorize.Add(entity);
                    });
                    ProcessFile("Sys_User.csv", colums => {
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
                        entity.CreatorTime = DateTime.MinValue;
                        entity.LastModifyTime = DateTime.MinValue;
                        entity.LastModifyUserId = colums[25];
                        dbContext.Users.Add(entity);
                    });
                    ProcessFile("Sys_UserLogOn.csv", colums => {
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
                        dbContext.UserLogOn.Add(entityt);
                    });
                    dbContext.SaveChanges();
                }
            }
        }
        public static void ProcessFile(string fileName, Action<string[]> processCode)
        {
            var pathToFile = WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Data_Init"
                            + Path.DirectorySeparatorChar.ToString()
                            + fileName;
            using (StreamReader sr = File.OpenText(pathToFile))
            {
                string firstLine = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string dataLine = sr.ReadLine();
                    string[] dataColum = dataLine.Split(',');
                    processCode(dataColum);
                }
            }
        }
        public static bool GetDefaultBool(string s, bool defaultV)
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
    }
}
