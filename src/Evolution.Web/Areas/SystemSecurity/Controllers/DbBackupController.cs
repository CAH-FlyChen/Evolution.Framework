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

namespace Evolution.Web.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    public class DbBackupController : ControllerBase
    {
        private DbBackupApp dbBackupApp = null;
        private readonly IHostingEnvironment wwwPath;

        public DbBackupController(IHostingEnvironment evn, DbBackupApp dbBackupApp)
        {
            wwwPath = evn;
            this.dbBackupApp = dbBackupApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            var data = dbBackupApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(DbBackupEntity dbBackupEntity)
        {
            dbBackupEntity.FilePath = wwwPath.ContentRootPath+"/Resource/DbBackup/" + dbBackupEntity.FileName + ".bak";
            dbBackupEntity.FileName = dbBackupEntity.FileName + ".bak";
            dbBackupApp.Save(dbBackupEntity);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            dbBackupApp.Delete(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        //[HandlerAuthorize]
        public void DownloadBackup(string keyValue)
        {
            var data = dbBackupApp.GetForm(keyValue);
            string filename = WebHelper.UrlDecode(data.FileName);
            string filepath = wwwPath+data.FilePath;
            //if (FileDownHelper.FileExists(filepath))
            //{
            //    FileDownHelper.DownLoadold(filepath, filename);
            //}
        }
    }
}
