/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemSecurity;
using Evolution.Domain.Entity.SystemSecurity;
using System.Threading.Tasks;
using Evolution.Web.Attributes;

namespace Evolution.Web.API.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    public class DbBackupController : EvolutionControllerBase
    {
        private DbBackupService dbBackupApp = null;
        private readonly IHostingEnvironment wwwPath;

        public DbBackupController(IHostingEnvironment evn, DbBackupService dbBackupApp)
        {
            wwwPath = evn;
            this.dbBackupApp = dbBackupApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetGridJson(string queryJson)
        {
            var data = await dbBackupApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(DbBackupEntity dbBackupEntity)
        {
            dbBackupEntity.FilePath = wwwPath.ContentRootPath+"/Resource/DbBackup/" + dbBackupEntity.FileName + ".bak";
            dbBackupEntity.FileName = dbBackupEntity.FileName + ".bak";
            await dbBackupApp.Save(dbBackupEntity);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await dbBackupApp.Delete(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        //[HandlerAuthorize]
        public async void DownloadBackup(string keyValue)
        {
            var data = await dbBackupApp.GetForm(keyValue);
            string filename = WebHelper.UrlDecode(data.FileName);
            string filepath = wwwPath+data.FilePath;
            //if (FileDownHelper.FileExists(filepath))
            //{
            //    FileDownHelper.DownLoadold(filepath, filename);
            //}
        }
    }
}
