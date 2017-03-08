/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/

using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Domain.Entity.SystemManage;
using System.Threading.Tasks;
using Evolution.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Evolution.Application.SystemManage;

namespace Evolution.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    [Authorize]
    public class TenantController : EvolutionControllerBase
    {
        private ITenantService tenantService = null;
        public TenantController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetGridJson(string keyword)
        {
            var data = await tenantService.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await tenantService.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(TenantEntity tenantEntity, string keyValue)
        {
            await tenantService.Save(tenantEntity, keyValue, HttpContext);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await tenantService.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
