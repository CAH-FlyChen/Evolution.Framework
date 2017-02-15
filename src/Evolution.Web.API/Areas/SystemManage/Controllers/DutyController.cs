﻿/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/

using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Evolution.Web.API.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class DutyController : EvolutionControllerBase
    {
        private DutyService dutyApp = null;

        public DutyController(DutyService dutyApp)
        {
            this.dutyApp = dutyApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetGridJson(string keyword)
        {
            var data = await dutyApp.GetList(keyword,this.TenantId);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await dutyApp.GetForm(keyValue,this.TenantId);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            await dutyApp.Save(roleEntity, keyValue,this.UserId);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await dutyApp.Delete(keyValue,this.TenantId);
            return Success("删除成功。");
        }
    }
}
