/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemSecurity;
using Evolution.Domain.Entity.SystemSecurity;
using System.Threading.Tasks;
using Evolution.Web.Attributes;

namespace Evolution.Web.API.Areas.SystemSecurity.Controllers
{
    [Area("SystemSecurity")]
    public class FilterIPController : EvolutionControllerBase
    {
        private FilterIPService filterIPApp = null;

        public FilterIPController(FilterIPService filterIPApp)
        {
            this.filterIPApp = filterIPApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetGridJson(string keyword)
        {
            var data = await filterIPApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await filterIPApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(FilterIPEntity filterIPEntity, string keyValue)
        {
            await filterIPApp.Save(filterIPEntity, keyValue,this.UserId);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await filterIPApp.Delete(keyValue);
            return Success("删除成功。");
        }
    }
}
