/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Web.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    public class LogController : ControllerBase
    {
        private LogApp logApp = null;

        public LogController(LogApp logApp)
        {
            this.logApp = logApp;
        }

        [HttpGet]
        public ActionResult RemoveLog()
        {
            return View();
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = await logApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRemoveLog(string keepTime)
        {
            await logApp.RemoveLog(keepTime);
            return Success("清空成功。");
        }
    }
}
